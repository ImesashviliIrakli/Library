﻿using Library.UI.Models.AuthorDtos;
using Library.UI.Models.Dtos;

namespace Library.UI.Interfaces;

public interface IAuthorService
{
    Task<ResponseDto> GetAuthorsAsync();
    Task<ResponseDto> GetAuthorByIdAsync(int id);
    Task<ResponseDto> AddAuthorAsync(CreateAuthorDto authorDto);
    Task<ResponseDto> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);
    Task<ResponseDto> DeleteAuthorAsync(int id);
}
