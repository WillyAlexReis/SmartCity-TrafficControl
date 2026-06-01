using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using App_Viatura_SCV.Models;
using Microsoft.Maui.Controls; // Biblioteca para usar pop-ups visuais

namespace App_Viatura_SCV.Services
{
    public class MqttService
    {
        private IMqttClient _mqttClient;

        public MqttService()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
        }

        public async Task EnviarTelemetriaAsync(string rota, bool sireneLigada)
        {
            try
            {
                // 1. Tenta estabelecer a conexão se a viatura estiver offline
                if (!_mqttClient.IsConnected)
                {
                    var options = new MqttClientOptionsBuilder()
                        .WithTcpServer("broker.emqx.io", 1883) // Mudança para o EMQX
                        .WithClientId("Viatura_SAMU_" + Guid.NewGuid().ToString())
                        .WithCleanSession()
                        .WithTimeout(TimeSpan.FromSeconds(15))
                        .Build();

                    await _mqttClient.ConnectAsync(options, CancellationToken.None);
                }

                // 2. Se chegou aqui, a internet está funcionando! Vamos montar o pacote.
                var dados = new ViaturaTelemetria
                {
                    id_viatura = "SAMU-192",
                    tipo = "AMBULANCIA",
                    sirene_ligada = sireneLigada,
                    rota_alvo = rota
                };

                string json = JsonSerializer.Serialize(dados);

                var mensagem = new MqttApplicationMessageBuilder()
                    .WithTopic("SmartCity/Viaturas/Telemetria")
                    .WithPayload(json)
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .Build();

                // 3. Dispara a mensagem para a nuvem
                await _mqttClient.PublishAsync(mensagem, CancellationToken.None);

                // 4. Dá um feedback visual na tela (Padrão moderno .NET 10)
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlertAsync("SUCESSO", $"Sinal emitido! Rota {rota} solicitada.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Se a internet cair, o app avisa o erro na tela (Padrão moderno .NET 10)
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlertAsync("FALHA DE REDE", $"Não foi possível conectar ao SCV.\n\nDetalhe técnico: {ex.Message}", "Entendi");
                }
            }
        }
    }
}