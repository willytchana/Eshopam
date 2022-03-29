using Eshopam.Models;
using Eshopam.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class ProductEdit : ContentPage
    {
        private readonly UserModel user;
        private readonly Action callBack;

        public ProductEdit(UserModel user, Action callBack)
        {
            InitializeComponent();
            this.user = user;
            this.callBack = callBack;
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                CategoryService service = new CategoryService(App.ServiceBaseAddress);
                var categories = await service.GetAsync();

                var list = categories.ToList();
                list.Insert(0, new CategoryModel { Name = "Choose a category" });

                CbCategory.ItemsSource = list;
                CbCategory.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "An error occured !", "Ok");
            }
            base.OnAppearing();
            Loader.IsVisible = false;
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnNext_Clicked(object sender, EventArgs e)
        {
            CheckForm();

            Loader.IsVisible = true;
            BtnNext.IsEnabled = false;
            try
            {
                var product = new ProductModel
                (
                    0,
                    TxtCode.Text,
                    TxtName.Text,
                    TxtDescription.Text,
                    float.Parse(TxtPrice.Text),
                    string.Empty,
                    user.Id,
                    (CbCategory.SelectedItem as CategoryModel)?.Id ?? 0,
                    DateTime.Now
                );
                await Navigation.PushModalAsync(new ProductPhoto(user, product, callBack));
            }
            catch (InvalidOperationException ex)
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

        private void CheckForm()
        {
            if (string.IsNullOrWhiteSpace(TxtCode.Text))
                throw new InvalidOperationException("Please enter product code !");
            if (string.IsNullOrWhiteSpace(TxtName.Text))
                throw new InvalidOperationException("Please enter product name !");
            if (string.IsNullOrWhiteSpace(TxtPrice.Text))
                throw new InvalidOperationException("Please enter product price !");
            if (string.IsNullOrWhiteSpace(TxtDescription.Text))
                throw new InvalidOperationException("Please enter product description !");
            if (CbCategory.SelectedIndex <= 0)
                throw new InvalidOperationException("Please choose product category !");
        }
    }
}
