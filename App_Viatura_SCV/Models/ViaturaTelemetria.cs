using System;

namespace App_Viatura_SCV.Models
{
    public class ViaturaTelemetria
    {
        public string? id_viatura { get; set; }
        public string? tipo { get; set; }
        public bool sirene_ligada { get; set; }
        public string? rota_alvo { get; set; }
    }
}