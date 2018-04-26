using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class BandeiraPostoController : Controller
    {
		public IActionResult List()
		{
			BandeiraPostoBLL BLL = new BandeiraPostoBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<BandeiraPosto> listaBandeiras = BLL.getBandeiras(null, "", out mensagemErro);

			return View(listaBandeiras);
		}

		public IActionResult Edit(int? codigoBandeira)
		{

			BandeiraPostoBLL BLL = new BandeiraPostoBLL();
			string mensagemErro;

			if (codigoBandeira != null && codigoBandeira != 0)
			{
				BandeiraPosto bandeiraCorrente = BLL.getBandeiras((int)codigoBandeira, "", out mensagemErro).FirstOrDefault();

				return View(bandeiraCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoBandeira)
		{
			BandeiraPostoBLL BLL = new BandeiraPostoBLL();
			string mensagemErro;

			if (codigoBandeira.HasValue)
			{
				if (BLL.deleteBandeiraPosto((int)codigoBandeira, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Bandeira removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(BandeiraPosto bandeira)
		{

			BandeiraPostoBLL BLL = new BandeiraPostoBLL();
			string mensagemErro;


			if (bandeira.Codigo == null)
			{
				if (BLL.insertBandeiraPosto(bandeira, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Bandeira cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateBandeiraPosto(bandeira, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Bandeira atualizada com sucesso!";
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