﻿namespace Application.Models.Author;

public class AuthorDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
