using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TelefoneClienteDAL
    {

		//INSERT
		public static bool InserTelefoneCliente(TelefoneCliente telefone, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefonesBLL BLL = new TelefonesBLL();

				int retorno = BLL.insertTelefone(telefone, out mensagemErro);

				if (retorno > 0)
				{
					telefone.Codigo = retorno;

					sql.Append("INSERT INTO CLIENTES_TELEFONES");
					sql.Append("	(CODIGO_TELEFONE, CODIGO_CLIENTE)");
					sql.Append("	VALUES");
					sql.Append("	('" + telefone.Codigo + "', '" + telefone.cliente.Codigo + "')");

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
						return false;
					}

				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool UpdateTelefoneCliente(TelefoneCliente telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE TELEFONES AS TE");
				sql.Append("	SET");
				//DADOS DA PESSOA
				sql.Append("	TE.DESCRICAO = '" + telefone.Descricao.RemoveMaskTelefone() + "',");
				sql.Append("	TE.OBSERVACAO = '" + telefone.Observacao + "'");
				sql.Append("	WHERE TE.CODIGO = " + telefone.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool DeleteTelefoneCliente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CIENTES_TELEFONES WHERE CODIGO_TELEFONE = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					sql.Clear();

					sql.Append("DELETE FROM TELEFONES WHERE CODIGO = " + codigo);

					cmd.CommandText = sql.ToString();

					retorno = cmd.Execute();

					if (retorno > 0)
					{
						return true;
					}
					else
					{
						mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
						return false;
					}
				}
				else
				{
					mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<TelefoneCliente> GetTelefonesCliente(int codigoCliente, out string mensagemErro)
		{

			List<TelefoneCliente> listaTelefones = new List<TelefoneCliente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CT.CODIGO_CLIENTE, TE.* FROM CLIENTES_TELEFONES AS CT");
			sql.Append("    LEFT JOIN TELEFONES AS TE ON TE.CODIGO = CT.CODIGO_TELEFONE");
			sql.Append("	WHERE 1 = 1");

			if (codigoCliente != 0)
			{
				sql.Append("	AND CT.CODIGO_CLIENTE = " + codigoCliente);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTelefones.Add(new TelefoneCliente()
					{
						cliente = new Cliente() { Codigo = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()) },
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Observacao = linha["OBSERVACAO"].ToString(),
						tipo = Enumeradores.Tipo.Old
					});
				}
			}

			return listaTelefones;

		}
	}
}
