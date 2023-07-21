using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using Pizzaaa.BLL.Services.Interfaces;

namespace Pizzaaa.BLL.Services;

public class UserPizzaPreferenceService : IUserPizzaPreferenceService
{
	private readonly IUserPizzaPreferencePort _userPizzaPreferencePort;
	private readonly ISecurityService _securityService;

	public UserPizzaPreferenceService(ISecurityService securityService, IUserPizzaPreferencePort userPizzaPreferencePort)
	{
		this._securityService = securityService;
		this._userPizzaPreferencePort = userPizzaPreferencePort;
	}

	public async Task<List<UserPizzaPreference>> FindAllByUser()
	{
		if (!_securityService.IsUserLogged())
		{
			return new();
		}
		return await _userPizzaPreferencePort.FindAllByUser();
	}

	public async Task<UserPizzaPreference> UpdateUserPreference(UserPizzaPreference userPizzaPreference)
	{
		if (!_securityService.IsUserLogged())
		{
			return userPizzaPreference;
		}
		userPizzaPreference.UserId = _securityService.GetLoggedUser().ID;
		return await _userPizzaPreferencePort.UpdateUserPreference(userPizzaPreference);
	}
}