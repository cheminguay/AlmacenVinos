using AlmacenVinos.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlmacenVinos.Infraestructure.Interfaces
{
    public interface IBodegaRepository : IDisposable
    {
        /// <summary>
        ///  Recupera todos los registros de la clase T del inventario
        /// </summary>
        /// <returns>Lista de registros</returns>
        IQueryable<T> GetAll<T>() where T : class;

        /// <summary>
        /// Encuentra un objeto porr la expresión especificada
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Añade un registro al inventario
        /// </summary>
        /// <param name="TObject"></param>
        /// <returns>T</returns>
        T Create<T>(T TObject) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TObject"></param>
        /// <returns>int</returns>
        int Delete<T>(T TObject) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TObject"></param>
        /// <returns></returns>
        T Update<T>(int id, T TObject) where T : class;
    }
}
