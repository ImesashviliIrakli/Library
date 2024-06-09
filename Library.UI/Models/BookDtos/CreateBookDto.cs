using Library.UI.Models.AuthorDtos;
using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.BookDtos;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public string Image { get; set; }
    [Required]
    public double Rating { get; set; }
    [Required]
    public DateTime PublicationDate { get; set; }
    public List<AuthorDto> Authors { get; set; }
}
