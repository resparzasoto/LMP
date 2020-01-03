using LMP.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace LMP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value)
                {
                    return;
                }
                username = value;
                RaisePropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute)
                .ObservesProperty(() => Username)
                .ObservesProperty(() => Password);
        }

        private bool LoginCommandCanExecute()
        {
            return !(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password));
        }

        private async void LoginCommandExecute()
        {
            await navigationService.NavigateAsync($"app:///{nameof(MainView)}/{nameof(RootNavigationPage)}/{nameof(SurveysView)}");
        }
    }
}
