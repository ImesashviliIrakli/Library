using Application.Models.Author;

namespace Application.Models.Book;

public class CreateBookDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public double Rating { get; set; }
    public DateTime PublicationDate { get; set; }
    public required List<AuthorDto> Authors { get; set; }
}
