using Eshopam.Models;
using Eshopam.Services;
using System;
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


        private async void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProductEdit(user), true);
        }
    }
}
