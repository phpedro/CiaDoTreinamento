using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class StatusNegociacaoController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoStatus)
		{
			StatusNegociacaoBLL BLL = new StatusNegociacaoBLL();
			string mensagemErro;

			if (codigoStatus != null && codigoStatus != 0)
			{
				StatusNegociacao statusCorrente = BLL.getStatusNegociacao((int)codigoStatus, "", out mensagemErro).FirstOrDefault();

				return View(statusCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro)
		{
			StatusNegociacaoBLL BLL = new StatusNegociacaoBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<StatusNegociacao> listaStatus = BLL.getStatusNegociacao(null, txtDescricaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaStatus);
		}

		public IActionResult Delete(int? codigoStatus)
		{
			StatusNegociacaoBLL BLL = new StatusNegociacaoBLL();
			string mensagemErro;

			if (codigoStatus.HasValue)
			{
				if (BLL.deleteStatusNegociacao((int)codigoStatus, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Status de negociação removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(StatusNegociacao status)
		{

			StatusNegociacaoBLL BLL = new StatusNegociacaoBLL();
			string mensagemErro;

			if (status.CodigoStatus == null)
			{
				if (BLL.insertStatusNegociacao(status, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Status de negociação cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateStatusNegociacao(status, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Status de negociação atualizada com sucesso!";
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