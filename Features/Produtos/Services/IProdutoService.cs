using MeuProjetoApi.Features.Produtos.Contracts;

namespace MeuProjetoApi.Features.Produtos.Services;

public interface IProdutoService
{
    Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProdutoResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProdutoResponse> CreateAsync(
        CreateProdutoRequest request,
        CancellationToken cancellationToken);
    Task<ProdutoResponse?> UpdateAsync(
        int id,
        UpdateProdutoRequest request,
        CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
