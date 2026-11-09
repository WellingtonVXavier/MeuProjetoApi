using Microsoft.EntityFrameworkCore;
using MeuProjetoApi.Data;
using MeuProjetoApi.Models;

namespace MeuProjetoApi.Features.Produtos.Repositories;

public sealed class ProdutoRepository(AppDbContexts dbContext) : IProdutoRepository
{
    public async Task<IReadOnlyList<Produto>> GetAllAsync(CancellationToken cancellationToken) =>
        await dbContext.Produtos.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Produto?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        dbContext.Produtos.FirstOrDefaultAsync(produto => produto.Id == id, cancellationToken);

    public async Task AddAsync(Produto produto, CancellationToken cancellationToken)
    {
        await dbContext.Produtos.AddAsync(produto, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    public void Remove(Produto produto) => dbContext.Produtos.Remove(produto);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
