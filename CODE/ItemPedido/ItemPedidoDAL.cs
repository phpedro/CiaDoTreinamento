using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ItemPedidoDAL
	{
		//INSERT
		public static bool insertItemPedido(ItemPedido item, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO ITENS_PEDIDOS");
				sql.AppendLine("(CODIGO_PRODUTO, DATA_INICIO_VIGENCIA, VALOR_FINAL, DATA_EXPIRACAO, OBSERVACAO_GENERICA, CODIGO_PEDIDO,");
				sql.AppendLine("VALOR_DESCONTO, SUBTOTAL, QUANTIDADE, CONFIRMADO, VALOR_ISS, CODIGO_MOTIVO_PEDIDO, DATA_INICIO_TREINAMENTO,");
				sql.AppendLine("DATA_FIM_TREINAMENTO, INFO_DATA_TREINAMENTO)");
				sql.AppendLine("VALUES");
				sql.AppendLine("(" + item.Produto.Codigo + ", '" + (item.DataInicioVigencia.HasValue ? Convert.ToDateTime(item.DataInicioVigencia).ToString("yyyy-MM-dd") : "") + "'," + item.valorFinal.ToString().Replace(",",".") + ",'" + item.DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss") + "','" + item.ObservacaoGenerica + "'," + item.CodigoPedido + ",");
				sql.AppendLine("'" + item.ValorDesconto.ToString().Replace(",",".") + "', " + item.Subtotal.ToString().Replace(",",".") + ", '" + item.Quantidade.ToString().Replace(",", ".") + "', '" + (item.Confirmado ? '1' : '0') + "', " + item.ValorEncargos.ToString().Replace(",",".") + ", " + item.CodigoMotivoPedido + ", '" + (item.DataInicioTreinamento.HasValue ? Convert.ToDateTime(item.DataInicioTreinamento).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")) + "',");
				sql.AppendLine("'" + (item.DataFimTreinamento.HasValue ? Convert.ToDateTime(item.DataFimTreinamento).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")) + "', '" + item.InformacoesParaTreinamento + "')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o item. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public static bool updateItemPedido(ItemPedido item, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE ITENS_PEDIDOS SET");
				sql.AppendLine("DATA_INICIO_VIGENCIA = '" + (item.DataInicioVigencia.HasValue ? Convert.ToDateTime(item.DataInicioVigencia).ToString("yyyy-MM-dd") : "") + "',");
				sql.AppendLine("VALOR_FINAL = '" + item.valorFinal.ToString().Replace(",",".") + "',");
				sql.AppendLine("DATA_EXPIRACAO = '" + item.DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.AppendLine("OBSERVACAO_GENERICA = '" + item.ObservacaoGenerica + "',");
				sql.AppendLine("VALOR_DESCONTO = '" + item.ValorDesconto.ToString().Replace(",", ".") + "',");
				sql.AppendLine("SUBTOTAL = '" + item.Subtotal.ToString().Replace(",", ".") + "',");
				sql.AppendLine("QUANTIDADE = '" + item.Quantidade.ToString().Replace(",",".") + "',");
				sql.AppendLine("CONFIRMADO = '" + (item.Confirmado ? '1' : '0') + "',");
				sql.AppendLine("VALOR_ISS = '" + item.ValorEncargos.ToString().Replace(",", ".") + "',");
				sql.AppendLine("CODIGO_MOTIVO_PEDIDO = " + item.CodigoMotivoPedido + ",");
				sql.AppendLine("DATA_INICIO_TREINAMENTO = '" + (item.DataInicioTreinamento.HasValue ? Convert.ToDateTime(item.DataInicioTreinamento).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")) + "',");
				sql.AppendLine("DATA_FIM_TREINAMENTO = '" + (item.DataFimTreinamento.HasValue ? Convert.ToDateTime(item.DataFimTreinamento).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")) + "',");
				sql.AppendLine("INFO_DATA_TREINAMENTO = '" + item.InformacoesParaTreinamento + "'");
				sql.AppendLine("WHERE CODIGO_PRODUTO = " + item.Produto.Codigo);
				sql.AppendLine("	AND CODIGO_PEDIDO = " + item.CodigoPedido);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o item. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public static bool deleteItemPedido(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("DELETE FROM ITENS_PEDIDOS");
				sql.AppendLine("WHERE CODIGO_PRODUTO = " + codigoProduto);
				sql.AppendLine("	AND CODIGO_PEDIDO = " + codigoPedido);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o item. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTA
		public static List<ItemPedido> getItemPedido(int? codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";
			List<ItemPedido> listaItens = new List<ItemPedido>();

			try
			{

				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("SELECT CODIGO_PRODUTO, DATA_INICIO_VIGENCIA, VALOR_FINAL, DATA_EXPIRACAO, OBSERVACAO_GENERICA, CODIGO_PEDIDO,");
				sql.AppendLine("	VALOR_DESCONTO, SUBTOTAL, QUANTIDADE, CONFIRMADO, VALOR_ISS, CODIGO_MOTIVO_PEDIDO, DATA_INICIO_TREINAMENTO,");
				sql.AppendLine("	DATA_FIM_TREINAMENTO, INFO_DATA_TREINAMENTO");
				sql.AppendLine("FROM ITENS_PEDIDOS");
				sql.AppendLine("WHERE CODIGO_PEDIDO = " + codigoPedido);

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
						listaItens.Add(new ItemPedido()
						{
							Produto = new Produto(Convert.ToInt32(linha["CODIGO_PRODUTO"].ToString())),
							DataInicioVigencia = (linha["DATA_INICIO_VIGENCIA"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_VIGENCIA"].ToString())),
							valorFinal = Convert.ToDecimal(linha["VALOR_FINAL"].ToString()),
							DataExpiracao = Convert.ToDateTime(linha["DATA_EXPIRACAO"].ToString()),
							ObservacaoGenerica = linha["OBSERVACAO_GENERICA"].ToString(),
							CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
							ValorDesconto = Convert.ToDecimal(linha["VALOR_DESCONTO"].ToString()),
							Subtotal = Convert.ToDecimal(linha["SUBTOTAL"].ToString()),
							Quantidade = Convert.ToDecimal(linha["QUANTIDADE"].ToString()),
							Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
							ValorEncargos = Convert.ToDecimal(linha["VALOR_ISS"].ToString()),
							CodigoMotivoPedido = Convert.ToInt32(linha["CODIGO_MOTIVO_PEDIDO"].ToString()),
							DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
							DataFimTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
							InformacoesParaTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString()
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

		public static bool verificaTemAlunosSemRG(int codigoProduto, int codigoPedido, out bool temAluno, ref string mensagemErro)
		{
			temAluno = true;

			Command cmd = new Command();

			string comandoSql = "SELECT " +
									"COUNT(1) AS TOTAL " +
								"FROM " +
									"ITENS_ALUNOS_NR20 ITA " +
										"INNER JOIN ALUNOS A ON A.CODIGO = ITA.CODIGO_ALUNO " +
								"WHERE " +
									"ITA.CODIGO_PEDIDO = " + codigoPedido + " AND " +
									"ITA.CODIGO_PRODUTO = " + codigoProduto + " AND " +
									"( " +
										"A.RG IS NULL OR " +
										"A.RG = '' " +
									") ";

			cmd.CommandText = comandoSql;

			int retorno = Convert.ToInt32(cmd.ExecuteScalar());

			temAluno = (retorno > 0);

			return true;
		}

		public static int quantidadeAlunosBrigada(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";
			int quantidadeAlunos = 0;

			try
			{
				Command cmd = new Command();

				string comandoSql = "SELECT " +
									"   COUNT(1) " +
									"FROM " +
									"   ITENS_ALUNOS_PAE " +
									"WHERE " +
									"   CODIGO_PEDIDO = " + codigoPedido + " AND " +
									"   CODIGO_PRODUTO = " + codigoProduto + " ";

				cmd.CommandText = comandoSql;

				quantidadeAlunos = Convert.ToInt32(cmd.ExecuteScalar());

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os alunos da brigada. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}

			return quantidadeAlunos;
		}

		public static int quantidadeAlunosCurso(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";
			int quantidadeAlunos = 0;

			try
			{
				Command cmd = new Command();

				string comandoSql = "SELECT " +
									"   COUNT(1) " +
									"FROM " +
									"   ITENS_ALUNOS_NR20 " +
									"WHERE " +
									"   CODIGO_PEDIDO = " + codigoPedido + " AND " +
									"   CODIGO_PRODUTO = " + codigoProduto + " ";

				cmd.CommandText = comandoSql;

				quantidadeAlunos = Convert.ToInt32(cmd.ExecuteScalar());

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os alunos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}

			return quantidadeAlunos;
		}
	}
}
