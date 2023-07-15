using Microsoft.AspNetCore.Http;
using Pizzaaa.BLL.Models;
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
    private readonly User _user;

    public SecurityService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
        var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
        string username = claimsPrincipal?.Identity?.Name ?? "Unknown";
        _user = new User()
        {
            Username = username,
            ID = 1 // TODO fixme
        };
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

    public User GetLoggedUser()
    {
        return _user;
    }
}