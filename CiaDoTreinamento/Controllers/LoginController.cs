using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class LoginController : Controller
    {
		public IActionResult Login(string urlRetorno)
		{
			ViewBag.urlRetorno = urlRetorno;

			return View();
		}

		public IActionResult Logar(Login login, string urlRetorno)
		{
			if (ModelState.IsValid)
			{
				string mensagemErro;
				Funcionario func;

				if (!LoginBLL.ValidaUsuario(login.Usuario, login.Senha, out func, out mensagemErro))
				{
					ViewBag.Message = mensagemErro;
				}
				else
				{
					//Usuário autenticado com sucesso
					CookieOptions options = new CookieOptions();
					options.Expires = DateTime.Now.AddHours(6);
					HttpContext.Response.Cookies.Append("USUARIO", login.Usuario, options);
					HttpContext.Response.Cookies.Append("CODIGO_USUARIO", func.Codigo.ToString(), options);
					HttpContext.Response.Cookies.Append("CODIGO_PERFIL", func.Perfil.Codigo.ToString(), options);
					HttpContext.Response.Cookies.Append("FUNCIONARIO", JsonConvert.SerializeObject(func), options);

					if (!String.IsNullOrEmpty(urlRetorno))
					{
						return Redirect(urlRetorno);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}

			return View("Login");
		}

		public IActionResult EsqueceuSenha(string Usuario)
		{

			if (String.IsNullOrEmpty(Usuario))
			{
				ViewBag.Message = "Informe o nome de usuário!";
			}
			else
			{
				string mensagemErro;

				Pessoa pessoa = PessoaBLL.getPessoaByLogin(Usuario, out mensagemErro);

				if (pessoa != null)
				{

					string newSenha = Uteis.GeraHashMD5(DateTime.Now.ToString("yyyyMMddhhmmss")).Substring(0, 6);

					bool retornoUpdate = PessoaBLL.updateSenhaPessoa((int)pessoa.Codigo, Uteis.GeraHashMD5(newSenha));

					if (retornoUpdate)
					{
						//Montar template de email
						string templateEmail = System.IO.File.ReadAllText("./wwwRoot/Templates/TemplateEmailEsqueciSenha.html");

						templateEmail = templateEmail.Replace("@user", pessoa.Nome)
														.Replace("@password", newSenha);

						string retornoEmail = Uteis.SendMail("Cia do Treinamento", pessoa.Email, "Cia do Treinamento - Esqueceu a senha!", templateEmail);

						ViewBag.MessageSucess = "Um email foi enviado com a nova senha para: " + pessoa.Email;
					}
					else
					{
						ViewBag.Message = "Não foi possível criar uma nova senha!";
					}
				}
				else
				{
					ViewBag.Message = mensagemErro;
				}
			}

			return View("Login");
		}

		public IActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete("USUARIO");
			HttpContext.Response.Cookies.Delete("CODIGO_USUARIO");
			HttpContext.Response.Cookies.Delete("CODIGO_PERFIL");

			return RedirectToAction("Login");
		}
	}
}