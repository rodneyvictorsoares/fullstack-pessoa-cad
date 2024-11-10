using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace apppessoa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //var mainPage = ServiceProvider.GetService<MainPage>();
            //MainPage = new NavigationPage(mainPage);
        }
    }
}
