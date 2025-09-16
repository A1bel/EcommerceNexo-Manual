namespace EcommerceNexo.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

    }
}
