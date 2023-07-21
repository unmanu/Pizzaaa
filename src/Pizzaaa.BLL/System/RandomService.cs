using Pizzaaa.BLL.System.Interfaces;

namespace Pizzaaa.BLL.System;

public class RandomService : IRandomService
{
	private readonly Random random = new();

	public int RandomInt(int? maxValue = null)
	{
		return maxValue != null ? random.Next(maxValue ?? 0) : random.Next();
	}
}