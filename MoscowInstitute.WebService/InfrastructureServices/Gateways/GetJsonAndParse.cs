using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.InfrastructureServices.Gateways.Database;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MoscowPayphones.WebService.InfrastructureServices.Gateways
{
    public class GetJsonAndParse
    {
        public async Task ParseAndPush()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/841/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PayphonesContext>();
            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"));
            string newnewpath = System.IO.Path.Combine(newPath, "MoscowInstitute.WebService", "MoscowPayphones.db");
            optionsBuilder.UseSqlite($"Data Source={newnewpath}");
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
