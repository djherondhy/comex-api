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
public class EnderecoController : Controller {
    private ProductContext _context;
    private IMapper _mapper;

    public EnderecoController(IMapper mapper, ProductContext context) {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public CreatedAtActionResult AddEndereco([FromBody] CreateEnderecoDto enderecoDto) {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(getEnderecoById), new { id = endereco.Id }, endereco);
    }

    [HttpGet]
    public IEnumerable<ReadEnderecoDto> getEnderecos() {
        var enderecoList = _mapper.Map<List<ReadEnderecoDto>>(_context.enderecos.ToList());
        return enderecoList;
    }

    [HttpGet("{id}")]
    public IActionResult getEnderecoById(int id) {
        var endereco = _context.enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null) return NotFound();
        var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
        return Ok(enderecoDto);
    }

    [HttpPut("{id}")]
    public IActionResult updateEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto) {
        var endereco = _context.enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null) return NotFound();
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult partialUpdateEndereco(int id, JsonPatchDocument<UpdateEnderecoDto> patch) {
        var endereco = _context.enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null) return NotFound();

        var enderecoForUpdate = _mapper.Map<UpdateEnderecoDto>(endereco);
        patch.ApplyTo(enderecoForUpdate, ModelState);

        if (!TryValidateModel(enderecoForUpdate)) {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(enderecoForUpdate, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult deleteEndereco(int id) {
        var endereco = _context.enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null) return NotFound();

        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
}
