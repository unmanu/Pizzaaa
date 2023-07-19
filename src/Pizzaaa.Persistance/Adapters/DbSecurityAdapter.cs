using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Data;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.Persistance.Adapters;

internal class DbSecurityAdapter : ISecurityPort
{
    private readonly SecurityRepository _securityRepository;
    private readonly IMapper _mapper;

    public DbSecurityAdapter(SecurityRepository securityRepository, IMapper mapper)
    {
        this._securityRepository = securityRepository;
        _mapper = mapper;

    }

    public async Task<BLL.Models.User?> FindByUsername(string username)
    {
        User? user = await _securityRepository.FindByUsername(username);
        return _mapper.Map<BLL.Models.User>(user);
    }

    public async Task<BLL.Models.User> Insert(BLL.Models.User user)
    {
        User userEntity = _mapper.Map<User>(user);
        await _securityRepository.Insert(userEntity);
        return _mapper.Map<BLL.Models.User>(userEntity);
    }

    public async Task UpdateLastAccess(int id)
    {
        await _securityRepository.Update(id, x => x.LastAccess = DateTime.Now);
    }
}