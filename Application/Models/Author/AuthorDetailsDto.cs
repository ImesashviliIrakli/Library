using Application.Models.Book;

namespace Application.Models.Author;

public class AuthorDetailsDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int YearOfBirth { get; set; }
    public required List<BookListDto> Books { get; set; }
}