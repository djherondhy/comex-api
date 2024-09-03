using AutoMapper;
using ComexAPI.Data;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComexAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase {

    private ProductContext _context;
    private IMapper _mapper;

    public ProdutoController(IMapper mapper, ProductContext context) {
        _mapper = mapper;
        _context = context;
    }

    /// <summary>
    /// Adiciona um novo produto
    /// </summary>
    /// <param name="produtoDto">Dados do produto a ser adicionado</param>
    /// <returns>CreatedAtActionResult</returns>
    /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    public CreatedAtActionResult AddProduto([FromBody] CreateProdutoDto produtoDto) {
        Produto produto = _mapper.Map<Produto>(produtoDto);
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(getProdutoById), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Obtém uma lista de produtos com paginação
    /// </summary>
    /// <param name="skip">Número de itens a serem ignorados</param>
    /// <param name="take">Número de itens a serem retornados</param>
    /// <returns>Lista de produtos</returns>
    /// <response code="200">Lista de produtos retornada com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadProdutoDto> getProduto([FromQuery] int skip = 0, [FromQuery] int take = 50) {
        return _mapper.Map<List<ReadProdutoDto>>(_context.Produtos.Skip(skip).Take(take)).ToList();
    }

    /// <summary>
    /// Obtém um produto específico pelo ID
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Produto retornado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpGet("{id}")]
    public IActionResult getProdutoById(int id) {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        var produtoDto = _mapper.Map<ReadProdutoDto>(produto);
        return Ok(produtoDto);
    }

    /// <summary>
    /// Atualiza um produto existente
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <param name="produtoDto">Dados atualizados do produto</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Produto atualizado com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpPut("{id}")]
    public IActionResult updateProduto(int id, [FromBody] UpdateProdutoDto produtoDto) {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Remove um produto existente pelo ID
    /// </summary>
    /// <param name="id">ID do produto a ser removido</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Produto removido com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    [HttpDelete("{id}")]
    public IActionResult deleteProduto(int id) {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        _context.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }
}
