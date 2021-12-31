using System.Threading.Tasks;

namespace DotNetSearch.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
