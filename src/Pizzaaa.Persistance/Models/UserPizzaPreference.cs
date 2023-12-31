﻿using Microsoft.EntityFrameworkCore;

namespace Pizzaaa.Persistance.Models;

[Index(nameof(PizzaId), nameof(UserId), IsUnique = true)]
internal class UserPizzaPreference : AuditedEntity
{
	public int PizzaId { get; set; }
	public Pizza Pizza { get; set; } = new();
	public int UserId { get; set; }
	public User User { get; set; } = new();

	public bool Blacklisted { get; set; }
	public bool Favourite { get; set; }
}