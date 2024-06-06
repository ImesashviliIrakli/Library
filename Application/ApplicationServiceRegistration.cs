using Application.Validators;
using Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<IValidator<Book>, BookValidator>();
        services.AddTransient<IValidator<Author>, AuthorValidator>();

        return services;
    }
}
