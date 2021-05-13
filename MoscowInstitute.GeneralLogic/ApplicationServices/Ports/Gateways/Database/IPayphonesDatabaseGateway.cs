using MoscowPayphones.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoscowPayphones.ApplicationServices.Ports.Gateways.Database
{
    public interface IPayphonesDatabaseGateway
    {
        Task AddPayphones(Payphones payphones);

        Task RemovePayphones(Payphones payphones);

        Task UpdatePayphones(Payphones payphones);

        Task<Payphones> GetPayphones(long id);

        Task<IEnumerable<Payphones>> GetAllPayphones();

        Task<IEnumerable<Payphones>> QueryPayphones(Expression<Func<Payphones, bool>> filter);
        Task ParseAndPush();
    }
}
