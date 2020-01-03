namespace LMP.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
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

        public AboutViewModel()
        {
            Title = "Acerca de...";
        }
    }
}
