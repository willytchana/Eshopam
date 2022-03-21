using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eshopam.Mobile
{
    public partial class App : Application
    {
        public const string ServiceBaseAddress = "http://192.168.137.1:8180/api/";
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
