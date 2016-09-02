using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebinarCodeStrategies1
{
	public class EmployeeDirectory
	{
		string _path;
		public EmployeeDirectory(string path)
		{
			_path = path;
			generateRandomDirectory();
		}

		void generateRandomDirectory()
		{
			DatabaseManager databaseManager = new DatabaseManager(_path);
			Employees = new ObservableCollection<Employee>();
			Random rdn = new Random();
			string[] names = { "Nombre1", "Nombre2", "Nombre3", "Nombre4" };
			string[] photos =
			{
			"http://www.femside.com/wp-content/uploads/2013/06/suit-woman.jpg",
				"http://previews.123rf.com/images/phakimata/phakimata1103/phakimata110300023/9030612-Experienced-female-business-lawyer-in-suit-Beautiful-Senior-old-woman-with-arms-crossed-isolated--Stock-Photo.jpg",
				"https://angrymiddleagewoman.files.wordpress.com/2011/12/woman-in-suit.jpg",
				"http://steezo.com/wp-content/uploads/2012/12/man-in-suit2.jpg",
				"http://attractmen.org/wp-content/uploads/2015/10/attractmen.org-libra-men.jpg",
			};

			for (int i = 0; i < 1; i++)
			{
				var name = names[rdn.Next(0, 3)];
				var newEmployee = new Employee(
					name,
					null,
					(JobPosition)rdn.Next(0, 2),
					name + "@mycompany.com"
				);
				Employees.Add(newEmployee);
				databaseManager.SaveValue<Employee>(newEmployee);
			}

		}

		public ObservableCollection<Employee> Employees
		{
			get;
			set;
		}

	}

}

