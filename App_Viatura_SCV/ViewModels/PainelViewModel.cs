using System.Windows.Input;
using App_Viatura_SCV.Services;
using Microsoft.Maui.Controls;

namespace App_Viatura_SCV.ViewModels
{
    public class PainelViewModel
    {
        private MqttService _mqttService;

        // Comandos que a tela visual vai acionar
        public ICommand SirenePrincipalCommand { get; }
        public ICommand SireneTransversalCommand { get; }

        public PainelViewModel()
        {
            _mqttService = new MqttService();

            // Quando o botão for clicado, ele aciona o serviço na nuvem
            SirenePrincipalCommand = new Command(async () => await _mqttService.EnviarTelemetriaAsync("PRINCIPAL", true));
            SireneTransversalCommand = new Command(async () => await _mqttService.EnviarTelemetriaAsync("TRANSVERSAL", true));
        }
    }
}