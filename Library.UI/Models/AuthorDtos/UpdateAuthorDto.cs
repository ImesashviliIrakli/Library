using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.AuthorDtos;
public class UpdateAuthorDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public int YearOfBirth { get; set; }
}
