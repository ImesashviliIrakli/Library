﻿using System.ComponentModel.DataAnnotations;

namespace Library.UI.Models.Dtos;

public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
