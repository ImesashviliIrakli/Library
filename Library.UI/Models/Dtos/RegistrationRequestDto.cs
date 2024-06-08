using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Dtos;

public class RegistrationRequestDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string UserName { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
}
