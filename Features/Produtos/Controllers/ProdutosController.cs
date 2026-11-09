using Microsoft.AspNetCore.Mvc;
using MeuProjetoApi.Features.Produtos.Contracts;
using MeuProjetoApi.Features.Produtos.Services;

namespace MeuProjetoApi.Features.Produtos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class ProdutosController(IProdutoService service) : ControllerBase
{
    /// <summary>Lista todos os produtos cadastrados.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProdutoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProdutoResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var produtos = await service.GetAllAsync(cancellationToken);
        return Ok(produtos);
    }

    /// <summary>Busca um produto pelo identificador.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var produto = await service.GetByIdAsync(id, cancellationToken);
        return produto is null ? NotFound() : Ok(produto);
    }

    /// <summary>Cadastra um produto.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProdutoResponse>> Create(
        CreateProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var produto = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    /// <summary>Atualiza os dados de um produto.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponse>> Update(
        int id,
        UpdateProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var produto = await service.UpdateAsync(id, request, cancellationToken);
        return produto is null ? NotFound() : Ok(produto);
    }

    /// <summary>Exclui um produto.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
