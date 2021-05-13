using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MoscowPayphones.ApplicationServices.GetPayphonesListUseCase
{
    public class StreetCriteria : ICriteria<Payphones>
    {
        public string Street { get; }

        public StreetCriteria(string street)
            => this.Street = street;

        public Expression<Func<Payphones, bool>> Filter
            => (tr => tr.Name == Street );
    }
}
