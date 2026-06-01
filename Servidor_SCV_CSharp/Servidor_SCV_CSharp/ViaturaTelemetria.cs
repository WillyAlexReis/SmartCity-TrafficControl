using System;

namespace Servidor_SCV_CSharp
{
    // Classe responsável pelo mapeamento dos dados da viatura recebidos via rede
    public class ViaturaTelemetria
    {
        public string id_viatura { get; set; }
        public string tipo { get; set; }
        public bool sirene_ligada { get; set; }
        public string rota_alvo { get; set; }
    }
}