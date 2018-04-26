using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class ItensAlunosNr20DAL
    {
		//INSERT
		public static bool insertItemAlunoNr20(ItensAlunosNr20 item, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO ITENS_ALUNOS_NR20");
				sql.AppendLine("(CODIGO_PEDIDO, CODIGO_ALUNO, CODIGO_PRODUTO)");
				sql.AppendLine("VALUES");
				sql.AppendLine("(" + item.codigoPedido + "," + item.aluno.Codigo + "," + item.codigoProduto + ")");

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

		//DELETE
		public static bool deleteItensAlunosNr20(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("DELETE FROM ITENS_ALUNOS_NR20");
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

		//SEARCH
		public static List<ItensAlunosNr20> getAlunos(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";
			List<ItensAlunosNr20> listaItens = new List<ItensAlunosNr20>();

			try
			{

				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("SELECT CODIGO_ALUNO, CODIGO_PEDIDO, CODIGO_PRODUTO");
				sql.AppendLine("FROM ITENS_ALUNOS_NR20");
				sql.AppendLine("WHERE CODIGO_PRODUTO = " + codigoProduto);
				sql.AppendLine("	AND CODIGO_PEDIDO = " + codigoPedido);

				cmd.CommandText = sql.ToString();

				DataTable retorno = cmd.GetData();

				if (retorno.Rows.Count > 0)
				{
					foreach (DataRow linha in retorno.Rows)
					{
						listaItens.Add(new ItensAlunosNr20()
						{
							codigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
							codigoProduto = Convert.ToInt32(linha["CODIGO_PRODUTO"].ToString()),
							aluno = new Aluno() { Codigo = Convert.ToInt32(linha["CODIGO_ALUNO"].ToString()) }
						});
					}
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os alunos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaItens;
		}
	}
}
