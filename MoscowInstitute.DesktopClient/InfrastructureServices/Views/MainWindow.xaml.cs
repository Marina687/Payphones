using MoscowPayphones.DesktopClient.InfrastructureServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json;
using MoscowPayphones.InfrastructureServices.Gateways.Database;
using MoscowPayphones.WebService.InfrastructureServices.Gateways;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Data.Sqlite;

namespace MoscowPayphones.DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Name = "btn1";
            btn.Click += btn1_Click;
            DataContext = viewModel;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string result = client.DownloadString("https://apidata.mos.ru/v1/datasets/841/rows?$top=1000&api_key=c941a998bbb9e1e374fc2d7a33f61ed0");
            List<ResultFromServer> resultServer = JsonConvert.DeserializeObject<List<ResultFromServer>>(result);
            var optionsBuilder = new DbContextOptionsBuilder<PayphonesContext>();
            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\"));
            string newnewpath = System.IO.Path.Combine(newPath, "MoscowPayphones.WebService", "MoscowPayphones.db");
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
        }
    }
}