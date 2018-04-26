using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class ItemOrcamentoDAL
    {

		public static bool GetInsertItemOrcamento(ItemOrcamento itemOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO ITENS_ORCAMENTO");
				sql.AppendLine("	(CODIGO_ORCAMENTO, CODIGO_PRODUTO, QUANTIDADE, PERC_DESCONTO,");
				sql.AppendLine("		SUBTOTAL, ACRESCIMO)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + itemOrcamento.cabecalhoOrcamento.Codigo + "','" + itemOrcamento.produto.Codigo + "','" + itemOrcamento.quantidade + "','" + itemOrcamento.percentualDesconto.ToString().Replace(",", ".") + "',");
				sql.AppendLine("	'" + itemOrcamento.subtotal.ToString().Replace(",", ".") + "','" + itemOrcamento.acrescimo.ToString().Replace(",", ".") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o item do orçamento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o item do orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static bool GetDeleteItemOrcamento(int codigoOrcamento, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM ITENS_ORCAMENTO WHERE CODIGO_ORCAMENTO = " + codigoOrcamento);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover os itens do orçamento. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os itens do orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public static List<ItemOrcamento> getItensOrcamento(int codigoOrcamento, int? codigoProduto, out string mensagemErro)
		{
			mensagemErro = "";
			List<ItemOrcamento> listaItens = new List<ItemOrcamento>();

			try
			{

				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("SELECT CODIGO_ORCAMENTO, CODIGO_PRODUTO, QUANTIDADE, PERC_DESCONTO, SUBTOTAL, ACRESCIMO");
				sql.AppendLine("FROM ITENS_ORCAMENTO");
				sql.AppendLine("WHERE CODIGO_ORCAMENTO = " + codigoOrcamento);

				if (codigoProduto.HasValue && codigoProduto != 0)
				{
					sql.AppendLine("	AND CODIGO_PRODUTO = " + codigoProduto);
				}

				cmd.CommandText = sql.ToString();

				DataTable retorno = cmd.GetData();

				if (retorno.Rows.Count > 0)
				{
					foreach (DataRow linha in retorno.Rows)
					{
						listaItens.Add(new ItemOrcamento()
						{
							cabecalhoOrcamento = new CabecalhoOrcamento() { Codigo = Convert.ToInt32(linha["CODIGO_ORCAMENTO"].ToString()) },
							produto = new Produto(Convert.ToInt32(linha["CODIGO_PRODUTO"].ToString())),
							quantidade = Convert.ToInt32(linha["QUANTIDADE"].ToString()),
							percentualDesconto = Convert.ToDecimal(linha["PERC_DESCONTO"].ToString()),
							subtotal = Convert.ToDecimal(linha["SUBTOTAL"].ToString()),
							acrescimo = Convert.ToDecimal(linha["ACRESCIMO"].ToString())
						});
					}
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os itens. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaItens;
		}

		public static List<ItemOrcamento.ItemOrcamentoTela> getItensOrcamentoTela(int codigoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";
			List<ItemOrcamento.ItemOrcamentoTela> listaItens = new List<ItemOrcamento.ItemOrcamentoTela>();

			try
			{

				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("SELECT IO.CODIGO_ORCAMENTO, IO.CODIGO_PRODUTO, IO.QUANTIDADE, IO.PERC_DESCONTO, IO.SUBTOTAL, IO.ACRESCIMO, PR.DESCRICAO AS NOME, PR.VALOR_POR_PESSOA");
				sql.AppendLine("FROM ITENS_ORCAMENTO AS IO");
				sql.AppendLine(" INNER JOIN PRODUTOS AS PR ON IO.CODIGO_PRODUTO = PR.CODIGO");
				sql.AppendLine("WHERE IO.CODIGO_ORCAMENTO = " + codigoOrcamento);

				cmd.CommandText = sql.ToString();

				DataTable retorno = cmd.GetData();

				if (retorno.Rows.Count > 0)
				{
					foreach (DataRow linha in retorno.Rows)
					{
						listaItens.Add(new ItemOrcamento.ItemOrcamentoTela()
						{
							produto_Codigo = Convert.ToInt32(linha["CODIGO_PRODUTO"].ToString()),
							Nome = linha["NOME"].ToString(),
							Quantidade = Convert.ToInt32(linha["QUANTIDADE"].ToString()),
							PercentualDesconto = Convert.ToDecimal(linha["PERC_DESCONTO"].ToString()),
							Subtotal = Convert.ToDecimal(linha["SUBTOTAL"].ToString()),
							Acrescimo = Convert.ToDecimal(linha["ACRESCIMO"].ToString()),
							valorUnitario = Convert.ToDecimal(linha["VALOR_POR_PESSOA"].ToString())
						});
					}
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os itens. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaItens;
		}

	}
}
