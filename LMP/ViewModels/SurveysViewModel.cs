using LMP.Models;
using LMP.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LMP.ViewModels
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

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

        private ObservableCollection<Survey> surveys;

        public ObservableCollection<Survey> Surveys
        {
            get { return surveys; }
            set
            {
                if (surveys == value)
                {
                    return;
                }
                surveys = value;
                RaisePropertyChanged();
            }
        }

        private Survey selectedSurvey;

        public Survey SelectedSurvey
        {
            get { return selectedSurvey; }
            set
            {
                if (selectedSurvey == value)
                {
                    return;
                }
                selectedSurvey = value;
                RaisePropertyChanged();
            }
        }

        public ICommand NewSurveyCommand { get; set; }

        public SurveysViewModel(INavigationService navigationService)
        {
            Title = "Encuestas";

            this.navigationService = navigationService;
            
            NewSurveyCommand = new DelegateCommand(ExecuteNewSurveyCommand);

            Surveys = new ObservableCollection<Survey>();
        }

        private async void ExecuteNewSurveyCommand()
        {
            await navigationService.NavigateAsync($"{nameof(SurveyDetailsView)}");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Messages.NewSurvey))
            {
                Surveys.Add(parameters[Messages.NewSurvey] as Survey);
            }
        }
    }
}
