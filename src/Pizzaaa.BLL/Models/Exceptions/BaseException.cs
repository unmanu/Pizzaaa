using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Models.Exceptions;

[Serializable]
public class BaseException : Exception
{
    public List<ErrorDetail> Errors { get; protected set; }


    public BaseException()
    {
        Errors = new();
    }

    public BaseException(string error)
        : base(error)
    {
        Errors = new() { new("", error)};
    }

    public BaseException(ErrorDetail error)
        : base(error.Description)
    {
        Errors = new() { error };
    }

    public BaseException(List<ErrorDetail> errors)
        : base()
    {
        Errors = errors;
    }

    public BaseException(string message, Exception inner)
        : base(message, inner)
    {
        Errors = new() { new("", message) };
    }

    public BaseException(ErrorDetail error, Exception inner)
        : base(error.Description, inner)
    {
        Errors = new() { error };
    }

    protected BaseException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
        Errors = new();
    }
}
