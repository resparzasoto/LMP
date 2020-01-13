using LMP.ServiceInterfaces;
using LMP.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LMP.ViewModels
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        private readonly ILocalDBService localDBService;

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

        public bool IsEmpty => Surveys is null || !Surveys.Any();

        private ObservableCollection<SurveyViewModel> surveys;

        public ObservableCollection<SurveyViewModel> Surveys
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

        private SurveyViewModel selectedSurvey;

        public SurveyViewModel SelectedSurvey
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

        public ICommand DeleteSurveyCommand { get; set; }

        public SurveysViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDBService localDBService)
        {
            Title = "Encuestas";

            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.localDBService = localDBService;

            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
            DeleteSurveyCommand = new DelegateCommand(DeleteSurveyCommandExecute, DeleteCommandCanExecute).ObservesProperty(() => SelectedSurvey);

            Surveys = new ObservableCollection<SurveyViewModel>();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadSurveysAsync();
        }

        private async void NewSurveyCommandExecute()
        {
            await navigationService.NavigateAsync($"{nameof(SurveyDetailsView)}");
        }

        private bool DeleteCommandCanExecute()
        {
            return SelectedSurvey != null;
        }

        private async void DeleteSurveyCommandExecute()
        {
            var result =
                await pageDialogService.DisplayAlertAsync(Literals.DeleteSurveyTitle, Literals.DeleteSurveyConfirmation, Literals.OK, Literals.Cancel);

            if (result)
            {
                await localDBService.DeleteSurveyAsync(SurveyViewModel.GetEntityFromViewModel(SelectedSurvey));
                await LoadSurveysAsync();
            }
        }


        private async Task LoadSurveysAsync()
        {
            var localDBTeams = await localDBService.GetAllTeamsAsync();
            var allSurveys = await localDBService.GetAllSurveysAsync();

            if (allSurveys != null)
            {
                Surveys = new ObservableCollection<SurveyViewModel>(allSurveys.Select(s => SurveyViewModel.GetViewModelFromEntity(s, localDBTeams)));
            }

            SelectedSurvey = null;
            RaisePropertyChanged(nameof(IsEmpty));
        }
    }
}
