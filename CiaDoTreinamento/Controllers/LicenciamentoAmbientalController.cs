using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class LicenciamentoAmbientalController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoLicenciamentoAmbiental)
		{
			LicenciamentoAmbientalBLL BLL = new LicenciamentoAmbientalBLL();
			TelefoneLicenciamentoAmbientalBLL telefoneBLL = new TelefoneLicenciamentoAmbientalBLL();
			string mensagemErro;

			if (codigoLicenciamentoAmbiental != null && codigoLicenciamentoAmbiental != 0)
			{
				LicenciamentoAmbiental licenciamentoCorrente = BLL.getLicenciamentoAmbiental((int)codigoLicenciamentoAmbiental, "", out mensagemErro).FirstOrDefault();

				if (licenciamentoCorrente != null)
				{
					List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones = telefoneBLL.getTelefonesLicenciamentoTela(licenciamentoCorrente.Codigo, out mensagemErro);

					ViewBag.listaTelefones = JsonConvert.SerializeObject(telefones);
				}

				return View(licenciamentoCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtRazaoFiltro)
		{
			LicenciamentoAmbientalBLL BLL = new LicenciamentoAmbientalBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<LicenciamentoAmbiental> listaLicenciamentos = BLL.getLicenciamentoAmbiental(null, txtRazaoFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaLicenciamentos);
		}

		public IActionResult Delete(int? codigoLicenciamentoAmbiental)
		{
			LicenciamentoAmbientalBLL BLL = new LicenciamentoAmbientalBLL();
			string mensagemErro;

			if (codigoLicenciamentoAmbiental.HasValue)
			{
				if (BLL.deleteLicenciamentoAmbiental((int)codigoLicenciamentoAmbiental, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de licenciamento ambiental removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(LicenciamentoAmbiental licenciamento, string hfListaTelefones)
		{

			LicenciamentoAmbientalBLL BLL = new LicenciamentoAmbientalBLL();
			string mensagemErro;

			List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones = new List<TelefoneLicenciamentoAmbiental.TelefoneTela>();

			if (!String.IsNullOrEmpty(hfListaTelefones))
			{
				telefones = JsonConvert.DeserializeObject<List<TelefoneLicenciamentoAmbiental.TelefoneTela>>(hfListaTelefones);
			}

			if (licenciamento.Codigo == null)
			{
				if (BLL.insertLicenciamentoAmbiental(licenciamento, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de licenciamento ambiental cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				//ATUALIZAR AMBIENTE
				if (BLL.updateLicenciamentoAmbiental(licenciamento, telefones, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Empresa de licenciamento ambiental atualizada com sucesso!";
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