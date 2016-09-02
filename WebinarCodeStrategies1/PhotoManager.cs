using System;
using System.Threading.Tasks;
using Xamarin.Media;
using Xamarin.Forms;
using System.IO;

namespace WebinarCodeStrategies1
{
	public class PhotoManager
	{
#if __ANDROID__
		public async static Task<Byte[]> TakePhoto ()
		{
			MediaPicker picker = new MediaPicker (Forms.Context);
#elif __IOS__
		public async static Task<Byte[]> TakePhoto()
		{
			MediaPicker picker = new MediaPicker();
#endif
			byte[] imageData;
			StoreCameraMediaOptions options = new StoreCameraMediaOptions();
			options.DefaultCamera = CameraDevice.Rear;
			options.Name = "photo";
			var photo = await picker.TakePhotoAsync(options);
			Stream stream = photo.GetStream();


			using (MemoryStream ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				imageData = ms.ToArray();
			}
			stream.Dispose();
			return imageData;
		}



	}
}

