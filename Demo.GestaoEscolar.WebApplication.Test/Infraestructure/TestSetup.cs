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
			CreateTables();
			Seed();
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

		private static void CreateTables()
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

											ALTER TABLE GES.PessoaFisica ADD CONSTRAINT PK_PessoaFisica PRIMARY KEY (Id)

											ALTER TABLE GES.PessoaFisica ADD CONSTRAINT UC_PessoaFisica_Cpf UNIQUE(Cpf);


											CREATE TABLE GES.AlunoSituacao (
												Id INT NOT NULL,
												Nome VARCHAR(400) NOT NULL
											);

											ALTER TABLE GES.AlunoSituacao ADD CONSTRAINT PK_AlunoSituacao PRIMARY KEY (Id);


											CREATE TABLE GES.Aluno(
												Id INT IDENTITY NOT NULL,
												EntityId UNIQUEIDENTIFIER NOT NULL,
												DataCriacao DATETIME NOT NULL,
												PessoaFisicaId INT NOT NULL,
												ResponsavelId INT NOT NULL,
												Matricula INT NOT NULL,
												SituacaoId INT NOT NULL,
											);

											ALTER TABLE GES.Aluno ADD CONSTRAINT PK_Aluno PRIMARY KEY (Id);

											ALTER TABLE GES.Aluno ADD CONSTRAINT UC_Aluno_Matricula UNIQUE(Matricula);

											ALTER TABLE GES.Aluno ADD CONSTRAINT FK_Aluno_PessoaFisica FOREIGN KEY (PessoaFisicaId) REFERENCES GES.PessoaFisica(Id);

											ALTER TABLE GES.Aluno ADD CONSTRAINT FK_Aluno_Responsavel FOREIGN KEY (ResponsavelId) REFERENCES GES.PessoaFisica(Id);

											ALTER TABLE GES.Aluno ADD CONSTRAINT FK_Aluno_Situacao FOREIGN KEY (SituacaoId) REFERENCES GES.AlunoSituacao(Id);


											CREATE TABLE GES.Escola (
												Id INT IDENTITY NOT NULL,
												EntityId UNIQUEIDENTIFIER NOT NULL,
												DataCriacao DATETIME NOT NULL,
												Nome VARCHAR(400) NOT NULL
											);
											ALTER TABLE GES.Escola ADD CONSTRAINT PK_Escola PRIMARY KEY (Id);

											CREATE TABLE GES.SalaTurno (
												Id INT NOT NULL,
												Nome VARCHAR(400) NOT NULL
											);

											ALTER TABLE GES.SalaTurno ADD CONSTRAINT PK_SalaTurno PRIMARY KEY (Id);

											CREATE TABLE GES.Sala (
												Id INT IDENTITY NOT NULL,
												EntityId UNIQUEIDENTIFIER NOT NULL,
												EscolaId INT NOT NULL,
												FaseAno VARCHAR(100) NOT NULL,
												TurnoId INT NOT NULL
											);

											ALTER TABLE GES.Sala ADD CONSTRAINT PK_Sala PRIMARY KEY (Id);

											ALTER TABLE GES.Sala ADD CONSTRAINT FK_Sala_Escola FOREIGN KEY (EscolaId) REFERENCES GES.Escola(Id);

											ALTER TABLE GES.Sala ADD CONSTRAINT FK_Sala_Turno FOREIGN KEY (TurnoId) REFERENCES GES.SalaTurno(Id);


											CREATE TABLE GES.SalaAluno (
												Id INT IDENTITY NOT NULL,
												SalaId INT NOT NULL,
												AlunoId INT NOT NULL
											);

											ALTER TABLE GES.SalaAluno ADD CONSTRAINT PK_SalaAluno PRIMARY KEY (Id);

											ALTER TABLE GES.SalaAluno ADD CONSTRAINT FK_SalaAluno_Sala FOREIGN KEY (SalaId) REFERENCES GES.Sala(Id);

											ALTER TABLE GES.SalaAluno ADD CONSTRAINT FK_SalaAluno_Aluno FOREIGN KEY (AlunoId) REFERENCES GES.Aluno(Id);

											ALTER TABLE GES.SalaAluno ADD CONSTRAINT UC_SalaAluno_Sala_Aluno UNIQUE(SalaId, AlunoId);
											");
		}

		private static void Seed()
		{
			ExecuteSqlCommand(GestaoEscolar, @"
			INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(1, 'Não Matriculado');
			INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(2, 'Matriculado');
			INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(3, 'Transferido');
			INSERT INTO GES.AlunoSituacao(Id, Nome) VALUES(4, 'Expulso');

			--SalaTurno
			INSERT INTO GES.SalaTurno(Id, Nome) VALUES(1, 'Matutino');
			INSERT INTO GES.SalaTurno(Id, Nome) VALUES(2, 'Vespertino');
			INSERT INTO GES.SalaTurno(Id, Nome) VALUES(3, 'Noturno');");
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
