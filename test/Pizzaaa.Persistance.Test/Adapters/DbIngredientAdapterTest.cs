using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbIngredientAdapterTest
{
    private readonly Mock<IIngredientRepository> _mockIngredientRepository;
    private readonly Mock<IMapper> _mockMapper;

    private readonly DbIngredientAdapter _adapter;

    public DbIngredientAdapterTest()
    {
        _mockIngredientRepository = new Mock<IIngredientRepository>();
        _mockMapper = new Mock<IMapper>();

        _adapter = new DbIngredientAdapter(_mockIngredientRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task FindAll_EverythingGoesWell_ReturnsIngredientList()
    {
        List<Ingredient> ingredientsFromRepo = new() { new() { Name = "potato" } };
        List<BLL.Models.Ingredient> mappedIngredients = new() { new() { Name = "tomato" } };

        _mockIngredientRepository.Setup(mock => mock.FindAll()).ReturnsAsync(ingredientsFromRepo);
        _mockMapper.Setup(mock => mock.Map<List<BLL.Models.Ingredient>>(ingredientsFromRepo)).Returns(mappedIngredients);

        List<BLL.Models.Ingredient>? result = await _adapter.FindAll();

        Assert.Equal(mappedIngredients, result);
    }
}
