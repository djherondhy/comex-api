using AutoMapper;
using ComexAPI.Data.Dtos;
using ComexAPI.Data;
using ComexAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilmeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : Controller {
    private ProductContext _context;
    private IMapper _mapper;

    public CategoriaController(IMapper mapper, ProductContext context) {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public CreatedAtActionResult AddCategoria([FromBody] CreateCategoriaDto categoriaDto) {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
        _context.categorias.Add(categoria);
        _context.SaveChanges();
        return CreatedAtAction(nameof(getCategoriaById), new { id = categoria.Id }, categoria);
    }

    [HttpGet]
    public IEnumerable<ReadCategoriaDto> getCategorias() {
        var categoriaList = _mapper.Map<List<ReadCategoriaDto>>(_context.categorias.ToList());
        return categoriaList;
    }

    [HttpGet("{id}")]
    public IActionResult getCategoriaById(int id) {
        var categoria = _context.categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();
        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return Ok(categoriaDto);
    }

    [HttpPut("{id}")]
    public IActionResult updateCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto) {
        var categoria = _context.categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();
        _mapper.Map(categoriaDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult partialUpdateCategoria(int id, JsonPatchDocument<UpdateCategoriaDto> patch) {
        var categoria = _context.categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        var categoriaForUpdate = _mapper.Map<UpdateCategoriaDto>(categoria);
        patch.ApplyTo(categoriaForUpdate, ModelState);

        if (!TryValidateModel(categoriaForUpdate)) {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(categoriaForUpdate, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult deleteCategoria(int id) {
        var categoria = _context.categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }
}
