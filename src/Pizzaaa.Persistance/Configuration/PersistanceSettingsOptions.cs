namespace Pizzaaa.Persistance.Configuration;

public class PersistanceSettingsOptions
{
	public const string PersistanceSettings = "PersistanceSettings";
	public string PersistanceType { get; set; } = default!;
	public string ConnectionString { get; set; } = default!;
	public string Host { get; set; } = default!;
	public string Port { get; set; } = default!;
	public string Database { get; set; } = default!;
	public string User { get; set; } = default!;
	public string Password { get; set; } = default!;
	public bool CreateDatabaseIfNotExists { get; set; } = default!;
	public bool RecreateDatabase { get; set; } = default!;
	public bool SqliteInAppFolder { get; set; } = default!;
	public string? JsonSourceInitializer { get; set; }

	public PersistanceType GetEnumTipoDatabase()
	{
		return (PersistanceType?.ToLower()) switch
		{
			"mssql" or "sqlserver" => Configuration.PersistanceType.SqlServer,
			"sqlite" => Configuration.PersistanceType.Sqlite,
			_ => Configuration.PersistanceType.InMemory,
		};
	}
}
