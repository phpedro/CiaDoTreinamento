using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class AmbienteDAL
    {
		//INSERT
		public static bool insertAmbiente(Ambiente ambiente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO AMBIENTES");
				sql.Append("	(DESCRICAO, NOME_ARQ_MODELO, DESCRICAO_FUNCAO, AGENTES_QUIMICOS, EXPOSICAO_QUIMICOS, FONTE_QUIMICOS, PROPAGACAO_QUIMICOS, AGENTES_FISICOS, EXPOSICAO_FISICOS, FONTE_FISICOS, PROPAGACAO_FISICOS, AGENTES_BIOLOGICOS, EXPOSICAO_BIOLOGICOS, FONTE_BIOLOGICOS, PROPAGACAO_BIOLOGICOS, ERGONOMICOS, ACIDENTES, CARGOS)");
				sql.Append("	VALUES ");
				sql.Append("	('" + ambiente.Descricao + "', '" + ambiente.NomeArqModelo + "', '" + ambiente.DescricaoAtividade + "', '" + ambiente.AgentesQuimicos + "', '" + ambiente.ExposicaoQuimicos + "', '" + ambiente.FonteQuimicos + "', '" + ambiente.PropagacaoQuimicos + "', '" + ambiente.AgentesFisicos + "', '" + ambiente.ExposicaoFisicos + "', '" + ambiente.FonteFisicos + "', '" + ambiente.PropagacaoFisicos + "', '" + ambiente.AgentesBiologicos + "', '" + ambiente.ExposicaoBiologicos + "', '" + ambiente.FonteBiologicos + "', '" + ambiente.PropagacaoBiologicos + "', '" + ambiente.RiscosErgonomicos + "', '" + ambiente.RiscosAcidentes + "', '" + ambiente.Cargos + "')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o ambiente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o ambiente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public static bool updateAmbiente(Ambiente ambiente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE AMBIENTES");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + ambiente.Descricao + "',");
				sql.Append("	NOME_ARQ_MODELO = '" + ambiente.NomeArqModelo + "',");
				sql.Append("	DESCRICAO_FUNCAO = '" + ambiente.DescricaoAtividade + "',");
				sql.Append("	AGENTES_QUIMICOS = '" + ambiente.AgentesQuimicos + "',");
				sql.Append("	EXPOSICAO_QUIMICOS = '" + ambiente.ExposicaoQuimicos + "',");
				sql.Append("	FONTE_QUIMICOS = '" + ambiente.FonteQuimicos + "',");
				sql.Append("	PROPAGACAO_QUIMICOS = '" + ambiente.PropagacaoQuimicos + "',");
				sql.Append("	AGENTES_FISICOS = '" + ambiente.AgentesFisicos + "',");
				sql.Append("	EXPOSICAO_FISICOS = '" + ambiente.ExposicaoFisicos + "',");
				sql.Append("	FONTE_FISICOS = '" + ambiente.FonteFisicos + "',");
				sql.Append("	PROPAGACAO_FISICOS = '" + ambiente.PropagacaoFisicos + "',");
				sql.Append("	AGENTES_BIOLOGICOS = '" + ambiente.AgentesBiologicos + "',");
				sql.Append("	EXPOSICAO_BIOLOGICOS = '" + ambiente.ExposicaoBiologicos + "',");
				sql.Append("	FONTE_BIOLOGICOS = '" + ambiente.FonteBiologicos + "',");
				sql.Append("	PROPAGACAO_BIOLOGICOS = '" + ambiente.PropagacaoBiologicos + "',");
				sql.Append("	ERGONOMICOS = '" + ambiente.RiscosErgonomicos + "',");
				sql.Append("	ACIDENTES= '" + ambiente.RiscosAcidentes + "',");
				sql.Append("	CARGOS = '" + ambiente.Cargos + "'");
				sql.Append("	WHERE CODIGO = " + ambiente.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o ambiente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o ambiente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public static bool deleteAmbiente(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM AMBIENTES WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o ambiente.";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o ambiente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAR
		public static List<Ambiente> getAmbientes(int? codigo, string descricao, out string mensagemErro)
		{
			List<Ambiente> listaAmbiente = new List<Ambiente>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM AMBIENTES");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(descricao))
			{
				sql.Append("	AND DESCRICAO LIKE CONCAT('%','" + descricao + "','%')");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaAmbiente.Add(new Ambiente()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						NomeArqModelo = linha["NOME_ARQ_MODELO"].ToString(),
						DescricaoAtividade = linha["DESCRICAO_FUNCAO"].ToString(),
						AgentesQuimicos = linha["AGENTES_QUIMICOS"].ToString(),
						ExposicaoQuimicos = linha["EXPOSICAO_QUIMICOS"].ToString(),
						FonteQuimicos = linha["FONTE_QUIMICOS"].ToString(),
						PropagacaoQuimicos = linha["PROPAGACAO_QUIMICOS"].ToString(),
						AgentesFisicos = linha["AGENTES_FISICOS"].ToString(),
						ExposicaoFisicos = linha["EXPOSICAO_FISICOS"].ToString(),
						FonteFisicos = linha["FONTE_FISICOS"].ToString(),
						PropagacaoFisicos = linha["PROPAGACAO_FISICOS"].ToString(),
						AgentesBiologicos = linha["AGENTES_BIOLOGICOS"].ToString(),
						ExposicaoBiologicos = linha["EXPOSICAO_BIOLOGICOS"].ToString(),
						FonteBiologicos = linha["FONTE_BIOLOGICOS"].ToString(),
						PropagacaoBiologicos = linha["PROPAGACAO_BIOLOGICOS"].ToString(),
						RiscosErgonomicos = linha["ERGONOMICOS"].ToString(),
						RiscosAcidentes = linha["ACIDENTES"].ToString(),
						Cargos = linha["CARGOS"].ToString()
					});
				}
			}

			return listaAmbiente;
		}
	}
}
