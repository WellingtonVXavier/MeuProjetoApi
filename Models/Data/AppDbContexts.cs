using Microsoft.EntityFrameworkCore;
using MeuProjetoApi.Models;

namespace MeuProjetoApi.Data;

public class AppDbContexts : DbContext
{
    public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
}