using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.Persistance.Adapters;

internal class DbUserAdapter : IUserPort
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public DbUserAdapter(UserRepository userRepository, IMapper mapper)
    {
        this._userRepository = userRepository;
        _mapper = mapper;

    }

    public async Task<BLL.Models.User?> FindByUsername(string username)
    {
        User? user = await _userRepository.FindByUsername(username);
        return _mapper.Map<BLL.Models.User>(user);
    }

    public async Task<BLL.Models.User> Insert(BLL.Models.User user)
    {
        User userEntity = _mapper.Map<User>(user);
        await _userRepository.Insert(userEntity);
        return _mapper.Map<BLL.Models.User>(userEntity);
    }

}