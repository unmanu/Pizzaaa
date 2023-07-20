using AutoMapper;
using Pizzaaa.Persistance.Configuration;

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