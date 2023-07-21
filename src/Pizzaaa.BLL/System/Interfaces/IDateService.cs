namespace Pizzaaa.BLL.System.Interfaces;

public interface IDateService
{
	DateOnly GetTodayDateOnly();
	DateTime GetNow();
}
