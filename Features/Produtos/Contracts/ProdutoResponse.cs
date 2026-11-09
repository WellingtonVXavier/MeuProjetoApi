namespace MeuProjetoApi.Features.Produtos.Contracts;

/// <summary>Representação de um produto retornado pela API.</summary>
public sealed record ProdutoResponse(int Id, string Nome, decimal Preco);
