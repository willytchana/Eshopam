using Eshopam.Models;
using Eshopam.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eshopam.Mobile
{
    public partial class ProductPhoto : ContentPage
    {
        private readonly UserModel user;
        private ProductModel product;
        private readonly Action callBack;
        private string imageFile;
        public ProductPhoto(UserModel user, ProductModel product, Action callBack)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;
            this.callBack = callBack;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            BtnSave.IsEnabled = false;
            try
            {
                if (string.IsNullOrEmpty(imageFile))
                    throw new InvalidOperationException("Please take a picture");

                ProductService service = new ProductService(App.ServiceBaseAddress);
                product = await service.CreateAsync(product, File.ReadAllBytes(imageFile));
                await DisplayAlert("Good", "Save done !", "Ok");
                int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;


                if (callBack != null)
                    callBack();

                for (int currModal = 0; currModal < numModals; currModal++)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
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
            BtnSave.IsEnabled = true;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnPicture_Clicked(object sender, EventArgs e)
        {
            string[] buttons = { "Galery", "Camera" };
            var result =  await DisplayActionSheet
            (
                "Take a picture",
                "Cancel",
                string.Empty,
                buttons
            );

            FileResult photo = null;
            if (result == buttons[0])
            {
                photo = await MediaPicker.PickPhotoAsync
                (
                    new MediaPickerOptions { Title = "Take a picture" }
                );
            }
            else if (result == buttons[1])
            {
                photo = await MediaPicker.CapturePhotoAsync
                (
                    new MediaPickerOptions { Title = "Take a picture" }
                );
            }

            Loader.IsVisible = true;
            imageFile = await loadPicture(photo);
            if(!string.IsNullOrEmpty(imageFile))
                Img.Source = ImageSource.FromFile(imageFile);
            Loader.IsVisible = false;
        }

        private async Task<string> loadPicture(FileResult photo)
        {
            if (photo == null)
                return null;

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            return newFile;
        }
    }
}
