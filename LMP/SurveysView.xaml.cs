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

            MessagingCenter.Subscribe<SurveyDetailsView, Survey>(this, Messages.NewSurveyComplete, (sender, args) =>
            {
                SurveysPanel.Children.Add(new Label()
                {
                    Text = args.ToString()
                });
            });
        }

        private async void AddSurveyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetailsView(), true);
        }
    }
}