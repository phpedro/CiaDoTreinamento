using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class FuncionarioDAL
    {

		public static bool InsertFuncionario(Funcionario func, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PESSOAS");
				sql.Append("	(NOME, ATIVO, SEXO, EMAIL)");
				sql.Append("	VALUES");
				sql.Append("	('" + func.Nome + "', '" + (func.Ativo ? 1 : 0) + "', '" + func.Sexo + "', '" + func.Email + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					func.Codigo = retorno;
					sql.Clear();

					sql.AppendLine("INSERT INTO FUNCIONARIOS");
					sql.AppendLine("	(CODIGO, LOGIN, SENHA, CPF, PODE_VER_RELATORIO_VENDAS, PODE_GERAR_BOLETO,");
					sql.AppendLine("	PODE_GERAR_REMESSA_RETORNO, PODE_CONFIGURAR_DIREITOS, CODIGO_PERFIL, PODE_CADASTRAR_PRODUTOS, PODE_ALTERAR_CORREIO,");
					sql.AppendLine("	 META_VENDA_MENSAL)");
					sql.AppendLine("	VALUES");
					sql.AppendLine("	('" + func.Codigo + "', '" + func.Login + "', '" + func.Senha + "', '" + func.CPF.RemoveMask() + "', '" + (func.PODE_VER_RELATORIO_VENDAS ? 1 : 0) + "', '" + (func.PODE_GERAR_BOLETO ? 1 : 0) + "',");
					sql.AppendLine("	'" + (func.PODE_GERAR_REMESSA_RETORNO ? 1 : 0) + "', '" + (func.PODE_CONFIGURAR_DIREITOS ? 1 : 0) + "', '" + func.Perfil.Codigo + "', '" + (func.PODE_CADASTRAR_PRODUTOS ? 1 : 0) + "', '" + (func.PODE_ALTERAR_CORREIO ? 1 : 0) + "',");
					sql.AppendLine("	'" + func.META_VENDA_MENSAL.ToString().Replace(",", ".") + "')");

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível cadastrar o funcionário. Contate o suporte!";
						return false;
					}

				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o funcionário. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o funcionário. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool UpdateFuncionario(Funcionario func, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE PESSOAS AS PE");
				sql.Append("	LEFT JOIN FUNCIONARIOS AS FU ON FU.CODIGO = PE.CODIGO");
				sql.Append("	SET");
				//DADOS DA PESSOA
				sql.Append("	PE.NOME = '" + func.Nome + "',");
				sql.Append("	PE.ATIVO = '" + (func.Ativo ? 1 : 0) + "',");
				sql.Append("	PE.SEXO = '" + func.Sexo + "',");
				sql.Append("	PE.EMAIL = '" + func.Email + "',");
				//DADOS DO FUNCIONARIO
				sql.Append("	FU.LOGIN = '" + func.Login + "',");
				sql.Append("	FU.SENHA = '" + func.Senha + "',");
				sql.Append("	FU.CPF = '" + func.CPF.RemoveMask() + "',");
				sql.Append("	FU.PODE_VER_RELATORIO_VENDAS = '" + (func.PODE_VER_RELATORIO_VENDAS ? 1 : 0) + "',");
				sql.Append("	FU.PODE_GERAR_BOLETO = '" + (func.PODE_GERAR_BOLETO ? 1 : 0) + "',");
				sql.Append("	FU.PODE_GERAR_REMESSA_RETORNO = '" + (func.PODE_GERAR_REMESSA_RETORNO ? 1 : 0) + "',");
				sql.Append("	FU.PODE_CONFIGURAR_DIREITOS = '" + (func.PODE_CONFIGURAR_DIREITOS ? 1 : 0) + "',");
				sql.Append("	FU.CODIGO_PERFIL = '" + func.Perfil.Codigo + "',");
				sql.Append("	FU.PODE_CADASTRAR_PRODUTOS = '" + (func.PODE_CADASTRAR_PRODUTOS ? 1 : 0) + "',");
				sql.Append("	FU.PODE_ALTERAR_CORREIO = '" + (func.PODE_ALTERAR_CORREIO ? 1 : 0) + "',");
				sql.Append("	FU.META_VENDA_MENSAL = '" + func.META_VENDA_MENSAL.ToString().Replace(",",".") + "'");
				sql.Append("	WHERE PE.CODIGO = " + func.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o funcionário. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o funcionário. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool DeleteFuncionario(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM FUNCIONARIOS WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					sql.Clear();

					sql.Append("DELETE FROM PESSOAS WHERE CODIGO = " + codigo);

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível remover o funcionário. Contate o suporte!";
						return false;
					}
				}
				else
				{
					mensagemErro = "Não foi possível remover o funcionário. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o funcionário. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<Funcionario> getAllFuncionarios(out string mensagemErro)
		{

			mensagemErro = "";

			List<Funcionario> listaFuncionarios = new List<Funcionario>();
			Command cmd = new Command();

			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT FU.*, PE.NOME, PE.ATIVO, PE.SEXO, PE.EMAIL, PE.TELEFONE FROM FUNCIONARIOS AS FU");
			sql.Append("	LEFT JOIN PESSOAS AS PE ON FU.CODIGO = PE.CODIGO");

			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					listaFuncionarios.Add(new Funcionario()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Login = linha["LOGIN"].ToString(),
						Senha = linha["SENHA"].ToString(),
						CPF = linha["CPF"].ToString(),
						PODE_VER_RELATORIO_VENDAS = Convert.ToBoolean(linha["PODE_VER_RELATORIO_VENDAS"].ToString()),
						PODE_GERAR_BOLETO = Convert.ToBoolean(linha["PODE_GERAR_BOLETO"].ToString()),
						PODE_GERAR_REMESSA_RETORNO = Convert.ToBoolean(linha["PODE_GERAR_REMESSA_RETORNO"].ToString()),
						PODE_CONFIGURAR_DIREITOS = Convert.ToBoolean(linha["PODE_CONFIGURAR_DIREITOS"].ToString()),
						PODE_CADASTRAR_PRODUTOS = Convert.ToBoolean(linha["PODE_CADASTRAR_PRODUTOS"].ToString()),
						PODE_ALTERAR_CORREIO = Convert.ToBoolean(linha["PODE_ALTERAR_CORREIO"].ToString()),
						Perfil = new Perfil() { Codigo = Convert.ToInt32(linha["CODIGO_PERFIL"].ToString()) },
						META_VENDA_MENSAL = Convert.ToDecimal(linha["META_VENDA_MENSAL"]),
						Nome = linha["NOME"].ToString(),
						Sexo = linha["SEXO"].ToString(),
						Email = linha["EMAIL"].ToString(),
						Telefone = linha["TELEFONE"].ToString(),
						Ativo = Convert.ToBoolean(linha["ATIVO"].ToString())
					});

				}
			}
			else
			{
				mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
			}

			return listaFuncionarios;

		}

		public static Funcionario getFuncionario(string login, out string mensagemErro)
		{
			Funcionario func = null;
			mensagemErro = "";

			try
			{
				StringBuilder sql = new StringBuilder();
				Command cmd = new Command();

				sql.Append("SELECT FU.*, PE.NOME, PE.ATIVO, PE.SEXO, PE.EMAIL, PE.TELEFONE FROM FUNCIONARIOS AS FU");
				sql.Append("	LEFT JOIN PESSOAS AS PE ON FU.CODIGO = PE.CODIGO");
				sql.Append("	WHERE LOGIN = '" + login + "'");

				cmd.CommandText = sql.ToString();

				DataTable retorno = cmd.GetData();

				if (retorno.Rows.Count > 0)
				{
					DataRow linha = retorno.Rows[0];

					func = new Funcionario();

					func.Codigo = Convert.ToInt32(linha["CODIGO"].ToString());
					func.Login = linha["LOGIN"].ToString();
					func.Senha = linha["SENHA"].ToString();
					func.CPF = linha["CPF"].ToString();
					func.PODE_VER_RELATORIO_VENDAS = Convert.ToBoolean(linha["PODE_VER_RELATORIO_VENDAS"].ToString());
					func.PODE_GERAR_BOLETO = Convert.ToBoolean(linha["PODE_GERAR_BOLETO"].ToString());
					func.PODE_GERAR_REMESSA_RETORNO = Convert.ToBoolean(linha["PODE_GERAR_REMESSA_RETORNO"].ToString());
					func.PODE_CONFIGURAR_DIREITOS = Convert.ToBoolean(linha["PODE_CONFIGURAR_DIREITOS"].ToString());
					func.PODE_CADASTRAR_PRODUTOS = Convert.ToBoolean(linha["PODE_CADASTRAR_PRODUTOS"].ToString());
					func.PODE_ALTERAR_CORREIO = Convert.ToBoolean(linha["PODE_ALTERAR_CORREIO"].ToString());
					func.Perfil = new Perfil() { Codigo = Convert.ToInt32(linha["CODIGO_PERFIL"].ToString()) };
					func.META_VENDA_MENSAL = Convert.ToDecimal(linha["META_VENDA_MENSAL"]);
					func.Nome = linha["NOME"].ToString();
					func.Sexo = linha["SEXO"].ToString();
					func.Email = linha["EMAIL"].ToString();
					func.Telefone = linha["TELEFONE"].ToString();
					func.Ativo = Convert.ToBoolean(linha["ATIVO"].ToString());

				}
				else
				{
					mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
				}

			}
			catch (Exception ex)
			{
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
			}

			return func;

		}

		public static Funcionario getFuncionarioByCodigo(int codigo, out string mensagemErro)
		{

			StringBuilder sql = new StringBuilder();
			Command cmd = new Command();
			mensagemErro = "";
			Funcionario func = new Funcionario();

			sql.Append("SELECT FU.*, PE.NOME, PE.ATIVO, PE.SEXO, PE.EMAIL, PE.TELEFONE FROM FUNCIONARIOS AS FU");
			sql.Append("	LEFT JOIN PESSOAS AS PE ON FU.CODIGO = PE.CODIGO");
			sql.Append("	WHERE FU.CODIGO = '" + codigo + "'");

			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				DataRow linha = retorno.Rows[0];

				func.Codigo = Convert.ToInt32(linha["CODIGO"].ToString());
				func.Login = linha["LOGIN"].ToString();
				func.Senha = linha["SENHA"].ToString();
				func.CPF = linha["CPF"].ToString();
				func.PODE_VER_RELATORIO_VENDAS = Convert.ToBoolean(linha["PODE_VER_RELATORIO_VENDAS"].ToString());
				func.PODE_GERAR_BOLETO = Convert.ToBoolean(linha["PODE_GERAR_BOLETO"].ToString());
				func.PODE_GERAR_REMESSA_RETORNO = Convert.ToBoolean(linha["PODE_GERAR_REMESSA_RETORNO"].ToString());
				func.PODE_CONFIGURAR_DIREITOS = Convert.ToBoolean(linha["PODE_CONFIGURAR_DIREITOS"].ToString());
				func.PODE_CADASTRAR_PRODUTOS = Convert.ToBoolean(linha["PODE_CADASTRAR_PRODUTOS"].ToString());
				func.PODE_ALTERAR_CORREIO = Convert.ToBoolean(linha["PODE_ALTERAR_CORREIO"].ToString());
				func.Perfil = new Perfil(Convert.ToInt32(linha["CODIGO_PERFIL"].ToString()));
				func.META_VENDA_MENSAL = Convert.ToDecimal(linha["META_VENDA_MENSAL"]);
				func.Nome = linha["NOME"].ToString();
				func.Sexo = linha["SEXO"].ToString();
				func.Email = linha["EMAIL"].ToString();
				func.Telefone = linha["TELEFONE"].ToString();
				func.Ativo = Convert.ToBoolean(linha["ATIVO"].ToString());

				return func;

			}
			else
			{
				mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
				return null;
			}
		}

		public static List<Funcionario> getFuncionarioByPerfil(int codigoPerfil, out string mensagemErro)
		{

			mensagemErro = "";

			List<Funcionario> listaFuncionarios = new List<Funcionario>();
			Command cmd = new Command();

			StringBuilder sql = new StringBuilder();
			sql.AppendLine("SELECT FU.*, PE.NOME, PE.ATIVO, PE.SEXO, PE.EMAIL, PE.TELEFONE FROM FUNCIONARIOS AS FU");
			sql.AppendLine("	LEFT JOIN PESSOAS AS PE ON FU.CODIGO = PE.CODIGO");
			sql.AppendLine("WHERE FU.CODIGO_PERFIL = " + codigoPerfil);
			sql.AppendLine("	AND PE.ATIVO = 1");

			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					listaFuncionarios.Add(new Funcionario()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Login = linha["LOGIN"].ToString(),
						Senha = linha["SENHA"].ToString(),
						CPF = linha["CPF"].ToString(),
						PODE_VER_RELATORIO_VENDAS = Convert.ToBoolean(linha["PODE_VER_RELATORIO_VENDAS"].ToString()),
						PODE_GERAR_BOLETO = Convert.ToBoolean(linha["PODE_GERAR_BOLETO"].ToString()),
						PODE_GERAR_REMESSA_RETORNO = Convert.ToBoolean(linha["PODE_GERAR_REMESSA_RETORNO"].ToString()),
						PODE_CONFIGURAR_DIREITOS = Convert.ToBoolean(linha["PODE_CONFIGURAR_DIREITOS"].ToString()),
						PODE_CADASTRAR_PRODUTOS = Convert.ToBoolean(linha["PODE_CADASTRAR_PRODUTOS"].ToString()),
						PODE_ALTERAR_CORREIO = Convert.ToBoolean(linha["PODE_ALTERAR_CORREIO"].ToString()),
						Perfil = new Perfil() { Codigo = Convert.ToInt32(linha["CODIGO_PERFIL"].ToString()) },
						META_VENDA_MENSAL = Convert.ToDecimal(linha["META_VENDA_MENSAL"]),
						Nome = linha["NOME"].ToString(),
						Sexo = linha["SEXO"].ToString(),
						Email = linha["EMAIL"].ToString(),
						Telefone = linha["TELEFONE"].ToString(),
						Ativo = Convert.ToBoolean(linha["ATIVO"].ToString())
					});

				}
			}
			else
			{
				mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
			}

			return listaFuncionarios;

		}

		public static Funcionario.MetaAgente getMetaAgente(int codigoAgente, DateTime inicioCiclo, DateTime fimCiclo, out string mensagemErro)
		{
			Funcionario.MetaAgente meta = null;
			mensagemErro = "";

			try
			{
				StringBuilder sql = new StringBuilder();
				Command cmd = new Command();

				sql.Append("call bd_ciatreinamento.SP_Get_Meta_Agente("+codigoAgente+", STR_TO_DATE('"+inicioCiclo.Day+","+inicioCiclo.Month+","+inicioCiclo.Year+ "', '%d,%m,%Y'), STR_TO_DATE('" + fimCiclo.Day + "," + fimCiclo.Month + "," + fimCiclo.Year + "', '%d,%m,%Y'));");

				cmd.CommandText = sql.ToString();

				DataTable retorno = cmd.GetData();

				if (retorno.Rows.Count > 0)
				{
					DataRow linha = retorno.Rows[0];

					meta = new Funcionario.MetaAgente();

					meta.CodigoAgente = Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"]);
					meta.ValorTotal = Convert.ToDecimal(linha["SUM(CP.VALOR_TOTAL)"]);
				}
				else
				{
					mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
				}

				if (meta == null)
				{
					meta = new Funcionario.MetaAgente();
					meta.CodigoAgente = codigoAgente;
					meta.ValorTotal = 0;
				}

			}
			catch (Exception ex)
			{
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
			}

			return meta;
		}

	}
}
