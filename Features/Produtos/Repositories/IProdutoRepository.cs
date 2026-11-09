using MeuProjetoApi.Models;

namespace MeuProjetoApi.Features.Produtos.Repositories;

public interface IProdutoRepository
{
    Task<IReadOnlyList<Produto>> GetAllAsync(CancellationToken cancellationToken);
    Task<Produto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Produto produto, CancellationToken cancellationToken);
    void Remove(Produto produto);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
