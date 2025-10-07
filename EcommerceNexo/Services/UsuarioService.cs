using EcommerceNexo.DataBase;
using EcommerceNexo.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EcommerceNexo.Services
{
    public class UsuarioService
    {
        private readonly UsuariosRepository _usuarioRepository;

        public UsuarioService(UsuariosRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public ValidationResult RegistrarUsuario(Usuario usuario)
        {
            
            ValidationResult result = ValidaUsuario(usuario);
            if (!result.Success) return result;

            try
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                usuario.DataCriacao = DateTime.Now;
                _usuarioRepository.Add(usuario);

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add("usuario", "Ocorreu um erro ao salvar o usuário: " + ex.Message);
                result.Success = false;
                return result;
            }
        }

        //public ValidationResult RegistrarUsuario(Usuario usuario)
        //{
        //    ValidationResult result = new ValidationResult();
        //    result.Success = true;

        //    if (usuario == null)
        //    {
        //        result.Errors.Add("usuario", "Usuario não pode ser nulo.");
        //        result.Success = false;
        //        return result;
        //    }

        //    if (string.IsNullOrWhiteSpace(usuario.Nome) || HasSpecialChar(usuario.Nome))
        //    {
        //        result.Errors.Add("nome", "Nome inválido ou não informado.");
        //        result.Success = false;
        //    }

        //    if (string.IsNullOrWhiteSpace(usuario.Email) || !IsValidEmail(usuario.Email))
        //    {
        //        result.Errors.Add("email", "Email inválido ou não informado.");
        //        result.Success = false;
        //    }

        //    if (!string.IsNullOrWhiteSpace(usuario.Email) && _usuarioRepository.GetByEmail(usuario.Email) != null)
        //    {
        //        result.Errors.Add("email", "Já existe um usuário com este email.");
        //        result.Success = false;
        //    }

        //    if (string.IsNullOrWhiteSpace(usuario.Senha))
        //    {
        //        result.Errors.Add("senha", "Senha não informada.");
        //        result.Success = false;
        //    }

        //    if (string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
        //    {
        //        result.Errors.Add("confirmacaoSenha", "Confirmação de senha não informada.");
        //        result.Success = false;
        //    }

        //    if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
        //        && usuario.Senha.Length < 8)
        //    {
        //        result.Errors.Add("senha", "A senha deve conter no mínimo 8 caracteres");
        //        result.Success = false;
        //    }

        //    if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
        //        && usuario.ConfirmacaoSenha.Length < 8)
        //    {
        //        result.Errors.Add("confirmacaoSenha", "A senha deve conter no mínimo 8 caracteres");
        //        result.Success = false;
        //    }

        //    if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
        //        && (usuario.Senha.Length >= 8 && usuario.ConfirmacaoSenha.Length >= 8) && usuario.Senha != usuario.ConfirmacaoSenha)
        //    {
        //        result.Errors.Add("senha", "As senhas não coincidem.");
        //        result.Success = false;
        //    }

        //    if (!result.Success) return result;

        //    try
        //    {
        //        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
        //        _usuarioRepository.Add(usuario);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Errors.Add("usuario", "Ocorreu um erro ao salvar o usuário: " + ex.Message);
        //        result.Success = false;
        //        return result;
        //    }
        //}

        public LoginResult Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(senha))
                return new LoginResult { Success = false, Message = "Preencha todos os campos"};

            Usuario usuario = _usuarioRepository.GetByEmail(email);
            if (usuario == null)
                return new LoginResult { Success = false, Message = "Email e/ou senha inválidos."};

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                return new LoginResult { Success = false, Message = "Email e/ou senha inválidos." };

            return new LoginResult { Success = true, Usuario = usuario};
        }

        public Usuario ObterUsuario(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido");

            return _usuarioRepository.Get(id); //?? throw new InvalidOperationException("Usuário não encontrado");
        }

        public List<Usuario> ObterTodosUsuarios()
        {

            List<Usuario> usuarios =  _usuarioRepository.GetAll();

            return usuarios;
        }

        public List<Usuario> ObterUsuariosFiltrados(string nome, string email, string perfil)
        {

            List<Usuario> usuarios =  _usuarioRepository.GetAll(nome, email, perfil);

            return usuarios;
        }

        public ValidationResult AtualizarUsuario(Usuario usuario)
        {
            ValidationResult result = new ValidationResult();

            if (usuario == null)
            {
                result.Errors.Add("usuario", "Usuário não pode ser nulo");
                result.Success = false;
                return result;
            }

            if (usuario.Id <= 0)
            {
                result.Errors.Add("id", "Id do usuário inválido");
                result.Success = false;
                return result;
            }

            Usuario existe = _usuarioRepository.Get(usuario.Id);
            if (existe == null)
            {
                result.Errors.Add("usuario", "Usuário não encontrado");
                result.Success = false;
                return result;
            }

            ValidationResult validation = ValidaUsuario(usuario, isUpdate: true);
            if(!validation.Success)
                return validation;
            
            existe.Nome = usuario.Nome;
            existe.Email = usuario.Email;

            if(usuario.Perfil != null)
                existe.Perfil = usuario.Perfil;

            if (!string.IsNullOrWhiteSpace(usuario.Senha) && !string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
                existe.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            bool atualizado = _usuarioRepository.Update(existe);
            if (!atualizado)
            {
                result.Errors.Add("update", "Erro ao atualizar usuário no repositório");
                result.Success = false;
                return result;
            }
            
            result.Success= true;
            return result;
        }
        
        //public bool AtualizarUsuario(Usuario usuario)
        //{
        //    if (usuario == null)
        //        throw new ArgumentNullException("Usuário não pode ser nulo");

        //    if (usuario.Id <= 0)
        //        throw new ArgumentException("Id do usuário é inválido");

        //    Usuario existe = _usuarioRepository.Get(usuario.Id);
        //    if (existe == null)
        //        throw new InvalidOperationException("Usuário não encontrado");

        //    if (string.IsNullOrWhiteSpace(usuario.Nome) || HasSpecialChar(usuario.Nome))
        //        throw new ArgumentException("Nome inválido ou não informado.");

        //    if (string.IsNullOrWhiteSpace(usuario.Email) || !IsValidEmail(usuario.Email))
        //        throw new ArgumentException("Email inválido ou não informado.");

        //    if(!string.Equals(usuario.Email, existe.Email, StringComparison.OrdinalIgnoreCase))
        //    {
        //        Usuario outro = _usuarioRepository.GetByEmail(usuario.Email);
        //        if (outro != null && outro.Id != usuario.Id)
        //            throw new InvalidOperationException("Já existe outro usuário com este email");
        //    }

        //    if (!string.IsNullOrWhiteSpace(usuario.Senha) || !string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
        //    {
        //        if (usuario.Senha != usuario.ConfirmacaoSenha)
        //            throw new ArgumentException("As senhas não coincidem.");

        //        if (usuario.Senha.Length < 8)
        //            throw new ArgumentException("Senha inválida. Deve ter pelo menos 8 caracteres.");

        //        existe.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
        //    }

        //    existe.Nome = usuario.Nome;
        //    existe.Email = usuario.Email;
        //    existe.Perfil = usuario.Perfil;
        //    existe.DataCriacao = existe.DataCriacao; 

        //    return _usuarioRepository.Update(usuario);
        //}

        public bool DeletarUsuario(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido");

            Usuario existe = _usuarioRepository.Get(id);
            if (existe == null)
                throw new ArgumentException("Usuário não encontrado");

            return _usuarioRepository.Delete(id);
        }

        #region Métodos de Validação
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool HasSpecialChar(string text)
        {
            return Regex.IsMatch(text, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?0-9]");
        }

        private bool IsValidCPF(string cpf)
        {
            var cleanCPF = Regex.Replace(cpf ?? "", @"\D", "");
            return cleanCPF.Length == 11;
        }

        private bool IsValidCNPJ(string cnpj)
        {
            var cleanCNPJ = Regex.Replace(cnpj ?? "", @"\D", "");
            return cleanCNPJ.Length == 14;
        }

        private ValidationResult ValidaUsuario(Usuario usuario, bool isUpdate = false)
        {
            ValidationResult result = new ValidationResult { Success = true };

            if (usuario == null)
            {
                result.Errors.Add("usuario", "Usuario não pode ser nulo.");
                result.Success = false;
                return result;
            }

            if (string.IsNullOrWhiteSpace(usuario.Nome) || HasSpecialChar(usuario.Nome))
            {
                result.Errors.Add("nome", "Nome inválido ou não informado.");
                result.Success = false;
            }

            if (string.IsNullOrWhiteSpace(usuario.Email) || !IsValidEmail(usuario.Email))
            {
                result.Errors.Add("email", "Email inválido ou não informado.");
                result.Success = false;
            }
            else
            {
                var emailExistente = _usuarioRepository.GetByEmail(usuario.Email);
                if (!isUpdate && emailExistente != null)
                {
                    result.Errors.Add("email", "Já existe um usuário com este email.");
                    result.Success = false;
                }
                else if(isUpdate && emailExistente != null && emailExistente.Id != usuario.Id)
                {
                    result.Errors.Add("email", "Já existe um usuário com este email.");
                    result.Success = false;
                } 
            }

            if(!isUpdate || (!string.IsNullOrWhiteSpace(usuario.Senha) || !string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha)))
            {
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    result.Errors.Add("senha", "Senha não informada.");
                    result.Success = false;
                }

                if (string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
                {
                    result.Errors.Add("confirmacaoSenha", "Confirmação de senha não informada.");
                    result.Success = false;
                }
                
                if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
                    && usuario.Senha.Length < 8)
                {
                    result.Errors.Add("senha", "A senha deve conter no mínimo 8 caracteres");
                    result.Success = false;
                }
                
                if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
                    && usuario.ConfirmacaoSenha.Length < 8)
                {
                    result.Errors.Add("confirmacaoSenha", "A senha deve conter no mínimo 8 caracteres");
                    result.Success = false;
                }
                
                if (!(string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.ConfirmacaoSenha))
                    && (usuario.Senha.Length >= 8 && usuario.ConfirmacaoSenha.Length >= 8) && usuario.Senha != usuario.ConfirmacaoSenha)
                {
                    result.Errors.Add("senha", "As senhas não coincidem.");
                    result.Success = false;
                }
            }

            return result;
        }
        #endregion
    }
}
