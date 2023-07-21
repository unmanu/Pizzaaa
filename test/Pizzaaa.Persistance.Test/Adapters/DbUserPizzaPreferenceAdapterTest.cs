using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;
using Pizzaaa.Persistance.Test.Mothers;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbUserPizzaPreferenceAdapterTest
{
	private readonly Mock<IUserPizzaPreferenceRepository> _mockUserPizzaPreferenceRepository;
	private readonly Mock<IPizzaRepository> _mockPizzaRepository;
	private readonly Mock<IUserRepository> _mockUserRepository;
	private readonly Mock<IMapper> _mockMapper;

	private readonly DbUserPizzaPreferenceAdapter _adapter;

	public DbUserPizzaPreferenceAdapterTest()
	{
		_mockUserPizzaPreferenceRepository = new Mock<IUserPizzaPreferenceRepository>();
		_mockPizzaRepository = new Mock<IPizzaRepository>();
		_mockUserRepository = new Mock<IUserRepository>();
		_mockMapper = new Mock<IMapper>();

		_adapter = new DbUserPizzaPreferenceAdapter(_mockUserPizzaPreferenceRepository.Object, _mockPizzaRepository.Object, _mockUserRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task FindAllByUser_EverythingGoesWell_ReturnsPreferencesList()
	{
		List<UserPizzaPreference> storeFromRepo = new() { new() { ID = 5 } };
		List<BLL.Models.UserPizzaPreference> mappedstore = new() { new() { ID = 7 } };

		_mockUserPizzaPreferenceRepository.Setup(mock => mock.FindAllByUser()).ReturnsAsync(storeFromRepo);
		_mockMapper.Setup(mock => mock.Map<List<BLL.Models.UserPizzaPreference>>(storeFromRepo)).Returns(mappedstore);

		List<BLL.Models.UserPizzaPreference> result = await _adapter.FindAllByUser();

		Assert.Equal(mappedstore, result);
	}

	[Fact]
	public async Task UpdateUserPreference_AlreadyInserted_UpdateValues()
	{
		int preferenceId = 508;
		BLL.Models.UserPizzaPreference preferenceToUpdate = UserPizzaPreferenceMother.ABllUserPizzaPreference();
		preferenceToUpdate.ID = preferenceId;

		await _adapter.UpdateUserPreference(preferenceToUpdate);

		_mockUserPizzaPreferenceRepository.Verify(v => v.Update(preferenceId, It.IsAny<Action<UserPizzaPreference>>()));
		_mockUserPizzaPreferenceRepository.Verify(mock => mock.Insert(It.IsAny<UserPizzaPreference>()), Times.Never);
	}

	[Fact]
	public async Task UpdateUserPreference_NotYetInserted_InsertPreferences()
	{
		BLL.Models.UserPizzaPreference preferenceToUpdate = UserPizzaPreferenceMother.ABllUserPizzaPreference();
		preferenceToUpdate.Favourite = true;
		UserPizzaPreference mappedEntityPreference = new()
		{
			PizzaId = preferenceToUpdate.PizzaId,
			UserId = preferenceToUpdate.UserId,
			Blacklisted = true
		};
		BLL.Models.UserPizzaPreference mappedBllPreference = UserPizzaPreferenceMother.ABllUserPizzaPreference();
		mappedBllPreference.Favourite = true;
		mappedBllPreference.Blacklisted = true;
		Pizza pizzaFromRepo = new() { Name = "pepperoni" };
		User userFromRepo = new() { Username = "jack" };
		UserPizzaPreference? preferenceAfterInsert = null;

		_mockMapper.Setup(mock => mock.Map<UserPizzaPreference>(preferenceToUpdate)).Returns(mappedEntityPreference);
		_mockMapper.Setup(mock => mock.Map<BLL.Models.UserPizzaPreference>(It.IsAny<UserPizzaPreference>()))
			.Callback<object>(input => preferenceAfterInsert = (UserPizzaPreference)input)
			.Returns(mappedBllPreference);
		_mockPizzaRepository.Setup(mock => mock.FindById(preferenceToUpdate.PizzaId)).ReturnsAsync(pizzaFromRepo);
		_mockUserRepository.Setup(mock => mock.FindById(preferenceToUpdate.UserId)).ReturnsAsync(userFromRepo);

		BLL.Models.UserPizzaPreference result = await _adapter.UpdateUserPreference(preferenceToUpdate);

		Assert.NotNull(result);
		Assert.NotNull(preferenceAfterInsert);
		Assert.Equal(pizzaFromRepo, preferenceAfterInsert.Pizza);
		Assert.Equal(userFromRepo, preferenceAfterInsert.User);

		_mockUserPizzaPreferenceRepository.Verify(v => v.Update(It.IsAny<int>(), It.IsAny<Action<UserPizzaPreference>>()), Times.Never);
	}
}
