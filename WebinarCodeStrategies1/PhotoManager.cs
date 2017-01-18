using System;
using System.Threading.Tasks;
using System.IO;
using Plugin.Media.Abstractions;
#if __IOS__
using WebinarCodeStrategies1.iOS;
using UIKit;
#endif

namespace WebinarCodeStrategies1
{
	public class PhotoManager
	{

		public async static Task<Byte[]> TakePhoto()
		{
			byte[] imageData;
			StoreCameraMediaOptions options = new StoreCameraMediaOptions();
			options.Name = "photo";
			var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(options);
			Stream stream = photo.GetStream();


			using (MemoryStream ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				imageData = ms.ToArray();
			}
			stream.Dispose();

#if __IOS__
				imageData = ScaleImage(imageData, 50);
			#endif
			return imageData;
		}


		#if __IOS__
		public static byte[] ResizeImage(byte[] originalImage, int newHeight, int newWidth)
		{

			byte[] resulImage = null;
			UIKit.UIImage uiImage = originalImage.ToImage();
			UIGraphics.BeginImageContext(new CoreGraphics.CGSize(newWidth, newHeight));
			uiImage.Draw(new CoreGraphics.CGRect(0, 0, newWidth, newHeight));
			UIImage resultUIImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			resulImage = resultUIImage.AsJPEG().ToArray();
			return resulImage;
		}


		public static byte[] ScaleImage(byte[] originalImage, double finalImagePercentage)
		{
			UIKit.UIImage uiImage = originalImage.ToImage();
			nfloat width = uiImage.Size.Width;
			nfloat height = uiImage.Size.Height;
			int scaleWidth = Convert.ToInt16(width * (finalImagePercentage * .01));
			int scaleHeight = Convert.ToInt16(height * (finalImagePercentage * .01));
			return ResizeImage(originalImage, scaleHeight, scaleWidth);
		}
		#endif


	}


}

