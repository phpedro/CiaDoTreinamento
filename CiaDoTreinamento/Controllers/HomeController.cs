using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CiaDoTreinamento.Models;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			int codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);
			ClienteBLL clienteBLL = new ClienteBLL();
			List<Cliente.ClienteTela> listaClientes = new List<Cliente.ClienteTela>();
			string mensagemErro = "";

			//VERIFICAR COM PESSOAL DE VENDAS O QUE DEVERÁ TRAZER NO GRID
			//listaClientes = clienteBLL.getClientesComProdutoExpirandoByAgenteVendas(codigoUsuario, out mensagemErro);

			if (!String.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
			}

			return View(listaClientes);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
