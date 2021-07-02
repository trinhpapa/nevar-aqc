#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> IRepositoryBase.cs </Name>
//         <Created> 21/1/2019 - 18:10:18 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity FindSingle(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void SaveChange();

        Task SaveChangeAsync();
    }
}