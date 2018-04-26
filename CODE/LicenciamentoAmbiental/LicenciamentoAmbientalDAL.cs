using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class LicenciamentoAmbientalDAL
    {
		//INSERT 
		public static bool insertLicenciamentoAmbiental(LicenciamentoAmbiental licenciamento, List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneLicenciamentoAmbientalBLL BLL = new TelefoneLicenciamentoAmbientalBLL();

				sql.Append("INSERT INTO LICENCIAMENTO_AMBIENTAL");
				sql.Append("	(RAZAO_SOCIAL, CNPJ, " + (licenciamento.Cidade.Codigo == null ? "" : "CODIGO_CIDADE,") + " ENDERECO, BAIRRO, CEP, DATA_CADASTRO, DESCRICAO)");
				sql.Append("	VALUES");
				sql.Append("	('" + licenciamento.RazaoSocial + "', '" + (licenciamento.CNPJ == null ? "" : licenciamento.CNPJ.RemoveMask()) + "', " + (licenciamento.Cidade.Codigo == null ? "" : "'" + licenciamento.Cidade.Codigo + "',") + " '" + licenciamento.Endereco + "', '" + licenciamento.Bairro + "', '" + (licenciamento.CEP == null ? "" : licenciamento.CEP.RemoveMask()) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + licenciamento.Descricao + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					licenciamento.Codigo = retorno;

					foreach (TelefoneLicenciamentoAmbiental.TelefoneTela item in telefones)
					{

						TelefoneLicenciamentoAmbiental telefone = new TelefoneLicenciamentoAmbiental()
						{
							CodigoConcorrente = (int)licenciamento.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneLicenciamento(telefone, out mensagemErro))
						{
							return false;
						}

					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a empresa de licenciamento ambiental. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateLicenciamentoAmbiental(LicenciamentoAmbiental licenciamento, List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();
				TelefoneLicenciamentoAmbientalBLL BLL = new TelefoneLicenciamentoAmbientalBLL();

				sql.Append("UPDATE LICENCIAMENTO_AMBIENTAL");
				sql.Append("	SET");
				sql.Append("	RAZAO_SOCIAL = '" + licenciamento.RazaoSocial + "',");
				sql.Append("	CNPJ = '" + (licenciamento.CNPJ == null ? "" : licenciamento.CNPJ.RemoveMask()) + "',");
				if (licenciamento.Cidade.Codigo != null && licenciamento.Cidade.Codigo != 0)
				{
					sql.Append("	CODIGO_CIDADE = '" + licenciamento.Cidade.Codigo + "',");
				}
				sql.Append("	ENDERECO = '" + licenciamento.Endereco + "',");
				sql.Append("	BAIRRO = '" + licenciamento.Bairro + "',");
				sql.Append("	CEP = '" + (licenciamento.CEP == null ? "" : licenciamento.CEP.RemoveMask()) + "',");
				sql.Append("	DESCRICAO = '" + licenciamento.Descricao + "'");
				sql.Append("	WHERE CODIGO = " + licenciamento.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{

					//REMOVER TELEFONES ANTIGOS
					BLL.deleteAllTelefoneLicenciamento((int)licenciamento.Codigo, out mensagemErro);

					//CADASTRAR NOVOS TELEFONES
					foreach (TelefoneLicenciamentoAmbiental.TelefoneTela item in telefones)
					{

						TelefoneLicenciamentoAmbiental telefone = new TelefoneLicenciamentoAmbiental()
						{
							CodigoConcorrente = (int)licenciamento.Codigo,
							Descricao = item.telefone.RemoveMaskTelefone(),
							Responsavel = item.responsavel
						};

						if (!BLL.insertTelefoneLicenciamento(telefone, out mensagemErro))
						{
							return false;
						}
					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a empresa de licenciamento ambiental. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteLicenciamentoAmbiental(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM LICENCIAMENTO_AMBIENTAL WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a empresa de licenciamento ambiental. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<LicenciamentoAmbiental> getLicenciamentoAmbiental(int? codigo, string razao, out string mensagemErro)
		{
			List<LicenciamentoAmbiental> listaLicenciamentoAmbiental = new List<LicenciamentoAmbiental>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT LA.*, CI.ESTADO FROM LICENCIAMENTO_AMBIENTAL AS LA");
			sql.Append("	LEFT JOIN CIDADES AS CI ON LA.CODIGO_CIDADE = CI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND LA.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(razao))
			{
				sql.Append("	AND LA.RAZAO_SOCIAL LIKE CONCAT('%','" + razao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaLicenciamentoAmbiental.Add(new LicenciamentoAmbiental()
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

			return listaLicenciamentoAmbiental;
		}
	}
}
