using System.Threading;
using System.Threading.Tasks;

namespace TT.Delieveries.Persistence
{
    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken);
    }
}
