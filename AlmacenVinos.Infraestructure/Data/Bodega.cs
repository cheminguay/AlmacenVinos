//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlmacenVinos.Infraestructure.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bodega
    {
        public int Id { get; set; }
        public bool NotificadoCaducidad { get; set; }
        public int IdBotella { get; set; }
        public int Unidades { get; set; }
    
        public virtual Botella Botella { get; set; }
    }
}
