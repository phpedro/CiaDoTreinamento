using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TelefoneLicenciamentoAmbientalDAL
    {

		//INSERT 
		public static bool insertTelefoneLicenciamento(TelefoneLicenciamentoAmbiental telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO TELEFONE_LICENCIAMENTO");
				sql.Append("	(CODIGO_CONCORRENTE, DESCRICAO, RESPONSAVEL)");
				sql.Append("	VALUES");
				sql.Append("	('" + telefone.CodigoConcorrente + "', '" + telefone.Descricao + "', '" + telefone.Responsavel + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateTelefoneLicenciamento(TelefoneLicenciamentoAmbiental telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE TELEFONE_LICENCIAMENTO");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + telefone.Descricao + "',");
				sql.Append("	RESPONSAVEL = '" + telefone.Responsavel + "'");
				sql.Append("	WHERE CODIGO = " + telefone.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteTelefoneLicenciamento(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_LICENCIAMENTO WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public static bool deleteAllTelefoneLicenciamento(int codigoConcorrente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_LICENCIAMENTO WHERE CODIGO_CONCORRENTE = " + codigoConcorrente);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<TelefoneLicenciamentoAmbiental> getTelefonesLicenciamento(int? codigoConcorrente, out string mensagemErro)
		{
			List<TelefoneLicenciamentoAmbiental> listaTelefones = new List<TelefoneLicenciamentoAmbiental>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_LICENCIAMENTO");
			sql.Append("	WHERE 1 = 1");

			if (codigoConcorrente != null && codigoConcorrente != 0)
			{
				sql.Append("	AND CODIGO_CONCORRENTE = " + codigoConcorrente);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneLicenciamentoAmbiental()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						CodigoConcorrente = Convert.ToInt32(linha["CODIGO_CONCORRENTE"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Responsavel = linha["RESPONSAVEL"].ToString()
					});
				}
			}

			return listaTelefones;
		}

		public static List<TelefoneLicenciamentoAmbiental.TelefoneTela> getTelefonesLicenciamentoTela(int? codigoConcorrente, out string mensagemErro)
		{
			List<TelefoneLicenciamentoAmbiental.TelefoneTela> listaTelefones = new List<TelefoneLicenciamentoAmbiental.TelefoneTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_LICENCIAMENTO");
			sql.Append("	WHERE 1 = 1");

			if (codigoConcorrente != null && codigoConcorrente != 0)
			{
				sql.Append("	AND CODIGO_CONCORRENTE = " + codigoConcorrente);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneLicenciamentoAmbiental.TelefoneTela()
					{
						sequencia = Convert.ToInt32(linha["CODIGO"].ToString()),
						telefone = linha["DESCRICAO"].ToString(),
						responsavel = linha["RESPONSAVEL"].ToString()
					});
				}
			}

			return listaTelefones;
		}

	}
}
