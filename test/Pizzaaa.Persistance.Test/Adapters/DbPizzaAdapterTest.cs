using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbPizzaAdapterTest
{
	private readonly Mock<IPizzaRepository> _mockPizzaRepository;
	private readonly Mock<IMapper> _mockMapper;

	private readonly DbPizzaAdapter _adapter;

	public DbPizzaAdapterTest()
	{
		_mockPizzaRepository = new Mock<IPizzaRepository>();
		_mockMapper = new Mock<IMapper>();

		_adapter = new DbPizzaAdapter(_mockPizzaRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task FindAllByStore_EverythingGoesWell_ReturnsPizzaList()
	{
		List<Pizza> pizzaFromRepo = new() { new() { Name = "banana pizza" } };
		List<BLL.Models.Pizza> mappedPizza = new() { new() { Name = "pineapple pizza" } };
		int storeId = 306;

		_mockPizzaRepository.Setup(mock => mock.FindAllByStore(storeId)).ReturnsAsync(pizzaFromRepo);
		_mockMapper.Setup(mock => mock.Map<List<BLL.Models.Pizza>>(pizzaFromRepo)).Returns(mappedPizza);

		List<BLL.Models.Pizza> result = await _adapter.FindAllByStore(storeId);

		Assert.Equal(mappedPizza, result);
	}
}
