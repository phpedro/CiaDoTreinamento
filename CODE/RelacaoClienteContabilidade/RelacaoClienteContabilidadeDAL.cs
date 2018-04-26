using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class RelacaoClienteContabilidadeDAL
    {
		//INSERT
		public static bool insertRelacaoClienteContabilidade(RelacaoClienteContabilidade relacao, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO CLIENTE_CONTABILIDADE");
				sql.Append("	(CODIGO_CLIENTE, CODIGO_CONTABILIDADE, DATA_CADASTRO)");
				sql.Append("	VALUES");
				sql.Append("	('" + relacao.CodigoCliente + "', '" + relacao.Concorrente.Codigo + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a relação. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteRelacaoClienteContabilidade(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CLIENTE_CONTABILIDADE WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a relação. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<RelacaoClienteContabilidade> getContabilidadesByCliente(int? codigoCliente, out string mensagemErro)
		{

			List<RelacaoClienteContabilidade> listaConcorrentes = new List<RelacaoClienteContabilidade>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CC.*, CO.RAZAO_SOCIAL FROM CONTABILIDADE AS CO");
			sql.Append("    LEFT JOIN CLIENTE_CONTABILIDADE AS CC ON CC.CODIGO_CONTABILIDADE = CO.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigoCliente != null && codigoCliente != 0)
			{
				sql.Append("	AND CC.CODIGO_CLIENTE = " + codigoCliente);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaConcorrentes.Add(new RelacaoClienteContabilidade()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						CodigoCliente = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()),
						Concorrente = new Contabilidade() { Codigo = Convert.ToInt32(linha["CODIGO_CONTABILIDADE"].ToString()), RazaoSocial = linha["RAZAO_SOCIAL"].ToString() },
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaConcorrentes;

		}

	}
}
