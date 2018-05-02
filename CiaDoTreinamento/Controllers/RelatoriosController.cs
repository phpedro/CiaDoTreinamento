using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CODE;

namespace CiaDoTreinamento.Controllers
{
    public class RelatoriosController : Controller
    {
        public IActionResult ClientesNaoAtendidos()
        {
            return View();
        }

		public IActionResult RelatorioPedidosNegadosPeloAdmVendas(int? ddlAgenteVendasFiltro, int? ddlInstrutorFiltro, string ddlEstadosFiltro, int? ddlCidadesFiltro,
																	int? ddlMesoFiltro, int? ddlMicroFiltro, string txtaRazaoNomeClienteFiltro, int? txtaCodigoPedidoFiltro,
																	int? ddlProdutosFiltro, DateTime? dtpDataInicioFechamentoPedido, DateTime? dtpDataFinalFechamentoPedido)
		{

			CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
			string mensagemErro;

			List<CabecalhoPedido> listaPedidos = BLL.BuscarPedidosNegadosPeloAdmVendas(ddlAgenteVendasFiltro, ddlInstrutorFiltro, txtaRazaoNomeClienteFiltro, ddlCidadesFiltro,
																						ddlEstadosFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, ddlMesoFiltro,
																						ddlMicroFiltro, txtaCodigoPedidoFiltro, ddlProdutosFiltro, out mensagemErro);

			if (!string.IsNullOrEmpty(mensagemErro))
			{
				TempData["mensagemErro"] = mensagemErro;
				return View("List");
			}

			return View(listaPedidos);
		}
	}
}