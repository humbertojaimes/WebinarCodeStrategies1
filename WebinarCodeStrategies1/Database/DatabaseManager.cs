using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace WebinarCodeStrategies1
{
	public interface IKeyObject
	{
		string Key { get; set; }
	}

	public class DatabaseManager : SQLiteConnection
	{
		public DatabaseManager(string path)
			: base(path)
		{
			// create the tables
			CreateTable<Employee>();
		}

		public void SaveValue<T>(T value) where T : IKeyObject, new()
		{


			var all = (from entry in Table<T>().AsEnumerable<T>()
					   where entry.Key == value.Key
					   select entry).ToList();
			if (all.Count == 0)
				Insert(value);
			else
				Update(value);

		}


		public void DeleteValue<T>(T value) where T : IKeyObject, new()
		{

			var all = (from entry in Table<T>().AsEnumerable<T>()
					   where entry.Key == value.Key
					   select entry).ToList();
			if (all.Count == 1)
				Delete(value);
			else
				throw new Exception("The db doesn't contain a entry with the specified key");


		}


		public List<TSource> GetAllItems<TSource>() where TSource : IKeyObject, new()
		{

			return Table<TSource>().AsEnumerable<TSource>().ToList();

		}


		public TSource GetItem<TSource>(string key) where TSource : IKeyObject, new()
		{

			var result = (from entry in Table<TSource>().AsEnumerable<TSource>()
						  where entry.Key == key
						  select entry).FirstOrDefault();
			return result;

		}



	}
}

