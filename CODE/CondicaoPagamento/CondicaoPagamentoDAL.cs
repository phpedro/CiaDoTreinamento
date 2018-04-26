using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class CondicaoPagamentoDAL
    {

		//INSERT
		public static bool insertCondicaoPagamento(CondicaoPagamento condicao, List<ParcelamentoCondicao.ParcelaTela> parcelas, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				ParcelamentoCondicaoBLL BLL = new ParcelamentoCondicaoBLL();

				sql.Append("INSERT INTO CONDICOES_PAGAMENTO");
				sql.Append("	(DESCRICAO, SOLICITA_CONFIRMACAO)");
				sql.Append("	VALUES");
				sql.Append("	('" + condicao.Descricao + "', " + (condicao.SolicitaConfirmacao ? 1 : 0) + ")");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					condicao.Codigo = retorno;

					foreach (ParcelamentoCondicao.ParcelaTela item in parcelas)
					{

						ParcelamentoCondicao parcela = new ParcelamentoCondicao()
						{
							CodigoCondicao = (int)condicao.Codigo,
							EhAVista = false,
							NumeroDiasPrazo = item.numeroDias
						};

						if (!BLL.insertParcela(parcela, out mensagemErro))
						{
							return false;
						}

					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a condição de pagamento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateCondicaoPagamento(CondicaoPagamento condicao, List<ParcelamentoCondicao.ParcelaTela> parcelas, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				ParcelamentoCondicaoBLL BLL = new ParcelamentoCondicaoBLL();

				sql.Append("UPDATE CONDICOES_PAGAMENTO");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + condicao.Descricao + "',");
				sql.Append("	SOLICITA_CONFIRMACAO = " + (condicao.SolicitaConfirmacao ? 1 : 0) + "");
				sql.Append("	WHERE CODIGO = " + condicao.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					//REMOVER PARCELAS ANTIGAS
					BLL.deleteAllParcelaByCondicao((int)condicao.Codigo, out mensagemErro);

					//CADASTRAR NOVAS PARCELAS
					foreach (ParcelamentoCondicao.ParcelaTela item in parcelas)
					{

						ParcelamentoCondicao parcela = new ParcelamentoCondicao()
						{
							CodigoCondicao = (int)condicao.Codigo,
							EhAVista = false,
							NumeroDiasPrazo = item.numeroDias
						};

						if (!BLL.insertParcela(parcela, out mensagemErro))
						{
							return false;
						}
					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a condição de pagamento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public static bool deleteCondicaoPagamento(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				ParcelamentoCondicaoBLL BLL = new ParcelamentoCondicaoBLL();
				//DEVEMOS REMOVER AS PARCELAS DA CONDIÇÃO
				BLL.deleteAllParcelaByCondicao((int)codigo, out mensagemErro);

				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CONDICOES_PAGAMENTO WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a condição de pagamento. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTA
		public static List<CondicaoPagamento> getCondicoes(int? codigo, string descricao, out string mensagemErro)
		{
			List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM CONDICOES_PAGAMENTO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(descricao))
			{
				sql.Append("	AND DESCRICAO LIKE CONCAT('%','" + descricao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaCondicoes.Add(new CondicaoPagamento()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						SolicitaConfirmacao = (linha["SOLICITA_CONFIRMACAO"].ToString() == "1")
					});
				}
			}

			return listaCondicoes;
		}

	}
}
