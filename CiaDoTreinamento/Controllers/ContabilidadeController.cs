using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class ContabilidadeController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoContabilidade)
		{
			ContabilidadeBLL BLL = new ContabilidadeBLL();
			TelefoneContabilidadeBLL telefoneBLL = new TelefoneContabilidadeBLL();
			string mensagemErro;

			if (codigoContabilidade != null && codigoContabilidade != 0)
			{
				Contabilidade contabilidadeCorrente = BLL.getContabilidades((int)codigoContabilidade, "", out mensagemErro).FirstOrDefault();

				if (contabilidadeCorrente != null)
				{
					List<TelefoneContabilidade.TelefoneTela> telefones = telefoneBLL.getTelefonesContabilidadeTela(contabilidadeCorrente.Codigo, out mensagemErro);

					ViewBag.listaTelefones = JsonConvert.SerializeObject(telefones);
				}

				return View(contabilidadeCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtRazaoFiltro)
		{
			ContabilidadeBLL BLL = new ContabilidadeBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Contabilidade> listaContabilidades = BLL.getContabilidades(null, txtRazaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaContabilidades);
		}

		public IActionResult Delete(int? codigoContabilidade)
		{
			ContabilidadeBLL BLL = new ContabilidadeBLL();
			string mensagemErro;

			if (codigoContabilidade.HasValue)
			{
				if (BLL.deleteContabilidade((int)codigoContabilidade, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de contabilidade removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Contabilidade contabilidade, string hfListaTelefones)
		{

			ContabilidadeBLL BLL = new ContabilidadeBLL();
			string mensagemErro;

			List<TelefoneContabilidade.TelefoneTela> telefones = new List<TelefoneContabilidade.TelefoneTela>();

			if (!String.IsNullOrEmpty(hfListaTelefones))
			{
				telefones = JsonConvert.DeserializeObject<List<TelefoneContabilidade.TelefoneTela>>(hfListaTelefones);
			}

			if (contabilidade.Codigo == null)
			{
				if (BLL.insertContabilidade(contabilidade, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de contabilidade cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				//ATUALIZAR AMBIENTE
				if (BLL.updateContabilidade(contabilidade, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de contabilidade atualizada com sucesso!";
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