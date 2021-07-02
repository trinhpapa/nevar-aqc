#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> RepositoryBase.cs </Name>
//         <Created> 22/1/2019 - 20:02:41 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data.EF.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>, IDisposable where TEntity : class where TContext : DbContext
    {
        private readonly TContext _context;

        public RepositoryBase(TContext context)
        {
            _context = context;
        }

        public TEntity Create(TEntity entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return expression == null ? items : items.Where(expression);
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Find(null, includeProperties).FirstOrDefault(expression);
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}