using AutoMapper;
using Pizzaaa.BLL.Ports;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories;

namespace Pizzaaa.Persistance.Adapters;

internal class DbUserPizzaPreferenceAdapter : IUserPizzaPreferencePort
{
    private readonly UserPizzaPreferenceRepository _userPizzaPreferenceRepository;
    private readonly PizzaRepository _pizzaRepository;
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public DbUserPizzaPreferenceAdapter(
        UserPizzaPreferenceRepository userPizzaPreferenceRepository,
        PizzaRepository pizzaRepository,
        UserRepository userRepository,
        IMapper mapper)
    {
        this._userPizzaPreferenceRepository = userPizzaPreferenceRepository;
        this._pizzaRepository = pizzaRepository;
        this._userRepository = userRepository;
        _mapper = mapper;

    }

    public async Task<List<BLL.Models.UserPizzaPreference>> FindAllByUser()
    {
        List<UserPizzaPreference> preferences = await _userPizzaPreferenceRepository.FindAllByUser();
        return _mapper.Map<List<BLL.Models.UserPizzaPreference>>(preferences);
    }

    public async Task<BLL.Models.UserPizzaPreference> UpdateUserPreference(BLL.Models.UserPizzaPreference userPizzaPreference)
    {
        if (userPizzaPreference.ID != 0)
        {
            await _userPizzaPreferenceRepository.Update(userPizzaPreference.ID, x =>
            {
                x.Favourite = userPizzaPreference.Favourite;
                x.Blacklisted = userPizzaPreference.Blacklisted;
            });
            return userPizzaPreference;
        }
        else
        {
            UserPizzaPreference entity = _mapper.Map<UserPizzaPreference>(userPizzaPreference);
            Pizza? pizza = await _pizzaRepository.FindById(entity.PizzaId);
            User? user = await _userRepository.FindById(entity.UserId);
            entity.Pizza = pizza!;
            entity.User = user!;
            await _userPizzaPreferenceRepository.Insert(entity);
            return _mapper.Map<BLL.Models.UserPizzaPreference>(entity);
        }
    }
}