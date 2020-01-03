using LMP.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace LMP.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;

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

        private ObservableCollection<string> teams;

        public ObservableCollection<string> Teams
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

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            Title = "Nueva Encuesta";

            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            Teams = new ObservableCollection<string>(new[]
            {
                "Águilas de Mexicali",
                "Algodoneros de Guasave",
                "Cañeros de Los Mochis",
                "Charros de Jalisco",
                "Mayos de Navojoa",
                "Naranjeros de Hermosillo",
                "Sultanes de Monterrey",
                "Tomateros de Culiacán",
                "Venados de Mazatlán",
                "Yaquis de Ciudad Obregón"
            });

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Birthdate)
                .ObservesProperty(() => FavoriteTeam);
        }

        private async void SelectTeamCommandExecute()
        {
            var teamSelected = await pageDialogService.DisplayActionSheetAsync(Literals.FavoriteTeamTitle, null, null, Teams.ToArray());

            if (!string.IsNullOrWhiteSpace(teamSelected))
            {
                FavoriteTeam = teamSelected;
            }
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(FavoriteTeam) || Birthdate.Date == Literals.DefaultDate);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Name = Name,
                Birthdate = Birthdate,
                FavoriteTeam = FavoriteTeam
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

            await navigationService.GoBackAsync(new NavigationParameters { { Messages.NewSurvey, newSurvey } });
        }
    }
}
