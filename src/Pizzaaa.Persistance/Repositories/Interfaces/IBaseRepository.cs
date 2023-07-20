using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pizzaaa.BLL.Security;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Repositories;

internal interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> FindById(int id);
    Task<List<T>> FindAll();

    Task Insert(T entity);

    Task Delete(T entity);

    Task<T?> Update(T entity, Action<T> updateFields);

    Task<T?> Update(int id, Action<T> updateFields);

}
