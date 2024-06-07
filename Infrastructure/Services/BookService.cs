using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Book;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Infrastructure.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<Book> _bookValidator;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, IValidator<Book> bookValidator)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _bookValidator = bookValidator;
    }

    public async Task<IEnumerable<BookListDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        var bookListDtos = _mapper.Map<IEnumerable<BookListDto>>(books);

        return bookListDtos;
    }

    public async Task<BookDetailsDto> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        CheckBook(book, id);

        return _mapper.Map<BookDetailsDto>(book);
    }

    public async Task AddBookAsync(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);

        await Validate(book);

        await _bookRepository.AddAsync(book);
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto bookDto)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        CheckBook(book, id);

        _mapper.Map(bookDto, book);

        await Validate(book);

        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        CheckBook(book, id);

        await _bookRepository.DeleteAsync(id);
    }

    public async Task CheckoutBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        CheckBook(book, id);

        if (book.IsTaken)
            throw new InvalidOperationException($"Book with id:{id} is already checked out.");

        book.IsTaken = true;
        await _bookRepository.UpdateAsync(book);
    }

    public async Task ReturnBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        CheckBook(book, id);

        if (!book.IsTaken)
            throw new InvalidOperationException($"Book with Id:{id} is not checked out.");

        book.IsTaken = false;
        await _bookRepository.UpdateAsync(book);
    }

    public async Task AddAuthorToBookAsync(int bookId, int authorId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        CheckBook(book, bookId);

        var author = await _authorRepository.GetByIdAsync(authorId);
        
        CheckAuthor(author, authorId);

        if (book.Authors.Exists(x => x.Id == authorId))
            throw new BadRequestException($"Author with Id:{authorId} already exists");

        await _bookRepository.AddAuthorToBookAsync(book, author);
    }

    public async Task RemoveAuthorFromBookAsync(int bookId, int authorId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        
        CheckBook(book, bookId);

        var authorToRemove = book.Authors.FirstOrDefault(a => a.Id == authorId);
        
        CheckAuthor(authorToRemove, authorId);

        await _bookRepository.RemoveAuthorFromBookAsync(book, authorToRemove);
    }

    private async Task Validate(Book book)
    {
        var validationResult = await _bookValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private void CheckBook(Book book, int id)
    {
        if (book == null)
            throw new NotFoundException($"Book with id:{id} not found");
    }

    private void CheckAuthor(Author author, int id)
    {
        if (author == null)
            throw new NotFoundException($"Book with id:{id} not found");
    }
}