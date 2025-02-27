﻿using BeoordelingProject.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class {

        internal BeoordelingsContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository() {

        }
        public GenericRepository(BeoordelingsContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All() {
            int aantal = dbSet.Count<TEntity>();
            return dbSet;
        }

        public virtual TEntity GetByID(object id) {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity) {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id) {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete) {
            if (context.Entry(entityToDelete).State == EntityState.Detached) {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate) {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}