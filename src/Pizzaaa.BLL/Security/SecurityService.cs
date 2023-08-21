using Microsoft.AspNetCore.Http;
using Pizzaaa.BLL.Models;
using Pizzaaa.BLL.Ports;
using Pizzaaa.BLL.Utils;
using System.Security.Cryptography;
using System.Text;

namespace Pizzaaa.BLL.Security;

public class SecurityService : ISecurityService
{
	public const int KEYSIZE = 64;
	private const int ITERATIONS = 350000;
	private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ISecurityPort _securityPort;

	public SecurityService(IHttpContextAccessor httpContextAccessor, ISecurityPort securityPort)
	{
		this._httpContextAccessor = httpContextAccessor;
		this._securityPort = securityPort;
	}

	public string? GetLoggedUsername()
	{
		var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
		return claimsPrincipal?.Identity?.Name;
	}

	public bool IsUserLogged()
	{
		return GetLoggedUsername() != null;
	}

	public async Task<User> GetLoggedUser()
	{
		string? username = GetLoggedUsername();
		if (username == null)
		{
			return new User()
			{
				ID = 0,
				Username = "Unknown"
			};
		}
		User? user = await _securityPort.FindByUsername(username);
		if (user == null)
		{
			return new User()
			{
				ID = 0,
				Username = "Unknown"
			};
		}
		return user;
	}

	public async Task<int> GetLoggedId()
	{
		User user = await GetLoggedUser();
		return user.ID;
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
			await CheckPassword(password, user);
		}
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

	private async Task CheckPassword(string password, User user)
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
}