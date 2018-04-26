using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class ParceiroController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoParceiro)
		{
			ParceiroBLL BLL = new ParceiroBLL();
			TelefoneParceiroBLL telefoneBLL = new TelefoneParceiroBLL();
			string mensagemErro;

			if (codigoParceiro != null && codigoParceiro != 0)
			{
				Parceiro parceiroCorrente = BLL.getParceiros((int)codigoParceiro, "", "", null, "", out mensagemErro).FirstOrDefault();

				if (parceiroCorrente != null)
				{
					List<TelefoneParceiro.TelefoneTela> telefones = telefoneBLL.getTelefonesParceiroTela(parceiroCorrente.Codigo, out mensagemErro);

					ViewBag.listaTelefones = JsonConvert.SerializeObject(telefones);
				}

				return View(parceiroCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro, string ddlTipoParceiroFiltro, string ddlEstadoFiltro, int? ddlCidadeFiltro)
		{
			ParceiroBLL BLL = new ParceiroBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Parceiro> listaParceiros = BLL.getParceiros(null, txtDescricaoFiltro, ddlEstadoFiltro, ddlCidadeFiltro, ddlTipoParceiroFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaParceiros);
		}

		public IActionResult Delete(int? codigoParceiro)
		{
			ParceiroBLL BLL = new ParceiroBLL();
			string mensagemErro;

			if (codigoParceiro.HasValue)
			{
				if (BLL.deleteParceiro((int)codigoParceiro, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Parceiro removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Parceiro parceiro, string hfListaTelefones)
		{

			ParceiroBLL BLL = new ParceiroBLL();
			string mensagemErro;

			List<TelefoneParceiro.TelefoneTela> telefones = new List<TelefoneParceiro.TelefoneTela>();

			if (!String.IsNullOrEmpty(hfListaTelefones))
			{
				telefones = JsonConvert.DeserializeObject<List<TelefoneParceiro.TelefoneTela>>(hfListaTelefones);
			}

			if (parceiro.Codigo == null)
			{
				if (BLL.insertParceiro(parceiro, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Parceiro cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateParceiro(parceiro, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Parceiro atualizado com sucesso!";
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