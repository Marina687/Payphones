using System;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using MoscowPayphones.ApplicationServices.Ports.Gateways.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowPayphones.ApplicationServices.Repositories
{
    public class DbPayphonesRepository : IReadOnlyPayphonesRepository,
                                                  IPayphonesRepository
    {
        private readonly IPayphonesDatabaseGateway _databaseGateway;

        public DbPayphonesRepository(IPayphonesDatabaseGateway databaseGateway)
            => _databaseGateway = databaseGateway;

        public async Task<Payphones> GetPayphones(long id)
            => await _databaseGateway.GetPayphones(id);

        public async Task<IEnumerable<Payphones>> GetAllPayphones()
            => await _databaseGateway.GetAllPayphones();

        public async Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria)
            => await _databaseGateway.QueryPayphones(criteria.Filter);

        public async Task AddPayphones(Payphones payphones)
            => await _databaseGateway.AddPayphones(payphones);

        public async Task RemovePayphones(Payphones payphones)
            => await _databaseGateway.RemovePayphones(payphones);

        public async Task UpdatePayphones(Payphones payphones)
            => await _databaseGateway.UpdatePayphones(payphones);

        public async Task ParseAndPush()
            => await _databaseGateway.ParseAndPush();
    }
}
