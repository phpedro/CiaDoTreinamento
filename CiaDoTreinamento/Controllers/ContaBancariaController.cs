using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class ContaBancariaController : Controller
    {
		public IActionResult List()
		{
			if (HttpContext.Request.Cookies["USUARIO"] == null)
			{
				return RedirectToAction("Login", "Login", new { urlRetorno = HttpContext.Request.Path });
			}

			ContaBancariaBLL BLL = new ContaBancariaBLL();
			string mensagemErro;
			List<ContaBancaria> listaContas = BLL.getContas(null, out mensagemErro);

			return View(listaContas);
		}

		public IActionResult Edit(int? codigoConta)
		{
			ContaBancariaBLL BLL = new ContaBancariaBLL();
			string mensagemErro;

			if (codigoConta != null && codigoConta != 0)
			{
				ContaBancaria contaCorrente = BLL.getContas((int)codigoConta, out mensagemErro).FirstOrDefault();

				return View(contaCorrente);
			}
			else
			{
				return View();
			}
		}

		public IActionResult Delete(int? codigoConta)
		{
			ContaBancariaBLL BLL = new ContaBancariaBLL();
			string mensagemErro;

			if (codigoConta.HasValue)
			{
				if (BLL.deleteContaBancaria((int)codigoConta, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Conta bancária removida com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}

			return RedirectToAction("List");

		}

		public IActionResult Salvar(ContaBancaria conta)
		{

			ContaBancariaBLL BLL = new ContaBancariaBLL();
			string mensagemErro;

			List<ContaBancaria> contas = BLL.getContas(conta.Codigo, out mensagemErro);

			if (contas.Count == 0)
			{
				conta.Ativo = true;
				conta.CodigoContaASC = 0;
				conta.CodigoCondicaoASC = 0;
				conta.IncrementalBoletos = 0;
				conta.IncrementalRemessa = 0;

				if (BLL.insertContaBancaria(conta, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Conta bancária cadastrada com sucesso!";
				}
				else
				{
					TempData["mensagemErro"] = mensagemErro;
				}
			}
			else
			{
				if (BLL.updateContaBancaria(conta, out mensagemErro))
				{
					TempData["mensagemSucesso"] = "Conta bancária atualizada com sucesso!";
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