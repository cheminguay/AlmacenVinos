using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using AlmacenVinos.Infraestructure.Data;
using AlmacenVinos.Infraestructure.Interfaces;

namespace AlmacenVinos.Infraestructure.Repositories
{
    public sealed class BodegaRepository : IBodegaRepository
    {
        private static object sync = new Object();
        public BodegaEntities DBBodega;

        public BodegaRepository()
        {
            lock (sync) // Evitamos que otros hilos interfieran
            {
                DBBodega = new BodegaEntities();
            }
        }

        /// <summary>
        /// Libera el recurso
        /// </summary>
        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return DBBodega.Set<T>().AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DBBodega.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TObject"></param>
        /// <returns></returns>
        public T Create<T>(T TObject) where T : class
        {
            DBBodega.Set<T>().Add(TObject);
            DBBodega.SaveChanges();
            return TObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TObject"></param>
        /// <returns></returns>
        public int Delete<T>(T TObject) where T : class
        {
            DBBodega.Set<T>().Remove(TObject);
            return DBBodega.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TObject"></param>
        /// <returns></returns>
        public T Update<T>(int id, T TObject) where T : class
        {
            DbEntityEntry<T> entry = DBBodega.Entry(TObject);
            ObjectContext objectContext = ((IObjectContextAdapter)DBBodega).ObjectContext;
            ObjectSet<T> set = objectContext.CreateObjectSet<T>();

            List<string> keyNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();

            T existing = DBBodega.Set<T>().Find(id);

            if (keyNames.Count < 1)
            {
                throw new ArgumentException("A primarykey was expected");
            }
            if (TObject == null)
            {
                throw new ArgumentException("Cannot add a null entity");
            }
            if (entry.State == EntityState.Detached)
            {
                DbSet<T> setD = DBBodega.Set<T>();

                var attachedEntity = setD.Find(setD.Create().GetType().GetProperty(keyNames[0]).GetValue(TObject));
                if (attachedEntity != null)
                {
                    DbEntityEntry<T> attachedEntry = DBBodega.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(TObject);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }

            if (existing != null)
            {
                DBBodega.SaveChangesAsync();
            }

            return DBBodega.Set<T>().Find(id); ;
        }
    }

}
