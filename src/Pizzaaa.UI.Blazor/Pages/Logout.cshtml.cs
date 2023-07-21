using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pizzaaa.Pages;

public class LogoutModel : PageModel
{
	public async Task<IActionResult> OnGetAsync()
	{
		// Clear the existing external cookie
		await HttpContext
			.SignOutAsync(
			CookieAuthenticationDefaults.AuthenticationScheme);
		return LocalRedirect(Url.Content("~/"));
	}
}
