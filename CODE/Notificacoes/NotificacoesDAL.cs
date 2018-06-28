using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class NotificacoesDAL
    {
		//INSERT
		public static bool insertNotificacao(Notificacoes notificacao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO NOTIFICACOES");
				sql.Append("	(USUARIO_CRIADOR, USUARIO_DESTINO, MENSAGEM, FLAG_LEITURA, URL_REDIRECT)");
				sql.Append("	VALUES");
				sql.Append("	('" + notificacao.FuncionarioCriador.Codigo + "', '" + notificacao.FuncionarioDestino.Codigo + "', '" + notificacao.Mensagem + "', 'NAO', '" + notificacao.UrlRedirect + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a notificação. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a notificação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public static bool updateNotificacao(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE NOTIFICACOES SET");
				sql.Append("	FLAG_LEITURA = 'SIM'");
				sql.Append("	WHERE");
				sql.Append("	CODIGO = " + codigo + "");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a notificação. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível atualizar a notificação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<Notificacoes> getNotificacoes(int? codigoUsuario, out string mensagemErro)
		{
			List<Notificacoes> listaNotificacoes = new List<Notificacoes>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT NT.*, PE.NOME AS NOME_CRIADOR FROM NOTIFICACOES AS NT");
			sql.Append("	LEFT JOIN PESSOAS AS PE ON NT.USUARIO_CRIADOR = PE.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigoUsuario != null && codigoUsuario != 0)
			{
				sql.Append("	AND USUARIO_DESTINO = " + codigoUsuario);
			}

			sql.Append("	AND FLAG_LEITURA = 'Nao'");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaNotificacoes.Add(new Notificacoes()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						FuncionarioCriador = new Funcionario() { Codigo = Convert.ToInt32(linha["USUARIO_CRIADOR"].ToString()), Nome = linha["NOME_CRIADOR"].ToString()},
						FuncionarioDestino = new Funcionario() { Codigo = Convert.ToInt32(linha["USUARIO_DESTINO"].ToString())},
						Mensagem = linha["MENSAGEM"].ToString(),
						UrlRedirect = linha["URL_REDIRECT"].ToString()
					});
				}
			}

			return listaNotificacoes;
		}
	}
}
