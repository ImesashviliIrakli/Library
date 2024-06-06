using Application.Enums;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ResponseModel _response;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
        _response = new ResponseModel(Status.Success, "Success");
    }

    [HttpGet]
    public async Task<ActionResult> GetAuthors()
    {
        _response.Result = await _authorService.GetAllAuthorsAsync();
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAuthor(int id)
    {
        _response.Result = await _authorService.GetAuthorByIdAsync(id);
        return Ok(_response);
    }

    [HttpPost]
    public async Task<ActionResult> AddAuthor(CreateAuthorDto authorDto)
    {
        await _authorService.AddAuthorAsync(authorDto);
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto authorDto)
    {
        await _authorService.UpdateAuthorAsync(id, authorDto);
        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return Ok(_response);
    }
}