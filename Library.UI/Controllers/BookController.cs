using Library.UI.Interfaces;
using Library.UI.Models.AuthorDtos;
using Library.UI.Models.BookDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library.UI.Controllers;

[Authorize]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BookController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

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
            TempData["error"] = response.Message;
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Search(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            // If no title provided, return to Index action
            return RedirectToAction(nameof(Index));
        }

        var response = await _bookService.GetBooksByTitleAsync(title);
        if (response.Status == 0)
        {
            var books = JsonConvert.DeserializeObject<List<BookListDto>>(response.Result.ToString());
            return View("Index", books);
        }
        else
        {
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        if (response.Status == 0)
        {
            var book = JsonConvert.DeserializeObject<BookDetailsDto>(response.Result.ToString());
            return View(book);
        }

        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Create()
    {
        var response = await _authorService.GetAuthorsAsync();
        if (response.Status == 0)
        {
            var authors = JsonConvert.DeserializeObject<List<AuthorDto>>(response.Result.ToString()); ;
            ViewBag.Authors = authors;
            return View();
        }

        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookDto bookDto, List<int> authors, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            var fileName = Path.GetFileName(imageFile.FileName);
            var extension = Path.GetExtension(fileName);

            var uniqueFileName = Guid.NewGuid().ToString() + extension;

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            bookDto.Image = $"/images/{uniqueFileName}";
        }

        var getAuthors = await _authorService.GetAuthorsByIdsAsync(authors);
        if (getAuthors.Status == 0)
        {
            var authorDtos = JsonConvert.DeserializeObject<List<AuthorDto>>(getAuthors.Result.ToString());
            foreach (var author in authorDtos)
                bookDto.Authors.Add(author);
        }
        else
        {
            TempData["error"] = getAuthors.Message;
            return View();
        }

        var response = await _bookService.AddBookAsync(bookDto);
        if (response.Status == 0)
        {
            TempData["success"] = "Created Book";
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = response.Message;
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        if (response.Status == 0)
        {
            var bookDetails = JsonConvert.DeserializeObject<BookDetailsDto>(response.Result.ToString());

            ViewBag.BookAuthors = bookDetails.Authors;

            var authorsResponse = await _authorService.GetAuthorsAsync();
            if (authorsResponse.Status == 0)
            {
                ViewBag.Authors = JsonConvert.DeserializeObject<List<AuthorDto>>(authorsResponse.Result.ToString());

                var updateBookDto = new UpdateBookDto
                {
                    Id = bookDetails.Id,
                    Title = bookDetails.Title,
                    Description = bookDetails.Description,
                    Image = bookDetails.Image,
                    Rating = bookDetails.Rating,
                    PublicationDate = bookDetails.PublicationDate,
                    IsTaken = bookDetails.IsTaken
                };

                return View(updateBookDto);
            }
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateBookDto bookDto, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            if (!string.IsNullOrEmpty(bookDto.Image))
            {
                var existingImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", bookDto.Image.TrimStart('/'));
                if (System.IO.File.Exists(existingImagePath))
                    System.IO.File.Delete(existingImagePath);
            }

            var fileName = Path.GetFileName(imageFile.FileName);
            var extension = Path.GetExtension(fileName);

            var uniqueFileName = Guid.NewGuid().ToString() + extension;

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            bookDto.Image = $"/images/{uniqueFileName}";
        }

        var response = await _bookService.UpdateBookAsync(id, bookDto);
        if (response.Status == 0)
        {
            TempData["success"] = "Updated Book";
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Edit), new { id = id });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> DeleteConfirmed(int id)
    {
        var getBookDetails = await _bookService.GetBookByIdAsync(id);
        if (getBookDetails.Status == 0)
        {
            var bookDto = JsonConvert.DeserializeObject<BookDetailsDto>(getBookDetails.Result.ToString());
            if (!string.IsNullOrEmpty(bookDto.Image))
            {
                var existingImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", bookDto.Image.TrimStart('/'));
                if (System.IO.File.Exists(existingImagePath))
                    System.IO.File.Delete(existingImagePath);
            }
        }

        var response = await _bookService.DeleteBookAsync(id);
        if (response.Status == 0)
        {
            TempData["success"] = "Deleted book";
            return Json(new { success = true });
        }

        TempData["error"] = response.Message;
        return Json(new { success = false });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Checkout(int id)
    {
        var response = await _bookService.CheckoutBookAsync(id);
        if (response.Status == 0)
            return Json(new { success = true });

        TempData["error"] = response.Message;
        return Json(new { success = false });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Return(int id)
    {
        var response = await _bookService.ReturnBookAsync(id);
        if (response.Status == 0)
            return Json(new { success = true });

        TempData["error"] = response.Message;
        return Json(new { success = false });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAuthor(int bookId, int authorId)
    {
        var response = await _bookService.AddAuthorToBookAsync(bookId, authorId);
        if (response.Status == 0)
        {
            TempData["success"] = "Added Author To Book";
            return RedirectToAction(nameof(Edit), new { id = bookId });
        }

        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Edit));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAuthor(int bookId, int authorId)
    {
        var response = await _bookService.RemoveAuthorFromBookAsync(bookId, authorId);
        if (response.Status == 0)
        {
            TempData["success"] = "Removed Author From Book";
            return RedirectToAction(nameof(Edit), new { id = bookId });
        }

        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Edit));
    }
}

