using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Models;

internal class UserPizzaPreference : AuditedEntity
{

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = new();
    public int UserId { get; set; }
    public User User { get; set; } = new();

    public bool Blacklisted { get; set; }
    public bool Favourite { get; set; }
}