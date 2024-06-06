using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.Include(a => a.Books).ToListAsync();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}