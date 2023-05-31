using ReadyWitted_Client.Views;

namespace ReadyWitted_Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PresentationSchemaPage),typeof(PresentationSchemaPage));
            Routing.RegisterRoute(nameof(PresentationPage),typeof(PresentationPage));
        }

#if WINDOWS
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Window.MinimumWidth = 500;
    }
#endif
    }
}