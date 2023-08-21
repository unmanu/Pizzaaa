using Moq;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.System.Interfaces;
using Pizzaaa.IntegrationTest.Mothers;
using Pizzaaa.IntegrationTest.Setup;
using Pizzaaa.IntegrationTest.TestUtils;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.IntegrationTest.Repositories;

public class BaseRepositoryIntegrationTest : BaseDatabaseTest
{
	private readonly Mock<ISecurityService> _mockSecurityService;
	private readonly Mock<IDateService> _mockDateService;
	private readonly DateTime now;

	public BaseRepositoryIntegrationTest()
	{
		_mockSecurityService = new Mock<ISecurityService>();
		_mockDateService = new Mock<IDateService>();
		now = new DateTime(2023, 3, 27, 23, 50, 31, 991);
	}

	[Fact]
	public async Task FindById_ElementExists_ReturnsElement()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		User? result = await repository.FindById(50);

		Assert.NotNull(result);
		Assert.Equal("bob", result.Username);
	}

	[Fact]
	public async Task FindById_ElementNotExists_ReturnsNull()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		User? result = await repository.FindById(9999);

		Assert.Null(result);
	}

	[Fact]
	public async Task FindAll_HasElements_ReturnsElements()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		List<User> result = await repository.FindAll();

		Assert.NotNull(result);
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Count);
	}

	[Fact]
	public async Task FindAll_HasNoElements_ReturnsEmptyList()
	{
		using PizzaContext context = CreateContext();
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		List<User> result = await repository.FindAll();

		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public async Task Insert_ValidElement_InsertUserWithAuditData()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);
		User userToInsert = UserMother.AEntityUser(0, "shalia");
		BLL.Models.User currentUser = new() { Username = "test" };

		_mockSecurityService.Setup(mock => mock.GetLoggedUser()).ReturnsAsync(currentUser);
		_mockDateService.Setup(mock => mock.GetNow()).Returns(now);

		await repository.Insert(userToInsert);

		Assert.NotNull(userToInsert);
		Assert.NotEqual(0, userToInsert.ID);
		Assert.Equal("test", userToInsert.InsertUser);
		Assert.Equal(now, userToInsert.InsertDate);
		Assert.Null(userToInsert.UpdateUser);
		Assert.Null(userToInsert.UpdateDate);
	}

	[Fact]
	public async Task Insert_DuplicateElement_ThrowsException()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);
		User userToInsert = UserMother.AEntityUser(7, "shalia");
		BLL.Models.User currentUser = new() { Username = "test" };

		_mockSecurityService.Setup(mock => mock.GetLoggedUser()).ReturnsAsync(currentUser);
		_mockDateService.Setup(mock => mock.GetNow()).Returns(now);

		InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => repository.Insert(userToInsert));

		Assert.NotNull(exception);
		Assert.Contains(TestConstants.ENTITY_FRAMEWORK_DUPLICATE_ERROR, exception.Message);
	}

	[Fact]
	public async Task Delete_FoundsElementToDelete_DeleteElement()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		int result = await repository.Delete(7);

		Assert.Equal(1, result);
		Assert.False(context.Users.Any(x => x.ID == 7));
	}

	[Fact]
	public async Task Delete_NotFoundsElementToDelete_DoesNothing()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		int result = await repository.Delete(9999);

		Assert.Equal(0, result);
	}

	[Fact]
	public async Task Update_FoundsElementToUpdate_UpdatesTheElement()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);
		BLL.Models.User currentUser = new() { Username = "test" };

		_mockSecurityService.Setup(mock => mock.GetLoggedUser()).ReturnsAsync(currentUser);
		_mockDateService.Setup(mock => mock.GetNow()).Returns(now);

		User? result = await repository.Update(7, x => x.Username = "joseph");

		Assert.NotNull(result);
		Assert.True(context.Users.Any(x => x.ID == 7 && x.Username == "joseph"));
		Assert.Equal("test", result.UpdateUser);
		Assert.Equal(now, result.UpdateDate);
	}

	[Fact]
	public async Task Update_NotFoundsElementToUpdate_ReturnsNull()
	{
		using PizzaContext context = CreateContext();
		await DbTestInitializer(context);
		BaseRepository<User> repository = new UserRepository(context, _mockSecurityService.Object, _mockDateService.Object);

		User? result = await repository.Update(99999, x => x.Username = "joseph");

		Assert.Null(result);
		Assert.False(context.Users.Any(x => x.Username == "joseph"));
	}

	private static async Task DbTestInitializer(PizzaContext context)
	{
		User user1 = UserMother.AEntityUser(7, "jack");
		User user2 = UserMother.AEntityUser(91, "tim");
		User user3 = UserMother.AEntityUser(50, "bob");
		await context.Users.AddAsync(user1);
		await context.Users.AddAsync(user2);
		await context.Users.AddAsync(user3);
		await context.SaveChangesAsync();
	}
}
