using System;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVinos.Domain.Models
{
    public class BotellaDto
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime? Caducidad { get; set; }
        [Required]
        public int? Añada { get; set; }
        public int IdVino { get; set; }
        public bool Disponible { get; set; }
        public VinoDto Vino { get; set; }
    }
}
