namespace Library.UI.Models.AuthorDtos;
public class CreateAuthorDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int YearOfBirth { get; set; }
}
