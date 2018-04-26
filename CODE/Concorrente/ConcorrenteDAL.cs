using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ConcorrenteDAL
    {

		//INSERT 
		public static bool insertConcorrente(Concorrente concorrente, List<TelefoneConcorrente.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneConcorrenteBLL BLL = new TelefoneConcorrenteBLL();

				sql.Append("INSERT INTO CONCORRENTES");
				sql.Append("	(RAZAO_SOCIAL, CNPJ, " + (concorrente.Cidade.Codigo == null ? "" : "CODIGO_CIDADE,") + " ENDERECO, BAIRRO, CEP, DATA_CADASTRO, DESCRICAO)");
				sql.Append("	VALUES");
				sql.Append("	('" + concorrente.RazaoSocial + "', '" + (concorrente.CNPJ == null ? "" : concorrente.CNPJ.RemoveMask()) + "', " + (concorrente.Cidade.Codigo == null ? "" : "'" + concorrente.Cidade.Codigo + "',") + " '" + concorrente.Endereco + "', '" + concorrente.Bairro + "', '" + (concorrente.CEP == null ? "" : concorrente.CEP.RemoveMask()) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + concorrente.Descricao + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					concorrente.Codigo = retorno;

					foreach (TelefoneConcorrente.TelefoneTela item in telefones)
					{

						TelefoneConcorrente telefone = new TelefoneConcorrente()
						{
							CodigoConcorrente = (int)concorrente.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneConcorrente(telefone, out mensagemErro))
						{
							return false;
						}

					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o concorrente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateConcorrente(Concorrente concorrente, List<TelefoneConcorrente.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneConcorrenteBLL BLL = new TelefoneConcorrenteBLL();

				sql.Append("UPDATE CONCORRENTES");
				sql.Append("	SET");
				sql.Append("	RAZAO_SOCIAL = '" + concorrente.RazaoSocial + "',");
				sql.Append("	CNPJ = '" + (concorrente.CNPJ == null ? "" : concorrente.CNPJ.RemoveMask()) + "',");
				if (concorrente.Cidade.Codigo != null && concorrente.Cidade.Codigo != 0)
				{
					sql.Append("	CODIGO_CIDADE = '" + concorrente.Cidade.Codigo + "',");
				}
				sql.Append("	ENDERECO = '" + concorrente.Endereco + "',");
				sql.Append("	BAIRRO = '" + concorrente.Bairro + "',");
				sql.Append("	CEP = '" + (concorrente.CEP == null ? "" : concorrente.CEP.RemoveMask()) + "',");
				sql.Append("	DESCRICAO = '" + concorrente.Descricao + "'");
				sql.Append("	WHERE CODIGO = " + concorrente.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					//REMOVER TELEFONES ANTIGOS
					BLL.deleteAllTelefoneConcorrente((int)concorrente.Codigo, out mensagemErro);

					//CADASTRAR NOVOS TELEFONES
					foreach (TelefoneConcorrente.TelefoneTela item in telefones)
					{

						TelefoneConcorrente telefone = new TelefoneConcorrente()
						{
							CodigoConcorrente = (int)concorrente.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneConcorrente(telefone, out mensagemErro))
						{
							return false;
						}
					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o concorrente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteConcorrente(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneConcorrenteBLL BLL = new TelefoneConcorrenteBLL();
				//REMOVER TELEFONES ANTIGOS
				BLL.deleteAllTelefoneConcorrente(codigo, out mensagemErro);

				sql.Append("DELETE FROM CONCORRENTES WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o concorrente. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Concorrente> getConcorrentes(int? codigo, string razao, out string mensagemErro)
		{
			List<Concorrente> listaConcorrentes = new List<Concorrente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CC.*, CI.ESTADO FROM CONCORRENTES AS CC");
			sql.Append("	LEFT JOIN CIDADES AS CI ON CC.CODIGO_CIDADE = CI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CC.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(razao))
			{
				sql.Append("	AND CC.RAZAO_SOCIAL LIKE CONCAT('%','" + razao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaConcorrentes.Add(new Concorrente()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						Cidade = new Cidade() {
							Codigo = (linha["CODIGO_CIDADE"].ToString() == "" ? 0 : Convert.ToInt32(linha["CODIGO_CIDADE"].ToString())),
							Estado = (linha["ESTADO"].ToString() == "" ? "" : linha["ESTADO"].ToString())
						},
						Endereco = linha["ENDERECO"].ToString(),
						Bairro = linha["BAIRRO"].ToString(),
						CEP = linha["CEP"].ToString(),
						DataCadastro = (linha["DATA_CADASTRO"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(linha["DATA_CADASTRO"].ToString())),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaConcorrentes;
		}

		public static List<Concorrente> getConcorrentesByCliente(int? codigoCliente, out string mensagemErro)
		{

			List<Concorrente> listaConcorrentes = new List<Concorrente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CO.* FROM CONCORRENTES AS CO");
			sql.Append("    LEFT JOIN CLIENTE_CONCORRENTE AS CC ON CC.CODIGO_CONCORRENTE = CO.CODIGO");
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
					listaConcorrentes.Add(new Concorrente()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						Cidade = new Cidade() { Codigo = (linha["CODIGO_CIDADE"].ToString() == "" ? 0 : Convert.ToInt32(linha["CODIGO_CIDADE"].ToString())) },
						Endereco = linha["ENDERECO"].ToString(),
						Bairro = linha["BAIRRO"].ToString(),
						CEP = linha["CEP"].ToString(),
						DataCadastro = (linha["DATA_CADASTRO"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(linha["DATA_CADASTRO"].ToString())),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaConcorrentes;

		}

	}
}
