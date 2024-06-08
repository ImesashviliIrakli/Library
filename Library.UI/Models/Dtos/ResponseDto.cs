namespace Library.UI.Models.Dtos;

public class ResponseDto
{
    public object Result { get; set; }
    public int Status { get; set; }
    public string Message { get; set; } = "Successful request";
}
