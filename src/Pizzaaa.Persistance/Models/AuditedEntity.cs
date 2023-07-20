using System.ComponentModel.DataAnnotations;

namespace Pizzaaa.Persistance.Models;

internal abstract class AuditedEntity : BaseEntity
{

    [MaxLength(200)]
    public string InsertUser { get; set; } = default!;
    public DateTime InsertDate { get; set; }

    [MaxLength(200)]
    public string? UpdateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
}