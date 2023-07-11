using AutoMapper;
using Pizzaaa.Persistance.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.IntegrationTest.Mappers;

public class MappingTest
{
	private readonly IMapper _mapper;

	public MappingTest() => _mapper = new MapperConfiguration(cfg =>
	{
		cfg.AddProfile<PersistanceMapperProfile>();
	}).CreateMapper();

	[Fact]
	public void Imapper_WhenConfigurazioniSonoValide_ShouldNonLanciareEccezioni() => _mapper.ConfigurationProvider.AssertConfigurationIsValid();


}