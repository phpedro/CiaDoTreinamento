using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ClienteBLL
    {
		public bool InsertCliente(Cliente cliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.InsertCliente(cliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool UpdateCliente(Cliente cliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.UpdateCliente(cliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool UpdateStatus(int codigoCliente, int CodigoStatus, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.UpdateStatus(codigoCliente, CodigoStatus, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o status do cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Cliente> GetClientes(int Codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.GetClientes(Codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cliente.ClienteTela> GetClientes(int? codigoCliente, string RazaoSocial, string CNPJ, int? CodigoPedido, string NomeCliente,
										string CpfCliente, string Estado, int? CodigoCidade, int? CodigoMicro, int? CodigoRede,
										string Email, string Telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.GetClientes(codigoCliente, RazaoSocial, CNPJ, CodigoPedido, NomeCliente,
												CpfCliente, Estado, CodigoCidade, CodigoMicro, CodigoRede,
												Email, Telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cliente.ClienteTela> GetClienteResumido(string busca, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.GetClienteResumido(busca, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cliente.ProdutoExpirando> GetProdutosExpirando(int codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.GetProdutosExpirando(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os produtos expirando do cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cliente.ProdutoExpirando> GetProdutosVencidos(int codigoCliente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				return ClienteDAL.GetProdutosVencidos(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os produtos vencidos do cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static List<ChaveValor> ObtemListaStatus()
		{
			try
			{
				return ClienteDAL.ObtemListaStatus();
			}
			catch (Exception ex)
			{
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public int getMaxCodigoPedido(int codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.getMaxCodigoPedido(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o código máximo da tabela de clientes. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}
		}

		public List<Cliente.ClienteTela> getClientesComProdutoExpirandoByAgenteVendas(int codigoAgenteVendas, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.getClientesComProdutoExpirandoByAgenteVendas(codigoAgenteVendas, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os clientes. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Cliente.ClienteRota> getClientesDetalheRota(int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ClienteDAL.getClientesDetalheRota(codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os clientes. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
