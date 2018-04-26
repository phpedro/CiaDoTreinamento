using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ParceiroDAL
    {
		//INSERT 
		public static bool insertParceiro(Parceiro parceiro, List<TelefoneParceiro.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";
			TelefoneParceiroBLL BLL = new TelefoneParceiroBLL();

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PARCEIROS");
				sql.Append("	(CODIGO_CIDADE, DESCRICAO, ENDERECO, OBSERVACAO, TIPO_PARCEIRO, BAIRRO, CUSTO, RESPONSAVEL, EMAIL, PASSA_CARTAO)");
				sql.Append("	VALUES");
				sql.Append("	('" + parceiro.Cidade.Codigo + "', '" + parceiro.Descricao + "','" + parceiro.Endereco + "','" + parceiro.Observacao + "','" + parceiro.TipoParceiro + "','" + parceiro.Bairro + "','" + parceiro.Custo.ToString().Replace(",", ".") + "','" + parceiro.Responsavel + "','" + parceiro.Email + "','" + (parceiro.PassaCartao ? 1 : 0) + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					parceiro.Codigo = retorno;

					foreach (TelefoneParceiro.TelefoneTela item in telefones)
					{

						TelefoneParceiro telefone = new TelefoneParceiro()
						{
							Parceiro = parceiro,
							Telefone = new Telefones()
							{
								Descricao = item.telefone.RemoveMaskTelefone(),
								Observacao = item.responsavel
							}
						};

						if (!BLL.insertTelefoneParceiro(telefone, out mensagemErro))
						{
							return false;
						}
					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o parceiro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateParceiro(Parceiro parceiro, List<TelefoneParceiro.TelefoneTela> telefones, out string mensagemErro)
		{

			mensagemErro = "";
			TelefoneParceiroBLL BLL = new TelefoneParceiroBLL();

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE PARCEIROS");
				sql.Append("	SET");
				sql.Append("	CODIGO_CIDADE = '" + parceiro.Cidade.Codigo + "',");
				sql.Append("	DESCRICAO = '" + parceiro.Descricao + "',");
				sql.Append("	ENDERECO = '" + parceiro.Endereco + "',");
				sql.Append("	OBSERVACAO = '" + parceiro.Observacao + "',");
				sql.Append("	TIPO_PARCEIRO = '" + parceiro.TipoParceiro + "',");
				sql.Append("	BAIRRO = '" + parceiro.Bairro + "',");
				sql.Append("	CUSTO = '" + parceiro.Custo.ToString().Replace(",",".") + "',");
				sql.Append("	RESPONSAVEL = '" + parceiro.Responsavel + "',");
				sql.Append("	EMAIL = '" + parceiro.Endereco + "',");
				sql.Append("	PASSA_CARTAO = '" + (parceiro.PassaCartao ? 1 : 0) + "'");
				sql.Append("	WHERE CODIGO = " + parceiro.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					//REMOVER TELEFONES ANTIGOS
					BLL.deleteAllTelefoneParceiro((int)parceiro.Codigo, out mensagemErro);

					//CADASTRAR NOVOS TELEFONES
					foreach (TelefoneParceiro.TelefoneTela item in telefones)
					{

						TelefoneParceiro telefone = new TelefoneParceiro()
						{
							Parceiro = parceiro,
							Telefone = new Telefones()
							{
								Descricao = item.telefone.RemoveMaskTelefone(),
								Observacao = item.responsavel
							}
						};

						if (!BLL.insertTelefoneParceiro(telefone, out mensagemErro))
						{
							return false;
						}
					}

					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o parceiro. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteParceiro(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PARCEIROS WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o parceiro. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Parceiro> getParceiros(int? codigo, string descricao, string estado, int? codigoCidade, string Tipo, out string mensagemErro)
		{
			List<Parceiro> listaParceiros = new List<Parceiro>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT PA.*, CI.DESCRICAO AS NOME_CIDADE, CI.ESTADO AS ESTADO");
			sql.Append("	 FROM PARCEIROS AS PA");
			sql.Append("	LEFT JOIN CIDADES AS CI ON PA.CODIGO_CIDADE = CI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND PA.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(descricao))
			{
				sql.Append("	AND PA.DESCRICAO LIKE CONCAT('%','" + descricao + "','%')");
			}

			if (!String.IsNullOrEmpty(estado))
			{
				sql.Append("	AND CI.ESTADO = '" + estado + "'");
			}

			if (codigoCidade != null && codigoCidade != 0)
			{
				sql.Append("	AND PA.CODIGO_CIDADE = " + codigoCidade);
			}

			if (!String.IsNullOrEmpty(Tipo))
			{
				sql.Append("	AND PA.TIPO_PARCEIRO LIKE CONCAT('%','" + Tipo + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaParceiros.Add(new Parceiro()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cidade = new Cidade() {
							Codigo = Convert.ToInt32(linha["CODIGO_CIDADE"].ToString()), Descricao = linha["NOME_CIDADE"].ToString(),
							Estado = linha["ESTADO"].ToString()
						},
						Descricao = linha["DESCRICAO"].ToString(),
						Endereco = linha["ENDERECO"].ToString(),
						Observacao = linha["OBSERVACAO"].ToString(),
						TipoParceiro = linha["TIPO_PARCEIRO"].ToString(),
						Bairro = linha["BAIRRO"].ToString(),
						Custo = Convert.ToDecimal(linha["CUSTO"].ToString()),
						Responsavel = linha["RESPONSAVEL"].ToString(),
						Email = linha["EMAIL"].ToString(),
						PassaCartao = Convert.ToBoolean(linha["PASSA_CARTAO"])
					});
				}
			}

			return listaParceiros;
		}

		public static List<string> getTiposParceiros(out string mensagemErro)
		{
			mensagemErro = "";
			List<string> listaTipos = new List<string>();

			StringBuilder sql = new StringBuilder();

			sql.Append("SELECT DISTINCT(TIPO_PARCEIRO) AS TIPO FROM PARCEIROS ORDER BY TIPO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTipos.Add(linha["TIPO"].ToString());
				}
			}

			return listaTipos;
		}
	}
}
