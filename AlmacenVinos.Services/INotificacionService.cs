using AlmacenVinos.Infraestructure.Data;

namespace AlmacenVinos.Services
{
    public interface INotificacionService
    {
        /// <summary>
        /// Notifica producto caducado
        /// </summary>
        /// <param name="bodega"></param>
        void NotificacionCaducidad(Bodega bodega);
        /// <summary>
        /// Notifica ingreso en bodega
        /// </summary>
        /// <param name="unidades"></param>
        /// <param name="bodega"></param>
        void NotificacionIngresado(int unidades, Bodega bodega);
        /// <summary>
        /// Notifica extracción de bodega
        /// </summary>
        /// <param name="unidades"></param>
        /// <param name="bodega"></param>
        void NotificacionExtraido(int unidades, Bodega bodega);
    }
}
