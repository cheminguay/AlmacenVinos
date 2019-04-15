using System;
using System.Diagnostics;
using System.Data.Entity.Validation;
using AlmacenVinos.Domain.Enums;

namespace AlmacenVinos.Services
{
    public class LogService : ILogService
    {
        //NOTA: Normalmente se suele usar una herramienta de Logs o un servicio que los guarda en archivo o BD,
        // en este caso por motivos de tiempo lo simulamos con Debug ...

        public void LogException(Exception ex)
        {
            Debug.WriteLine(StringEnum.GetStringValue(MensajeError.ErrorException), ex.Source, ex.Message);
            while (ex.InnerException != null)
            {
                Exception exie = ex.InnerException;
                Debug.WriteLine(StringEnum.GetStringValue(MensajeError.ErrorException), exie.Source, exie.Message);
            }
        }
        public void LogDbEntityValidationException(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult evErr in ex.EntityValidationErrors)
            {
                foreach (DbValidationError error in evErr.ValidationErrors)
                {
                    Console.WriteLine(StringEnum.GetStringValue(MensajeError.ErrorEntityException), error.PropertyName, error.ErrorMessage);
                }
            }
        }
        public void LogInventario(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

    }
}
