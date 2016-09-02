using Xamarin.Forms;

namespace WebinarCodeStrategies1
{
	public partial class WebinarCodeStrategies1Page : ContentPage
	{
		public WebinarCodeStrategies1Page()
		{
			InitializeComponent();
			BindingContext = new EmployeeVM();
		}
	}
}

