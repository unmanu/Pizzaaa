﻿using Pizzaaa.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Ports;

public interface IUserPizzaPreferencePort
{
    Task<List<UserPizzaPreference>> FindAllByUser();
    Task<UserPizzaPreference> UpdateUserPreference(UserPizzaPreference userPizzaPreference);
}