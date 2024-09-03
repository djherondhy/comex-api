using System.ComponentModel.DataAnnotations;

namespace ComexAPI.Data.Dtos; 
public class CreateProdutoDto {

  
    [Required(ErrorMessage = "O Nome é Obrigatório")]
    [MaxLength(100, ErrorMessage = "O Nome não pode exceder 100 caracteres")]
    public string Nome { get; set; }

    [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O Preço Unitário é obrigatório")]
    [Range(0.1, float.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal PrecoUnitario { get; set; }

    [Required(ErrorMessage = "A Quantidade é obrigatório")]
    [Range(0, int.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public int Quantidade { get; set; }
}
