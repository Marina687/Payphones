using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MoscowPayphones.DomainObjects.Ports
{
    public interface IReadOnlyPayphonesRepository
    {
        Task<Payphones> GetPayphones(long id);

        Task<IEnumerable<Payphones>> GetAllPayphones();

        Task<IEnumerable<Payphones>> QueryPayphones(ICriteria<Payphones> criteria);

    }

    public interface IPayphonesRepository
    {
        Task AddPayphones(Payphones payphones);

        Task RemovePayphones(Payphones trafficRestriction);
             
        Task UpdatePayphones(Payphones payphones);
        Task ParseAndPush();
    }
}
