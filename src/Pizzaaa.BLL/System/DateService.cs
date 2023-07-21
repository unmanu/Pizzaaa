using Pizzaaa.BLL.System.Interfaces;

namespace Pizzaaa.BLL.System;

public class DateService : IDateService
{

	public DateOnly GetTodayDateOnly()
	{
		return DateOnly.FromDateTime(DateTime.Now);
	}

	public DateTime GetNow()
	{
		return DateTime.Now;
	}
}
