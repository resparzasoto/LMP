using Prism.Mvvm;
using Prism.Navigation;

namespace LMP
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
