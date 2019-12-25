using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LMP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveysView : ContentPage
    {
        public SurveysView()
        {
            InitializeComponent();
        }

        private async void AddSurveyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetailsView(), true);
        }
    }
}