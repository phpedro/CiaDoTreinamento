using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TelefoneContabilidadeDAL
    {

		//INSERT 
		public static bool insertTelefoneContabilidade(TelefoneContabilidade telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO TELEFONE_CONTABILIDADE");
				sql.Append("	(CODIGO_CONTABILIDADE, DESCRICAO, RESPONSAVEL)");
				sql.Append("	VALUES");
				sql.Append("	('" + telefone.CodigoContabilidade + "', '" + telefone.Descricao + "', '" + telefone.Responsavel + "') ");

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
		public static bool updateTelefoneContabilidade(TelefoneContabilidade telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE TELEFONE_CONTABILIDADE");
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
		public static bool deleteTelefoneContabilidade(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_CONTABILIDADE WHERE CODIGO = " + codigo);

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

		public static bool deleteAllTelefoneContabilidade(int codigoContabilidade, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM TELEFONE_CONTABILIDADE WHERE CODIGO_CONTABILIDADE = " + codigoContabilidade);

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
		public static List<TelefoneContabilidade> getTelefonesContabilidade(int? codigoContabilidade, out string mensagemErro)
		{
			List<TelefoneContabilidade> listaTelefones = new List<TelefoneContabilidade>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_CONTABILIDADE");
			sql.Append("	WHERE 1 = 1");

			if (codigoContabilidade != null && codigoContabilidade != 0)
			{
				sql.Append("	AND CODIGO_CONTABILIDADE = " + codigoContabilidade);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneContabilidade()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						CodigoContabilidade = Convert.ToInt32(linha["CODIGO_CONTABILIDADE"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Responsavel = linha["RESPONSAVEL"].ToString()
					});
				}
			}

			return listaTelefones;
		}

		public static List<TelefoneContabilidade.TelefoneTela> getTelefonesContabilidadeTela(int? codigoContabilidade, out string mensagemErro)
		{
			List<TelefoneContabilidade.TelefoneTela> listaTelefones = new List<TelefoneContabilidade.TelefoneTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TELEFONE_CONTABILIDADE");
			sql.Append("	WHERE 1 = 1");

			if (codigoContabilidade != null && codigoContabilidade != 0)
			{
				sql.Append("	AND CODIGO_CONTABILIDADE = " + codigoContabilidade);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneContabilidade.TelefoneTela()
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
