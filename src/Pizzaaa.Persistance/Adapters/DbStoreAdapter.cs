using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;

namespace Pizzaaa.Persistance.Adapters;

internal class DbStoreAdapter : IStorePort
{
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public DbStoreAdapter(IStoreRepository storeRepository, IMapper mapper)
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