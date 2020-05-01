using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public class TestSetup : IDisposable
	{
		private readonly string _applicationPhysicalPath;
		private static readonly int _timeInSecondsFilesDelete = 30;
		private static string DBTestIntegrationNameSufix;

		protected static string DBNameTestIntegration => $"GestaoEscolar{DBTestIntegrationNameSufix}";

		public TestSetup(IWebHostEnvironment env)
		{
			_applicationPhysicalPath = env.ContentRootPath;
			DBTestIntegrationNameSufix = "Test";

			DestroyDatabase();
			CreateDatabase();
			RunScriptsFiles();

		}

		public void Dispose()
		{
			DestroyDatabase();
		}

		private static void CreateDatabase()
		{
			ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [{DBNameTestIntegration}]
                ON (NAME = '{DBNameTestIntegration}',
                FILENAME = '{Filename}')");
		}

		private void RunScriptsFiles()
		{
			var scripts = GetScriptsFromFiles();

			foreach (var script in scripts)
			{
				ExecuteSqlCommand(GestaoEscolar, script);
			}

		}

		private static void DestroyDatabase()
		{
			var fileNames = ExecuteSqlQuery(Master, $@"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('{DBNameTestIntegration}')",
				row => (string)row["physical_name"]);

			if (fileNames.Any())
			{
				ExecuteSqlCommand(Master, $@"
                    ALTER DATABASE [{DBNameTestIntegration}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db '{DBNameTestIntegration}'");

				fileNames.ForEach(File.Delete);

			}
		}

		private static void ExecuteSqlCommand(
			SqlConnectionStringBuilder connectionStringBuilder,
			string commandText)
		{
			using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = commandText;
					command.ExecuteNonQuery();
				}
			}
		}

		private static List<T> ExecuteSqlQuery<T>(
			SqlConnectionStringBuilder connectionStringBuilder,
			string queryText,
			Func<SqlDataReader, T> read)
		{
			var result = new List<T>();
			using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = queryText;
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(read(reader));
						}
					}
				}
			}
			return result;
		}

		private static SqlConnectionStringBuilder Master =>
			new SqlConnectionStringBuilder
			{
				DataSource = @"(LocalDB)\MSSQLLocalDB",
				InitialCatalog = "master",
				IntegratedSecurity = true
			};

		private static SqlConnectionStringBuilder GestaoEscolar =>
			new SqlConnectionStringBuilder
			{
				DataSource = @"(LocalDB)\MSSQLLocalDB",
				InitialCatalog = DBNameTestIntegration,
				IntegratedSecurity = true
			};

		private static string Filename => Path.Combine(
			Path.GetDirectoryName(
				typeof(TestSetup).GetTypeInfo().Assembly.Location),
			$"GestaoEscolar{DBTestIntegrationNameSufix}.mdf");

		public void DeleteFilesDatabaseLastsTests()
		{
			string[] files = Directory.GetFiles(Path.GetDirectoryName(
				typeof(TestSetup).GetTypeInfo().Assembly.Location));

			foreach (string file in files)
			{
				var fi = new FileInfo(file);

				if (!(new List<string>() { ".ldf", ".mdf" }.Contains(fi.Extension)))
				{
					continue;
				}

				if (fi.LastAccessTime.AddSeconds(_timeInSecondsFilesDelete) > DateTime.Now)
				{
					continue;
				}

				fi.Delete();
			}
		}

		public IEnumerable<string> GetScriptsFromFiles()
		{
			var result = new List<string>();

			var path = $"{Directory.GetParent(_applicationPhysicalPath).FullName}\\scripts";

			var files = Directory.GetFiles(path);

			foreach (var file in files)
			{
				var fi = new FileInfo(file);

				result.Add(File.ReadAllText(fi.FullName));
			}

			return result;
		}
	}
}
