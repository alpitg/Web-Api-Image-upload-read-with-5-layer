﻿using Listing1.DATA;
using Listing1.DATA.Infrastructure;
using Listing1.DATA.Repositories;
using Listing1.ENTITIES;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Listing1.DATA.Repositories
{
    public sealed class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private StudentContext dataContext;

        public IQueryable<T> GetAll()
        {
            return studentContext.Set<T>().Where(x => x.IsDeleted == false);
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <value>
        /// All.
        /// </value>
        public IQueryable<T> All => GetAll();



        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = studentContext.Set<T>().Where(x => x.IsDeleted == false);
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }



        public T GetSingle(long id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }



        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return studentContext.Set<T>().Where(predicate).Where(x => x.IsDeleted == false);
        }



        public IQueryable<T> FindByAll(Expression<Func<T, bool>> predicate)
        {
            return studentContext.Set<T>().Where(predicate);
        }



        public void Add(T entity)
        {
            entity.IsDeleted = false;
            studentContext.Entry(entity);
            studentContext.Set<T>().Add(entity);
        }



        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
                studentContext.Entry(entity);
                studentContext.Set<T>().Add(entity);
            }
        }



        public void Edit(T oldEntity, T newEntity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry<T>(newEntity);
            //dbEntityEntry.State = EntityState.Modified;
            studentContext.Entry(oldEntity).CurrentValues.SetValues(newEntity);
        }


        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = studentContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }


        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            DbEntityEntry dbEntityEntry = studentContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }





        #region Properties

        private IDbFactory DbFactory { get; }

        private StudentContext studentContext => dataContext ?? (dataContext = DbFactory.Init());

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion
    }
}
