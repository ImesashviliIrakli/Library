namespace Library.UI.Models.BookDtos;

public class UpdateBookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public required double Rating { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool IsTaken { get; set; }
}
