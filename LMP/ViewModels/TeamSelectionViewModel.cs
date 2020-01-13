using LMP.ServiceInterfaces;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace LMP.ViewModels
{
    public class TeamSelectionViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly ILocalDBService localDBService;

        private ObservableCollection<TeamViewModel> teams;

        public ObservableCollection<TeamViewModel> Teams
        {
            get { return teams; }
            set
            {
                if (teams == value)
                {
                    return;
                }
                teams = value;
                RaisePropertyChanged();
            }
        }

        private TeamViewModel selectedTeam;

        public TeamViewModel SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam == value)
                {
                    return;
                }
                selectedTeam = value;
                RaisePropertyChanged();
            }
        }

        public TeamSelectionViewModel(INavigationService navigationService, ILocalDBService localDBService)
        {
            this.navigationService = navigationService;
            this.localDBService = localDBService;

            PropertyChanged += TeamSelectionViewModel_PropertyChanged;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var allTeams = await localDBService.GetAllTeamsAsync();

            if (allTeams != null)
            {
                Teams = new ObservableCollection<TeamViewModel>(allTeams.Select(TeamViewModel.GetViewModelFromEntity));
            }
        }

        private async void TeamSelectionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedTeam))
            {
                if (SelectedTeam is null)
                {
                    return;
                }

                var param = new NavigationParameters()
                {
                    { "Id", SelectedTeam.Id }
                };

                await navigationService.GoBackAsync(param, true, true);
            }
        }
    }
}
