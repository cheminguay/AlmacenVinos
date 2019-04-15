using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Infraestructure.Data;
using System.Diagnostics;

namespace AlmacenVinos.Services
{
    public class NotificacionService : INotificacionService
    {
        //NOTA: La forma de enviar notificaciones usualmente es por email, o servicios similares,
        // en este caso por motivos de tiempo lo simulamos con Debug ...
        public void NotificacionCaducidad(Bodega bodega)
        {
            Debug.WriteLine(StringEnum.GetStringValue(MensajeNotificaciones.BotellaCaducada), bodega.Botella.Vino.Nombre, bodega.Botella.Caducidad);
        }

        public void NotificacionIngresado(int unidades, Bodega bodega)
        {
            Debug.WriteLine(StringEnum.GetStringValue(MensajeNotificaciones.Ingreso), unidades, bodega.Botella.Vino.Nombre);
        }

        public void NotificacionExtraido(int unidades, Bodega bodega)
        {
            Debug.WriteLine(StringEnum.GetStringValue(MensajeNotificaciones.Extraccion), unidades, bodega.Botella.Vino.Nombre);
        }
    }
}
