namespace Pizzaaa.BLL.Models.Exceptions;

public class ErrorDetail
{
	public string Code { get; private set; }
	public string Description { get; private set; }

	public ErrorDetail(string codice, string messaggio)
	{
		Code = codice;
		Description = messaggio;
	}
}