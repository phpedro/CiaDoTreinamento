using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class TiposTelefonesDAL
    {
		//CONSULTAS
		public static List<TiposTelefones> getTiposTelefones(out string mensagemErro)
		{
			List<TiposTelefones> listaTipos = new List<TiposTelefones>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM TIPOS_TELEFONES");
			sql.Append("	WHERE 1 = 1");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaTipos.Add(new TiposTelefones()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaTipos;
		}
	}
}
