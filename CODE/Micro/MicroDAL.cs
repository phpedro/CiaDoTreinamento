using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class MicroDAL
    {
		//INSERT 
		public static bool insertMicro(Micro micro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO MICRO");
				sql.Append("	(DESCRICAO, FUNCIONARIO_RESPONSAVEL, CODIGO_MESO)");
				sql.Append("	VALUES");
				sql.Append("	('" + micro.Descricao + "', '" + micro.FuncionarioResponsavel.Codigo + "', '" + micro.Meso.Codigo + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a micro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateMicro(Micro micro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE MICRO");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + micro.Descricao + "',");
				sql.Append("	FUNCIONARIO_RESPONSAVEL = '" + micro.FuncionarioResponsavel.Codigo + "',");
				sql.Append("	CODIGO_MESO = '" + micro.Meso.Codigo + "'");
				sql.Append("	WHERE CODIGO = " + micro.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a micro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteMicro(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM MICRO WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a micro. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Micro> getMicros(int? codigo, string descricao, out string mensagemErro)
		{
			List<Micro> listaMicros = new List<Micro>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT MI.*, ME.DESCRICAO AS NOME_MESO FROM MICRO AS MI");
			sql.Append("    LEFT JOIN MESO AS ME ON MI.CODIGO_MESO = ME.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND MI.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(descricao))
			{
				sql.Append("	AND MI.DESCRICAO LIKE CONCAT('%','" + descricao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaMicros.Add(new Micro()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Meso = new Meso() { Codigo = Convert.ToInt32(linha["CODIGO_MESO"].ToString()), Descricao = linha["NOME_MESO"].ToString() },
						FuncionarioResponsavel = new Funcionario() { Codigo = Convert.ToInt32(linha["FUNCIONARIO_RESPONSAVEL"].ToString()) }
					});
				}
			}

			return listaMicros;
		}
	}
}
