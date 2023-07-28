namespace Pizzaaa.IntegrationTest.Mappers;

public class MappingTest : BaseMappingTest
{

	[Fact]
	public void Imapper_WhenConfigurazioniSonoValide_ShouldNonLanciareEccezioni() => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

}