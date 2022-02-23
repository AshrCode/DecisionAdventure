using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        protected static IDictionary<string, T> _storage = new ConcurrentDictionary<string, T>();
        protected readonly ILogger<RepositoryBase<T>> _logger;

        protected RepositoryBase(ILogger<RepositoryBase<T>> logger)
        {
            _logger = logger;
        }

        // This method can be useful in future.
        public IDictionary<string, T> GetAll() => _storage;

        public Task<T> Get(string key)
        {
            _storage.TryGetValue(key, out T val);

            return Task.FromResult(val);
        }

        // Adds or update
        // Update capability can be avail in future.
        public async Task Save(T element, string key)
        {
            if (element == null)
            {
                return;
            }

            T existing = await Get(key);
            if (existing != null)
            {
                _storage.Remove(key);

                _logger.LogInformation($"Record found! Updating the record with the key {key}.");
            }

            _storage.Add(key, element);
        }
    }
}
