using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Adapters;

internal class DbStoreAdapter : IStorePort
{
    private readonly StoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public DbStoreAdapter(StoreRepository storeRepository, IMapper mapper)
    {
        this._storeRepository = storeRepository;
        _mapper = mapper;

    }

    public async Task<List<BLL.Models.Store>> FindAll()
    {
        List<Store> stores = await _storeRepository.FindAll();

        return _mapper.Map<List<BLL.Models.Store>>(stores);
    }
}