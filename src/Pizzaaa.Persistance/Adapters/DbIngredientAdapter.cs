using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Adapters;

internal class DbIngredientAdapter : IIngredientPort
{
	private readonly IngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

	public DbIngredientAdapter(IngredientRepository ingredientRepository, IMapper mapper)
	{
		this._ingredientRepository = ingredientRepository;
        this._mapper = mapper;

	}

	public async Task<List<BLL.Models.Ingredient>> FindAll()
	{
		List<Ingredient> ingredients = await _ingredientRepository.FindAll();

		return _mapper.Map<List<BLL.Models.Ingredient>>(ingredients);
	}

}