using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class MesoController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoMeso)
		{
			MesoBLL BLL = new MesoBLL();
			string mensagemErro;

			if (codigoMeso != null && codigoMeso != 0)
			{
				Meso mesoCorrente = BLL.getMesos((int)codigoMeso, "", out mensagemErro).FirstOrDefault();

				return View(mesoCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro)
		{
			MesoBLL BLL = new MesoBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Meso> listaMesos = BLL.getMesos(null, txtDescricaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaMesos);
		}

		public IActionResult Delete(int? codigoMeso)
		{
			MesoBLL BLL = new MesoBLL();
			string mensagemErro;

			if (codigoMeso.HasValue)
			{
				if (BLL.deleteMeso((int)codigoMeso, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Meso removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Meso meso)
		{

			MesoBLL BLL = new MesoBLL();
			string mensagemErro;

			if (meso.Codigo == null)
			{
				if (BLL.insertMeso(meso, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Meso cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateMeso(meso, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Meso atualizada com sucesso!";
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