using System.Threading.Tasks;

namespace Persistence
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(string id);

        Task Save(T element, string id);
    }
}
