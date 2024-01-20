using System;
using System.Linq.Expressions;
using EduHome.Core.Entities.BaseEntities;
using EduHome.Core.Repositories;
using EduHome.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Data.Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
    {
		readonly EduDbContext _context;
        public Repository(EduDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, params string[] Includes)
        {
            var query = _context.Set<T>().Where(expression);

            if (Includes != null)
            {
                foreach (string include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(); ;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

