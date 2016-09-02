using Xamarin.Forms;

namespace DemoSqlite
{
	public partial class DemoSqlitePage : ContentPage
	{
		public DemoSqlitePage()
		{
			InitializeComponent();
			BindingContext = new EmployeeVM(); 
		}
	}
}

