using Microsoft.AspNetCore.Http;
using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaaa.BLL.Security;

public class SecurityService
{
    public const int KEYSIZE = 64;
    private const int ITERATIONS = 350000;
    private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISecurityPort _securityPort;
    private User? _user;

    public SecurityService(IHttpContextAccessor httpContextAccessor, ISecurityPort securityPort)
    {
        this._httpContextAccessor = httpContextAccessor;
        this._securityPort = securityPort;
    }


    public async Task LoginOrRegister(string username, string password)
    {
        User? user = await _securityPort.FindByUsername(username);
        if (user == null)
        {
            await InsertNewUser(username, password);
        }
        else
        {
            await CheckPassword(username, password, user);
        }
        await UpdateLoggedUser(username);
    }

    private async Task InsertNewUser(string username, string password)
    {
        string passwordHashed = HashPassword(password, out string salt);
        User user = new()
        {
            Username = username,
            Password = passwordHashed,
            Salt = salt
        };
        User insertedUser = await _securityPort.Insert(user);
        await _securityPort.UpdateLastAccess(insertedUser.ID);
    }

    private async Task CheckPassword(string username, string password, User user)
    {
        if (VerifyPassword(password, user.Password, user.Salt))
        {
            //TODO cool
            await _securityPort.UpdateLastAccess(user.ID);
        }
        else
        {
            throw new NotImplementedException("TODO wrong password");//TODO
        }
    }

    public string HashPassword(string password, out string hexSalt)
    {
        byte[] saltAsBytes = RandomNumberGenerator.GetBytes(KEYSIZE);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltAsBytes,
            ITERATIONS,
            _hashAlgorithm,
            KEYSIZE);

        hexSalt = Convert.ToHexString(saltAsBytes);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, string hexSalt)
    {
        byte[] saltAsBytes = HexUtils.HexToByteArray(hexSalt);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltAsBytes, ITERATIONS, _hashAlgorithm, KEYSIZE);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }

    public async Task UpdateLoggedUser(string? username = null)
    {
        if (username == null)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            username = claimsPrincipal?.Identity?.Name ?? "Unknown";
        }
        User? user = await _securityPort.FindByUsername(username);
        if (user != null)
        {
            _user = user;
        }
        else
        {
            _user = new User()
            {
                ID = 0,
                Username = username
            };
        }
    }

    public User GetLoggedUser()
    {
        return _user!;
    }

    public string GetLoggedUsername()
    {
        return _user?.Username ?? "Unknown";
    }

    public int GetLoggedId()
    {
        return _user?.ID ?? 0;
    }

    public bool IsUserLogged()
    {
        var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
        return claimsPrincipal?.Identity?.Name != null;
    }
}