namespace EcommerceNexo.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
