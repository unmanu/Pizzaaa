namespace Pizzaaa.UI.Blazor.Test.TestUtils;

public class FallbackServiceProvider : IServiceProvider
{
	public object GetService(Type serviceType)
	{
		return new DummyService();
	}
}

public class DummyService { }