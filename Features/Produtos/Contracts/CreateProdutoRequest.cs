using System.ComponentModel.DataAnnotations;

namespace MeuProjetoApi.Features.Produtos.Contracts;

/// <summary>Dados necessários para cadastrar um produto.</summary>
public sealed record CreateProdutoRequest
{
    [Required, StringLength(200)]
    public required string Nome { get; init; }

    [Range(0.01, 999999999)]
    public decimal Preco { get; init; }
}
