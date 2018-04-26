using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class CabecalhoOrcamentoDAL
    {

		public static bool GetInsertCabecalhoOrcamento(CabecalhoOrcamento cabecalhoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO CABECALHOS_ORCAMENTO");
				sql.AppendLine("	(CODIGO_CLIENTE, DATA_CRIACAO, CODIGO_FUNC_CRIADOR, CODIGO_CONDICAO,");
				sql.AppendLine("		CODIGO_STATUS, VALIDADE_ORCAMENTO, VALOR_TOTAL, TELEFONE_CONTATO, DATA_EXPIRACAO)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + cabecalhoOrcamento.Cliente.Codigo + "','" + cabecalhoOrcamento.DataCriacao.ToString("yyyy-MM-dd") + "','" + cabecalhoOrcamento.FuncionarioVendedor.Codigo + "','" + cabecalhoOrcamento.CondicaoPagamento.Codigo + "', ");
				sql.AppendLine("	'" + cabecalhoOrcamento.StatusOrcamento.Codigo + "','" + cabecalhoOrcamento.ValidadeOrcamento + "','" + cabecalhoOrcamento.ValorOrcamento.ToString().Replace(",",".") + "','" + cabecalhoOrcamento.TelefoneContato + "' ,'" + cabecalhoOrcamento.DataExpiracao.ToString("yyyy-MM-dd") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					cabecalhoOrcamento.Codigo = retorno;

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o orçamento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static bool GetUpdateCabecalhoOrcamento(CabecalhoOrcamento cabecalhoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CABECALHOS_ORCAMENTO");
				sql.Append("	SET");
				sql.Append("	CODIGO_CLIENTE = '" + cabecalhoOrcamento.Cliente.Codigo + "',");
				sql.Append("	CODIGO_FUNC_CRIADOR = '" + cabecalhoOrcamento.FuncionarioVendedor.Codigo + "',");
				sql.Append("	CODIGO_CONDICAO = '" + cabecalhoOrcamento.CondicaoPagamento.Codigo + "',");
				sql.Append("	CODIGO_STATUS = '" + cabecalhoOrcamento.StatusOrcamento.Codigo + "',");
				sql.Append("	VALIDADE_ORCAMENTO = '" + cabecalhoOrcamento.ValidadeOrcamento + "',");
				sql.Append("	VALOR_TOTAL = '" + cabecalhoOrcamento.ValorOrcamento.ToString().Replace(",",".") + "',");
				sql.Append("	TELEFONE_CONTATO = '" + cabecalhoOrcamento.TelefoneContato + "',");
				sql.Append("	DATA_EXPIRACAO = '" + cabecalhoOrcamento.DataExpiracao.ToString("yyyy-MM-dd") + "'");
				sql.Append("	WHERE CODIGO = " + cabecalhoOrcamento.Codigo);

				cmd.CommandText = sql.ToString();
				

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o orçamento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static List<CabecalhoOrcamento.CabecalhoOrcamentoTela> GetPedidoByCliente(int? codigoCliente, out string mensagemErro)
		{
			List<CabecalhoOrcamento.CabecalhoOrcamentoTela> listaPedidos = new List<CabecalhoOrcamento.CabecalhoOrcamentoTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CC.CODIGO, CC.DATA_CRIACAO, CC.DATA_EXPIRACAO, PE.NOME, SN.DESCRICAO, CC.VALOR_TOTAL");
			sql.Append("    FROM CABECALHOS_ORCAMENTO AS CC");
			sql.Append("        INNER JOIN PESSOAS AS PE ON CC.CODIGO_FUNC_CRIADOR = PE.CODIGO");
			sql.Append("        INNER JOIN STATUS_ORCAMENTO AS SN ON CC.CODIGO_STATUS = SN.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigoCliente != null && codigoCliente != 0)
			{
				sql.Append("	AND CC.CODIGO_CLIENTE = " + codigoCliente);
			}

			sql.Append("    ORDER BY CC.DATA_CRIACAO DESC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoOrcamento.CabecalhoOrcamentoTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						DataCadastro = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						DataExpiracao = Convert.ToDateTime(linha["DATA_EXPIRACAO"].ToString()),
						NomeVendedor = linha["NOME"].ToString(),
						StatusOrcamento = linha["DESCRICAO"].ToString(),
						ValorOrcamento = Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())
					});
				}
			}

			return listaPedidos;
		}

		public static CabecalhoOrcamento GetCabecalhoOrcamento(int codigoOrcamento, out string mensagemErro)
		{
			CabecalhoOrcamento orcamento = new CabecalhoOrcamento();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CO.*, CP.DESCRICAO AS DESCRICAO_CONDICAO FROM CABECALHOS_ORCAMENTO AS CO");
			sql.AppendLine("	LEFT JOIN CONDICOES_PAGAMENTO AS CP ON CO.CODIGO_CONDICAO = CP.CODIGO");
			sql.AppendLine("WHERE CO.CODIGO = " + codigoOrcamento);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					orcamento.Codigo = Convert.ToInt32(linha["CODIGO"]);
					orcamento.Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"]));
					orcamento.DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"]);
					orcamento.FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNC_CRIADOR"]));
					orcamento.CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"]), Descricao = linha["DESCRICAO_CONDICAO"].ToString() };
					orcamento.StatusOrcamento = new StatusOrcamento() { Codigo = Convert.ToInt32(linha["CODIGO_STATUS"]) };
					orcamento.ValidadeOrcamento = Convert.ToInt32(linha["VALIDADE_ORCAMENTO"]);
					orcamento.ValorOrcamento = Convert.ToDecimal(linha["VALOR_TOTAL"]);
					orcamento.TelefoneContato = linha["TELEFONE_CONTATO"].ToString();
					orcamento.DataExpiracao = Convert.ToDateTime(linha["DATA_EXPIRACAO"]);
				}
			}

			return orcamento;
		}
		
	}
}
