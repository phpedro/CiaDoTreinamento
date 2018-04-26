using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class PerfilDAL
    {

		//INSERT 
		public static bool insertPerfil(Perfil perfil, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PERFIS");
				sql.Append("	(DESCRICAO, PODE_ALTERAR_VALOR_ITEM_PEDIDO, PODE_FAZER_PEDIDO, PODE_ALTERAR_PERFIS, PODE_CONFIRMAR_PEDIDO, PODE_GERAR_REMESSA_RETORNO, PODE_VISUALIZAR_RELATORIOS,");
				sql.Append("	PODE_ALTERAR_PERC_DESC_PED, PODE_VISUALIZAR_REL_OUTROS_USUARIOS, PODE_EXPORTAR_DADOS, PODE_FILTRAR_PED_STATUS_CLIENTE, PODE_OCULTAR_ITEM_TELA_PRINCIPAL, PODE_VENDER_SEM_ISS,");
				sql.Append("	PODE_MANTER_PRODUTOS, PODE_MANTER_CONDS_PAGTO, PODE_ALTERAR_STATUS_BOLETO, PODE_CANCELAR_PEDIDO, PODE_ALTERAR_PEDIDO_APOS_FINALIZADO, PODE_ALTERAR_RESPONSAVEL_CIDADE,");
				sql.Append("	PODE_VENDER_COND_RESTRITA, PODE_ALTERAR_NOME_REDE, PODE_REG_ATENDIMENTO_PED_FECHADO, PODE_EXCLUIR_TEL_CLIENTE, PODE_GERAR_DOCUMENTOS, PODE_FINALIZAR_PEDIDO_COM_PENDENCIA,");
				sql.Append("	PODE_ALTERAR_STATUS_PEDIDO)");
				sql.Append("	VALUES");
				sql.Append("	('" + perfil.Descricao + "', " + perfil.PodeAlterarValorItemPedido + ", " + perfil.PodeFazerPedidos + "," + perfil.PodeAlterarPerfis + "," + perfil.PodeConfirmarPedido + ", " + perfil.PodeGerarRemImpRetBoletos + ", " + perfil.PodeVisualizarRelatorios + ",");
				sql.Append("	" + perfil.PodeAlterarPercDescPedido + ", " + perfil.PodeVisualizarRelOutrosFuncionarios + ", " + perfil.PodeExportarDados + ", " + perfil.PodeFiltrarPedStsCli + ", " + perfil.PodeOcultarItemTelaPrincipal + ", " + perfil.PodeVenderSemISS + ",");
				sql.Append("	" + perfil.PodeManterProdutos + ", " + perfil.PodeManterCondicoesPagamento + ", " + perfil.PodeAlterarStatusBoletos + ", " + perfil.PodeCancelarPedidos + ", " + perfil.PodeAlterarPedidoAposFinalizado + ", " + perfil.PodeAlterarResponsavelCidade + ",");
				sql.Append("	" + perfil.PodeVenderCondRestrita + ", " + perfil.PodeAlterarNomeRede + ", " + perfil.PodeRegAtendimentoPedFechado + ", " + perfil.PodeExcluirTelCliente + ", " + perfil.PodeGerarDocumentos + ", " + perfil.PodeFinalziarPedidoComPendencia + ",");
				sql.Append("	" + perfil.PodeAlterarStatusPedido + ")");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o perfil. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o perfil. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updatePerfil(Perfil perfil, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE PERFIS SET");
				sql.Append("	DESCRICAO = '" + perfil.Descricao + "',");
				sql.Append("	PODE_ALTERAR_VALOR_ITEM_PEDIDO = '" + (perfil.PodeAlterarValorItemPedido ? 1 : 0) + "',");
				sql.Append("	PODE_FAZER_PEDIDO = " + (perfil.PodeFazerPedidos ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_PERFIS = " + (perfil.PodeAlterarPerfis ? 1 : 0) + ",");
				sql.Append("	PODE_CONFIRMAR_PEDIDO = " + (perfil.PodeConfirmarPedido ? 1 : 0) + ",");
				sql.Append("	PODE_GERAR_REMESSA_RETORNO = " + (perfil.PodeGerarRemImpRetBoletos ? 1 : 0) + ",");
				sql.Append("	PODE_VISUALIZAR_RELATORIOS = " + (perfil.PodeVisualizarRelatorios ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_PERC_DESC_PED = " + (perfil.PodeAlterarPercDescPedido ? 1 : 0) + ",");
				sql.Append("	PODE_VISUALIZAR_REL_OUTROS_USUARIOS = " + (perfil.PodeVisualizarRelOutrosFuncionarios ? 1 : 0) + ",");
				sql.Append("	PODE_EXPORTAR_DADOS = " + (perfil.PodeExportarDados ? 1 : 0) + ",");
				sql.Append("	PODE_FILTRAR_PED_STATUS_CLIENTE = " + (perfil.PodeFiltrarPedStsCli ? 1 : 0) + ",");
				sql.Append("	PODE_OCULTAR_ITEM_TELA_PRINCIPAL = " + (perfil.PodeOcultarItemTelaPrincipal ? 1 : 0) + ",");
				sql.Append("	PODE_VENDER_SEM_ISS = " + (perfil.PodeVenderSemISS ? 1 : 0) + ",");
				sql.Append("	PODE_MANTER_PRODUTOS = " + (perfil.PodeManterProdutos ? 1 : 0) + ",");
				sql.Append("	PODE_MANTER_CONDS_PAGTO = " + (perfil.PodeManterCondicoesPagamento ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_STATUS_BOLETO = " + (perfil.PodeAlterarStatusBoletos ? 1 : 0) + ",");
				sql.Append("	PODE_CANCELAR_PEDIDO = " + (perfil.PodeCancelarPedidos ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_PEDIDO_APOS_FINALIZADO = " + (perfil.PodeAlterarPedidoAposFinalizado ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_RESPONSAVEL_CIDADE = " + (perfil.PodeAlterarResponsavelCidade ? 1 : 0) + ",");
				sql.Append("	PODE_VENDER_COND_RESTRITA = " + (perfil.PodeVenderCondRestrita ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_NOME_REDE = " + (perfil.PodeAlterarNomeRede ? 1 : 0) + ",");
				sql.Append("	PODE_REG_ATENDIMENTO_PED_FECHADO = " + (perfil.PodeRegAtendimentoPedFechado ? 1 : 0) + ",");
				sql.Append("	PODE_EXCLUIR_TEL_CLIENTE = " + (perfil.PodeExcluirTelCliente ? 1 : 0) + ",");
				sql.Append("	PODE_GERAR_DOCUMENTOS = " + (perfil.PodeGerarDocumentos ? 1 : 0) + ",");
				sql.Append("	PODE_FINALIZAR_PEDIDO_COM_PENDENCIA = " + (perfil.PodeFinalziarPedidoComPendencia ? 1 : 0) + ",");
				sql.Append("	PODE_ALTERAR_STATUS_PEDIDO = " + (perfil.PodeAlterarStatusPedido ? 1 : 0));
				sql.Append("	WHERE");
				sql.Append("	CODIGO = " + perfil.Codigo + "");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o perfil. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível atualizar o perfil. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public static bool deletePerfil(int codigo, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE FROM PERFIS WHERE CODIGO = " + codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível remover o perfil. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o perfil. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAS
		public static List<Perfil> getPerfis(int? codigo, string descricao, out string mensagemErro)
		{
			List<Perfil> listaPerfis = new List<Perfil>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM PERFIS");
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
					listaPerfis.Add(new Perfil()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						PodeAlterarValorItemPedido = (linha["PODE_ALTERAR_VALOR_ITEM_PEDIDO"].ToString() == "1"),
						PodeFazerPedidos = (linha["PODE_FAZER_PEDIDO"].ToString() == "1"),
						PodeAlterarPerfis = (linha["PODE_ALTERAR_PERFIS"].ToString() == "1"),
						PodeConfirmarPedido = (linha["PODE_CONFIRMAR_PEDIDO"].ToString() == "1"),
						PodeGerarRemImpRetBoletos = (linha["PODE_GERAR_REMESSA_RETORNO"].ToString() == "1"),
						PodeVisualizarRelatorios = (linha["PODE_VISUALIZAR_RELATORIOS"].ToString() == "1"),
						PodeAlterarPercDescPedido = (linha["PODE_ALTERAR_PERC_DESC_PED"].ToString() == "1"),
						PodeVisualizarRelOutrosFuncionarios = (linha["PODE_VISUALIZAR_REL_OUTROS_USUARIOS"].ToString() == "1"),
						PodeExportarDados = (linha["PODE_EXPORTAR_DADOS"].ToString() == "1"),
						PodeFiltrarPedStsCli = (linha["PODE_FILTRAR_PED_STATUS_CLIENTE"].ToString() == "1"),
						PodeOcultarItemTelaPrincipal = (linha["PODE_OCULTAR_ITEM_TELA_PRINCIPAL"].ToString() == "1"),
						PodeVenderSemISS = (linha["PODE_VENDER_SEM_ISS"].ToString() == "1"),
						PodeManterProdutos = (linha["PODE_MANTER_PRODUTOS"].ToString() == "1"),
						PodeManterCondicoesPagamento = (linha["PODE_MANTER_CONDS_PAGTO"].ToString() == "1"),
						PodeAlterarStatusBoletos = (linha["PODE_ALTERAR_STATUS_BOLETO"].ToString() == "1"),
						PodeCancelarPedidos = (linha["PODE_CANCELAR_PEDIDO"].ToString()) == "1",
						PodeAlterarPedidoAposFinalizado = (linha["PODE_ALTERAR_PEDIDO_APOS_FINALIZADO"].ToString() == "1"),
						PodeAlterarResponsavelCidade = (linha["PODE_ALTERAR_RESPONSAVEL_CIDADE"].ToString() == "1"),
						PodeVenderCondRestrita = (linha["PODE_VENDER_COND_RESTRITA"].ToString() == "1"),
						PodeAlterarNomeRede = (linha["PODE_ALTERAR_NOME_REDE"].ToString() == "1"),
						PodeRegAtendimentoPedFechado = (linha["PODE_REG_ATENDIMENTO_PED_FECHADO"].ToString() == "1"),
						PodeExcluirTelCliente = (linha["PODE_EXCLUIR_TEL_CLIENTE"].ToString() == "1"),
						PodeGerarDocumentos = (linha["PODE_GERAR_DOCUMENTOS"].ToString() == "1"),
						PodeFinalziarPedidoComPendencia = (linha["PODE_FINALIZAR_PEDIDO_COM_PENDENCIA"].ToString() == "1"),
						PodeAlterarStatusPedido = (linha["PODE_ALTERAR_STATUS_PEDIDO"].ToString() == "1")
					});
				}
			}

			return listaPerfis;
		}

	}
}
