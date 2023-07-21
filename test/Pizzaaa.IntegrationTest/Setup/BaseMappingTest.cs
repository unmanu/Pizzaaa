using AutoMapper;
using Pizzaaa.Persistance.Configuration;

namespace Pizzaaa.IntegrationTest.Mappers;

public abstract class BaseMappingTest
{
	protected readonly IMapper _mapper;

	public BaseMappingTest() => _mapper = new MapperConfiguration(cfg =>
	{
		cfg.AddProfile<PersistanceMapperProfile>();
	}).CreateMapper();

}