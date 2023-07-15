using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Services;

public class UserService
{

    private readonly SecurityService _securityService;
    private readonly IUserPort _userPort;

    public UserService(SecurityService securityService, IUserPort userPort)
    {
        this._securityService = securityService;
        this._userPort = userPort;
    }

    public async Task LoginOrRegister(string username, string password)
    {
        User? user = await _userPort.FindByUsername(username);
        if (user == null)
        {
            await InsertNewUser(username, password);
        }
        else
        {
            await CheckPassword(username, password, user);
        }
    }

    private async Task InsertNewUser(string username, string password)
    {
        string passwordHashed = _securityService.HashPassword(password, out string salt);
        User user = new()
        {
            Username = username,
            Password = passwordHashed,
            Salt = salt
        };
        User insertedUser = await _userPort.Insert(user);
        await _userPort.UpdateLastAccess(insertedUser.ID);
    }

    private async Task CheckPassword(string username, string password, User user)
    {
        if (_securityService.VerifyPassword(password, user.Password, user.Salt))
        {
            //TODO cool
            await _userPort.UpdateLastAccess(user.ID);
        }
        else
        {
            throw new NotImplementedException("TODO wrong password");//TODO
        }
    }
}