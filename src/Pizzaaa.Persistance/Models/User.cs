using System.ComponentModel.DataAnnotations;

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

    public List<UserPizzaPreference> UserPizzaPreference { get; } = new();
}