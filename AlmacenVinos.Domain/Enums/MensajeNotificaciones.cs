using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmacenVinos.Domain.Enums
{
    public enum MensajeNotificaciones
    {
        [StringValue("Botella caducada: {0} caducó {1}.")]
        BotellaCaducada = 1,
        [StringValue("Ingreso de {0} botellas de {1} del inventario.")]
        Ingreso = 2,
        [StringValue("Extracción de {0} botellas de {1} del inventario.")]
        Extraccion = 3,
    }
}
