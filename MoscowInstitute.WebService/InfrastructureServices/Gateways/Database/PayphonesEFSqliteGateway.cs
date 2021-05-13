using MoscowPayphones.DomainObjects;
using MoscowPayphones.ApplicationServices.Ports.Gateways.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using MoscowPayphones.WebService.InfrastructureServices.Gateways;
using System.Text;
using Newtonsoft.Json;

namespace MoscowPayphones.InfrastructureServices.Gateways.Database
{
    public class PayphonesEFSqliteGateway : IPayphonesDatabaseGateway
    {
        private readonly PayphonesContext _payphonesContext;

        public PayphonesEFSqliteGateway(PayphonesContext payphonesContext)
            => _payphonesContext = payphonesContext;

        public async Task<Payphones> GetPayphones(long id)
           => await _payphonesContext.Payphones.Where(r => r.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Payphones>> GetAllPayphones()
            => await _payphonesContext.Payphones.ToListAsync();

        public async Task<IEnumerable<Payphones>> QueryPayphones(Expression<Func<Payphones, bool>> filter)
            => await _payphonesContext.Payphones.Where(filter).ToListAsync();

        public async Task AddPayphones(Payphones payphones)
        {
            _payphonesContext.Payphones.Add(payphones);
            await _payphonesContext.SaveChangesAsync();
        }

        public async Task UpdatePayphones(Payphones payphones)
        {
            _payphonesContext.Entry(payphones).State = EntityState.Modified;
            await _payphonesContext.SaveChangesAsync();
        }

        public async Task RemovePayphones(Payphones payphones)
        {
            _payphonesContext.Payphones.Remove(payphones);
            await _payphonesContext.SaveChangesAsync();
        }

        public async Task ParseAndPush()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/841/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PayphonesContext>();
            optionsBuilder.UseSqlite("Data Source=E:/PZ/MoscowInstitute.WebService/MoscowPayphones.db"); ;
            var context = new PayphonesContext(options: optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("DELETE FROM Payphones");
            using (context)
            {
                foreach (var item in resultServer)
                {
                    DomainObjects.Payphones payphones = new DomainObjects.Payphones();
                    payphones.Name = item.Cells.Name;
                    payphones.DescriptionLocation = item.Cells.DescriptionLocation;
                    payphones.PayWay = item.Cells.PayWay;
                    payphones.IntercityConnectionPayment = item.Cells.IntercityConnectionPayment;
                    payphones.ValidUniversalServicesCard = item.Cells.ValidUniversalServicesCard;
                    context.Entry(payphones).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            await Task.CompletedTask;
        }
    }
}
