using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LMP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}