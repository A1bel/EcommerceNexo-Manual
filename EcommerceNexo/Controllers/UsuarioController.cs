using EcommerceNexo.Models;
using EcommerceNexo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceNexo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index(string nome = null, string email = null, string perfil = null)
        {
            List<Usuario> usuarios = _usuarioService.ObterUsuariosFiltrados(nome, email, perfil);
            UsuarioViewModel viewModel = new UsuarioViewModel
            {
                Nome = nome,
                Email = email,
                Perfil = perfil,
                Usuarios = usuarios
            };

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult FilterUsers(string nome = null, string email = null, string perfil = null)
        {
            var usuarios = _usuarioService.ObterUsuariosFiltrados(nome, email, perfil);
            return PartialView("_UsuariosTable", usuarios);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isAdmin = User.IsInRole("admin");

            if (isAdmin)
            {
                Usuario usuario = _usuarioService.ObterUsuario(id);
                if(usuario == null)
                    return RedirectToAction("CustomError", "Error", new { title = "Usuário Inválido", message = "O usuário informado não existe." });

                return View(usuario);
            }
            
            if(id != userId)
            {
                return RedirectToAction("CustomError", "Error", new { title = "Acesso Negado", message = "Você não tem permissão para editar outro usuário" });
            }

            Usuario user = _usuarioService.ObterUsuario(userId);
            if(user == null)
                return NotFound();

            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            try
            {
                ValidationResult response = _usuarioService.RegistrarUsuario(usuario);

                if (response.Success)
                    return new JsonResult(new { success = true, message = "Cadastro realizado com sucesso!" });

                return Json(new { success = false, erros = response.Errors});
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResult response = _usuarioService.Login(request.Email, request.Password);

            if (!response.Success)
                return new JsonResult(new { success = false, message = response.Message });

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, response.Usuario.Nome),
                new Claim(ClaimTypes.Email, response.Usuario.Email),
                new Claim(ClaimTypes.Role, response.Usuario.Perfil)

            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(new
            {
                success = true,
                message = "Login realizado com sucesso!",
                user = new Usuario
                {
                    Id = response.Usuario.Id,
                    Nome = response.Usuario.Nome,
                    Email = response.Usuario.Email,
                    Perfil = response.Usuario.Perfil
                }
            });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Usuario> response = new List<Usuario>();
            try
            {
                response = _usuarioService.ObterTodosUsuarios();
                return new JsonResult(new { success = true, users = response });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string userRole = User.FindFirst(ClaimTypes.Role).Value;

                if (userRole != "admin" && usuario.Id != userId)
                    return new JsonResult(new { success = false, message = "Você não tem permissão para alterar outro usuário" });

                ValidationResult response = _usuarioService.AtualizarUsuario(usuario);

                if (response.Success)
                    return Json(new { success = true, role= userRole, id = userId, message = "Usuário atualizado com sucesso!" });
                    
                return new JsonResult(new { success = false, erros = response.Errors });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                bool response = _usuarioService.DeletarUsuario(id);
                if (!response)
                    return new JsonResult(new { success = false, message = "Erro ao deletar usuário"});

                return new JsonResult(new { success = true, message = "Usuário deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}
