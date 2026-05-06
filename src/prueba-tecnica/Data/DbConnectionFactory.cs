using System.Data;
using Microsoft.Data.SqlClient;

namespace prueba_tecnica.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private const string ConnectionSectionPath = "Connections:sqlserver";

    public IDbConnection CreateConnection()
    {
        var section = configuration.GetSection(ConnectionSectionPath);

        var server = section["Server"];
        var database = section["Database"];

        if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(database))
        {
            throw new InvalidOperationException(
                "Configura Connections:sqlserver:Server y Connections:sqlserver:Database en appsettings.json.");
        }

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = $"{server},{section["Port"] ?? "1433"}",
            InitialCatalog = database,
            ConnectTimeout = int.TryParse(section["Timeout"], out var timeout) ? timeout : 30,
            IntegratedSecurity = bool.TryParse(section["IntegratedSecurity"], out var integratedSecurity) && integratedSecurity,
            Encrypt = bool.TryParse(section["Encrypt"], out var encrypt) && encrypt,
            TrustServerCertificate = bool.TryParse(section["TrustServerCertificate"], out var trustServerCertificate) && trustServerCertificate
        };

        if (builder.IntegratedSecurity) return new SqlConnection(builder.ConnectionString);
        builder.UserID = section["User"];
        builder.Password = section["Password"];

        return new SqlConnection(builder.ConnectionString);
    }
}
