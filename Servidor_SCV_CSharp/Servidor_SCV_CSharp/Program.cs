using System;
using System.Threading.Tasks;

namespace Servidor_SCV_CSharp
{
    internal class Program
    {
        // O Main agora recebe a palavra "async Task"
        static async Task Main(string[] args)
        {
            GerenciadorTrafego scv = new GerenciadorTrafego();

            // Colocamos o "await" para ele esperar a conexão com a internet terminar antes de abrir o menu
            await scv.IniciarServidorAsync();

            while (scv.ServidorAtivo)
            {
                Console.WriteLine("\n=== [Console Admin] Gerador de Ocorrências ===");

                Console.WriteLine("Selecione a Viatura em deslocamento:");
                Console.WriteLine("A - Ambulância (SAMU)");
                Console.WriteLine("B - Bombeiros");
                Console.WriteLine("P - Polícia");
                Console.WriteLine("0 - Desligar Servidor");
                Console.Write("Viatura: ");

                string inputViatura = Console.ReadLine().ToUpper();

                if (inputViatura == "0")
                {
                    scv.PararServidor();
                    break;
                }

                string idViaturaSelecionada = "";

                switch (inputViatura)
                {
                    case "A": idViaturaSelecionada = "SAMU-192"; break;
                    case "B": idViaturaSelecionada = "BOM-193"; break;
                    case "P": idViaturaSelecionada = "POL-190"; break;
                    default:
                        Console.WriteLine(">> Erro: Viatura não reconhecida. Tente novamente.");
                        continue;
                }

                Console.WriteLine("\nSelecione a Rota de aproximação ao cruzamento:");
                Console.WriteLine("P  - Via Principal");
                Console.WriteLine("T  - Via Transversal");
                Console.WriteLine("CM - Contra-Mão (Risco Crítico)");
                Console.Write("Rota: ");

                string inputVia = Console.ReadLine().ToUpper();
                string viaSelecionada = "";

                switch (inputVia)
                {
                    case "P": viaSelecionada = "Principal"; break;
                    case "T": viaSelecionada = "Transversal"; break;
                    case "CM": viaSelecionada = "Contra_Mao"; break;
                    default:
                        Console.WriteLine(">> Erro: Rota não reconhecida. Operação cancelada.");
                        continue;
                }

                Console.WriteLine("\n---------------------------------------------------");
                scv.ProcessarAlertaViatura(idViaturaSelecionada, "CRUZAMENTO_01", viaSelecionada);
                Console.WriteLine("---------------------------------------------------");
            }

            Console.WriteLine("Sistema encerrado. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}