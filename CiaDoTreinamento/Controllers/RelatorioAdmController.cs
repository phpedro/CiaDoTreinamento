using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODE;
using Microsoft.AspNetCore.Mvc;

namespace CiaDoTreinamento.Controllers
{
    public class RelatorioAdmController : Controller
    {
        public IActionResult RelatorioAdmRota()
        {
            return View();
        }

        public IActionResult RelatorioAdmVistoria()
        {
            return View();
        }

        public IActionResult RelatorioAdmCorreio()
        {
            return View();
        }

        public IActionResult RelatorioAdmRotaConsulta(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                        int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                        DateTime? dtpDataFinalFechamentoPedido)
        {

            CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
            var mensagemErro = String.Empty;

            List<CabecalhoPedido> listaPedidos = BLL.BuscaPedidosAdmRota(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                    ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                TempData["mensagemErro"] = mensagemErro;
                return View("RelatorioAdmRota");
            }

            return View("RelatorioAdmRota", listaPedidos);
        }

        public IActionResult RelatorioAdmVistoriaConsulta(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                 int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                 DateTime? dtpDataFinalFechamentoPedido)
        {

            CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
            var mensagemErro = String.Empty;

            List<CabecalhoPedido> listaPedidos = BLL.BuscaPedidosAdmVistoria(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                    ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                TempData["mensagemErro"] = mensagemErro;
                return View("RelatorioAdmVistoria");
            }

            return View("RelatorioAdmVistoria", listaPedidos);
        }

        public IActionResult RelatorioAdmCorreioConsulta(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                DateTime? dtpDataFinalFechamentoPedido)
        {

            CabecalhoPedidoBLL BLL = new CabecalhoPedidoBLL();
            var mensagemErro = String.Empty;

            List<CabecalhoPedido> listaPedidos = BLL.BuscaPedidosAdmRota(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                    ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido, dtpDataFinalFechamentoPedido, out mensagemErro);

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                TempData["mensagemErro"] = mensagemErro;
                return View("RelatorioAdmCorreio");
            }

            return View("RelatorioAdmCorreio", listaPedidos);
        }

        [HttpPost]
        public JsonResult AtualizarStatusPedido(int codigoStatus, List<int> pedidos)
        {
            RoteirizacaoBLL roteirizacaoBLL = new RoteirizacaoBLL();
            CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
            NotificacoesBLL notificacoesBLL = new NotificacoesBLL();
            string mensagemErro;

            StatusNegociacao status = new StatusNegociacao(codigoStatus);

            var codigoUsuario = Convert.ToInt32(HttpContext.Request.Cookies["CODIGO_USUARIO"]);

            foreach (int pedido in pedidos)
            {
                if (!roteirizacaoBLL.updateStatusPedido(pedido, codigoStatus, out mensagemErro))
                {
                    return Json(new { sucesso = false, mensagemErro = mensagemErro });
                }

                //Grava Notificação
                CabecalhoPedido cabecalhoPedido = cabecalhoPedidoBLL.GetPedidoByCodigo(pedido, out mensagemErro);
                //Notificacoes notificacao = new Notificacoes();
                //notificacao.FuncionarioCriador = new Funcionario() { Codigo = codigoUsuario };
                //notificacao.FuncionarioDestino = new Funcionario() { Codigo = cabecalhoPedido.FuncionarioVendedor.Codigo };
                //notificacao.Mensagem = "Status do pedido " + cabecalhoPedido.Codigo + " atualizado para " + status.Descricao + "!";
                //notificacoesBLL.insertNotificacao(notificacao, out mensagemErro);
            }

            TempData["mensagemSucesso"] = "Pedidos atualizados com sucesso!";
            return Json(new { sucesso = true });
        }
    }
}