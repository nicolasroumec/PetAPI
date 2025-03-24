using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PetAPI.Models;
using PetAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetAPI.Repositories.Implementations
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class //Es una clase abstracta genérica que funciona con cualquier tipo de clase
    {
        public PetContext RepositoryContext { get; set; }
        public BaseRepository(PetContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        //AsNoTrackingWithIdentityResolution() mejora el rendimiento al no rastrear cambios
        //Set <T> permite trabajar con cualquier entidad genéricamente
        public IQueryable<T> FindAll() 
        {
            return RepositoryContext.Set<T>().AsNoTrackingWithIdentityResolution(); 
        }
        //Permite incluir relaciones (joins) de manera opcional
        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = RepositoryContext.Set<T>();
            if (includes != null)
            {
                queryable = includes(queryable);
            }
            return queryable.AsNoTrackingWithIdentityResolution();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTrackingWithIdentityResolution();
        }
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
            RepositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }
    }
}
