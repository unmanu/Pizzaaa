namespace Pizzaaa.IntegrationTest.Mappers;

public class PizzaMapperTest : BaseMappingTest
{

	[Fact]
	public void DtoToEntity_EmptyInput_ReturnEmptyObject()
	{
		BLL.Models.Pizza input = new();
		Persistance.Models.Pizza expectedResult = new();

		Persistance.Models.Pizza result = _mapper.Map<Persistance.Models.Pizza>(input);

		Assert.NotNull(result);
		Assert.Equivalent(expectedResult, result);
	}

	[Fact]
	public void DtoToEntity_CompleteInput_ReturnMappedObject()
	{
		BLL.Models.Ingredient ingredientInput = new()
		{
			ID = 15,
			Name = "tomato",
			Type = "fruit"
		};
		BLL.Models.Pizza pizzaInput = new()
		{
			ID = 18,
			Name = "pepperoni",
			Price = 7
		};
		pizzaInput.Ingredients.Add(ingredientInput);

		Persistance.Models.Pizza pizzaResult = _mapper.Map<Persistance.Models.Pizza>(pizzaInput);

		Assert.NotNull(pizzaResult);
		Assert.Equal(18, pizzaResult.ID);
		Assert.Equal("pepperoni", pizzaResult.Name);
		Assert.Equal(7, pizzaResult.Price);
		Assert.Null(pizzaResult.InsertUser);
		Assert.NotNull(pizzaResult.Ingredients);
		Assert.Single(pizzaResult.Ingredients);
		Persistance.Models.Ingredient ingredientResult = pizzaResult.Ingredients.First();
		Assert.Equal(15, ingredientResult.ID);
		Assert.Equal("tomato", ingredientResult.Name);
		Assert.Equal("fruit", ingredientResult.Type);
	}


	[Fact]
	public void EntityToDto_EmptyInput_ReturnEmptyObject()
	{
		Persistance.Models.Pizza input = new();
		BLL.Models.Pizza expectedResult = new();

		BLL.Models.Pizza result = _mapper.Map<BLL.Models.Pizza>(input);

		Assert.NotNull(result);
		Assert.Equivalent(expectedResult, result);
	}


	[Fact]
	public void EntityToDto_CompleteInput_ReturnMappedObject()
	{
		Persistance.Models.Ingredient ingredientInput = new()
		{
			ID = 33,
			Name = "tomato",
			Type = "fruit"
		};
		Persistance.Models.Store storeInput = new()
		{
			ID = 31,
			Name = "pizza hut"
		};
		Persistance.Models.UserPizzaPreference preferencesInput = new()
		{
			ID = 29,
			Favourite = true
		};
		Persistance.Models.Pizza pizzaInput = new()
		{
			ID = 27,
			Name = "pepperoni",
			Price = 46,
			InsertDate = new DateTime(2023, 6, 16, 10, 26, 34),
			InsertUser = "jack",
			UpdateDate = new DateTime(2023, 7, 18, 10, 26, 34),
			UpdateUser = "tom",
			Store = storeInput,
			StoreId = storeInput.ID
		};
		pizzaInput.Ingredients.Add(ingredientInput);
		pizzaInput.UserPizzaPreference.Add(preferencesInput);

		BLL.Models.Pizza pizzaResult = _mapper.Map<BLL.Models.Pizza>(pizzaInput);

		Assert.NotNull(pizzaResult);
		Assert.Equal(27, pizzaResult.ID);
		Assert.Equal("pepperoni", pizzaResult.Name);
		Assert.Equal(46, pizzaResult.Price);
		BLL.Models.Ingredient ingredientResult = pizzaResult.Ingredients.First();
		Assert.Equal(33, ingredientResult.ID);
		Assert.Equal("tomato", ingredientResult.Name);
		Assert.Equal("fruit", ingredientResult.Type);
	}
}
