using System.ComponentModel.DataAnnotations;

namespace BookStore.WebUI.Models;

public class RegisterViewModel
{
	public string Username { get; set; } = null!;
	[DataType(DataType.Password)]
	public string Password { get; set; } = null!;
	public string Email { get; set; } = null!;
}
