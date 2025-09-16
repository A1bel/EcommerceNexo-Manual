namespace EcommerceNexo.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Tamanho { get; set; }
        public string Cor {  get; set; }
        public string Imagem { get; set; }
    }
}
