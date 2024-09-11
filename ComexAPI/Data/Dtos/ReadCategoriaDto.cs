using ComexAPI.Models;

namespace ComexAPI.Data.Dtos {
    public class ReadCategoriaDto {

        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<ReadProdutoDto> Produtos {get; set; }
}
}
