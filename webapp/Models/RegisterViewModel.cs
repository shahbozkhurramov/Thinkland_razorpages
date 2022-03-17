using System.ComponentModel.DataAnnotations;

namespace webapp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}