using Pizzaaa.BLL.Models;
namespace Pizzaaa.BLL.Security;

public interface ISecurityService
{
	Task LoginOrRegister(string username, string password);

	string HashPassword(string password, out string hexSalt);

	bool VerifyPassword(string password, string hash, string hexSalt);

	Task UpdateLoggedUser(string? username = null);

	User GetLoggedUser();

	string GetLoggedUsername();

	int GetLoggedId();

	bool IsUserLogged();
}
