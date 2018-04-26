using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class PessoaDAL
    {
		public static bool updateSenhaPessoa(int codigoPessoa, string senha)
		{

			Command cmd = new Command();

			StringBuilder sql = new StringBuilder();

			sql.Append("UPDATE FUNCIONARIOS SET SENHA = '" + senha + "' WHERE CODIGO=" + codigoPessoa + ";");

			cmd.CommandText = sql.ToString();

			int retorno = cmd.Execute();

			if (retorno > 0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		//CONSULTAS
		public static Pessoa getPessoaByLogin(string login, out string mensagemErro)
		{
			Pessoa pessoa = null;
			Command cmd = new Command();

			mensagemErro = "";

			StringBuilder sql = new StringBuilder();

			sql.Append("SELECT PE.* FROM PESSOAS AS PE");
			sql.Append("	LEFT JOIN FUNCIONARIOS AS FU ON PE.CODIGO = FU.CODIGO");
			sql.Append("	WHERE FU.LOGIN = '" + login + "'");

			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				DataRow linha = retorno.Rows[0];

				pessoa = new Pessoa();

				pessoa.Codigo = Convert.ToInt32(linha["CODIGO"].ToString());
				pessoa.Nome = linha["NOME"].ToString();
				pessoa.Sexo = linha["SEXO"].ToString();
				pessoa.Email = linha["EMAIL"].ToString();
				pessoa.Telefone = linha["TELEFONE"].ToString();
				pessoa.Ativo = Convert.ToBoolean(linha["ATIVO"].ToString());

			}
			else
			{
				mensagemErro = "Não foi possível localizar o usuário informado! Contate o administrador!";
			}

			return pessoa;

		}

	}
}
