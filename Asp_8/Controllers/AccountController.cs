using BookStore.WebUI.Entities;
using BookStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers;

public class AccountController : Controller
{
	private UserManager<CustomIdentityUser> _userManager;
	private RoleManager<CustomIdentityRole> _roleManager;
	private SignInManager<CustomIdentityUser> _signInManager;

	public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
	}
	public IActionResult LogIn()
	{
		return View();
	}

	[HttpPost]
	public ActionResult LogIn(LoginViewModel model, string? returnUrl)
	{
		var user = _userManager.FindByNameAsync(model.UserName).Result;

		if (ModelState.IsValid && user != null)
		{
			var result = _signInManager.PasswordSignInAsync(user, model.Password, true, false).Result;

			if (result.Succeeded && _userManager.IsInRoleAsync(user, "Admin").Result)
				return RedirectToAction("Index", "Home");
			
			if(string.IsNullOrEmpty(returnUrl))
				return RedirectToAction(returnUrl);

			ModelState.AddModelError("", "Invalid Login");
		}
		return View(model);
	}

	public ActionResult Register() => View();

	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			CustomIdentityUser user = new CustomIdentityUser
			{
				UserName = model.Username,
				Email = model.Email
			};

			IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

			if (result.Succeeded)
			{
				if (!_roleManager.RoleExistsAsync("Admin").Result)
				{
					CustomIdentityRole role = new CustomIdentityRole
					{
						Name = "Admin"
					};

					IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
					if (!roleResult.Succeeded)
					{
						ModelState.AddModelError("", "We can not add the role");
						return View(model);
					}
				}

				_userManager.AddToRoleAsync(user, "Admin").Wait();
				return RedirectToAction("Login", "Account", new { area = "" });
			}
		}
		return View(model);
	}
}
