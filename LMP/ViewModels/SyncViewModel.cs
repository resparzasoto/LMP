using LMP.ServiceInterfaces;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LMP.ViewModels
{
    public class SyncViewModel : ViewModelBase
    {
        private readonly ILocalDBService localDBService;
        private readonly IWebAPIService webAPIService;

        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                {
                    return;
                }
                title = value;
                RaisePropertyChanged();
            }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                if (status == value)
                {
                    return;
                }
                status = value;
                RaisePropertyChanged();
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                {
                    return;
                }
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SyncCommand { get; set; }

        public SyncViewModel(ILocalDBService localDBService, IWebAPIService webAPIService)
        {
            Title = "Sincronización";

            this.localDBService = localDBService;
            this.webAPIService = webAPIService;

            SyncCommand = new DelegateCommand(SyncCommandExecuteAsync);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            Status = Application.Current.Properties.ContainsKey("lastSync") ?
                $"Última actualización: {Convert.ToDateTime(Application.Current.Properties["lastSync"]).ToString("dd/MM/yyyy HH:mm:ss")}" :
                $"No se han sincronizado los datos...";
        }

        private async void SyncCommandExecuteAsync()
        {
            IsBusy = true;

            //Enviar encuestas
            var allSurveys = await localDBService.GetAllSurveysAsync();

            if (allSurveys != null && allSurveys.Any())
            {
                await webAPIService.SaveSurveysAsync(allSurveys);

                await localDBService.DeleteAllSurveysAsync();
            }

            //Obtener equipos
            var allTeams = await webAPIService.GetTeamsAsync();

            if (allTeams != null && allTeams.Any())
            {
                await localDBService.DeleteAllTeamsAsync();

                await localDBService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;

            await Application.Current.SavePropertiesAsync();

            Status = $"Se enviarón {allSurveys.Count()} encuestas y se obtuvierón {allTeams.Count()} equipos";

            IsBusy = false;
        }
    }
}
