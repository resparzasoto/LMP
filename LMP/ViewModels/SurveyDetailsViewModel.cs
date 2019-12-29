using LMP.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LMP.ViewModels
{
    public class SurveyDetailsViewModel : NotificationObject
    {
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel()
        {
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

            SelectTeamCommand = new Command(SelectTeamCommandExecute);

            EndSurveyCommand = new Command(EndSurveyCommandExecute, EndSurveyCommandCanExecute);

            MessagingCenter.Subscribe<ContentPage, string>(this, Messages.TeamSelected, (sender, args) =>
            {
                FavoriteTeam = args;
            });

            PropertyChanged += SurveyDetailsViewModel_PropertyChanged;
        }

        private void SurveyDetailsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(Birthdate) || e.PropertyName == nameof(FavoriteTeam))
            {
                (EndSurveyCommand as Command)?.ChangeCanExecute();
            }
        }

        private void SelectTeamCommandExecute()
        {
            MessagingCenter.Send(this, Messages.SelectTeam, Teams.ToArray());
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

            MessagingCenter.Send(this, Messages.NewSurveyComplete, newSurvey);
        }
    }
}
