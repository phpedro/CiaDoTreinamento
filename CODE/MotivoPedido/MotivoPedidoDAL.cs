using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class MotivoPedidoDAL
    {
		public static List<MotivoPedido> getMotivosPedido(out string mensagemErro)
		{
			List<MotivoPedido> listaMotivos = new List<MotivoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM MOTIVOS_PEDIDO");
			sql.Append("	WHERE 1 = 1");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaMotivos.Add(new MotivoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaMotivos;
		}
	}
}
