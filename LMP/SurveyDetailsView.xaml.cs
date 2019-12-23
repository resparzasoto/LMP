using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LMP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyDetailsView : ContentPage
    {
        private readonly string[] teams =
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
        };

        public SurveyDetailsView()
        {
            InitializeComponent();
        }

        private async void FavoriteTeamButton_Clicked(object sender, EventArgs e)
        {
            var favoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, teams);

            if (!string.IsNullOrWhiteSpace(favoriteTeam))
            {
                FavoriteTeamLabel.Text = favoriteTeam;
            }
        }

        private async void OKButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(FavoriteTeamLabel.Text) || BirthdatePicker.Date == Literals.DefaultDate)
            {
                await DisplayAlert(Title, Literals.SurveyValidation, Literals.OK);

                return;
            }

            var newSurvey = new Survey()
            {
                Name = NameEntry.Text,
                Birthdate = BirthdatePicker.Date,
                FavoriteTeam = FavoriteTeamLabel.Text
            };

            MessagingCenter.Send(this, Messages.NewSurveyComplete, newSurvey);

            await Navigation.PopAsync(true);
        }
    }
}