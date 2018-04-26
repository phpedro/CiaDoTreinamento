using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TelefoneConcorrenteDAL
    {

		//INSERT 
		public static bool insertTelefoneConcorrente(TelefoneConcorrente telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO TELEFONE_CONCORRENTE");
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
		public static bool updateTelefoneConcorrente(TelefoneConcorrente telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE TELEFONE_CONCORRENTE");
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
		public static bool deleteTelefoneConcorrente(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_CONCORRENTE WHERE CODIGO = " + codigo);

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

		public static bool deleteAllTelefoneConcorrente(int codigoConcorrente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_CONCORRENTE WHERE CODIGO_CONCORRENTE = " + codigoConcorrente);

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
		public static List<TelefoneConcorrente> getTelefonesConcorrente(int? codigoConcorrente, out string mensagemErro)
		{
			List<TelefoneConcorrente> listaTelefones = new List<TelefoneConcorrente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_CONCORRENTE");
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
					listaTelefones.Add(new TelefoneConcorrente()
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

		public static List<TelefoneConcorrente.TelefoneTela> getTelefonesConcorrenteTela(int? codigoConcorrente, out string mensagemErro)
		{
			List<TelefoneConcorrente.TelefoneTela> listaTelefones = new List<TelefoneConcorrente.TelefoneTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_CONCORRENTE");
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
					listaTelefones.Add(new TelefoneConcorrente.TelefoneTela()
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
