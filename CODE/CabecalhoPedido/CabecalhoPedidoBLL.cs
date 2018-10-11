using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class CabecalhoPedidoBLL
    {

        #region CONSULTAS
        public List<CabecalhoPedido.CabecalhoPedidoTela> GetPedidoByCliente(int? codigoCliente, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.GetPedidoByCliente(codigoCliente, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        public CabecalhoPedido GetPedidoByCodigo(int codigoPedido, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.GetPedidoByCodigo(codigoPedido, out mensagemErro)[0];
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        public string GetProdutosVendidosResumido(int codigoPedido, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.GetProdutosVendidosResumido(codigoPedido, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        public List<CabecalhoPedido> BuscarPedidosNegadosPeloAdmVendas(int? codigoAgenteVendas, int? codigoInstrutor, string razaoSocial, int? codigoCidade, string codigoEstado,
                                                                DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, int? codigoMeso, int? codigoMicro,
                                                                int? codigoPedido, int? codigoProduto, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.BuscarPedidosNegadosPeloAdmVendas(codigoAgenteVendas, codigoInstrutor, razaoSocial, codigoCidade, codigoEstado,
                                                                            dataInicioFechamentoPedido, dataFimFechamentoPedido, codigoMeso, codigoMicro,
                                                                            codigoPedido, codigoProduto, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        public List<CabecalhoPedido> BuscaPedidosAdmRota(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                        int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                        DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.BuscaPedidosAdmRota(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                        ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido,
                                                        dtpDataFinalFechamentoPedido, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }


        public List<CabecalhoPedido> BuscaPedidosAdmVistoria(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                        int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                        DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.BuscaPedidosAdmVistoria(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                        ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido,
                                                        dtpDataFinalFechamentoPedido, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        public List<CabecalhoPedido> BuscaPedidosAdmCorreio(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.BuscaPedidosAdmCorreio(txtCnpjFiltro, txtCodigoPedidoFiltro, txtRazaoSocialFiltro, ddlAgenteVendasFiltro,
                                                        ddlInstrutorFiltro, ddlEstadosFiltro, ddlCidadesFiltro, dtpDataInicioFechamentoPedido,
                                                        dtpDataFinalFechamentoPedido, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return null;
            }
        }

        #endregion

        #region INSERT
        public bool insertCabecalhoPedido(CabecalhoPedido cabecalho, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.insertCabecalhoPedido(cabecalho, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return false;
            }
        }
        #endregion

        #region UPDATE
        public bool updateCabecalhoPedido(CabecalhoPedido cabecalho, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.updateCabecalhoPedido(cabecalho, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return false;
            }
        }

        public bool updateEncargosItensPedidos(int codigoPedido, bool cobrarEncargos, out string mensagemErro)
        {
            mensagemErro = "";
            try
            {
                return CabecalhoPedidoDAL.updateEncargosItensPedidos(codigoPedido, cobrarEncargos, out mensagemErro);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
                return false;
            }
        }

        public bool updateCabecalhoPedidoTodo(int codigoPedido, out string mensagemErro)
        {
            try
            {
                ItemPedidoBLL itemPedidoBLL = new ItemPedidoBLL();

                //BUSCAR PEDIDO
                CabecalhoPedido cab = this.GetPedidoByCodigo(codigoPedido, out mensagemErro);
                if (!String.IsNullOrEmpty(mensagemErro))
                {
                    return false;
                }

                //BUSCAR OS ITENS DO PEDIDO
                List<ItemPedido> itens = itemPedidoBLL.getItemPedido(null, codigoPedido, out mensagemErro);
                if (!String.IsNullOrEmpty(mensagemErro))
                {
                    return false;
                }

                //CALCULAR VALOR TOTAL PEDIDO
                decimal valorTotalProduto = 0;
                decimal valorTotalVendido = 0;

                foreach (ItemPedido item in itens)
                {
                    if (cab.CobrarISS)
                    {
                        valorTotalProduto += (item.Quantidade * item.Produto.ValorPorPessoa) + item.ValorEncargos;
                        valorTotalVendido += (item.Quantidade * item.valorFinal) + item.ValorEncargos;
                    }
                    else
                    {
                        valorTotalProduto += (item.Quantidade * item.Produto.ValorPorPessoa);
                        valorTotalVendido += (item.Quantidade * item.valorFinal);
                    }
                }

                cab.ValorTotal = valorTotalVendido;

                if (valorTotalProduto > valorTotalVendido)
                {
                    cab.ValorDesconto = valorTotalProduto - valorTotalVendido;
                    cab.PercentualDesconto = 100 - ((valorTotalVendido / valorTotalProduto) * 100);
                }
                else
                {
                    cab.ValorDesconto = 0;
                    cab.PercentualDesconto = 0;
                }

                if (!this.updateCabecalhoPedido(cab, out mensagemErro))
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
                return false;
            }

            return true;

        }


    }
}
#endregion