using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class MicroController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoMicro)
		{
			MicroBLL BLL = new MicroBLL();
			string mensagemErro;

			if (codigoMicro != null && codigoMicro != 0)
			{
				Micro microCorrente = BLL.getMicros((int)codigoMicro, "", out mensagemErro).FirstOrDefault();

				return View(microCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro)
		{
			MicroBLL BLL = new MicroBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Micro> listaMicros = BLL.getMicros(null, txtDescricaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaMicros);
		}

		public IActionResult Delete(int? codigoMicro)
		{
			MicroBLL BLL = new MicroBLL();
			string mensagemErro;

			if (codigoMicro.HasValue)
			{
				if (BLL.deleteMicro((int)codigoMicro, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Micro removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Micro micro)
		{

			MicroBLL BLL = new MicroBLL();
			string mensagemErro;

			if (micro.Codigo == null)
			{
				if (BLL.insertMicro(micro, out mensagemErro))
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
				if (BLL.updateMicro(micro, out mensagemErro))
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