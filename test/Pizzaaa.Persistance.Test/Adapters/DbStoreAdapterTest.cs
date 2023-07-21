using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbStoreAdapterTest
{
	private readonly Mock<IStoreRepository> _mockStoreRepository;
	private readonly Mock<IMapper> _mockMapper;

	private readonly DbStoreAdapter _adapter;

	public DbStoreAdapterTest()
	{
		_mockStoreRepository = new Mock<IStoreRepository>();
		_mockMapper = new Mock<IMapper>();

		_adapter = new DbStoreAdapter(_mockStoreRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task FindAll_EverythingGoesWell_ReturnsStoreList()
	{
		List<Store> storeFromRepo = new() { new() { Name = "domino" } };
		List<BLL.Models.Store> mappedstore = new() { new() { Name = "pizza hut" } };

		_mockStoreRepository.Setup(mock => mock.FindAll()).ReturnsAsync(storeFromRepo);
		_mockMapper.Setup(mock => mock.Map<List<BLL.Models.Store>>(storeFromRepo)).Returns(mappedstore);

		List<BLL.Models.Store> result = await _adapter.FindAll();

		Assert.Equal(mappedstore, result);
	}
}
