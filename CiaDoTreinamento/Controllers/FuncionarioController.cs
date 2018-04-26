using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Microsoft.AspNetCore.Http;

namespace CiaDoTreinamento.Controllers
{
    public class FuncionarioController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			string mensagemErro;
			List<Funcionario> listaFuncionarios = FuncionarioBLL.getAllFuncionarios(out mensagemErro);

			return View(listaFuncionarios);
		}

		public IActionResult Edit(int? codigoFuncionario)
		{
			string mensagemErro;

			if (codigoFuncionario != null && codigoFuncionario != 0)
			{
				Funcionario funcionarioCorrente = FuncionarioBLL.getFuncionarioByCodigo((int)codigoFuncionario, out mensagemErro);

				return View(funcionarioCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoFuncionario)
		{
			FuncionarioBLL BLL = new FuncionarioBLL();
			string mensagemErro;

			if (codigoFuncionario.HasValue)
			{
				if (BLL.DeleteFuncionario((int)codigoFuncionario, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Funcionário removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Funcionario funcionario)
		{

			FuncionarioBLL BLL = new FuncionarioBLL();
			string mensagemErro;

			if (funcionario.Perfil == null)
			{
				TempData["mensagemErro"] = "Informe o perfil do funcionário!";
				funcionario.Senha = "";
				return View("Edit", funcionario);
			}

			funcionario.Senha = Uteis.GeraHashMD5(funcionario.Senha);

			if (funcionario.Codigo == null)
			{
				if (BLL.InserFuncionario(funcionario, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Funcionário cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.UpdateFuncionario(funcionario, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Funcionário atualizado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			if (funcionario.Codigo.ToString() == HttpContext.Request.Cookies["CODIGO_USUARIO"])
			{
				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddHours(6);
				HttpContext.Response.Cookies.Delete("CODIGO_PERFIL");
				HttpContext.Response.Cookies.Append("CODIGO_PERFIL", funcionario.Perfil.Codigo.ToString(), options);
			}

			return RedirectToAction("List");
		}
	}
}