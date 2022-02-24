using System.Threading.Tasks;

namespace Persistence
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(string id);

        Task SaveOrUpdate(T element, string id);

        Task Save(T element, string key);

        Task Update(T element, string key);


    }
}
