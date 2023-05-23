using Spicify.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spicify
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new Recipe());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
