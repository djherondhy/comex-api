using ComexAPI.Models;

namespace ComexAPI.Data.Dtos; 
public class UpdateClienteDto {

    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Profissao { get; set; }
    public string Telefone { get; set; }
    public int EnderecoId { get; set; }
    

}
