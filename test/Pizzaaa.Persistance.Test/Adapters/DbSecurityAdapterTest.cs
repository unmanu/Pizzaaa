using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbSecurityAdapterTest
{
	private readonly Mock<ISecurityRepository> _mockSecurityRepository;
	private readonly Mock<IMapper> _mockMapper;

	private readonly DbSecurityAdapter _adapter;

	public DbSecurityAdapterTest()
	{
		_mockSecurityRepository = new Mock<ISecurityRepository>();
		_mockMapper = new Mock<IMapper>();

		_adapter = new DbSecurityAdapter(_mockSecurityRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task FindByUsername_UserFound_ReturnsUser()
	{
		User userFromRepo = new() { Username = "jack1" };
		BLL.Models.User mappedUser = new() { Username = "jack2" };
		string username = "jack";

		_mockSecurityRepository.Setup(mock => mock.FindByUsername(username)).ReturnsAsync(userFromRepo);
		_mockMapper.Setup(mock => mock.Map<BLL.Models.User>(userFromRepo)).Returns(mappedUser);

		BLL.Models.User? result = await _adapter.FindByUsername(username);

		Assert.Equal(mappedUser, result);
	}

	[Fact]
	public async Task FindByUsername_UserNotFound_ReturnsNull()
	{
		BLL.Models.User? result = await _adapter.FindByUsername(username: "jack");

		Assert.Null(result);
	}


	[Fact]
	public async Task Insert_EverythingGoesWell_RetursInsertedUser()
	{
		BLL.Models.User userToInsert = new() { Username = "jack1" };
		User mappedEntityUser = new() { Username = "jack2" };
		BLL.Models.User mappedBllUser = new() { Username = "jack3" };

		_mockMapper.Setup(mock => mock.Map<User>(userToInsert)).Returns(mappedEntityUser);
		_mockMapper.Setup(mock => mock.Map<BLL.Models.User>(mappedEntityUser)).Returns(mappedBllUser);

		BLL.Models.User result = await _adapter.Insert(userToInsert);

		Assert.NotNull(result);
		Assert.Equal(mappedBllUser, result);
	}


	[Fact]
	public async Task UpdateLastAccess_EverythingGoesWell_CallUpdateMethod()
	{
		int userId = 407;

		await _adapter.UpdateLastAccess(userId);

		_mockSecurityRepository.Verify(v => v.Update(userId, It.IsAny<Action<User>>()));
	}
}
