using Common;
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

        public RepositoryBase(ILogger<RepositoryBase<T>> logger)
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

        public async Task Save(T element, string key)
        {
            if (element is null)
            {
                _logger.LogWarning("Nothing to save.");
                return;
            }

            // Validates the existance of the record.
            T existing = await Get(key);
            if (existing is not null)
            {
                var errMessage = $"Key {key} already exists.";

                _logger.LogError(errMessage);
                throw new ApiException(ApiErrorCodes.Conflict, errMessage);
            }

            _storage.Add(key, element);
        }

        public async Task Update(T element, string key)
        {
            if (element is null)
            {
                _logger.LogWarning("Nothing to update.");
                return;
            }

            // Validates the existance of the record.
            T existing = await Get(key);
            if (existing is null)
            {
                var errMessage = $"Key {key} could not be found.";

                _logger.LogError(errMessage);
                throw new ApiException(ApiErrorCodes.BadRequest, errMessage);
            }

            _storage.Remove(key);
            _storage.Add(key, element);
        }

        // Adds or update
        // Update capability can be avail in future.
        public async Task SaveOrUpdate(T element, string key)
        {
            if (element is null)
            {
                _logger.LogWarning("Nothing to save or update.");
                return;
            }

            T existing = await Get(key);
            if (existing is not null)
            {
                _storage.Remove(key);

                _logger.LogInformation($"Record found! Updating the record with the key {key}.");
            }

            _storage.Add(key, element);
        }
    }
}
