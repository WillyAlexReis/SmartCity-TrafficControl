using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace Servidor_SCV_CSharp
{
    public class GerenciadorTrafego
    {
        public bool ServidorAtivo { get; private set; }
        private IMqttClient _mqttClient;

        public GerenciadorTrafego()
        {
            ServidorAtivo = false;
        }

        public async Task IniciarServidorAsync()
        {
            ServidorAtivo = true;
            Console.WriteLine("===================================================");
            Console.WriteLine("[SCV - Servidor Central de Viaturas] Iniciado.");

            await ConectarMQTTAsync();

            Console.WriteLine("Aguardando conexões de Viaturas e Semáforos...");
            Console.WriteLine("===================================================\n");
        }

        private async Task ConectarMQTTAsync()
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.emqx.io", 1883) // <-- Alterado para o EMQX com a porta explícita
                .Build();

            // VINCULA O OUVINTE DE REDE: Avisa ao cliente MQTT para executar nosso método sempre que uma mensagem chegar
            _mqttClient.ApplicationMessageReceivedAsync += InterstellarMensagemRecebidaAsync;

            Console.WriteLine("[REDE] Conectando ao Broker publico EMQX...");
            await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
            Console.WriteLine("[REDE] Conectado ao Broker com sucesso!");

            // INSCRIÇÃO NO TÓPICO DA VIATURA: Diz ao broker que queremos ouvir a telemetria do Python
            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic("SmartCity/Viaturas/Telemetria"))
                .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            Console.WriteLine("[REDE] Inscrito no topico de Telemetria de Viaturas.");
        }

        // ESSE MÉTODO É DISPARADO AUTOMATICAMENTE QUANDO O PYTHON ENVIA DADOS
        private async Task InterstellarMensagemRecebidaAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            string topico = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            if (topico == "SmartCity/Viaturas/Telemetria")
            {
                try
                {
                    // Converte o texto bruto JSON vindo do Python em um Objeto C# instanciado
                    var viatura = JsonSerializer.Deserialize<ViaturaTelemetria>(payload);

                    if (viatura != null)
                    {
                        // Regra de Negócio: Só interfere se o veículo declarar emergência real (Sirene ativada)
                        if (viatura.sirene_ligada && !string.IsNullOrEmpty(viatura.rota_alvo))
                        {
                            // Encaminha os dados para o fluxo padrão de processamento e disparo
                            ProcessarAlertaViatura(viatura.id_viatura, "CRUZAMENTO_01", viatura.rota_alvo);
                        }
                        else
                        {
                            Console.WriteLine($"\n[SCV - INFO] Viatura {viatura.id_viatura} em patrulha normal (Sirene Desligada). Monitorando apenas posicao.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERRO SCV] Falha na interpretacao do pacote de telemetria: {ex.Message}");
                }
            }
        }

        public void PararServidor()
        {
            ServidorAtivo = false;
            Console.WriteLine("\n[SCV] Servidor sendo desligado com segurança...");
        }

        public void ProcessarAlertaViatura(string idViatura, string cruzamentoAlvo, string viaOrigem)
        {
            Console.WriteLine($"\n[SCV - CENTRAL] Alerta Ativado por Rede!");
            Console.WriteLine($"Viatura {idViatura} aproximando-se do {cruzamentoAlvo} pela via {viaOrigem.ToUpper()}.");
            Console.WriteLine(">> Calculando rotas e chaveando comandos de hardware...");

            // Dispara a mensagem para o semáforo em C++
            _ = DispararComandoPreempcaoAsync(cruzamentoAlvo, viaOrigem);
        }

        private async Task DispararComandoPreempcaoAsync(string cruzamentoDestino, string comandoVia)
        {
            var mensagem = new MqttApplicationMessageBuilder()
                .WithTopic("SmartCity/Cruzamento01/Emergencia")
                .WithPayload(comandoVia.ToUpper())
                .Build();

            if (_mqttClient.IsConnected)
            {
                await _mqttClient.PublishAsync(mensagem, CancellationToken.None);
                Console.WriteLine($"[SCV - SUCESSO] Comando enviado para o hardware. Rota Liberada: {comandoVia.ToUpper()}\n");
            }
            else
            {
                Console.WriteLine("[ERRO CENTRAL] Conexao MQTT perdida. Nao foi possivel comandar o semaforo.");
            }
        }
    }
}