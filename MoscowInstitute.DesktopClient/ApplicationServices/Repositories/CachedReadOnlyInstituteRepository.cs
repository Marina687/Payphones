using MoscowPayphones.ApplicationServices.Ports.Cache;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using MoscowPayphones.DomainObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoscowPayphones.InfrastructureServices.Repositories
{
    public class CachedReadOnlyPayphonesRepository : ReadOnlyPayphonesRepositoryDecorator
    {
        private readonly IDomainObjectsCache<Payphones> _payphonesCache;

        public CachedReadOnlyPayphonesRepository(IReadOnlyPayphonesRepository payphonesRepository,
                                             IDomainObjectsCache<Payphones> payphonesCache)
            : base(payphonesRepository)
            => _payphonesCache = payphonesCache;

        public async override Task<Payphones> GetPayphones(long id)
            => _payphonesCache.GetObject(id) ?? await base.GetPayphones(id);

        public async override Task<IEnumerable<Payphones>> GetAllPayphones()
            => _payphonesCache.GetObjects() ?? await base.GetAllPayphones();

        public async override Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria)
            => _payphonesCache.GetObjects()?.Where(criteria.Filter.Compile()) ?? await base.QueryPayphones(criteria);
    }
}
