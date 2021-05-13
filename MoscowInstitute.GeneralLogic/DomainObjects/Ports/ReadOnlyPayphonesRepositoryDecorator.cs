using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoscowPayphones.DomainObjects.Repositories
{
    public abstract class ReadOnlyPayphonesRepositoryDecorator : IReadOnlyPayphonesRepository
    {
        private readonly IReadOnlyPayphonesRepository _payphonesRepository;

        public ReadOnlyPayphonesRepositoryDecorator(IReadOnlyPayphonesRepository payphonesRepository)
        {
            _payphonesRepository = payphonesRepository;
        }

        public virtual async Task<IEnumerable<Payphones>> GetAllPayphones()
        {
            return await _payphonesRepository?.GetAllPayphones();
        }

        public virtual async Task<Payphones> GetPayphones(long id)
        {
            return await _payphonesRepository?.GetPayphones(id);
        }

        public virtual async Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria)
        {
            return await _payphonesRepository?.QueryPayphones(criteria);
        }
    }
}
