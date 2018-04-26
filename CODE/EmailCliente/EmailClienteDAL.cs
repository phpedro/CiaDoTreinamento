using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class EmailClienteDAL
    {
		//INSERT 
		public static bool insertEmail(EmailCliente email, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO EMAILS_CLIENTES");
				sql.Append("	(DESCRICAO, CODIGO_CLIENTE, DATA_CADASTRO)");
				sql.Append("	VALUES");
				sql.Append("	('" + email.Descricao + "','" + email.Cliente + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o email. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o email. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateEmail(EmailCliente email, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE EMAILS_CLIENTES");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + email.Descricao + "'");
				sql.Append("	WHERE CODIGO = " + email.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o email. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o email. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<EmailCliente> GetEmails(int codigoCliente, out string mensagemErro)
		{

			List<EmailCliente> listaEmails = new List<EmailCliente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM EMAILS_CLIENTES");
			sql.Append("	WHERE 1 = 1");

			if (codigoCliente != 0)
			{
				sql.Append("	AND CODIGO_CLIENTE = " + codigoCliente);
			}

			sql.Append("    ORDER BY DESCRICAO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaEmails.Add(new EmailCliente()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Cliente = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()),
						DataCadastro = (linha["DATA_CADASTRO"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(linha["DATA_CADASTRO"].ToString())),
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaEmails;

		}
	}
}
