using System;
namespace WebinarCodeStrategies1.iOS
{
	public static class ImageUtils
	{
		public static UIKit.UIImage ToImage(this byte[] data)
		{
			UIKit.UIImage image;
			try
			{
				image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
			}
			catch
			{
				return null;
			}
			return image;
		}

	}
}
