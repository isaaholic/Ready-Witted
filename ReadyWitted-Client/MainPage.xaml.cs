using ReadyWitted_Client.Views;

namespace ReadyWitted_Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSchema_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(PresentationSchemaPage));
        }

        private void btnPres_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(PresentationPage));
        }
    }
}