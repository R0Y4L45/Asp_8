using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
	public string Index()
	{
		return "Controller here";
	}
}
