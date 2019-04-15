using System;
using System.Data.Entity.Validation;

namespace AlmacenVinos.Services
{
    public interface ILogService
    {
        /// <summary>
        /// Escribe logs de Excepciones
        /// </summary>
        /// <param name="ex">Exception</param>
        void LogException(Exception ex);

        /// <summary>
        /// Escribe logs de Excepciones de Entity Framework
        /// </summary>
        /// <param name="ex"></param>
        void LogDbEntityValidationException(DbEntityValidationException ex);
        /// <summary>
        /// Escribe logs de Inventario
        /// </summary>
        /// <param name="mensaje"></param>
        void LogInventario(string mensaje);
    }
}
