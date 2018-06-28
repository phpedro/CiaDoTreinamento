using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class ItemRotaDAL
    {
		//INSERT 
		public static int insertItemRota(ItemRota itemRota, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO ITENS_ROTA");
				sql.AppendLine("	(CODIGO_ROTA, CODIGO_PEDIDO, CODIGO_PARCEIRO_SALA, CODIGO_PARCEIRO_HOTEL, DATA_INICIO_TREINAMENTO, DATA_FIM_TREINAMENTO, OBSERVACAO, CONFIRMADO, DATA_INICIO_COLETA, DATA_FIM_COLETA)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + itemRota.Rota.Codigo + "', '" + itemRota.CabecalhoPedido.Codigo + "', '" + (itemRota.ParceiroSala == null ? 0 : itemRota.ParceiroSala.Codigo) + "', '"+ (itemRota.ParceiroHotel == null ? 0 : itemRota.ParceiroHotel.Codigo) + "', '" + itemRota.DataInicio.ToString("yyyy-MM-dd HH:mm:ss") + "','" + itemRota.DataFim.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + itemRota.Observacao + "', 0, '" + itemRota.DataInicioColeta.ToString("yyyy-MM-dd HH:mm:ss") + "','" + itemRota.DataFimColeta.ToString("yyyy-MM-dd HH:mm:ss") + "') ");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return retorno;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o item rota. Contate o suporte!";
					return -1;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o item rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}

		}

		//UPDATE
		public static bool updateRota(ItemRota itemRota, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE ITENS_ROTA");
				sql.AppendLine("	SET");
				sql.AppendLine("	CODIGO_PARCEIRO_SALA = '" + (itemRota.ParceiroSala == null ? 0 : itemRota.ParceiroSala.Codigo) + "',");
				sql.AppendLine("	CODIGO_PARCEIRO_HOTEL = '" + (itemRota.ParceiroHotel == null ? 0 : itemRota.ParceiroHotel.Codigo) + "',");
				sql.AppendLine("	DATA_INICIO_TREINAMENTO = '" + itemRota.DataInicio.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.AppendLine("	DATA_FIM_TREINAMENTO = '" + itemRota.DataFim.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.AppendLine("	DATA_INICIO_COLETA = '" + itemRota.DataInicioColeta.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.AppendLine("	DATA_FIM_COLETA = '" + itemRota.DataFimColeta.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.AppendLine("	OBSERVACAO = '" + itemRota.Observacao + "',");
				sql.AppendLine("	CONFIRMADO = '" + (itemRota.Aprovado ? 1 : 0) + "'");
				sql.AppendLine("	WHERE CODIGO_ROTA = " + itemRota.Rota.Codigo);
				sql.AppendLine("			AND CODIGO_PEDIDO = " + itemRota.CabecalhoPedido.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar a rota. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//DELETE
		public static bool deleteItemRota(int codigoRota, int codigoPedido, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("DELETE FROM ITENS_ROTA" );
				sql.AppendLine("WHERE CODIGO_ROTA = " + codigoRota);
				sql.AppendLine("		AND CODIGO_PEDIDO = " + codigoPedido);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o item rota. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o item rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public static bool deleteItensRota(int codigoRota, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("DELETE FROM ITENS_ROTA");
				sql.AppendLine("WHERE CODIGO_ROTA = " + codigoRota);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover os itens da rota. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os itens da rota. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//SELECT
		public static List<ItemRota> selectItensRota(int codigoRota, out string mensagemErro)
		{
			CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();
			List<ItemRota> lista = new List<ItemRota>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT * FROM ITENS_ROTA");
			sql.AppendLine("WHERE CODIGO_ROTA = " + codigoRota);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{

					lista.Add(new ItemRota()
					{
						Rota = new Rota() { Codigo = Convert.ToInt32(linha["CODIGO_ROTA"]) },
						CabecalhoPedido = cabecalhoPedidoBLL.GetPedidoByCodigo(Convert.ToInt32(linha["CODIGO_PEDIDO"]), out mensagemErro),
						ParceiroSala = new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"]) },
						ParceiroHotel = new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"]) },
						DataInicio = Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"]),
						DataFim = Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"]),
						DataInicioColeta = Convert.ToDateTime(linha["DATA_INICIO_COLETA"]),
						DataFimColeta = Convert.ToDateTime(linha["DATA_FIM_COLETA"]),
						Observacao = linha["OBSERVACAO"].ToString(),
						Aprovado = (linha["CONFIRMADO"].ToString() == "0" ? false : true)
					});

				}
			}

			return lista;
		}
	}
}
