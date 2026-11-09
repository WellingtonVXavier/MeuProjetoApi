using System.ComponentModel.DataAnnotations;

namespace MeuProjetoApi.Features.Produtos.Contracts;

/// <summary>Dados para atualizar um produto existente.</summary>
public sealed record UpdateProdutoRequest
{
    [Required, StringLength(200)]
    public required string Nome { get; init; }

    [Range(0.01, 999999999)]
    public decimal Preco { get; init; }
}
