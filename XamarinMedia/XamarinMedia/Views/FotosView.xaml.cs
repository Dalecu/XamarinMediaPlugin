using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMedia.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FotosView : ContentPage
	{
		public FotosView ()
		{
			InitializeComponent ();
            this.btnfoto.Clicked += Btnfoto_Clicked;
		}

        private async void Btnfoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No hay camara", "No se deteca la camara.", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    // Variable para guardar la foto en el album público
                    SaveToAlbum = true
                });

                if (file == null)
                    return;

                this.imgcamara.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                await DisplayAlert("Foto realizada", "Localización: " + file.AlbumPath, "Ok");

            } catch(Exception ex)
            {
                await DisplayAlert("Permiso denegado", "Da permisos de cámara al dispositivo.\nError: "+ex.Message, "Ok");
            }
        }
    }
}