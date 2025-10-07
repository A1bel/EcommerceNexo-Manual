namespace EcommerceNexo.Models
{
    public class UsuarioViewModel
    {
        public string Nome {  get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
