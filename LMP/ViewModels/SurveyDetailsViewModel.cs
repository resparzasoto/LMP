using LMP.Entities;
using LMP.ServiceInterfaces;
using LMP.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace LMP.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly ILocalDBService localDBService;
        private IEnumerable<Team> localDBTeams;

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

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                RaisePropertyChanged();
            }
        }

        private DateTime birthdate = Literals.DefaultDate;

        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            {
                if (birthdate == value)
                {
                    return;
                }
                birthdate = value;
                RaisePropertyChanged();
            }
        }

        private string favoriteTeam;

        public string FavoriteTeam
        {
            get { return favoriteTeam; }
            set
            {
                if (favoriteTeam == value)
                {
                    return;
                }
                favoriteTeam = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, ILocalDBService localDBService)
        {
            Title = "Nueva Encuesta";

            this.navigationService = navigationService;
            this.localDBService = localDBService;

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Birthdate)
                .ObservesProperty(() => FavoriteTeam);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            localDBTeams = await localDBService.GetAllTeamsAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Id"))
            {
                FavoriteTeam = localDBTeams.First(t => t.Id == (int)parameters["Id"]).Name;
            }
        }

        private async void SelectTeamCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(TeamSelectionView), null, true, true);
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(FavoriteTeam) || Birthdate.Date == Literals.DefaultDate);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Birthdate = Birthdate,
                TeamId = localDBTeams.First(t => t.Name == FavoriteTeam).Id
            };

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    newSurvey.Lat = location.Latitude;
                    newSurvey.Lon = location.Longitude;
                }
            }
            catch (Exception)
            {
                newSurvey.Lat = 0d;
                newSurvey.Lon = 0d;
            }

            await localDBService.InsertSurveyAsync(newSurvey);

            await navigationService.GoBackAsync();
        }
    }
}
