using System.Threading;
using System.Threading.Tasks;
using TT.Delieveries.Application;

namespace TT.Delieveries.Persistence 
{ 
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> Get(string id, CancellationToken cancellationToken);
    } 
}