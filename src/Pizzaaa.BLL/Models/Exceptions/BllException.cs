using System.Runtime.Serialization;

namespace Pizzaaa.BLL.Models.Exceptions;

[Serializable]
public class BllException : BaseException
{

	public BllException()
	{
		Errors = new();
	}

	public BllException(string error)
		: base(error)
	{
	}

	public BllException(ErrorDetail error)
		: base(error)
	{
	}

	public BllException(List<ErrorDetail> errors)
		: base(errors)
	{
	}

	public BllException(string message, Exception inner)
		: base(message, inner)
	{
	}

	public BllException(ErrorDetail error, Exception inner)
		: base(error, inner)
	{
	}

	protected BllException(SerializationInfo info, StreamingContext context)
	: base(info, context)
	{
	}
}
