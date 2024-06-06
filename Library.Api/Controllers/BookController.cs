using Application.Enums;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ResponseModel _response;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
        _response = new ResponseModel(Status.Success, "Success");
    }

    [HttpGet]
    public async Task<ActionResult> GetBooks()
    {
        _response.Result = await _bookService.GetAllBooksAsync();
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBook(int id)
    {
        _response.Result = await _bookService.GetBookByIdAsync(id);
        return Ok(_response);
    }

    [HttpPost]
    public async Task<ActionResult> AddBook(CreateBookDto bookDto)
    {
        await _bookService.AddBookAsync(bookDto);
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto bookDto)
    {
        await _bookService.UpdateBookAsync(id, bookDto);
        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return Ok(_response);
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> CheckoutBook(int id)
    {
        await _bookService.CheckoutBookAsync(id);
        return Ok(_response);
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        await _bookService.ReturnBookAsync(id);
        return Ok(_response);
    }
}
