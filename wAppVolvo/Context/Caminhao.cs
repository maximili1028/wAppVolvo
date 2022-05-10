using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wAppVolvo.Context
{
    public class Caminhao
    {
        public int CaminhaoId { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int AnoFabricacao { get; set; }
        [Required]
        public int AnoModelo { get; set; }
    }
}
