using CiaDoTreinamento.BancoDados;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class AlunoDAL
    {
		//INSERT
		public static bool InserAluno(Aluno aluno, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PESSOAS");
				sql.Append("	(NOME, ATIVO, SEXO, EMAIL)");
				sql.Append("	VALUES");
				sql.Append("	('" + aluno.Nome + "', '" + (aluno.Ativo ? 1 : 0) + "', '" + aluno.Sexo + "', '" + aluno.Email + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					aluno.Codigo = retorno;
					sql.Clear();

					sql.Append("INSERT INTO ALUNOS");
					sql.Append("	(CODIGO, CODIGO_CLIENTE, CARGO, CODIGO_CARGO_BRIGADA, RG, CPF");
					sql.Append("	) VALUES");
					sql.Append("	('" + aluno.Codigo + "', '" + aluno.Cliente.Codigo + "', '" + aluno.Cargo + "', '" + aluno.CargoBrigada + "', '" + aluno.Rg + "', '" + (aluno.CPF == null ? "" : aluno.CPF.RemoveMask()) + "'");
					sql.Append(")");

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível cadastrar o aluno. Contate o suporte!";
						return false;
					}

				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o aluno. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool UpdateAluno(Aluno aluno, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE PESSOAS AS PE");
				sql.Append("	LEFT JOIN ALUNOS AS FU ON FU.CODIGO = PE.CODIGO");
				sql.Append("	SET");
				//DADOS DA PESSOA
				sql.Append("	PE.NOME = '" + aluno.Nome + "',");
				sql.Append("	PE.ATIVO = '" + (aluno.Ativo ? 1 : 0) + "',");
				sql.Append("	PE.SEXO = '" + aluno.Sexo + "',");
				sql.Append("	PE.EMAIL = '" + aluno.Email + "',");
				//DADOS DO FUNCIONARIO
				sql.Append("	FU.CODIGO_CLIENTE = '" + aluno.Cliente.Codigo + "',");
				sql.Append("	FU.CARGO = '" + aluno.Cargo + "',");
				sql.Append("	FU.CODIGO_CARGO_BRIGADA = '" + aluno.CargoBrigada + "',");
				sql.Append("	FU.RG = '" + aluno.Rg + "',");
				sql.Append("	FU.CPF = '" + (aluno.CPF == null ? "" : aluno.CPF.RemoveMask()) + "'");
				sql.Append("	WHERE PE.CODIGO = " + aluno.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o aluno. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool DeleteAluno(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM ALUNOS WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					sql.Clear();

					sql.Append("DELETE FROM ALUNOS WHERE CODIGO = " + codigo);

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível remover o aluno. Contate o suporte!";
						return false;
					}
				}
				else
				{
					mensagemErro = "Não foi possível remover o aluno. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<Aluno.AlunoTela> GetAlunos(int? CodigoCliente, out string mensagemErro)
		{
			List<Aluno.AlunoTela> listaAlunos = new List<Aluno.AlunoTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT AL.CODIGO, PE.NOME AS NOME_ALUNO, AL.CARGO AS CARGO_ALUNO, PE.ATIVO FROM ALUNOS AS AL");
			sql.AppendLine("    LEFT JOIN PESSOAS AS PE ON PE.CODIGO = AL.CODIGO");
			sql.AppendLine("	WHERE 1 = 1");

			if (CodigoCliente != null && CodigoCliente != 0)
			{
				sql.AppendLine("	AND CODIGO_CLIENTE = " + CodigoCliente);
			}

			sql.AppendLine(" ORDER BY NOME_ALUNO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaAlunos.Add(new Aluno.AlunoTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Nome = linha["NOME_ALUNO"].ToString(),
						Cargo = linha["CARGO_ALUNO"].ToString(),
						Ativo = Convert.ToBoolean(linha["ATIVO"].ToString()),
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaAlunos;
		}

		public static string GetAlunosJson(int? CodigoCliente, out string mensagemErro)
		{
			List<Aluno> listaAlunos = new List<Aluno>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT PE.*, AL.CODIGO_CLIENTE, AL.CARGO, AL.CODIGO_CARGO_BRIGADA, AL.RG, AL.CPF, AL.DATA_NASCIMENTO FROM PESSOAS AS PE");
			sql.Append("    LEFT JOIN ALUNOS AS AL ON PE.CODIGO = AL.CODIGO");
			sql.Append("    WHERE AL.CODIGO_CLIENTE = " + CodigoCliente);
			sql.Append("    ORDER BY PE.NOME");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{

				foreach (DataRow linha in retorno.Rows)
				{
					listaAlunos.Add(new Aluno()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Nome = linha["NOME"].ToString(),
						Sexo = linha["SEXO"].ToString(),
						Email = linha["EMAIL"].ToString(),
						Telefone = linha["TELEFONE"].ToString(),
						Ativo = Convert.ToBoolean(linha["ATIVO"].ToString()),
						Cliente = new Cliente() { Codigo = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()) },
						Cargo = linha["CARGO"].ToString(),
						CargoBrigada = Convert.ToInt32(linha["CODIGO_CARGO_BRIGADA"].ToString()),
						Rg = linha["RG"].ToString(),
						CPF = linha["CPF"].ToString(),
						tipo = Enumeradores.Tipo.Old
					});
				}

			}

			return JsonConvert.SerializeObject(listaAlunos);
		}

		public static bool GetAluno(int CodigoAluno, int CodigoCliente, out Aluno aluno, out string mensagemErro)
		{
			aluno = new Aluno();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT PE.*, AL.CODIGO_CLIENTE, AL.CARGO, AL.CODIGO_CARGO_BRIGADA, AL.RG, AL.CPF, AL.DATA_NASCIMENTO FROM PESSOAS AS PE");
			sql.Append("    LEFT JOIN ALUNOS AS AL ON PE.CODIGO = AL.CODIGO");
			sql.Append("    WHERE PE.CODIGO = " + CodigoAluno);
			sql.Append("    AND AL.CODIGO_CLIENTE = " + CodigoCliente);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				DataRow linha = retorno.Rows[0];

				aluno.Codigo = Convert.ToInt32(linha["CODIGO"].ToString());
				aluno.Nome = linha["NOME"].ToString();
				aluno.Sexo = linha["SEXO"].ToString();
				aluno.Email = linha["EMAIL"].ToString();
				aluno.Telefone = linha["TELEFONE"].ToString();
				aluno.Ativo = Convert.ToBoolean(linha["ATIVO"].ToString());
				aluno.Cliente = new Cliente() { Codigo = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()) };
				aluno.Cargo = linha["CARGO"].ToString();
				aluno.CargoBrigada = Convert.ToInt32(linha["CODIGO_CARGO_BRIGADA"].ToString());
				aluno.Rg = linha["RG"].ToString();
				aluno.CPF = linha["CPF"].ToString();

			}
			else
			{
				mensagemErro = "Não foi possível localizar o aluno informado! Contate o administrador!";
				return false;
			}

			return true;
		}
	}
}
