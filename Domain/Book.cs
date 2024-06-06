namespace Domain;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public double Rating { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool IsTaken { get; set; }
    public List<Author> Authors { get; set; } = new List<Author>();
}
