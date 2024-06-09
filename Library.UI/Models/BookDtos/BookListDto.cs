using Library.UI.Models.AuthorDtos;

namespace Library.UI.Models.BookDtos;

public class BookListDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string Image { get; set; }
    public bool IsTaken { get; set; }
    public required List<AuthorDto> Authors { get; set; }
}
