using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class CidadeDAL
    {
		//INSERT 
		public static bool insertCidade(Cidade cidade, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO CIDADES");
				sql.Append("	(DESCRICAO, TELEFONE_HOSPITAL, TELEFONE_PREFEITURA, ESTADO, CODIGO_MESO, CODIGO_MICRO, LATITUDE, LONGITUDE)");
				sql.Append("	VALUES");
				sql.Append("	('" + cidade.Descricao + "','" + (cidade.TelefoneHospital == null ? "" : cidade.TelefoneHospital.RemoveMaskTelefone()) + "', '" + (cidade.TelefonePrefeitura == null ? "" : cidade.TelefonePrefeitura.RemoveMask()) + "', '" + cidade.Estado + "','" + cidade.Meso.Codigo + "','" + cidade.Micro.Codigo + "', '" + (String.IsNullOrEmpty(cidade.Latitude) ? "0" : cidade.Latitude) + "', '"+ (String.IsNullOrEmpty(cidade.Longitude) ? "0" : cidade.Longitude) + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar a cidade. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateCidade(Cidade cidade, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CIDADES");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + cidade.Descricao + "',");
				sql.Append("	TELEFONE_HOSPITAL = '" + (cidade.TelefoneHospital == null ? "" : cidade.TelefoneHospital.RemoveMaskTelefone()) + "',");
				sql.Append("	TELEFONE_PREFEITURA = '" + (cidade.TelefonePrefeitura == null ? "" : cidade.TelefonePrefeitura.RemoveMaskTelefone()) + "',");
				sql.Append("	ESTADO = '" + cidade.Estado + "',");
				sql.Append("	CODIGO_MESO = '" + cidade.Meso.Codigo + "',");
				sql.Append("	CODIGO_MICRO = '" + cidade.Micro.Codigo + "',");
				sql.Append("	LATITUDE = '" + (String.IsNullOrEmpty(cidade.Latitude) ? "0" : cidade.Latitude) + "',");
				sql.Append("	LONGITUDE = '" + (String.IsNullOrEmpty(cidade.Longitude) ? "0" : cidade.Longitude) + "'");
				sql.Append("	WHERE CODIGO = " + cidade.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a cidade. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteCidade(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM CIDADES WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover a cidade. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a cidade. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Cidade> getCidade(int? codigo, string descricao, int? meso, int? micro, out string mensagemErro)
		{
			List<Cidade> listaCidades = new List<Cidade>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CID.*, ME.DESCRICAO AS MESO, MI.DESCRICAO AS MICRO");
			sql.Append("	FROM CIDADES AS CID");
			sql.Append("	LEFT JOIN MESO AS ME ON CID.CODIGO_MESO = ME.CODIGO");
			sql.Append("	LEFT JOIN MICRO AS MI ON CID.CODIGO_MICRO = MI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigo != null && codigo != 0)
			{
				sql.Append("	AND CID.CODIGO = " + codigo);
			}

			if (!String.IsNullOrEmpty(descricao))
			{
				sql.Append("	AND CID.DESCRICAO LIKE CONCAT('%','" + descricao + "','%')");
			}

			if (meso != null && meso != 0)
			{
				sql.Append("	AND CID.CODIGO_MESO = " + meso);
			}

			if (micro != null && micro != 0)
			{
				sql.Append("	AND CID.CODIGO_MICRO = " + micro);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaCidades.Add(new Cidade()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						TelefoneHospital = linha["TELEFONE_HOSPITAL"].ToString(),
						TelefonePrefeitura = linha["TELEFONE_PREFEITURA"].ToString(),
						Estado = linha["ESTADO"].ToString(),
						Meso = new Meso() { Codigo = Convert.ToInt32(linha["CODIGO_MESO"]), Descricao = linha["MESO"].ToString() },
						Micro = new Micro() { Codigo = Convert.ToInt32(linha["CODIGO_MICRO"]), Descricao = linha["MICRO"].ToString() },
						Latitude = linha["LATITUDE"].ToString(),
						Longitude = linha["LONGITUDE"].ToString()
					});
				}
			}

			return listaCidades;
		}

		public static List<Cidade> getCidadeByEstado(string Estado, out string mensagemErro)
		{
			List<Cidade> listaCidades = new List<Cidade>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CID.*, ME.DESCRICAO AS MESO, MI.DESCRICAO AS MICRO");
			sql.Append("	FROM CIDADES AS CID");
			sql.Append("	LEFT JOIN MESO AS ME ON CID.CODIGO_MESO = ME.CODIGO");
			sql.Append("	LEFT JOIN MICRO AS MI ON CID.CODIGO_MICRO = MI.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (!String.IsNullOrEmpty(Estado))
			{
				sql.Append("	AND CID.ESTADO = '" + Estado + "'");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaCidades.Add(new Cidade()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						TelefoneHospital = linha["TELEFONE_HOSPITAL"].ToString(),
						TelefonePrefeitura = linha["TELEFONE_PREFEITURA"].ToString(),
						Estado = linha["ESTADO"].ToString(),
						Meso = new Meso() { Codigo = Convert.ToInt32(linha["CODIGO_MESO"]), Descricao = linha["MESO"].ToString() },
						Micro = new Micro() { Codigo = Convert.ToInt32(linha["CODIGO_MICRO"]), Descricao = linha["MICRO"].ToString() },
						Latitude = linha["LATITUDE"].ToString(),
						Longitude = linha["LONGITUDE"].ToString()
					});
				}
			}

			return listaCidades;
		}
	}
}
