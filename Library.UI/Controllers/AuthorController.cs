using Library.UI.Interfaces;
using Library.UI.Models.AuthorDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Library.UI.Controllers;

[Authorize]
public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _authorService.GetAuthorsAsync();
        if (response.Status == 0)
        {
            var authors = JsonConvert.DeserializeObject<List<AuthorDto>>(response.Result.ToString());
            return View(authors);
        }
        return View(new List<AuthorDto>());
    }

    [HttpGet]
    public async Task<IActionResult> Search(string authorName)
    {
        if (string.IsNullOrEmpty(authorName))
            return RedirectToAction(nameof(Index));

        var response = await _authorService.GetAuthorsByNameAsync(authorName);
        if (response.Status == 0)
        {
            var authors = JsonConvert.DeserializeObject<List<AuthorDto>>(response.Result.ToString());
            return View("Index", authors);
        }
        else
        {
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAuthorDto createAuthorDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _authorService.AddAuthorAsync(createAuthorDto);
            if (response.Status == 0)
            {
                TempData["success"] = "Created Author";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = response.Message;
        }
        return View(createAuthorDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _authorService.GetAuthorByIdAsync(id);
        if (response.Status == 0)
        {
            var author = JsonConvert.DeserializeObject<UpdateAuthorDto>(response.Result.ToString());
            return View(author);
        }
        
        TempData["error"] = response.Message;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateAuthorDto updateAuthorDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _authorService.UpdateAuthorAsync(updateAuthorDto.Id, updateAuthorDto);
            if (response.Status == 0)
            {
                TempData["success"] = "Updated Author";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = response.Message;
        }
        return View(updateAuthorDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _authorService.DeleteAuthorAsync(id);
        if (response.Status == 0)
        {
            TempData["success"] = "Deleted Author";
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = response.Message;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _authorService.GetAuthorByIdAsync(id);
        if (response.Status == 0)
        {
            var author = JsonConvert.DeserializeObject<AuthorDetailsDto>(response.Result.ToString());
            return View(author);
        }
        TempData["error"] = response.Message;
        return View();
    }
}


