using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowPayphones.ApplicationServices.Repositories
{
    public class InMemoryPayphonesRepository : IReadOnlyPayphonesRepository,
                                                    IPayphonesRepository
    {
        private readonly List<Payphones> _payphones = new List<Payphones>();

        public InMemoryPayphonesRepository(IEnumerable<Payphones> payphones = null)
        {
            if (payphones != null)
            {
                _payphones.AddRange(payphones);
            }
        }

        public Task AddPayphones(Payphones payphones)
        {
            _payphones.Add(payphones);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Payphones>> GetAllPayphones()
        {
            return Task.FromResult(_payphones.AsEnumerable());
        }

        public Task<Payphones> GetPayphones(long id)
        {
            return Task.FromResult(_payphones.Where(o => o.Id == id).FirstOrDefault());
        }

        public Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria)
        {
            return Task.FromResult(_payphones.Where(criteria.Filter.Compile()).AsEnumerable());
        }

        public Task RemovePayphones(Payphones payphones)
        {
            _payphones.Remove(payphones);
            return Task.CompletedTask;
        }

        public Task UpdatePayphones(Payphones payphones)
        {
            var foundPayphones = GetPayphones(payphones.Id).Result;
            if (foundPayphones == null)
            {
                AddPayphones(payphones);
            }
            else
            {
                if (foundPayphones != payphones)
                {
                    _payphones.Remove(foundPayphones);
                    _payphones.Add(payphones);
                }
            }
            return Task.CompletedTask;
        }
        public Task ParseAndPush()
        {
            throw new NotImplementedException();
        }
    }
}
