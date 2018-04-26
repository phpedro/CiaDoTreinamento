using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class RegistroCorreioDAL
    {

		//INSERT 
		public static int insertRegistroCorreio(RegistroCorreio registro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO REGISTRO_CORREIO");
				sql.AppendLine("	(CODIGO_PEDIDO, CODIGO_CLIENTE, CODIGO_POSTAGEM, DESCRICAO, COMENTARIO, DATA_POSTAGEM)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + registro.CodigoPedido+ "', '" + registro.cliente.Codigo + "', '" + (registro.CodigoPostagem == null ? " " : registro.CodigoPostagem) + "','" + registro.Descricao + "', '" + registro.Comentario + "', '" + registro.dataPostagem.ToString("yyyy-MM-dd") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					return retorno;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o registro. Contate o suporte!";
					return -1;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}

		}

		public static bool insertRegistroCorreioEmail(int codigoRegistroEmail, int codigoEmail, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO REGISTRO_CORREIO_EMAIL");
				sql.AppendLine("	(CODIGO_REGISTRO, CODIGO_EMAIL)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + codigoRegistroEmail + "', '" + codigoEmail + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o registro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateRegistroCorreio(RegistroCorreio registro, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE REGISTRO_CORREIO");
				sql.AppendLine("	SET");
				sql.AppendLine("	CODIGO_PEDIDO = '" + registro.CodigoPedido + "',");
				sql.AppendLine("	CODIGO_CLIENTE = '" + registro.cliente.Codigo + "',");
				sql.AppendLine("	CODIGO_POSTAGEM = '" + registro.CodigoPostagem + "',");
				sql.AppendLine("	DESCRICAO = '" + registro.Descricao + "',");
				sql.AppendLine("	COMENTARIO = '" + registro.Comentario + "',");
				sql.AppendLine("	DATA_POSTAGEM = '" + registro.dataPostagem.ToString("yyyy-MM-dd") + "'");
				sql.AppendLine("	WHERE CODIGO = " + registro.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o registro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteRegistroCorreio(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM REGISTRO_CORREIO WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o registro. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public static bool deleteRegistroCorreioEmail(int codigoRegistroCorreio, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM REGISTRO_CORREIO_EMAIL WHERE CODIGO_REGISTRO = " + codigoRegistroCorreio);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				return true;
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static string getDescricaoEmailsRegitroCorreio(int codigoRegistro, out string mensagemErro)
		{
			string resultado = "";
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT EC.DESCRICAO");
			sql.AppendLine("	FROM REGISTRO_CORREIO_EMAIL RCE");
			sql.AppendLine("		INNER JOIN EMAILS_CLIENTES AS EC ON EC.CODIGO = RCE.CODIGO_EMAIL");
			sql.AppendLine("WHERE RCE.CODIGO_REGISTRO = " + codigoRegistro);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					resultado += linha["DESCRICAO"].ToString() + ",";
				}

				resultado = resultado.Substring(0, resultado.Length - 1);
			}

			return resultado;
		}

		public static List<RegistroCorreio> getRegistroCorreioByCodigo(int codigoRegistro, out string mensagemErro)
		{
			List<RegistroCorreio> lista = new List<RegistroCorreio>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT RC.* FROM REGISTRO_CORREIO AS RC");
			sql.AppendLine("WHERE RC.CODIGO = " + codigoRegistro);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new RegistroCorreio()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"]),
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"]),
						cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"])),
						CodigoPostagem = linha["CODIGO_POSTAGEM"].ToString(),
						Descricao = linha["DESCRICAO"].ToString(),
						Comentario = linha["COMENTARIO"].ToString(),
						dataPostagem = Convert.ToDateTime(linha["DATA_POSTAGEM"])
					});

				}
			}

			return lista;
		}

		public static List<RegistroCorreio> getRegistrosCorreio(string CNPJ, string razaoSocial, int? codigoCliente, string CPF, string nomeCliente, 
															int? codigoPedido, DateTime? dataInicio, DateTime? dataFim, out string mensagemErro)
		{
			List<RegistroCorreio> lista = new List<RegistroCorreio>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT RC.* FROM REGISTRO_CORREIO AS RC");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON RC.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("WHERE 1 = 1");

			if (!String.IsNullOrEmpty(CNPJ))
			{
				sql.AppendLine("	AND CL.CNPJ = " + CNPJ.RemoveMask());
			}

			if (!String.IsNullOrEmpty(razaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL = " + razaoSocial);
			}

			if (codigoCliente.HasValue && codigoCliente> 0)
			{
				sql.AppendLine("	AND CL.CODIGO = " + codigoCliente);
			}

			if (!String.IsNullOrEmpty(CPF))
			{
				sql.AppendLine("	AND CL.CPF_CLIENTE = " + CPF.RemoveMask());
			}

			if (!String.IsNullOrEmpty(nomeCliente))
			{
				sql.AppendLine("	AND CL.NOME_CLIENTE = " + CPF.RemoveMask());
			}

			if (codigoPedido.HasValue && codigoPedido > 0)
			{
				sql.AppendLine("	AND RC.CODIGO_PEDIDO = " + codigoPedido);
			}

			sql.AppendLine("	AND RC.DATA_POSTAGEM BETWEEN ");
			sql.AppendLine("		'" + (dataInicio.HasValue ? Convert.ToDateTime(dataInicio).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")) + "' AND ");
			sql.AppendLine("		'" + (dataFim.HasValue ? Convert.ToDateTime(dataFim).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss")) + "'");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new RegistroCorreio() {
						Codigo = Convert.ToInt32(linha["CODIGO"]),
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"]),
						cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"])),
						CodigoPostagem = linha["CODIGO_POSTAGEM"].ToString(),
						Descricao = linha["DESCRICAO"].ToString(),
						Comentario = linha["COMENTARIO"].ToString(),
						dataPostagem = Convert.ToDateTime(linha["DATA_POSTAGEM"]),
						CodigoEmail = Convert.ToInt32(linha["CODIGO_EMAIL"])
					});

				}
			}

			return lista;
			
		}

		public static List<RegistroCorreioEmail> getEmailsRegistroCorreio(int codigoRegistro, out string mensagemErro)
		{
			List<RegistroCorreioEmail> lista = new List<RegistroCorreioEmail>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT * FROM REGISTRO_CORREIO_EMAIL");
			sql.AppendLine("WHERE CODIGO_REGISTRO = " + codigoRegistro);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					lista.Add(new RegistroCorreioEmail()
					{
						CodigoRegistroCorreio = Convert.ToInt32(linha["CODIGO_REGISTRO"]),
						CodigoEmail = Convert.ToInt32(linha["CODIGO_EMAIL"])
					});

				}
			}

			return lista;
		}
    }
}
