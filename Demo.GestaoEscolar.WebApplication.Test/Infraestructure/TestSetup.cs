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
		public TestSetup()
		{
			DestroyDatabase();
			CreateDatabase();
			CreateSchema();
			CreateTablePessoaFisica();
		}

		public void Dispose()
		{
			DestroyDatabase();
		}

		private static void CreateDatabase()
		{
			ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [GestaoEscolar]
                ON (NAME = 'GestaoEscolar',
                FILENAME = '{Filename}')");
		}

		private static void CreateSchema()
		{
			ExecuteSqlCommand(GestaoEscolar, $@"CREATE SCHEMA GES;");
		}

		private static void CreateTablePessoaFisica()
		{
			ExecuteSqlCommand(GestaoEscolar, $@"CREATE TABLE GES.PessoaFisica(
											Id INT IDENTITY NOT NULL,
											EntityId UNIQUEIDENTIFIER NOT NULL,
											DataCriacao DATETIME NOT NULL,
											Nome VARCHAR(400) NOT NULL,
											Cpf VARCHAR(11) NOT NULL,
											NomeSocial VARCHAR(400) NULL,
											Sexo CHAR(1) NOT NULL,
											DataNascimento DATETIME NOT NULL
										);
										ALTER TABLE GES.PessoaFisica ADD CONSTRAINT PK_PessoaFisica PRIMARY KEY (Id);
										ALTER TABLE GES.PessoaFisica ADD CONSTRAINT UC_PessoaFisica_Cpf UNIQUE(Cpf);");
		}

		private static void DestroyDatabase()
		{
			var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('GestaoEscolar')",
				row => (string)row["physical_name"]);

			if (fileNames.Any())
			{
				ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [GestaoEscolar] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'GestaoEscolar'");

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
				InitialCatalog = "GestaoEscolar",
				IntegratedSecurity = true
			};

		private static string Filename => Path.Combine(
			Path.GetDirectoryName(
				typeof(TestSetup).GetTypeInfo().Assembly.Location),
			"GestaoEscolar.mdf");
	}
}
