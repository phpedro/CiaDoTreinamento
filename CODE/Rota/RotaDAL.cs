using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class RotaDAL
    {
		//INSERT 
		public static int insertRota(Rota rota, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO ROTA");
				sql.AppendLine("	(CODIGO_INSTRUTOR, DATA_INICIO, DATA_FIM, OBSERVACAO)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + rota.Instrutor.Codigo + "', '" + rota.DataInicio.ToString("yyyy-MM-dd") + "','" + rota.DataFim.ToString("yyyy-MM-dd") + "', '" + rota.Observacao + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					return retorno;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a rota. Contate o suporte!";
					return -1;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}

		}

		//UPDATE
		public static bool updateRota(Rota rota, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE ROTA");
				sql.AppendLine("	SET");
				sql.AppendLine("	CODIGO_INSTRUTOR = '" + rota.Instrutor.Codigo + "',");
				sql.AppendLine("	DATA_INICIO = '" + rota.DataInicio.ToString("yyyy-MM-dd") + "',");
				sql.AppendLine("	DATA_FIM = '" + rota.DataFim.ToString("yyyy-MM-dd") + "',");
				sql.AppendLine("	OBSERVACAO = '" + rota.Observacao + "'");
				sql.AppendLine("	WHERE CODIGO_ROTA = " + rota.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a rota. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteRota(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM ROTA WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a rota. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//SELECT
		public static List<Rota> selectRotas(int codigo, out string mensagemErro)
		{
			List<Rota> lista = new List<Rota>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT * FROM ROTA");
			sql.AppendLine("WHERE CODIGO_ROTA = " + codigo);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new Rota()
					{
						Codigo = Convert.ToInt32(linha["CODIGO_ROTA"]),
						Instrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_INSTRUTOR"])),
						DataInicio = Convert.ToDateTime(linha["DATA_INICIO"]),
						DataFim = Convert.ToDateTime(linha["DATA_FIM"]),
						Observacao = linha["OBSERVACAO"].ToString()
					});

				}
			}

			return lista;
		}

		public static List<Rota> selectRotasByInstrutor(int? codigoRota, int? codigoInstrutor, int? codigoCidade, DateTime? dataInicio, DateTime? dataFinal, out string mensagemErro)
		{
			List<Rota> lista = new List<Rota>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT DISTINCT R.CODIGO_ROTA, R.CODIGO_INSTRUTOR, R.DATA_INICIO, R.DATA_FIM, R.OBSERVACAO");
			sql.AppendLine("FROM ROTA R");
			sql.AppendLine("	INNER JOIN ITENS_ROTA I ON I.CODIGO_ROTA = R.CODIGO_ROTA");
			sql.AppendLine("	INNER JOIN CABECALHOS_PEDIDOS CP ON I.CODIGO_PEDIDO = CP.CODIGO");
			sql.AppendLine("	INNER JOIN CLIENTES CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("WHERE 1 = 1");

			if (codigoRota.HasValue && codigoRota > 0)
			{
				sql.AppendLine("AND R.CODIGO_ROTA = " + codigoRota);
			}

			if (codigoInstrutor.HasValue && codigoInstrutor > 0)
			{
				sql.AppendLine("AND R.CODIGO_INSTRUTOR = " + codigoInstrutor);
			}

			if (codigoCidade.HasValue && codigoCidade > 0)
			{
				sql.AppendLine("AND CL.CODIGO_CIDADE = " + codigoCidade);
			}

			if (dataInicio.HasValue)
			{
				sql.AppendLine("	AND DATE(R.DATA_INICIO) >= '" + Convert.ToDateTime(dataInicio).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFinal.HasValue)
			{
				sql.AppendLine("	AND DATE(R.DATA_INICIO) <= '" + Convert.ToDateTime(dataFinal).ToString("yyyy-MM-dd") + "'");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new Rota()
					{
						Codigo = Convert.ToInt32(linha["CODIGO_ROTA"]),
						Instrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_INSTRUTOR"])),
						DataInicio = Convert.ToDateTime(linha["DATA_INICIO"]),
						DataFim = Convert.ToDateTime(linha["DATA_FIM"]),
						Observacao = linha["OBSERVACAO"].ToString()
					});

				}
			}

			return lista;
		}

		public static List<Rota> selectAllRotas(out string mensagemErro)
		{
			List<Rota> lista = new List<Rota>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT * FROM ROTA");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new Rota()
					{
						Codigo = Convert.ToInt32(linha["CODIGO_ROTA"]),
						Instrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_INSTRUTOR"])),
						DataInicio = Convert.ToDateTime(linha["DATA_INICIO"]),
						DataFim = Convert.ToDateTime(linha["DATA_FIM"]),
						Observacao = linha["OBSERVACAO"].ToString()
					});

				}
			}

			return lista;
		}
	}
}
