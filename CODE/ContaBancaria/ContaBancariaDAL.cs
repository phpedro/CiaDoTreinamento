using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ContaBancariaDAL
    {
		//INSERT 
		public static bool insertContaBancaria(ContaBancaria conta, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO CONTAS_BANCARIAS");
				sql.Append("	(CODIGO, CODIGO_BANCO, DESCRICAO, ATIVA, DIGITO_VERIFICADOR, CODIGO_BENEFICIARIO, INCREMENTAL_BOLETOS, ");
				sql.Append("	CODIGO_AGENCIA, DIGITO_VERIFICADOR_BENEFICIARIO, RAZAO_SOCIAL, CNPJ, DV_AGENCIA, CODIGO_CLIENTE, ");
				sql.Append("	VALOR_POR_BOLETO, INCREMENTAL_REMESSA, ENDERECO, CODIGO_CIDADE, DESCRICAO_BAIRRO, TELEFONE, ");
				sql.Append("	NOME_REPRESENTANTE, CPF, CODIGO_CONTA_ASC, CODIGO_CONDICAO_ASC)");
				sql.Append("	VALUES");
				sql.Append("	('" + conta.Codigo + "','" + conta.CodigoBanco + "', '" + conta.Descricao + "', '" + (conta.Ativo ? 1 : 0) + "', '" + conta.DigitoVerificador + "', '" + conta.CodigoBeneficiario + "', '" + conta.IncrementalBoletos + "', ");
				sql.Append("	'" + conta.CodigoAgencia + "', '" + conta.DigitoVerificadorBeneficiario + "', '" + conta.RazaoSocial + "', '" + conta.CNPJ + "', '" + conta.DigitoVerificadorAgencia + "', '" + conta.CodigoCliente + "', ");
				sql.Append("	'" + conta.ValorPorBoleto + "', '" + conta.IncrementalRemessa + "', '" + conta.Endereco + "', '" + conta.Cidade.Codigo + "', '" + conta.DescricaoBairro + "', '" + conta.Telefone + "', ");
				sql.Append("	'" + conta.NomeRepresentante + "', '" + conta.CPF.RemoveMask() + "', '" + conta.CodigoContaASC + "', '" + conta.CodigoCondicaoASC + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a conta bancária. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateContaBancaria(ContaBancaria conta, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CONTAS_BANCARIAS");
				sql.Append("	SET");
				sql.Append("	CODIGO_BANCO = '" + conta.CodigoBanco + "',");
				sql.Append("	DESCRICAO = '" + conta.Descricao + "',");
				sql.Append("	ATIVA = '" + (conta.Ativo ? 1 : 0) + "',");
				sql.Append("	DIGITO_VERIFICADOR = '" + conta.DigitoVerificador + "',");
				sql.Append("	CODIGO_BENEFICIARIO = '" + conta.CodigoBeneficiario + "',");
				sql.Append("	INCREMENTAL_BOLETOS = '" + conta.IncrementalBoletos + "',");
				sql.Append("	CODIGO_AGENCIA = '" + conta.CodigoAgencia + "',");
				sql.Append("	DIGITO_VERIFICADOR_BENEFICIARIO = '" + conta.DigitoVerificadorBeneficiario + "',");
				sql.Append("	RAZAO_SOCIAL = '" + conta.RazaoSocial + "',");
				sql.Append("	CNPJ = '" + conta.CNPJ.RemoveMask() + "',");
				sql.Append("	DV_AGENCIA = '" + conta.DigitoVerificadorAgencia + "',");
				sql.Append("	CODIGO_CLIENTE = '" + conta.CodigoCliente + "',");
				sql.Append("	VALOR_POR_BOLETO = '" + conta.ValorPorBoleto.ToString().Replace(",",".") + "',");
				sql.Append("	INCREMENTAL_REMESSA = '" + conta.IncrementalRemessa + "',");
				sql.Append("	ENDERECO = '" + conta.Endereco + "',");
				sql.Append("	CODIGO_CIDADE = '" + conta.Cidade.Codigo + "',");
				sql.Append("	DESCRICAO_BAIRRO = '" + conta.DescricaoBairro + "',");
				sql.Append("	TELEFONE = '" + conta.Telefone.RemoveMaskTelefone() + "',");
				sql.Append("	NOME_REPRESENTANTE = '" + conta.NomeRepresentante + "',");
				sql.Append("	CPF = '" + conta.CPF.RemoveMask() + "',");
				sql.Append("	CODIGO_CONTA_ASC = '" + conta.CodigoContaASC + "',");
				sql.Append("	CODIGO_CONDICAO_ASC = '" + conta.CodigoCondicaoASC + "'");
				sql.Append("	WHERE CODIGO = " + conta.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a conta bancária. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteContaBancaria(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CONTAS_BANCARIAS WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a conta bancária. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<ContaBancaria> getContas(int? codigo, out string mensagemErro)
		{
			List<ContaBancaria> listaContas = new List<ContaBancaria>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CB.*, CI.ESTADO FROM CONTAS_BANCARIAS AS CB");
			sql.Append("	LEFT JOIN CIDADES AS CI ON CB.CODIGO_CIDADE = CI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CB.CODIGO = " + codigo);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaContas.Add(new ContaBancaria()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						CodigoBanco = Convert.ToInt32(linha["CODIGO_BANCO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						Ativo = (linha["ATIVA"].ToString() == "1" ? true : false),
						DigitoVerificador = Convert.ToInt32(linha["DIGITO_VERIFICADOR"].ToString()),
						CodigoBeneficiario = Convert.ToInt32(linha["CODIGO_BENEFICIARIO"].ToString()),
						IncrementalBoletos = Convert.ToInt32(linha["INCREMENTAL_BOLETOS"].ToString()),
						CodigoAgencia = Convert.ToInt32(linha["CODIGO_AGENCIA"].ToString()),
						DigitoVerificadorBeneficiario = Convert.ToInt32(linha["DIGITO_VERIFICADOR_BENEFICIARIO"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						DigitoVerificadorAgencia = Convert.ToInt32(linha["DV_AGENCIA"].ToString()),
						CodigoCliente = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()),
						ValorPorBoleto = Convert.ToDecimal(linha["VALOR_POR_BOLETO"].ToString()),
						IncrementalRemessa = Convert.ToInt32(linha["INCREMENTAL_REMESSA"].ToString()),
						Endereco = linha["ENDERECO"].ToString(),
						Cidade = new Cidade() {
							Codigo = Convert.ToInt32(linha["CODIGO_CIDADE"].ToString()),
							Estado = linha["ESTADO"].ToString()
						},
						DescricaoBairro = linha["DESCRICAO_BAIRRO"].ToString(),
						Telefone = linha["TELEFONE"].ToString(),
						NomeRepresentante = linha["NOME_REPRESENTANTE"].ToString(),
						CPF = linha["CPF"].ToString(),
						CodigoContaASC = Convert.ToInt32(linha["CODIGO_CONTA_ASC"].ToString()),
						CodigoCondicaoASC = Convert.ToInt32(linha["CODIGO_CONDICAO_ASC"].ToString())
					});
				}
			}

			return listaContas;
		}
	}
}
