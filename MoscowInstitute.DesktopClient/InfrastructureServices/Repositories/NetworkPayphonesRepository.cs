using MoscowPayphones.ApplicationServices.Ports.Cache;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoscowPayphones.InfrastructureServices.Repositories
{
    public class NetworkPayphonesRepository : NetworkRepositoryBase, IReadOnlyPayphonesRepository
    {
        private readonly IDomainObjectsCache<Payphones> _payphonesCache;

        public NetworkPayphonesRepository(string host, ushort port, bool useTls, IDomainObjectsCache<Payphones> payphonesCache)
            : base(host, port, useTls)
            => _payphonesCache = payphonesCache;

        public async Task<Payphones> GetPayphones(long id)
            => CacheAndReturn(await ExecuteHttpRequest<Payphones>($"Payphones/{id}"));

        public async Task<IEnumerable<Payphones>> GetAllPayphones()
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Payphones>>($"Payphones"), allObjects: true);

        public async Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria)
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Payphones>>($"Payphones"), allObjects: true)
               .Where(criteria.Filter.Compile());

        private IEnumerable<Payphones> CacheAndReturn(IEnumerable<Payphones> payphones, bool allObjects = false)
        {
            if (allObjects)
            {
                _payphonesCache.ClearCache();
            }
            _payphonesCache.UpdateObjects(payphones, DateTime.Now.AddDays(1), allObjects);
            return payphones;
        }

        private Payphones CacheAndReturn(Payphones payphones)
        {
            _payphonesCache.UpdateObject(payphones, DateTime.Now.AddDays(1));
            return payphones;
        }
    }
}
