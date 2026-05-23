using LSalto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Tests.Helpers;

/// <summary>
/// Cria um AppDbContext em memória isolado por teste.
/// Cada chamada gera um banco com nome único para evitar interferência entre testes.
/// </summary>
public static class DbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
