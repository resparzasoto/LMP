using LMP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LMP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveysView : ContentPage
    {
        public SurveysView()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<SurveysViewModel>(this, Messages.NewSurvey, async (sender) =>
            {
                await Navigation.PushAsync(new SurveyDetailsView(), true);
            });
        }
    }
}