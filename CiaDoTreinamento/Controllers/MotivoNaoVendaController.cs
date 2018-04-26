using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class MotivoNaoVendaController : Controller
    {
		public IActionResult List()
		{
			MotivoNaoVendaBLL BLL = new MotivoNaoVendaBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<MotivoNaoVenda> listaMotivos = BLL.getMotivosNaoVenda(null, "", out mensagemErro);

			return View(listaMotivos);
		}

		public IActionResult Edit(int? codigoMotivo)
		{

			MotivoNaoVendaBLL BLL = new MotivoNaoVendaBLL();
			string mensagemErro;

			if (codigoMotivo != null && codigoMotivo != 0)
			{
				MotivoNaoVenda motivoCorrente = BLL.getMotivosNaoVenda((int)codigoMotivo, "", out mensagemErro).FirstOrDefault();

				return View(motivoCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoMotivo)
		{
			MotivoNaoVendaBLL BLL = new MotivoNaoVendaBLL();
			string mensagemErro;

			if (codigoMotivo.HasValue)
			{
				if (BLL.deleteMotivoNaoVenda((int)codigoMotivo, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Motivo removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(MotivoNaoVenda motivo)
		{

			MotivoNaoVendaBLL BLL = new MotivoNaoVendaBLL();
			string mensagemErro;


			if (motivo.Codigo == null)
			{
				if (BLL.insertMotivoNaoVenda(motivo, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Motivo cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateMotivoNaoVenda(motivo, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Motivo atualizado com sucesso!";
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