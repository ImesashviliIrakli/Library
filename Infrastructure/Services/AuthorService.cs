using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Author;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<Author> _authorValidator;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper, IValidator<Author> authorValidator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _authorValidator = authorValidator;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var authorListDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        return authorListDtos;
    }

    public async Task<AuthorDetailsDto> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        CheckAuthor(author, id);

        return _mapper.Map<AuthorDetailsDto>(author);
    }

    public async Task AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);

        await Validate(author);

        await _authorRepository.AddAsync(author);
    }

    public async Task UpdateAuthorAsync(int id, UpdateAuthorDto authorDto)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        CheckAuthor(author, id);

        _mapper.Map(authorDto, author);

        await Validate(author);

        await _authorRepository.UpdateAsync(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        CheckAuthor(author, id);

        await _authorRepository.DeleteAsync(id);
    }

    private async Task Validate(Author author)
    {
        var validationResult = await _authorValidator.ValidateAsync(author);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private void CheckAuthor(Author author, int id)
    {
        if (author == null)
            throw new NotFoundException($"Author with id:{id} not found");
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsByIdsAsync(List<int> ids)
    {
        if (ids == null)
            throw new BadRequestException($"Author is neccessary");

        var authors = await _authorRepository.GetByIdsAsync(ids);

        var result = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        return result;
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string authorName)
    {
        var authors = await _authorRepository.GetAuthorsByNameAsync(authorName);

        var result = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        return result;
    }
}
