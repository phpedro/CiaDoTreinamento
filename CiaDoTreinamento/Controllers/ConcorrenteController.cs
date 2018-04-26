using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class ConcorrenteController : Controller
    {
        public IActionResult List()
        {
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoConcorrente)
		{
			ConcorrenteBLL BLL = new ConcorrenteBLL();
			TelefoneConcorrenteBLL telefoneBLL = new TelefoneConcorrenteBLL();
			string mensagemErro;

			if (codigoConcorrente != null && codigoConcorrente != 0)
			{
				Concorrente concorrenteCorrente = BLL.getConcorrentes((int)codigoConcorrente, "", out mensagemErro).FirstOrDefault();

				if (concorrenteCorrente != null)
				{
					List<TelefoneConcorrente.TelefoneTela> telefones = telefoneBLL.getTelefonesConcorrenteTela(concorrenteCorrente.Codigo, out mensagemErro);

					ViewBag.listaTelefones = JsonConvert.SerializeObject(telefones);
				}

				return View(concorrenteCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtRazaoFiltro)
		{
			ConcorrenteBLL BLL = new ConcorrenteBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Concorrente> listaConcorrentes = BLL.getConcorrentes(null, txtRazaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaConcorrentes);
		}

		public IActionResult Delete(int? codigoConcorrente)
		{
			ConcorrenteBLL BLL = new ConcorrenteBLL();
			string mensagemErro;

			if (codigoConcorrente.HasValue)
			{
				if (BLL.deleteConcorrente((int)codigoConcorrente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Concorrente removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Concorrente concorrente, string hfListaTelefones)
		{

			ConcorrenteBLL BLL = new ConcorrenteBLL();
			string mensagemErro;

			List<TelefoneConcorrente.TelefoneTela> telefones = new List<TelefoneConcorrente.TelefoneTela>();

			if (!String.IsNullOrEmpty(hfListaTelefones))
			{
				telefones = JsonConvert.DeserializeObject<List<TelefoneConcorrente.TelefoneTela>>(hfListaTelefones);
			}

			if (concorrente.Codigo == null)
			{
				if (BLL.insertConcorrente(concorrente, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Concorrente cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				//ATUALIZAR AMBIENTE
				if (BLL.updateConcorrente(concorrente, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Concorrente atualizado com sucesso!";
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