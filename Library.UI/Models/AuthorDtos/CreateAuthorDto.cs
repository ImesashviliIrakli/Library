using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.AuthorDtos;
public class CreateAuthorDto
{
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public int YearOfBirth { get; set; }
}
