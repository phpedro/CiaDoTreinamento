using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class RedePostoController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoRede)
		{
			RedePostoBLL BLL = new RedePostoBLL();
			string mensagemErro;

			if (codigoRede != null && codigoRede != 0)
			{
				RedePosto redeCorrente = BLL.getRedes((int)codigoRede, "", out mensagemErro).FirstOrDefault();

				return View(redeCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro)
		{
			RedePostoBLL BLL = new RedePostoBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<RedePosto> listaRedes = BLL.getRedes(null, txtDescricaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaRedes);
		}

		public IActionResult Delete(int? codigoRede)
		{
			RedePostoBLL BLL = new RedePostoBLL();
			string mensagemErro;

			if (codigoRede.HasValue)
			{
				if (BLL.deleteRedePosto((int)codigoRede, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Rede de posto removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(RedePosto rede)
		{

			RedePostoBLL BLL = new RedePostoBLL();
			string mensagemErro;

			if (rede.Codigo == null)
			{
				if (BLL.insertRedePosto(rede, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Rede de posto cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateRedePosto(rede, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Rede de posto atualizada com sucesso!";
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