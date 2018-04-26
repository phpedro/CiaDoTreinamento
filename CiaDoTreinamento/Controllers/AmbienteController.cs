using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CiaDoTreinamento.Controllers
{
    public class AmbienteController : Controller
    {
        public IActionResult List()
        {
			AmbienteBLL BLL = new AmbienteBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}
			
			List<Ambiente> listaAmbientes = BLL.getAmbientes(null, "", out mensagemErro);

			return View(listaAmbientes);
        }

		public IActionResult Edit(int? codigoAmbiente)
		{

			AmbienteBLL BLL = new AmbienteBLL();
			string mensagemErro;

			if (codigoAmbiente != null && codigoAmbiente != 0)
			{
				Ambiente ambienteCorrente = BLL.getAmbientes((int)codigoAmbiente, "", out mensagemErro).FirstOrDefault();

				return View(ambienteCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoAmbiente)
		{
			AmbienteBLL BLL = new AmbienteBLL();
			string mensagemErro;

			if (codigoAmbiente.HasValue)
			{
				if (BLL.deleteAmbiente((int)codigoAmbiente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Ambiente removido com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Ambiente ambiente)
		{

			AmbienteBLL BLL = new AmbienteBLL();
			string mensagemErro;


			if (ambiente.Codigo == null)
			{
				//INSERT NOVO AMBIENTES
				if (BLL.insertAmbiente(ambiente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Ambiente cadastrado com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				//ATUALIZAR AMBIENTE
				if (BLL.updateAmbiente(ambiente, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Ambiente atualizado com sucesso!";
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