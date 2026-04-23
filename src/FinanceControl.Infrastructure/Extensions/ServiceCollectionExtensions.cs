using FinanceControl.Application.Interfaces;
using FinanceControl.Domain.Repositories;
using FinanceControl.Domain.Services;
using FinanceControl.Infrastructure.Authentication;
using FinanceControl.Infrastructure.Data;
using FinanceControl.Infrastructure.Queries;
using FinanceControl.Infrastructure.Repositories;
using FinanceControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceControl.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Services
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtService, JwtService>();

        // Repositórios de escrita — usados pelos Command Handlers (com tracking)
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICardTransactionRepository, CardTransactionRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        // Queries de leitura — usadas pelos Query Handlers (sem tracking, projeção para DTO)
        services.AddScoped<IUserQueries, UserQueries>();
        services.AddScoped<ICategoryQueries, CategoryQueries>();
        services.AddScoped<ICardQueries, CardQueries>();
        services.AddScoped<ITransactionQueries, TransactionQueries>();
        services.AddScoped<ICardTransactionQueries, CardTransactionQueries>();
        services.AddScoped<IInvoiceQueries, InvoiceQueries>();

        return services;
    }
}
