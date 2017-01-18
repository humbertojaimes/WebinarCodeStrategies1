using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WebinarCodeStrategies1
{
	public class EmployeeVM:INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged = delegate
		{

		};

		void RaiseProperty([CallerMemberName]string propertyName = "")
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		bool isBusy =false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { isBusy = value; 
				RaiseProperty();}
		}

		ObservableCollection<Employee> employees;
		public ObservableCollection<Employee> Employees
		{
			get { return employees; }
			set { employees = value; RaiseProperty(); }
		}

		Employee selectedEmployee;
		public Employee SelectedEmployee
		{
			get { return selectedEmployee; }
			set { selectedEmployee = value; RaiseProperty(); }
		}



		public Command LoadDirectoryCommand
		{
			get;
			set;
		}

		public Command TakePhotoCommand
		{
			get;
			set;
		}


		string _documentsPath,_path;
		DatabaseManager databaseManager;
		void LoadDirectory() 
		{ 
			if (!IsBusy)
			{
				IsBusy = true;
				Employees = 
					new ObservableCollection<Employee>(databaseManager.GetAllItems<Employee>());

				if (!Employees.Any())
				{
					EmployeeDirectory directory = new EmployeeDirectory(_path);
					Employees = directory.Employees;
				}
				SelectedEmployee = Employees[0];


				IsBusy = false;
			}
		}





		public EmployeeVM()
		{
			_documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			_path = Path.Combine(_documentsPath, "Employees.db");
			databaseManager = new DatabaseManager(_path);
			LoadDirectoryCommand = new Command(()=> LoadDirectory(),
			                                     ()=> !IsBusy);

			TakePhotoCommand = new Command(() => TakePhoto(),
												 () => !IsBusy);

		}

		async void TakePhoto()
		{
			var photo = await PhotoManager.TakePhoto();
			using (MemoryStream stream = new MemoryStream(photo))
			{
				var scores = await EmotionManager.EmotionManager.GetAverage(stream);
				var highestEmotion =  EmotionManager.EmotionManager.GetHighestEmotion(scores);
				if (highestEmotion.Item1 != EmotionManager.EmotionManager.EmotionType.Happiness)
					throw new Exception("La persona no esta feliz :/ ");
			
				if(highestEmotion.Item1 == EmotionManager.EmotionManager.EmotionType.Happiness &&
				   highestEmotion.Item2 < 70)
					throw new Exception("La persona no esta suficientemente feliz :(");
				SelectedEmployee.Photo = photo;
				databaseManager.SaveValue<Employee>(SelectedEmployee);
			}
		}
	}
}

