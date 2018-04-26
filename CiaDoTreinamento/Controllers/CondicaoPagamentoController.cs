using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class CondicaoPagamentoController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoCondicao)
		{
			CondicaoPagamentoBLL BLL = new CondicaoPagamentoBLL();
			ParcelamentoCondicaoBLL parcelaBLL = new ParcelamentoCondicaoBLL();
			string mensagemErro;

			if (codigoCondicao != null && codigoCondicao != 0)
			{
				CondicaoPagamento condicaoCorrente = BLL.getCondicoes((int)codigoCondicao, "", out mensagemErro).FirstOrDefault();

				if (condicaoCorrente != null)
				{
					List<ParcelamentoCondicao.ParcelaTela> parcelas = parcelaBLL.getParcelasTela((int)condicaoCorrente.Codigo, out mensagemErro);

					ViewBag.listaParcelas = JsonConvert.SerializeObject(parcelas);
				}

				return View(condicaoCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro)
		{
			CondicaoPagamentoBLL BLL = new CondicaoPagamentoBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<CondicaoPagamento> listaCondicoes = BLL.getCondicoes(null, txtDescricaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaCondicoes);
		}

		public IActionResult Delete(int? codigoCondicao)
		{
			CondicaoPagamentoBLL BLL = new CondicaoPagamentoBLL();
			string mensagemErro;

			if (codigoCondicao.HasValue)
			{
				if (BLL.deleteCondicaoPagamento((int)codigoCondicao, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Condição de pagamento removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(CondicaoPagamento condicao, string hfListaParcelas)
		{

			CondicaoPagamentoBLL BLL = new CondicaoPagamentoBLL();
			string mensagemErro;

			List<ParcelamentoCondicao.ParcelaTela> parcelas = new List<ParcelamentoCondicao.ParcelaTela>();

			if (!String.IsNullOrEmpty(hfListaParcelas))
			{
				parcelas = JsonConvert.DeserializeObject<List<ParcelamentoCondicao.ParcelaTela>>(hfListaParcelas);
			}

			if (condicao.Codigo == null)
			{
				if (BLL.insertCondicaoPagamento(condicao, parcelas, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Condição de pagamento cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateCondicaoPagamento(condicao, parcelas, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Condição de pagamento atualizada com sucesso!";
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