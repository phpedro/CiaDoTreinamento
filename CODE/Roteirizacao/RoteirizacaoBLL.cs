using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RoteirizacaoBLL
    {
		#region UPDATE

		public bool updatePedidoRota(int codigoPedido, int codigoInstrutor, string salaTreinamento, string informacoesParaTreinamento,
										DateTime dataInicioTreinamento, DateTime dataFimTreinamento, int codigoStatus, out string mensagemErro, string detalheRetornoPedido = "")
		{
			mensagemErro = "";
			try
			{
				return RoteirizacaoDAL.updatePedidoRota(codigoPedido, codigoInstrutor, salaTreinamento, informacoesParaTreinamento,
															dataInicioTreinamento, dataFimTreinamento, codigoStatus, detalheRetornoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateStatusPedido(int codigoPedido, int codigoStatus, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return RoteirizacaoDAL.updateStatusPedido(codigoPedido, codigoStatus, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}
		#endregion

		#region CONSULTAS

		public List<CabecalhoPedido> BuscarPedidosRoteirizacao(int? codigoAgenteVendas, int? codigoInstrutor, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, int? codigoMeso, int? codigoMicro,
																int? codigoPedido, int? codigoProduto, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RoteirizacaoDAL.BuscarPedidosRoteirizacao(codigoAgenteVendas, codigoInstrutor, razaoSocial, codigoCidade, codigoEstado,
																	dataInicioFechamentoPedido, dataFimFechamentoPedido, codigoMeso, codigoMicro,
																	codigoPedido, codigoProduto, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os pedidos para roteirização. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<CabecalhoPedido> BuscarPedidosCorreio(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RoteirizacaoDAL.BuscarPedidosCorreio(codigoAgenteVendas, razaoSocial, codigoCidade, codigoEstado,
																	dataInicioFechamentoPedido, dataFimFechamentoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os pedidos para roteirização. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<CabecalhoPedido> BuscarPedidosPendenteRota(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RoteirizacaoDAL.BuscarPedidosPendenteRota(codigoAgenteVendas, razaoSocial, codigoCidade, codigoEstado,
																	dataInicioFechamentoPedido, dataFimFechamentoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os pedidos para roteirização. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<CabecalhoPedido> BuscarPedidosPendenteVistoria(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RoteirizacaoDAL.BuscarPedidosPendenteVistoria(codigoAgenteVendas, razaoSocial, codigoCidade, codigoEstado,
																	dataInicioFechamentoPedido, dataFimFechamentoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os pedidos para roteirização. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<CabecalhoPedido> BuscarPedidosRoteirizacao(string codigoEstado, int? cidade, int? meso, int? micro, int? produto, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RoteirizacaoDAL.BuscarPedidosRoteirizacao(codigoEstado, cidade, meso, micro, produto, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os pedidos para roteirização. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		#endregion
	}
}
