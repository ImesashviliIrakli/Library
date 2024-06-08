using Library.UI.Interfaces;
using Library.UI.Models.AuthorDtos;
using Library.UI.Models.BookDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library.UI.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BookController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    // Index action to display all books
    public async Task<IActionResult> Index()
    {
        var response = await _bookService.GetAllBooksAsync();
        if (response.Status == 0)
        {
            var books = JsonConvert.DeserializeObject<List<BookListDto>>(response.Result.ToString());
            return View(books);
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // Details action to display book details and checkout functionality
    public async Task<IActionResult> Details(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        if (response.Status == 0)
        {
            var book = JsonConvert.DeserializeObject<BookDetailsDto>(response.Result.ToString());
            return View(book);
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // GET action to display form for creating a new book
    public async Task<IActionResult> Create()
    {
        var response = await _authorService.GetAuthorsAsync();
        if (response.Status == 0)
        {
            var authors = JsonConvert.DeserializeObject<List<AuthorDto>>(response.Result.ToString()); ;
            ViewBag.Authors = authors;
            return View();
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle creation of a new book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookDto bookDto, List<int> authors)
    {
        foreach (var authorId in authors)
            bookDto.Authors.Add(new AuthorDto { Id = authorId });

        var response = await _bookService.AddBookAsync(bookDto);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // GET action to display form for editing an existing book
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        if (response.Status == 0)
        {
            var book = response.Result as BookDetailsDto;

            var authorsResponse = await _authorService.GetAuthorsAsync();
            if (authorsResponse.Status == 0)
            {
                var authors = JsonConvert.DeserializeObject<List<AuthorDto>>(authorsResponse.Result.ToString()); ;
                ViewBag.Authors = authors;
                return View(book);
            }
            else
            {
                // Handle error response
                return RedirectToAction("Error", "Home");
            }
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle updating an existing book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateBookDto bookDto)
    {
        var response = await _bookService.UpdateBookAsync(id, bookDto);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle deleting an existing book
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle checking out a book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(int id)
    {
        var response = await _bookService.CheckoutBookAsync(id);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Details), new { id });
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle returning a book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(int id)
    {
        var response = await _bookService.ReturnBookAsync(id);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Details), new { id });
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle adding an author to a book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAuthor(int bookId, int authorId)
    {
        var response = await _bookService.AddAuthorToBookAsync(bookId, authorId);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Edit), new { id = bookId });
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }

    // POST action to handle removing an author from a book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAuthor(int bookId, int authorId)
    {
        var response = await _bookService.RemoveAuthorFromBookAsync(bookId, authorId);
        if (response.Status == 0)
        {
            return RedirectToAction(nameof(Edit), new { id = bookId });
        }
        else
        {
            // Handle error response
            return RedirectToAction("Error", "Home");
        }
    }
}

