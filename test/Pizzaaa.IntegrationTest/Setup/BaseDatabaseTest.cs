using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pizzaaa.Persistance.Data;
using System.Data.Common;

namespace Pizzaaa.IntegrationTest.Setup;


public abstract class BaseDatabaseTest
{
	private readonly DbConnection _connection;
	private readonly DbContextOptions<PizzaContext> _contextOptions;

	protected BaseDatabaseTest()
	{
		// Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
		// at the end of the test (see Dispose below).
		_connection = new SqliteConnection("Filename=:memory:");
		_connection.Open();

		// These options will be used by the context instances in this test suite, including the connection opened above.
		_contextOptions = new DbContextOptionsBuilder<PizzaContext>()
			.UseSqlite(_connection)
			.Options;

		// Create the schema and seed some data
		using var context = new PizzaContext(_contextOptions);

		context.Database.EnsureCreated();
		DbInitializer.Initialize(context);
	}

	internal PizzaContext CreateContext() => new(_contextOptions);

	public void Dispose() => _connection.Dispose();


}
