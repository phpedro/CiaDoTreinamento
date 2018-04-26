using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ContabilidadeDAL
    {

		//INSERT 
		public static bool insertContabilidade(Contabilidade contabilidade, List<TelefoneContabilidade.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneContabilidadeBLL BLL = new TelefoneContabilidadeBLL();

				sql.Append("INSERT INTO CONTABILIDADE");
				sql.Append("	(RAZAO_SOCIAL, CNPJ, " + (contabilidade.Cidade.Codigo == null ? "" : "CODIGO_CIDADE,") + " ENDERECO, BAIRRO, CEP, DATA_CADASTRO, DESCRICAO)");
				sql.Append("	VALUES");
				sql.Append("	('" + contabilidade.RazaoSocial + "', '" + (contabilidade.CNPJ == null ? "" : contabilidade.CNPJ.RemoveMask()) + "', " + (contabilidade.Cidade.Codigo == null ? "" : "'" + contabilidade.Cidade.Codigo + "',") + " '" + contabilidade.Endereco + "', '" + contabilidade.Bairro + "', '" + (contabilidade.CEP == null ? "" : contabilidade.CEP.RemoveMask()) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + contabilidade.Descricao + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					contabilidade.Codigo = retorno;

					foreach (TelefoneContabilidade.TelefoneTela item in telefones)
					{

						TelefoneContabilidade telefone = new TelefoneContabilidade()
						{
							CodigoContabilidade = (int)contabilidade.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneContabilidade(telefone, out mensagemErro))
						{
							return false;
						}

					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a empresa de contabilidade. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateContabilidade(Contabilidade contabilidade, List<TelefoneContabilidade.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneContabilidadeBLL BLL = new TelefoneContabilidadeBLL();

				sql.Append("UPDATE CONTABILIDADE");
				sql.Append("	SET");
				sql.Append("	RAZAO_SOCIAL = '" + contabilidade.RazaoSocial + "',");
				sql.Append("	CNPJ = '" + (contabilidade.CNPJ == null ? "" : contabilidade.CNPJ.RemoveMask()) + "',");
				if (contabilidade.Cidade.Codigo != null && contabilidade.Cidade.Codigo != 0)
				{
					sql.Append("	CODIGO_CIDADE = '" + contabilidade.Cidade.Codigo + "',");
				}
				sql.Append("	ENDERECO = '" + contabilidade.Endereco + "',");
				sql.Append("	BAIRRO = '" + contabilidade.Bairro + "',");
				sql.Append("	CEP = '" + (contabilidade.CEP == null ? "" : contabilidade.CEP.RemoveMask()) + "',");
				sql.Append("	DESCRICAO = '" + contabilidade.Descricao + "'");
				sql.Append("	WHERE CODIGO = " + contabilidade.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					//REMOVER TELEFONES ANTIGOS
					BLL.deleteAllTelefoneContabilidade((int)contabilidade.Codigo, out mensagemErro);

					//CADASTRAR NOVOS TELEFONES
					foreach (TelefoneContabilidade.TelefoneTela item in telefones)
					{

						TelefoneContabilidade telefone = new TelefoneContabilidade()
						{
							CodigoContabilidade = (int)contabilidade.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneContabilidade(telefone, out mensagemErro))
						{
							return false;
						}
					}


					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a empresa de contabilidade. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteContabilidade(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CONTABILIDADE WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a empresa de contabilidade. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a empresa de contabilidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Contabilidade> getContabilidades(int? codigo, string razao, out string mensagemErro)
		{
			List<Contabilidade> listaContabilidades = new List<Contabilidade>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CO.*, CI.ESTADO FROM CONTABILIDADE AS CO");
			sql.Append("	LEFT JOIN CIDADES AS CI ON CO.CODIGO_CIDADE = CI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CO.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(razao))
			{
				sql.Append("	AND CO.RAZAO_SOCIAL LIKE CONCAT('%','" + razao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaContabilidades.Add(new Contabilidade()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						Cidade = new Cidade()
						{
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

			return listaContabilidades;
		}

	}
}
