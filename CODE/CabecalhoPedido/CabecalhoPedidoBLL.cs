using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class CabecalhoPedidoBLL
    {

		//CONSULTAS
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

		//INSERT
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

		//UPDATE
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

		
	}
}
