using Microsoft.EntityFrameworkCore;
using Pizzaaa.IntegrationTest.Mothers;
using Pizzaaa.IntegrationTest.Setup;
using Pizzaaa.IntegrationTest.TestUtils;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.IntegrationTest.Repositories;

public class SecurityRepositoryIntegrationTest : BaseDatabaseTest
{

    [Fact]
    public async Task FindByUsername_UserFound_ReturnsUser()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);

        User? risultato = await repository.FindByUsername("tim");

        Assert.NotNull(risultato);
        Assert.Equal(91, risultato?.ID);
    }

    [Fact]
    public async Task FindByUsername_UserNotFound_ReturnsNull()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);

        User? risultato = await repository.FindByUsername("slim");

        Assert.Null(risultato);
    }

    [Fact]
    public async Task Insert_ValidInput_InsertUser()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);
        User userToInsert = UserMother.AEntityUser(null, "smith");

        await repository.Insert(userToInsert);

        Assert.NotEqual(0, userToInsert.ID);
        Assert.Equal(userToInsert.Username, userToInsert.InsertUser);
        Assert.True(context.Users.Any(x => x.Username == userToInsert.Username));
    }

    [Fact]
    public async Task Insert_MissingNonNullData_ThrowsException()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);
        User userToInsert = new() { Username = "smith" };

        DbUpdateException exception = await Assert.ThrowsAsync<DbUpdateException>(
            () => repository.Insert(userToInsert));

        Assert.Contains(TestConstants.ENTITY_FRAMEWORK_SAVING_ERROR, exception.Message);
        Assert.False(context.Users.Any(x => x.Username == userToInsert.Username));
    }

    [Fact]
    public async Task Update_UserFound_ReturnsUserUpdated()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);

        User? updatedUser = await repository.Update(7, x => x.Salt = "test");

        Assert.NotNull(updatedUser);
        Assert.Equal(7, updatedUser.ID);
        Assert.Equal(updatedUser.Username, updatedUser.UpdateUser);
        Assert.True(context.Users.Any(x => x.Salt == "test"));
    }

    [Fact]
    public async Task Update_UserNotFound_ReturnsNull()
    {
        using PizzaContext context = CreateContext();
        await DbTestInitializer(context);
        SecurityRepository repository = new(context);

        User? updatedUser = await repository.Update(999, x => x.Salt = "test");

        Assert.Null(updatedUser);
        Assert.False(context.Users.Any(x => x.Salt == "test"));
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
