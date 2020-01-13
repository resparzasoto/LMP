using LMP.Models;
using LMP.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace LMP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Module> modules;

        public ObservableCollection<Module> Modules
        {
            get { return modules; }
            set
            {
                if (modules == value)
                {
                    return;
                }
                modules = value;
                RaisePropertyChanged();
            }
        }

        private Module selectedModule;

        public Module SelectedModule
        {
            get { return selectedModule; }
            set
            {
                if (selectedModule == value)
                {
                    return;
                }
                selectedModule = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            Modules = new ObservableCollection<Module>
            {
                new Module
                {
                    Icon = Device.RuntimePlatform == Device.UWP ? "assets/survey.png" : "survey.png",
                    Title = "Encuestas",
                    LoadModuleCommand = new DelegateCommand(
                        async () => await navigationService.NavigateAsync($"{nameof(RootNavigationPage)}/{nameof(SurveysView)}"))
                },
                new Module
                {
                    Icon = Device.RuntimePlatform == Device.UWP ? "assets/sync.png" : "sync.png",
                    Title = "Sincronización",
                    LoadModuleCommand = new DelegateCommand(
                        async () => await navigationService.NavigateAsync($"{nameof(RootNavigationPage)}/{nameof(SyncView)}"))
                },
                new Module
                {
                    Icon = Device.RuntimePlatform == Device.UWP ? "assets/about.png" : "about.png",
                    Title = "Acerca de...",
                    LoadModuleCommand = new DelegateCommand(
                        async () => await navigationService.NavigateAsync($"{nameof(RootNavigationPage)}/{nameof(AboutView)}"))
                }
            };

            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedModule))
            {
                selectedModule?.LoadModuleCommand.Execute(null);
            }
        }
    }
}
