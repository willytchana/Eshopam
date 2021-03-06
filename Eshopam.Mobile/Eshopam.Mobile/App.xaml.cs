using Eshopam.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eshopam.Mobile
{
    public partial class App : Application
    {
        private static string serviceBaseAddress;

        public static string ServiceBaseAddress => serviceBaseAddress;
        public App()
        {
            InitializeComponent();

            //#if DEBUG
                if(DeviceInfo.DeviceType == DeviceType.Virtual)
                    serviceBaseAddress = "http://10.0.2.2:8083/api/";
                else
                    serviceBaseAddress = "http://192.168.43.183:8083/api/";
            //#else
            //serviceBaseAddress = "https://eshopam.com/api/";
            //#endif
            var json = SecureStorage.GetAsync("user_session").Result;
            if(string.IsNullOrEmpty(json))
                MainPage = new NavigationPage(new LoginPage());
            else
            {
                var user = JsonConvert.DeserializeObject<UserModel>(json);
                MainPage = new NavigationPage(new MainPage(user));
            }
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
