using MoscowPayphones.ApplicationServices.GetPayphonesListUseCase;
using MoscowPayphones.ApplicationServices.Ports;
using MoscowPayphones.DomainObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MoscowPayphones.DesktopClient.InfrastructureServices.ViewModels 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGetPayphonesListUseCase _getPayphonesListUseCase;

        public MainViewModel(IGetPayphonesListUseCase Payphones)
            => _getPayphonesListUseCase = Payphones;

        private Task<bool> _loadingTask;
        private Payphones _currentPayphones;
        private ObservableCollection<Payphones> _Payphones;

        public event PropertyChangedEventHandler PropertyChanged;

        public Payphones CurrentPayphones
        {
            get => _currentPayphones;
            set
            {
                if (_currentPayphones != value)
                {
                    _currentPayphones = value;
                    OnPropertyChanged(nameof(CurrentPayphones));
                }
            }
        }

        private async Task<bool> LoadPayphones()
        {
            var outputPort = new OutputPort();
            bool result = await _getPayphonesListUseCase.Handle(GetPayphonesListUseCaseRequest.CreateAllPayphonesRequest(), outputPort);
            if (result)
            {
                Payphones = new ObservableCollection<Payphones>(outputPort.Payphones);
            }
            return result;
        }

        public ObservableCollection<Payphones> Payphones
        {
            get
            {
                if (_loadingTask == null)
                {
                    _loadingTask = LoadPayphones();
                }

                return _Payphones;
            }
            set
            {
                if (_Payphones != value)
                {
                    _Payphones = value;
                    OnPropertyChanged(nameof(Payphones));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private class OutputPort : IOutputPort<GetPayphonesListUseCaseResponse>
        {
            public IEnumerable<Payphones> Payphones { get; private set; }

            public void Handle(GetPayphonesListUseCaseResponse response)
            {
                if (response.Success)
                {
                    Payphones = new ObservableCollection<Payphones>(response.Payphones);
                }
            }
        }
    }
}
