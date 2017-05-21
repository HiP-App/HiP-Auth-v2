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
            CmsAddress = configuration.GetValue<string>("CMS_ADDRESS");
            TokenGeneratorAddress = configuration.GetValue<string>("TOKEN_GENERATOR_ADDRESS");
            WebApiAddress = configuration.GetValue<string>("WEB_API_ADDRESS");

            ClientSecrets = new Secrets
            {
                Cms = configuration.GetValue<string>("SECRET_CMS"),
                Generator = configuration.GetValue<string>("SECRET_GENERATOR"),
                Mobile = configuration.GetValue<string>("SECRET_MOBILE")
            };
        }

        public string CmsAddress { get; set; }

        public string WebApiAddress { get; set; }

        public string TokenGeneratorAddress { get; set; }

        public DatabaseConfig IdentityDatabaseConfig { get; }

        public DatabaseConfig IdentityServerDatabaseConfig { get; }

        public Secrets ClientSecrets { get; }

        public string AdminPassword { get; set; }

        public string AdminUsername { get; set; }

        public int Port { get; set; }
    }

  public class Secrets
  {
    public string Cms { get; internal set; }
    public string Generator { get; internal set; }
    public string Mobile { get; internal set; }
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
