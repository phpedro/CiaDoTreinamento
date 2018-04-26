using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TelefoneParceiroDAL
    {

		//INSERT 
		public static bool insertTelefoneParceiro(TelefoneParceiro telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PARCEIROS_TELEFONES");
				sql.Append("	(CODIGO_TELEFONE, CODIGO_PARCEIRO)");
				sql.Append("	VALUES");
				sql.Append("	('" + telefone.Telefone.Codigo + "', '" + telefone.Parceiro.Codigo + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o telefone do parceiro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o telefone do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteTelefoneParceiro(int codigoTelefone, int codigoParceiro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PARCEIROS_TELEFONES WHERE CODIGO_TELEFONE = " + codigoTelefone + " AND CODIGO_PARCEIRO = " + codigoParceiro);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o telefone do parceiro. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public static bool deleteAllTelefoneParceiro(int codigoParceiro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PARCEIROS_TELEFONES WHERE CODIGO_PARCEIRO = " + codigoParceiro);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover os telefones do parceiro. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os telefones do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<TelefoneParceiro.TelefoneTela> getTelefonesParceiroTela(int? codigoParceiro, out string mensagemErro)
		{
			List<TelefoneParceiro.TelefoneTela> listaTelefones = new List<TelefoneParceiro.TelefoneTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT TE.* FROM PARCEIROS_TELEFONES AS PT");
			sql.Append("	LEFT JOIN TELEFONES AS TE ON PT.CODIGO_TELEFONE = TE.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigoParceiro != null && codigoParceiro != 0)
			{
				sql.Append("	AND PT.CODIGO_PARCEIRO = " + codigoParceiro);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneParceiro.TelefoneTela()
					{
						sequencia = Convert.ToInt32(linha["CODIGO"].ToString()),
						telefone = linha["DESCRICAO"].ToString(),
						responsavel = linha["OBSERVACAO"].ToString()
					});
				}
			}

			return listaTelefones;
		}

	}
}
