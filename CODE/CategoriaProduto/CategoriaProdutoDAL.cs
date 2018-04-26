using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class CategoriaProdutoDAL
    {

		public static List<CategoriaProduto> getCategorias(out string mensagemErro)
		{
			List<CategoriaProduto> listaCategorias = new List<CategoriaProduto>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM CATEGORIA_PRODUTO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaCategorias.Add(new CategoriaProduto()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaCategorias;
		}

	}
}
