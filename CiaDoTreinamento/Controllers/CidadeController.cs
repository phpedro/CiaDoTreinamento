using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CiaDoTreinamento.Controllers
{
    public class CidadeController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			return View();
		}

		public IActionResult Edit(int? codigoCidade)
		{
			CidadeBLL BLL = new CidadeBLL();
			string mensagemErro;

			if (codigoCidade != null && codigoCidade != 0)
			{
				Cidade cidadeCorrente = BLL.getCidade((int)codigoCidade, "", null, null, out mensagemErro).FirstOrDefault();

				return View(cidadeCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Consultar(string txtDescricaoFiltro, int txtMesoFiltro, int txtMicroFiltro)
		{
			CidadeBLL BLL = new CidadeBLL();
			string mensagemErro;

			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			List<Cidade> listaCidades = BLL.getCidade(null, txtDescricaoFiltro, txtMesoFiltro, txtMicroFiltro, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View("List", listaCidades);
		}

		public IActionResult Delete(int? codigoCidade)
		{
			CidadeBLL BLL = new CidadeBLL();
			string mensagemErro;

			if (codigoCidade.HasValue)
			{
				if (BLL.deleteCidade((int)codigoCidade, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Cidade removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(Cidade cidade)
		{

			CidadeBLL BLL = new CidadeBLL();
			string mensagemErro;

			if (cidade.Codigo == null)
			{
				if (BLL.insertCidade(cidade, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Cidade cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateCidade(cidade, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Cidade atualizada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");
		}

		public JsonResult GetCidade(string Estado)
		{

			string mensagemErro;
			CidadeBLL BLL = new CidadeBLL();

			List<Cidade> cidades = BLL.getCidadeByEstado(Estado, out mensagemErro);
			List<SelectListItem> listaCidades = new List<SelectListItem>();

			foreach (Cidade item in cidades)
			{

				listaCidades.Add(new SelectListItem()
				{
					Value = item.Codigo.ToString(),
					Text = item.Descricao
				});
			}

			return Json(listaCidades);
		}
	}
}