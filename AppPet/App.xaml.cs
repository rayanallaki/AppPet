using Microsoft.Extensions.DependencyInjection;
using AppPet.Views;

namespace AppPet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new FrmPrincipal());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 400;
            window.Height = 600;
            return window;
        }
    }
}