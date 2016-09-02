using System;
using System.Globalization;
using Xamarin.Forms;

namespace WebinarCodeStrategies1
{
	public class PositionToColorConverter:IValueConverter
	{
		public PositionToColorConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			JobPosition position = (JobPosition)value;
			Color color;
			switch (position)
			{
				case JobPosition.Excecutive:
					color = Color.Red;
					break;
				case JobPosition.Operator:
					color = Color.Yellow;
					break;
				case JobPosition.Supervisor:
					color = Color.Green;
					break;
				default:
					color = Color.Gray;
					break;
			}
			return color;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

