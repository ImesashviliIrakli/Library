using Application.Models.Author;

namespace Application.Models.Book;

public class BookDetailsDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public required double Rating { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool IsTaken { get; set; }
    public required List<AuthorDto> Authors { get; set; }
}
