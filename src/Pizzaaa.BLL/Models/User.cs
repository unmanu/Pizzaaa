using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Models;

public class User
{
	public int ID { get; set; }

	public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Salt { get; set; } = default!;
}