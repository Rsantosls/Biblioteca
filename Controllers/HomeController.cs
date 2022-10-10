using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login l)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                // Objeto vazio para receber os dados da database
                Login login = new Login();
                // Variável para receber a senha criptografa  
                string senhaCrip = MD5Hash.senhaHash(l.Senha).ToLower();                  
                // Comando que busca dentro da database(bc) se o valor em (l.usuario) existe para que armazene em "login.Usuario". Do contrário, virá um padrão nulo pelo "SingleOrDefault".
                login.Usuario = bc.Login.Where(lg => lg.Usuario == l.Usuario).Select(lg => lg.Usuario).FirstOrDefault();
                login.Senha = bc.Login.Where(lg => lg.Senha == senhaCrip).Select(lg => lg.Senha).FirstOrDefault();
                login.Id = bc.Login.Where(lg => lg.Usuario == l.Usuario).Select(lg => lg.Id ).FirstOrDefault();
                login.Nome = bc.Login.Where(lg => lg.Usuario == l.Usuario).Select(lg => lg.Nome ).FirstOrDefault();
                     
                // Debug
                // string senhaDBCrip = MD5Hash.senhaHash(login.Senha);
                // Saída das variaveis que armazenam os dados escritos 
                // Console.WriteLine("==================");
                // Console.WriteLine("Usuario (input): " + l.Usuario);
                // Console.WriteLine("senha (input): " + l.Senha);
                // Console.WriteLine("senha Criptografada (input): " + senhaCrip);
                // Console.WriteLine("==================");
                // Console.WriteLine("");

                // Saída das variaveis que armazenam os dados da database
                // Console.WriteLine("==================");
                // Console.WriteLine("Usuário (database): " + login.Usuario);
                // Console.WriteLine("Senha (database): " + login.Senha);
                // Console.WriteLine("Senha Criptografada (database): " + login.Senha);
                // Console.WriteLine("Id (database): " + login.Id);
                // Console.WriteLine("Nome (database): " + login.Nome);
                // Console.WriteLine("==================");
                // Console.WriteLine("");


                // Caso algum campo seja nulo, já emite o erro
                if (login.Usuario != null)
                {
                    // Caso o que foi escrito seja diferente do que foi encontrado na database, gera o mesmo erro. Portanto, se o cruzamento de dados for correto, o login é feito.
                    if (Equals(l.Usuario, login.Usuario) && Equals(login.Senha, senhaCrip))
                    {
                        HttpContext.Session.SetInt32("idLogin", login.Id);
                        HttpContext.Session.SetString("nomeLogin", login.Nome);
                        HttpContext.Session.SetString("usuarioLogin", login.Usuario);

                        // Debug
                        // Console.WriteLine("==================");
                        // Console.WriteLine("Login feito com sucesso");
                        // Console.WriteLine("==================");
                        // Console.WriteLine("");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Erro02"] = "Falha no login. Tente novamente.";
                        // Console.WriteLine("Erro de login_02");
                        return View();
                    }
                }
                else
                {
                    ViewData["Erro01"] = "Usuário incorreto. Tente novamente.";
                    // Console.WriteLine("Erro de login_01");
                    return View();
                }
            }
        }

        public IActionResult UserLogout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        // Manutenção de Usuários
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Login l)
        {
            LoginServices loginService = new LoginServices();

            if (l.Id == 0)
            {
                loginService.Inserir(l);
            }
            else
            {
                loginService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

        // Listagem dos usuarios
        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            LoginServices LoginService = new LoginServices();

            return View(LoginService.ListarTodos());
        }

        // Edição dos usuarios
        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LoginServices ls = new LoginServices();
            Login l = ls.ObterPorId(id);
            return View(l);
        }

        // Remoção de usuário
        public IActionResult Remover(int id)
        {
            Autenticacao.CheckLogin(this);
            LoginServices ls = new LoginServices();
            ls.Remover(id);
            return RedirectToAction("Listagem");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
