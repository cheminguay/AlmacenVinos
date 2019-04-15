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
    
    public partial class Vino
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vino()
        {
            this.Botellas = new HashSet<Botella>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Denominacion { get; set; }
        public Nullable<decimal> Capacidad { get; set; }
        public string Variedad { get; set; }
        public string Crianza { get; set; }
        public string Color { get; set; }
        public bool Baja { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Botella> Botellas { get; set; }
    }
}
