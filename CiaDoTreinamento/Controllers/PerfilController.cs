using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class PerfilController : Controller
    {
		public IActionResult List()
		{
			PerfilBLL BLL = new PerfilBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Perfil> listaPerfis = BLL.getPerfis(null, "", out mensagemErro);

			return View(listaPerfis);
		}

		public IActionResult Edit(int? codigoPerfil)
		{

			PerfilBLL BLL = new PerfilBLL();
			string mensagemErro;

			if (codigoPerfil != null && codigoPerfil != 0)
			{
				Perfil perfilCorrente = BLL.getPerfis((int)codigoPerfil, "", out mensagemErro).FirstOrDefault();

				return View(perfilCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoPerfil)
		{
			PerfilBLL BLL = new PerfilBLL();
			string mensagemErro;

			if (codigoPerfil.HasValue)
			{
				if (BLL.deletePerfil((int)codigoPerfil, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Perfil removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Perfil perfil)
		{

			PerfilBLL BLL = new PerfilBLL();
			string mensagemErro;


			if (perfil.Codigo == null)
			{
				if (BLL.insertPerfil(perfil, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Perfil cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updatePerfil(perfil, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Perfil atualizado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");
		}
	}
}