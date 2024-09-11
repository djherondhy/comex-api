using AutoMapper;
using ComexAPI.Data.Dtos;
using ComexAPI.Data;
using ComexAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FilmeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : Controller {
    private ProductContext _context;
    private IMapper _mapper;

    public ClienteController(IMapper mapper, ProductContext context) {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public CreatedAtActionResult AddCliente([FromBody] CreateClienteDto clienteDto) {
        Cliente cliente = _mapper.Map<Cliente>(clienteDto);
        _context.clientes.Add(cliente);
        _context.SaveChanges();
        return CreatedAtAction(nameof(getClienteById), new { id = cliente.Id }, cliente);
    }

    [HttpGet]
    public IEnumerable<ReadClienteDto> getClientes([FromQuery] int? enderecoId = null) {
        if (enderecoId == null) {
            var clienteList = _mapper.Map<List<ReadClienteDto>>(_context.clientes.ToList());
            return clienteList;
        }

        return _mapper.Map<List<ReadClienteDto>>(
            _context.clientes
            .FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM clientes WHERE clientes.EnderecoId = {enderecoId}")
            .ToList());
    }

    [HttpGet("{id}")]
    public IActionResult getClienteById(int id) {
        var cliente = _context.clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();
        var clienteDto = _mapper.Map<ReadClienteDto>(cliente);
        return Ok(clienteDto);
    }

    [HttpPut("{id}")]
    public IActionResult updateCliente(int id, [FromBody] UpdateClienteDto clienteDto) {
        var cliente = _context.clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();
        _mapper.Map(clienteDto, cliente);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult partialUpdateCliente(int id, JsonPatchDocument<UpdateClienteDto> patch) {
        var cliente = _context.clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();

        var clienteForUpdate = _mapper.Map<UpdateClienteDto>(cliente);
        patch.ApplyTo(clienteForUpdate, ModelState);

        if (!TryValidateModel(clienteForUpdate)) {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(clienteForUpdate, cliente);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult deleteCliente(int id) {
        var cliente = _context.clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();

        _context.Remove(cliente);
        _context.SaveChanges();
        return NoContent();
    }
}
