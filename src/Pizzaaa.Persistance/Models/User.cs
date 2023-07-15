using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Models;

internal class User : AuditedEntity
{

    [MaxLength(200)]
    public string Username { get; set; } = default!;

    [MaxLength(200)]
    public string Password { get; set; } = default!;

    [MaxLength(200)]
    public string Salt { get; set; } = default!;

    public DateTime? LastAccess { get; set; }
}