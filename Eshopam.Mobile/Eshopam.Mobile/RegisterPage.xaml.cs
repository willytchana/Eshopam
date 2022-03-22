using Eshopam.Models;
using Eshopam.Services;
using System;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            BtnSave.IsEnabled = false;
            try
            {
                UserService service = new UserService(App.ServiceBaseAddress);
                var user = await service.CreateAsync
                (
                    new UserModel
                    (
                        0,
                        TxtUserName.Text, 
                        TxtFullName.Text,
                        "Visitor",
                        TxtPassword.Text
                    )
                );
                await DisplayAlert("Good", user.Id.ToString(), "Ok");
                BtnLogin_Clicked(sender, e);
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
            BtnSave.IsEnabled = true;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void BtnEye_Clicked(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
            FisEye.FontFamily = TxtPassword.IsPassword ? "FontFaSolid900" : "FontFaRegular400";
        }
    }
}
