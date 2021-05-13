using Microsoft.Extensions.DependencyInjection;
using MoscowPayphones.ApplicationServices.GetPayphonesListUseCase;
using MoscowPayphones.ApplicationServices.Ports.Cache;
using MoscowPayphones.ApplicationServices.Repositories;
using MoscowPayphones.DesktopClient.InfrastructureServices.ViewModels;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using MoscowPayphones.InfrastructureServices.Cache;
using MoscowPayphones.InfrastructureServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoscowPayphones.DesktopClient 
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDomainObjectsCache<Payphones>, DomainObjectsMemoryCache<Payphones>>();
            services.AddSingleton<NetworkPayphonesRepository>(
                x => new NetworkPayphonesRepository("localhost", 80, useTls: false, x.GetRequiredService<IDomainObjectsCache<Payphones>>())
            );
            services.AddSingleton<CachedReadOnlyPayphonesRepository>(
                x => new CachedReadOnlyPayphonesRepository(
                    x.GetRequiredService<NetworkPayphonesRepository>(),
                    x.GetRequiredService<IDomainObjectsCache<Payphones>>()
                )
            );
            services.AddSingleton<IReadOnlyPayphonesRepository>(x => x.GetRequiredService<CachedReadOnlyPayphonesRepository>());
            services.AddSingleton<IGetPayphonesListUseCase, GetPayphonesListUseCase>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
