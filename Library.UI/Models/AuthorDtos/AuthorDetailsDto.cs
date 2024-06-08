using Library.UI.Models.BookDtos;

namespace Library.UI.Models.AuthorDtos;
public class AuthorDetailsDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int YearOfBirth { get; set; }
    public required List<BookListDto> Books { get; set; }
}