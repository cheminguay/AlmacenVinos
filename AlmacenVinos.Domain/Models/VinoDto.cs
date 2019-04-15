using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlmacenVinos.Domain.Models
{
    public class VinoDto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Denominacion { get; set; }
        [Range(0, 10)]
        public decimal? Capacidad { get; set; }
        [Required]
        public string Variedad { get; set; }
        [Required]
        public string Crianza { get; set; }
        [Required]
        public string Color { get; set; }
        public bool Baja { get; set; }
    }
}
