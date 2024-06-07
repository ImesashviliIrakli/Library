using Application.Exceptions;
using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories;
public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.Include(b => b.Authors).ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Book book)
    {
        foreach (var author in book.Authors)
            _context.Authors.Attach(author);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddAuthorToBookAsync(Book book, Author author)
    {
        if (!book.Authors.Contains(author))
            book.Authors.Add(author);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAuthorFromBookAsync(Book book, Author author)
    {
        book.Authors.Remove(author);

        await _context.SaveChangesAsync();
    }
}
