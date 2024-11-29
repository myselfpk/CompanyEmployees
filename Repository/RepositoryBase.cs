using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsTracking() : RepositoryContext.Set<T>().Where(expression);

        public void Create(T entry) => RepositoryContext.Set<T>().Add(entry);

        public void Update(T entry) => RepositoryContext.Set<T>().Update(entry);

        public void Delete(T entry) => RepositoryContext.Set<T>().Remove(entry);
    }
}
