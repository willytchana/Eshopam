using Eshopam.Models;
using Eshopam.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly UserModel user;

        public MainPage(UserModel user)
        {
            InitializeComponent();
            this.user = user;
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                ProductService service = new ProductService(App.ServiceBaseAddress);
                var products = await service.GetAsync();
                CvProduct.ItemsSource = products;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "An error occured !", "Ok");
            }
            Loader.IsVisible = false;
            base.OnAppearing();
        }


        private async void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProductEdit(user, OnAppearing), true);
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Rv.IsRefreshing = false;
            OnAppearing();
        }
    }
}
