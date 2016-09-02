using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebinarCodeStrategies1
{
	public enum JobPosition
	{
		Excecutive,
		Operator,
		Supervisor
	}

	public class Employee :INotifyPropertyChanged,IKeyObject
	{
		[SQLite.PrimaryKey]
		public string Key
		{
			get;
			set;
		}

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value;
				Email = name + "@mycompany.com";
				RaiseProperty();
			}
		}

		private byte[] photo;
		public byte[] Photo
		{
			get { return photo; }
			set { photo = value; RaiseProperty(); }
		}

		private JobPosition position;
		public JobPosition Position { 
			get; 
			set; 
		}

		private string email;

		public event PropertyChangedEventHandler PropertyChanged = delegate {
			
		};

		void RaiseProperty([CallerMemberName]string propertyName = "")
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public string Email
		{
			get { return email; }
			set { 
				email = value;
				RaiseProperty();
			}
		}


		public Employee(string name, byte[] photo, JobPosition position, string email)
		{
			Name = name;
			Key = name;
			Photo = photo;
			Position = position;
			Email = email;
		}

		public Employee()
		{

		}
	}
}

