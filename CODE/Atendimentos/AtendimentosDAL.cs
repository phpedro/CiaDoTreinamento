using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class AtendimentosDAL
    {
		//INSERT 
		public static bool insertAtendimento(Atendimentos atendimento, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO ATENDIMENTOS2");
				sql.Append("	(CODIGO_FUNCIONARIO, CODIGO_PEDIDO, DESCRICAO, DATA_REGISTRO)");
				sql.Append("	VALUES");
				sql.Append("	('" + atendimento.Funcionario.Codigo + "', '" + atendimento.CodigoPedido + "', '" + atendimento.Descricao + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o atendimento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateAtendimento(Atendimentos atendimento, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE ATENDIMENTOS2");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + atendimento.Descricao + "'");
				sql.Append("	WHERE CODIGO = " + atendimento.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o atendimento. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteAtendimento(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM ATENDIMENTOS2 WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o atendimento. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o atendimento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Atendimentos> getAtendimentos(int? codigoCliente, out string mensagemErro)
		{
			List<Atendimentos> listaAtendimentos = new List<Atendimentos>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT AT.CODIGO, AT.CODIGO_FUNCIONARIO, AT.CODIGO_PEDIDO, AT.DESCRICAO, DATE_FORMAT(DATA_REGISTRO, '%d/%m/%Y %H:%i:%s') AS DATA_REGISTRO, PE.NOME AS NOME_FUNCIONARIO");
			sql.Append(" FROM ATENDIMENTOS2 AS AT");
			sql.Append("    LEFT JOIN CABECALHOS_PEDIDOS AS CP ON AT.CODIGO_PEDIDO = CP.CODIGO");
			sql.Append("	LEFT JOIN PESSOAS AS PE ON AT.CODIGO_FUNCIONARIO = PE.CODIGO");
			sql.Append("    WHERE CP.CODIGO_CLIENTE = " + codigoCliente);
			sql.Append("    ORDER BY AT.DATA_REGISTRO DESC");
			sql.Append("    LIMIT 10");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaAtendimentos.Add(new Atendimentos()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Funcionario = new Funcionario() {
							Codigo = Convert.ToInt32(linha["CODIGO_FUNCIONARIO"].ToString()),
							Nome = linha["NOME_FUNCIONARIO"].ToString()
						},
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						DataRegistro = Convert.ToDateTime(linha["DATA_REGISTRO"].ToString()),
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaAtendimentos;
		}

		public static List<Atendimentos> getAtendimentosPedido(int? codigoPedido, out string mensagemErro)
		{
			List<Atendimentos> listaAtendimentos = new List<Atendimentos>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT AT.CODIGO, AT.CODIGO_FUNCIONARIO, AT.CODIGO_PEDIDO, AT.DESCRICAO, DATE_FORMAT(DATA_REGISTRO, '%d/%m/%Y %H:%i:%s') AS DATA_REGISTRO, PE.NOME AS NOME_FUNCIONARIO");
			sql.Append(" FROM ATENDIMENTOS2 AS AT");
			sql.Append("    LEFT JOIN CABECALHOS_PEDIDOS AS CP ON AT.CODIGO_PEDIDO = CP.CODIGO");
			sql.Append("	LEFT JOIN PESSOAS AS PE ON AT.CODIGO_FUNCIONARIO = PE.CODIGO");
			sql.Append("    WHERE CP.CODIGO = " + codigoPedido);
			sql.Append("    ORDER BY AT.DATA_REGISTRO DESC");
			sql.Append("    LIMIT 10");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaAtendimentos.Add(new Atendimentos()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Funcionario = new Funcionario()
						{
							Codigo = Convert.ToInt32(linha["CODIGO_FUNCIONARIO"].ToString()),
							Nome = linha["NOME_FUNCIONARIO"].ToString()
						},
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						DataRegistro = Convert.ToDateTime(linha["DATA_REGISTRO"].ToString()),
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaAtendimentos;
		}

	}
}
