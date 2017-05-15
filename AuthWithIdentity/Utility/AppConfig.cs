using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
  public class AppConfig
    {
        public AppConfig(IConfiguration configuration)
        {
			IdentityDatabaseConfig = new DatabaseConfig
			{
				Host = configuration.GetValue<string>("ID_DB_HOST"),
				Username = configuration.GetValue<string>("ID_DB_USERNAME"),
				Password = configuration.GetValue<string>("ID_DB_PASSWORD"),
				Name = configuration.GetValue<string>("ID_DB_NAME")
			};
            IdentityServerDatabaseConfig = new DatabaseConfig
            {
                Host = configuration.GetValue<string>("IDS_DB_HOST"),
                Username = configuration.GetValue<string>("IDS_DB_USERNAME"),
                Password = configuration.GetValue<string>("IDS_DB_PASSWORD"),
                Name = configuration.GetValue<string>("IDS_DB_NAME")
            };
            Port = configuration.GetValue<int>("PORT");
            AdminUsername = configuration.GetValue<string>("ADMIN_USERNAME");
            AdminPassword = configuration.GetValue<string>("ADMIN_PASSWORD");
            // UserSecret = configuration.GetValue<string>("User-Secret");
        }

		public DatabaseConfig IdentityDatabaseConfig { get; }

        public DatabaseConfig IdentityServerDatabaseConfig { get; }

        public string AdminPassword { get; set; }

        public string AdminUsername { get; set; }

        public int Port { get; set; }

        public string UserSecret { get; set; }
    }

	public class DatabaseConfig
	{
		public string Name { get; set; }

		public string Host { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string ConnectionString
		{
			get
			{
				SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
				connectionBuilder.DataSource = Host;
				connectionBuilder.UserID = Username;
				connectionBuilder.Password = Password;
				connectionBuilder.InitialCatalog = Name;
				return connectionBuilder.ConnectionString;
			}
		}
	}
}
