namespace VideoRental.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T:class, IEntityBase, new()
    {
        private VideoRentalContext dbContext;

        #region Properties

        protected IDbFactory DbFactory { get; private set; }

        public VideoRentalContext DbContext => dbContext ?? (dbContext = DbFactory.Init());

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion
        
        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }

            return query;
        }

        public virtual IQueryable<T> All => GetAll();

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Added;
        }
        
        public virtual void Edit(T entity)
        {
            var dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
    }
}