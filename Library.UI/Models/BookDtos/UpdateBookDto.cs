using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.BookDtos;

public class UpdateBookDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }
    public required string Image { get; set; }
    [Required]
    public required double Rating { get; set; }
    [Required]
    public DateTime PublicationDate { get; set; }
    [Required]
    public bool IsTaken { get; set; }
}
