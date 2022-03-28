using Eshopam.Services;
using System;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnConnect_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            BtnConnect.IsEnabled = false;
            try
            {
                UserService service = new UserService(App.ServiceBaseAddress);
                var user = await service.LoginAsync(TxtUserName.Text, TxtPassword.Text);
                Application.Current.MainPage = new NavigationPage(new MainPage(user));
            }
            catch(UnauthorizedAccessException ex)
            {
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "An error occured !", "Ok");
            }
            Loader.IsVisible = false;
            BtnConnect.IsEnabled = true;
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private void BtnEye_Clicked(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
            FisEye.FontFamily = TxtPassword.IsPassword ? "FontFaSolid900" : "FontFaRegular400";
        }
    }
}
