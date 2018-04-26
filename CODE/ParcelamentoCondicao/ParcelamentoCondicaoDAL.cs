using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ParcelamentoCondicaoDAL
    {
		//INSERT
		public static bool insertParcela(ParcelamentoCondicao parcela, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PARCELAMENTO_CONDICAO");
				sql.Append("	(CODIGO_CONDICAO, EH_A_VISTA, NUMERO_DIAS)");
				sql.Append("	VALUES ");
				sql.Append("	('" + parcela.CodigoCondicao + "', " + (parcela.EhAVista ? 1 : 0) + ", '" + parcela.NumeroDiasPrazo + "')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o ambiente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o ambiente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public static bool deleteParcela(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PARCELAMENTO_CONDICAO WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a parcela. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static bool deleteAllParcelaByCondicao(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PARCELAMENTO_CONDICAO WHERE CODIGO_CONDICAO = " + codigoCondicao);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover as parcelas. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover as parcelas. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTA
		public static List<ParcelamentoCondicao> getParcelas(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";
			List<ParcelamentoCondicao> listaParcelas = new List<ParcelamentoCondicao>();

			try
			{
				string comandoSql = "SELECT " +
								"   CODIGO, " +
								"   CODIGO_CONDICAO, " +
								"   EH_A_VISTA, " +
								"   NUMERO_DIAS " +
								"FROM " +
								"   PARCELAMENTO_CONDICAO " +
								"WHERE " +
								"   CODIGO_CONDICAO = " + codigoCondicao.ToString() + " " +
								"ORDER BY " +
								"   NUMERO_DIAS ";

				Command cmd = new Command();
				cmd.CommandText = comandoSql;

				DataTable resultado = cmd.GetData();

				if (resultado.Rows.Count > 0)
				{

					foreach (DataRow linha in resultado.Rows)
					{
						listaParcelas.Add(new ParcelamentoCondicao()
						{
							Codigo = Convert.ToInt32(linha["CODIGO"]),
							CodigoCondicao = Convert.ToInt32(linha["CODIGO_CONDICAO"]),
							EhAVista = Convert.ToBoolean(linha["EH_A_VISTA"]),
							NumeroDiasPrazo = Convert.ToInt32(linha["NUMERO_DIAS"])
						});
					}

				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as parcelas para está condição. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaParcelas;
		}

		public static List<ParcelamentoCondicao.ParcelaTela> getParcelasTela(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";
			List<ParcelamentoCondicao.ParcelaTela> listaParcelas = new List<ParcelamentoCondicao.ParcelaTela>();

			try
			{
				string comandoSql = "SELECT " +
								"   CODIGO, " +
								"   CODIGO_CONDICAO, " +
								"   EH_A_VISTA, " +
								"   NUMERO_DIAS " +
								"FROM " +
								"   PARCELAMENTO_CONDICAO " +
								"WHERE " +
								"   CODIGO_CONDICAO = " + codigoCondicao.ToString() + " " +
								"ORDER BY " +
								"   NUMERO_DIAS ";

				Command cmd = new Command();
				cmd.CommandText = comandoSql;

				DataTable resultado = cmd.GetData();

				if (resultado.Rows.Count > 0)
				{

					foreach (DataRow linha in resultado.Rows)
					{
						listaParcelas.Add(new ParcelamentoCondicao.ParcelaTela()
						{
							sequencia = Convert.ToInt32(linha["CODIGO"]),
							numeroDias = Convert.ToInt32(linha["NUMERO_DIAS"])
						});
					}

				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as parcelas para está condição. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaParcelas;
		}
	}
}
