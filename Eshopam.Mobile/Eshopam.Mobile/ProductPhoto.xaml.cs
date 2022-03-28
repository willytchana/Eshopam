using Eshopam.Models;
using Eshopam.Services;
using System;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class ProductPhoto : ContentPage
    {
        private readonly UserModel user;
        private readonly ProductModel product;

        public ProductPhoto(UserModel user, ProductModel product)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            BtnNext.IsEnabled = false;
            try
            {
                //UserService service = new UserService(App.ServiceBaseAddress);
                //var user = await service.LoginAsync(TxtUserName.Text, TxtPassword.Text);
                //await DisplayAlert("Good", user.Fullname, "Ok");
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
            BtnNext.IsEnabled = true;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
    }
}
