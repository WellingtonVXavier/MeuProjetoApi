using MeuProjetoApi.Features.Produtos.Contracts;
using MeuProjetoApi.Features.Produtos.Repositories;
using MeuProjetoApi.Models;

namespace MeuProjetoApi.Features.Produtos.Services;

public sealed class ProdutoService(IProdutoRepository repository) : IProdutoService
{
    public async Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var produtos = await repository.GetAllAsync(cancellationToken);
        return produtos.Select(ToResponse).ToArray();
    }

    public async Task<ProdutoResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var produto = await repository.GetByIdAsync(id, cancellationToken);
        return produto is null ? null : ToResponse(produto);
    }

    public async Task<ProdutoResponse> CreateAsync(
        CreateProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var produto = new Produto
        {
            Nome = request.Nome,
            Preco = request.Preco
        };

        await repository.AddAsync(produto, cancellationToken);
        return ToResponse(produto);
    }

    public async Task<ProdutoResponse?> UpdateAsync(
        int id,
        UpdateProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var produto = await repository.GetByIdAsync(id, cancellationToken);
        if (produto is null)
        {
            return null;
        }

        produto.Nome = request.Nome;
        produto.Preco = request.Preco;
        await repository.SaveChangesAsync(cancellationToken);

        return ToResponse(produto);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var produto = await repository.GetByIdAsync(id, cancellationToken);
        if (produto is null)
        {
            return false;
        }

        repository.Remove(produto);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ProdutoResponse ToResponse(Produto produto) =>
        new(produto.Id, produto.Nome, produto.Preco);
}
