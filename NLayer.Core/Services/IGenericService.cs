using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsycn();
        Task<T> GetByIdAsycn(int id);
        Task<T> GetByIdsAsycn(int id, int id2);
        IQueryable<T> Where(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsycn(Expression<Func<T, bool>> filter);
        Task<T> AddAsycn(T entity);
        Task<IEnumerable<T>> AddRangeAsycn(IEnumerable<T> entities);
        Task UpdateAsycn(T entity);
        Task RemoveAsycn(T entity);
        Task RemoveRangeAsycn(IEnumerable<T> entites);
    }
}
