namespace Demo.GestaoEscolar.Infra.Dapper
{
	public class AppConnectionString
    {
		private readonly string _connectionString;

		public AppConnectionString(string connectionString)
		{
			_connectionString = connectionString;
		}

		public static implicit operator string(AppConnectionString value)
		{
			return value._connectionString;
		}
	}
}
