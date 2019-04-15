using AlmacenVinos.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVinos.Domain.Models
{
    
    public class BodegaDto
    {
        public int Id { get; set; }
        [Required]
        public bool NotificadoCaducidad { get; set; }
        [Range(1, 1000000, ErrorMessage = "El número de unidades tiene que ser mayor que cero.")]
        public int Unidades { get; set; }
        [Required]
        public int IdBotella { get; set; }
        public BotellaDto Botella { get; set; }
    }
}
