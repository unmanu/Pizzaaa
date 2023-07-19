using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class StoreService
{
    private readonly IStorePort _storePort;

    public StoreService(IStorePort storePort)
    {
        this._storePort = storePort;
    }

    public async Task<List<Store>> FindAll()
    {
        return await _storePort.FindAll();
    }
}