using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Navigation;

namespace BlankApp1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _resultText;
        private string _serverUrl;
        private bool _isLoading;
        private string _myAccessToken;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            ConnectCommand = new DelegateCommand(Connect);
            //ServerUrl = "https://192.168.178.124:8888/chatHub";
            ServerUrl = "https://iot-rtbackend20200220110705.azurewebsites.net/";
        }

        private async void Connect()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(new Uri(ServerUrl), options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
                    }).Build();

            try
            {
                IsLoading = true;
                await connection.StartAsync();
                ResultText = "SUCCESS " + _myAccessToken + " " + connection.ConnectionId;
            }
            catch (Exception e)
            {
                ResultText = e.Message + e.ToString();
            }
            IsLoading = false;
        }

        public string ResultText
        {
            get => _resultText;
            set => SetProperty(ref _resultText, value);
        }

        public string ServerUrl
        {
            get => _serverUrl;
            set => SetProperty(ref _serverUrl, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand ConnectCommand { get; set; }
    }
}
