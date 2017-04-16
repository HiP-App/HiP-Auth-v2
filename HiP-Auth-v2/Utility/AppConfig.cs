using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
  public class AppConfig
    {
        public AppConfig(IConfiguration configuration)
        {
			DatabaseConfig = new DatabaseConfig
			{
				Host = configuration.GetValue<string>("DB_HOST"),
				Username = configuration.GetValue<string>("DB_USERNAME"),
				Password = configuration.GetValue<string>("DB_PASSWORD"),
				Name = configuration.GetValue<string>("DB_NAME")
			};
            Port = configuration.GetValue<int>("Port");
            User = configuration.GetValue<string>("User");
            UserPassword = configuration.GetValue<string>("User-Password");
            UserSecret = configuration.GetValue<string>("User-Secret");
        }

		public DatabaseConfig DatabaseConfig { get; }

        public string UserPassword { get; set; }

        public string User { get; set; }

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
