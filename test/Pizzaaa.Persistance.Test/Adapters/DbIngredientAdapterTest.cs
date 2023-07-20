using AutoMapper;
using Moq;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Repositories;
using Pizzaaa.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        List<Models.Ingredient> ingredientsEntity = new() { new() {Name= "potato" } };
        List<BLL.Models.Ingredient> ingredientsBll = new() { new() { Name = "tomato" } };

        _mockIngredientRepository.Setup(mock => mock.FindAll()).ReturnsAsync(ingredientsEntity);
        _mockMapper.Setup(mock => mock.Map<List<BLL.Models.Ingredient>>(ingredientsEntity)).Returns(ingredientsBll);

        List<BLL.Models.Ingredient>? result = await _adapter.FindAll();

        Assert.Equal(ingredientsBll, result);
    }
}
