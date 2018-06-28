using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class RoteirizacaoDAL
    {
		#region INSERT

		#endregion

		#region UPDATE

		public static bool updatePedidoRota(int codigoPedido, int codigoInstrutor, string salaTreinamento, string informacoesParaTreinamento, 
										DateTime dataInicioTreinamento, DateTime dataFimTreinamento, int codigoStatus, string detalheRetornoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CABECALHOS_PEDIDOS");
				sql.Append("	SET");
				if (codigoInstrutor > 0) { sql.Append("	CODIGO_FUNCIONARIO_INSTRUTOR = '" + codigoInstrutor + "',"); };
				if (!String.IsNullOrEmpty(salaTreinamento)) { sql.Append("	CODIGO_PARCEIRO_SALA = '" + salaTreinamento + "',"); }
				sql.Append("	INFO_DATA_TREINAMENTO = '" + informacoesParaTreinamento + "',");
				sql.Append("	DATA_INICIO_TREINAMENTO = '" + dataInicioTreinamento.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.Append("	DATA_FIM_TREINAMENTO = '" + dataFimTreinamento.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				sql.Append("	DETALHAMENTO_RETORNO_PEDIDO = '" + detalheRetornoPedido + "',");
				sql.Append("	CODIGO_STATUS = '" + codigoStatus + "'");
				sql.Append("	WHERE CODIGO = " + codigoPedido);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o pedido. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o pedido. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static bool updateStatusPedido(int codigoPedido, int codigoStatus, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CABECALHOS_PEDIDOS");
				sql.Append("	SET");
				sql.Append("	CODIGO_STATUS = '" + codigoStatus + "'");
				sql.Append("	WHERE CODIGO = " + codigoPedido);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o pedido. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o pedido. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}


		}

		#endregion

		#region DELETE

		#endregion

		#region CONSULTAS

		public static List<CabecalhoPedido> BuscarPedidosRoteirizacao(int? codigoAgenteVendas, int? codigoInstrutor, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, int? codigoMeso, int? codigoMicro,
																int? codigoPedido, int? codigoProduto, out string mensagemErro)
		{

			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT DISTINCT CP.*");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("	INNER JOIN ITENS_PEDIDOS IP ON CP.CODIGO = IP.CODIGO_PEDIDO");
			sql.AppendLine("WHERE (CP.CODIGO_STATUS = 8 OR CP.CODIGO_STATUS = 15) AND CP.ENVIAR_POR_CORREIO = 0");

			if (codigoAgenteVendas.HasValue && codigoAgenteVendas > 0)
			{
				sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + codigoAgenteVendas);
			}

			if (codigoInstrutor.HasValue && codigoInstrutor > 0)
			{
				sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_INSTRUTOR	 = " + codigoInstrutor);
			}

			if (!String.IsNullOrEmpty(razaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + razaoSocial + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + razaoSocial + "','%')"); 
			}

			if (codigoCidade.HasValue && codigoCidade > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_CIDADE = " + codigoCidade);
			}

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (dataInicioFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND DATE(CP.DATA_FECHAMENTO) >= '" + Convert.ToDateTime(dataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFimFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND DATE(CP.DATA_FECHAMENTO) <= '" + Convert.ToDateTime(dataFimFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (codigoMeso.HasValue && codigoMeso > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MESO = '" + codigoMeso + "'");
			}

			if (codigoMicro.HasValue && codigoMicro > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO = '" + codigoMicro + "'");
			}

			if (codigoPedido.HasValue && codigoPedido > 0)
			{
				sql.AppendLine("	AND CP.CODIGO = '" + codigoPedido + "'");
			}

			if (codigoProduto.HasValue && codigoProduto > 0)
			{
				sql.AppendLine("	AND IP.CODIGO_PRODUTO = '" + codigoProduto + "'");
			}

			sql.AppendLine("ORDER BY CI.ESTADO ASC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString())),
						DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"].ToString()) },
						ContaBancaria = (linha["CODIGO_CONTA"].ToString() == "" ? null : new ContaBancaria() { Codigo = Convert.ToInt32(linha["CODIGO_CONTA"].ToString()) }),
						FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
						FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
						StatusNegociacao = new StatusNegociacao(Convert.ToInt32(linha["CODIGO_STATUS"].ToString())),
						LocalRealizacao = linha["LOCAL_REALIZACAO"].ToString(),
						ParceiroHotel = (linha["CODIGO_PARCEIRO_HOTEL"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"].ToString()) }),
						ParceiraSalaTreinamento = (linha["CODIGO_PARCEIRO_SALA"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"].ToString()) }),
						NumeroNota = linha["NUMERO_NOTA"].ToString(),
						MotivoNaoVenda = (linha["CODIGO_MOTIVO_NAO_VENDA"].ToString() == "" ? null : new MotivoNaoVenda() { Codigo = Convert.ToInt32(linha["CODIGO_MOTIVO_NAO_VENDA"].ToString()) }),
						DetalheMotivoNaoVenda = linha["DETALHAMENTO_MOTIVO_NAO_VENDA"].ToString(),
						Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
						ValorBoletos = (linha["VALOR_TOTAL_BOLETOS"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL_BOLETOS"].ToString())),
						RealizouContratoVerbal = (linha["REALIZOU_CONTRATO_VERBAL"].ToString() == "0" ? false : true),
						NumeroART = linha["NUMERO_ART"].ToString(),
						ObservacaoEnvioART = linha["OBSERVACAO_ENVIO_ART"].ToString(),
						ObservacaoART = linha["OBSERVACAO_ART"].ToString(),
						PercentualDesconto = (linha["PERC_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["PERC_DESC"].ToString())),
						ValorDesconto = (linha["VLR_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VLR_DESC"].ToString())),
						ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
						EnviarPorCorreio = Convert.ToBoolean(linha["ENVIAR_POR_CORREIO"].ToString()),
						CobrarISS = (linha["COBRAR_ISS"].ToString() == "0" ? false : true),
						DataFechamento = (linha["DATA_FECHAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FECHAMENTO"].ToString())),
						DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
						DataFinalTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
						InfoTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString(),
						DetalheRetornoPedido = linha["DETALHAMENTO_RETORNO_PEDIDO"].ToString(),
						CobrarBoletos = (linha["COBRAR_BOLETOS"].ToString() == "0" ? false : true)
					});
				}
			}

			return listaPedidos;

		}

		public static List<CabecalhoPedido> BuscarPedidosCorreio(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, out string mensagemErro)
		{

			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CP.*");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("WHERE (CP.CODIGO_STATUS = 8 OR CP.CODIGO_STATUS = 15) AND CP.ENVIAR_POR_CORREIO = 1");

			if (codigoAgenteVendas.HasValue && codigoAgenteVendas > 0)
			{
				sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + codigoAgenteVendas);
			}

			if (!String.IsNullOrEmpty(razaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + razaoSocial + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + razaoSocial + "','%')");
			}

			if (codigoCidade.HasValue && codigoCidade > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_CIDADE = " + codigoCidade);
			}

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (dataInicioFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFimFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dataFimFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			sql.AppendLine("ORDER BY CI.ESTADO ASC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString())),
						DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"].ToString()) },
						ContaBancaria = (linha["CODIGO_CONTA"].ToString() == "" ? null : new ContaBancaria() { Codigo = Convert.ToInt32(linha["CODIGO_CONTA"].ToString()) }),
						FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
						FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
						StatusNegociacao = new StatusNegociacao(Convert.ToInt32(linha["CODIGO_STATUS"].ToString())),
						LocalRealizacao = linha["LOCAL_REALIZACAO"].ToString(),
						ParceiroHotel = (linha["CODIGO_PARCEIRO_HOTEL"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"].ToString()) }),
						ParceiraSalaTreinamento = (linha["CODIGO_PARCEIRO_SALA"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"].ToString()) }),
						NumeroNota = linha["NUMERO_NOTA"].ToString(),
						MotivoNaoVenda = (linha["CODIGO_MOTIVO_NAO_VENDA"].ToString() == "" ? null : new MotivoNaoVenda() { Codigo = Convert.ToInt32(linha["CODIGO_MOTIVO_NAO_VENDA"].ToString()) }),
						DetalheMotivoNaoVenda = linha["DETALHAMENTO_MOTIVO_NAO_VENDA"].ToString(),
						Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
						ValorBoletos = (linha["VALOR_TOTAL_BOLETOS"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL_BOLETOS"].ToString())),
						RealizouContratoVerbal = (linha["REALIZOU_CONTRATO_VERBAL"].ToString() == "0" ? false : true),
						NumeroART = linha["NUMERO_ART"].ToString(),
						ObservacaoEnvioART = linha["OBSERVACAO_ENVIO_ART"].ToString(),
						ObservacaoART = linha["OBSERVACAO_ART"].ToString(),
						PercentualDesconto = (linha["PERC_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["PERC_DESC"].ToString())),
						ValorDesconto = (linha["VLR_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VLR_DESC"].ToString())),
						ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
						EnviarPorCorreio = Convert.ToBoolean(linha["ENVIAR_POR_CORREIO"].ToString()),
						CobrarISS = (linha["COBRAR_ISS"].ToString() == "0" ? false : true),
						DataFechamento = (linha["DATA_FECHAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FECHAMENTO"].ToString())),
						DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
						DataFinalTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
						InfoTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString(),
						DetalheRetornoPedido = linha["DETALHAMENTO_RETORNO_PEDIDO"].ToString(),
						CobrarBoletos = (linha["COBRAR_BOLETOS"].ToString() == "0" ? false : true)
					});
				}
			}

			return listaPedidos;

		}

		public static List<CabecalhoPedido> BuscarPedidosPendenteRota(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, int? codigoMeso, int? codigoMicro,
																int? codigoProduto, out string mensagemErro)
		{

			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CP.*");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("	INNER JOIN ITENS_PEDIDOS IP ON CP.CODIGO = IP.CODIGO_PEDIDO");
			sql.AppendLine("WHERE CP.CODIGO_STATUS = 17");

			if (codigoAgenteVendas.HasValue && codigoAgenteVendas > 0)
			{
				sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + codigoAgenteVendas);
			}

			if (!String.IsNullOrEmpty(razaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + razaoSocial + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + razaoSocial + "','%')");
			}

			if (codigoCidade.HasValue && codigoCidade > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_CIDADE = " + codigoCidade);
			}

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (dataInicioFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND DATE(CP.DATA_FECHAMENTO) >= '" + Convert.ToDateTime(dataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFimFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND DATE(CP.DATA_FECHAMENTO) <= '" + Convert.ToDateTime(dataFimFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (codigoMeso.HasValue && codigoMeso > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MESO = '" + codigoMeso + "'");
			}

			if (codigoMicro.HasValue && codigoMicro > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO = '" + codigoMicro + "'");
			}

			if (codigoProduto.HasValue && codigoProduto > 0)
			{
				sql.AppendLine("	AND IP.CODIGO_PRODUTO = '" + codigoProduto + "'");
			}

			sql.AppendLine("ORDER BY CI.ESTADO ASC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString())),
						DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"].ToString()) },
						ContaBancaria = (linha["CODIGO_CONTA"].ToString() == "" ? null : new ContaBancaria() { Codigo = Convert.ToInt32(linha["CODIGO_CONTA"].ToString()) }),
						FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
						FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
						StatusNegociacao = new StatusNegociacao(Convert.ToInt32(linha["CODIGO_STATUS"].ToString())),
						LocalRealizacao = linha["LOCAL_REALIZACAO"].ToString(),
						ParceiroHotel = (linha["CODIGO_PARCEIRO_HOTEL"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"].ToString()) }),
						ParceiraSalaTreinamento = (linha["CODIGO_PARCEIRO_SALA"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"].ToString()) }),
						NumeroNota = linha["NUMERO_NOTA"].ToString(),
						MotivoNaoVenda = (linha["CODIGO_MOTIVO_NAO_VENDA"].ToString() == "" ? null : new MotivoNaoVenda() { Codigo = Convert.ToInt32(linha["CODIGO_MOTIVO_NAO_VENDA"].ToString()) }),
						DetalheMotivoNaoVenda = linha["DETALHAMENTO_MOTIVO_NAO_VENDA"].ToString(),
						Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
						ValorBoletos = (linha["VALOR_TOTAL_BOLETOS"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL_BOLETOS"].ToString())),
						RealizouContratoVerbal = (linha["REALIZOU_CONTRATO_VERBAL"].ToString() == "0" ? false : true),
						NumeroART = linha["NUMERO_ART"].ToString(),
						ObservacaoEnvioART = linha["OBSERVACAO_ENVIO_ART"].ToString(),
						ObservacaoART = linha["OBSERVACAO_ART"].ToString(),
						PercentualDesconto = (linha["PERC_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["PERC_DESC"].ToString())),
						ValorDesconto = (linha["VLR_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VLR_DESC"].ToString())),
						ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
						EnviarPorCorreio = Convert.ToBoolean(linha["ENVIAR_POR_CORREIO"].ToString()),
						CobrarISS = (linha["COBRAR_ISS"].ToString() == "0" ? false : true),
						DataFechamento = (linha["DATA_FECHAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FECHAMENTO"].ToString())),
						DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
						DataFinalTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
						InfoTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString(),
						DetalheRetornoPedido = linha["DETALHAMENTO_RETORNO_PEDIDO"].ToString(),
						CobrarBoletos = (linha["COBRAR_BOLETOS"].ToString() == "0" ? false : true)
					});
				}
			}

			return listaPedidos;

		}

		public static List<CabecalhoPedido> BuscarPedidosPendenteVistoria(int? codigoAgenteVendas, string razaoSocial, int? codigoCidade, string codigoEstado,
																DateTime? dataInicioFechamentoPedido, DateTime? dataFimFechamentoPedido, out string mensagemErro)
		{

			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CP.*");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("WHERE CP.CODIGO_STATUS = 18");

			if (codigoAgenteVendas.HasValue && codigoAgenteVendas > 0)
			{
				sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + codigoAgenteVendas);
			}

			if (!String.IsNullOrEmpty(razaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + razaoSocial + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + razaoSocial + "','%')");
			}

			if (codigoCidade.HasValue && codigoCidade > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_CIDADE = " + codigoCidade);
			}

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (dataInicioFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFimFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dataFimFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			sql.AppendLine("ORDER BY CI.ESTADO ASC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString())),
						DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"].ToString()) },
						ContaBancaria = (linha["CODIGO_CONTA"].ToString() == "" ? null : new ContaBancaria() { Codigo = Convert.ToInt32(linha["CODIGO_CONTA"].ToString()) }),
						FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
						FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
						StatusNegociacao = new StatusNegociacao(Convert.ToInt32(linha["CODIGO_STATUS"].ToString())),
						LocalRealizacao = linha["LOCAL_REALIZACAO"].ToString(),
						ParceiroHotel = (linha["CODIGO_PARCEIRO_HOTEL"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"].ToString()) }),
						ParceiraSalaTreinamento = (linha["CODIGO_PARCEIRO_SALA"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"].ToString()) }),
						NumeroNota = linha["NUMERO_NOTA"].ToString(),
						MotivoNaoVenda = (linha["CODIGO_MOTIVO_NAO_VENDA"].ToString() == "" ? null : new MotivoNaoVenda() { Codigo = Convert.ToInt32(linha["CODIGO_MOTIVO_NAO_VENDA"].ToString()) }),
						DetalheMotivoNaoVenda = linha["DETALHAMENTO_MOTIVO_NAO_VENDA"].ToString(),
						Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
						ValorBoletos = (linha["VALOR_TOTAL_BOLETOS"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL_BOLETOS"].ToString())),
						RealizouContratoVerbal = (linha["REALIZOU_CONTRATO_VERBAL"].ToString() == "0" ? false : true),
						NumeroART = linha["NUMERO_ART"].ToString(),
						ObservacaoEnvioART = linha["OBSERVACAO_ENVIO_ART"].ToString(),
						ObservacaoART = linha["OBSERVACAO_ART"].ToString(),
						PercentualDesconto = (linha["PERC_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["PERC_DESC"].ToString())),
						ValorDesconto = (linha["VLR_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VLR_DESC"].ToString())),
						ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
						EnviarPorCorreio = Convert.ToBoolean(linha["ENVIAR_POR_CORREIO"].ToString()),
						CobrarISS = (linha["COBRAR_ISS"].ToString() == "0" ? false : true),
						DataFechamento = (linha["DATA_FECHAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FECHAMENTO"].ToString())),
						DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
						DataFinalTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
						InfoTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString(),
						DetalheRetornoPedido = linha["DETALHAMENTO_RETORNO_PEDIDO"].ToString(),
						CobrarBoletos = (linha["COBRAR_BOLETOS"].ToString() == "0" ? false : true)
					});
				}
			}

			return listaPedidos;

		}

		public static List<CabecalhoPedido> BuscarPedidosRoteirizacao(string codigoEstado, int? cidade, int? meso, int? micro, int? produto, int? codigoRede, out string mensagemErro)
		{

			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CP.*, CASE WHEN SUM(PR.TEM_VISTORIA) > 0 THEN 1 ELSE 0 END TEM_VISTORIA");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("	INNER JOIN ITENS_PEDIDOS AS IP ON IP.CODIGO_PEDIDO = CP.CODIGO");
			sql.AppendLine("	INNER JOIN PRODUTOS AS PR ON IP.CODIGO_PRODUTO = PR.CODIGO");
			sql.AppendLine("WHERE (CP.CODIGO_STATUS = 8 OR CP.CODIGO_STATUS = 15 OR CP.CODIGO_STATUS = 17) AND CP.ENVIAR_POR_CORREIO = 0");

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (cidade.HasValue && cidade > 0)
			{
				sql.AppendLine("	AND CI.CODIGO = '" + cidade + "'");
			}

			if (meso.HasValue && meso > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MESO = '" + meso + "'");
			}

			if (micro.HasValue && micro > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO = '" + micro + "'");
			}

			if (produto.HasValue && produto > 0)
			{
				sql.AppendLine("	AND IP.CODIGO_PRODUTO = '" + produto + "'");
			}

			if (codigoRede.HasValue && codigoRede > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_REDE = '" + codigoRede + "'");
			}

			sql.AppendLine("GROUP BY CP.CODIGO");
			sql.AppendLine("ORDER BY CI.ESTADO ASC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Cliente = new Cliente(Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString())),
						DataCriacao = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						CondicaoPagamento = new CondicaoPagamento() { Codigo = Convert.ToInt32(linha["CODIGO_CONDICAO"].ToString()) },
						ContaBancaria = (linha["CODIGO_CONTA"].ToString() == "" ? null : new ContaBancaria() { Codigo = Convert.ToInt32(linha["CODIGO_CONTA"].ToString()) }),
						FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
						FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
						StatusNegociacao = new StatusNegociacao(Convert.ToInt32(linha["CODIGO_STATUS"].ToString())),
						LocalRealizacao = linha["LOCAL_REALIZACAO"].ToString(),
						ParceiroHotel = (linha["CODIGO_PARCEIRO_HOTEL"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_HOTEL"].ToString()) }),
						ParceiraSalaTreinamento = (linha["CODIGO_PARCEIRO_SALA"].ToString() == "" ? null : new Parceiro() { Codigo = Convert.ToInt32(linha["CODIGO_PARCEIRO_SALA"].ToString()) }),
						NumeroNota = linha["NUMERO_NOTA"].ToString(),
						MotivoNaoVenda = (linha["CODIGO_MOTIVO_NAO_VENDA"].ToString() == "" ? null : new MotivoNaoVenda() { Codigo = Convert.ToInt32(linha["CODIGO_MOTIVO_NAO_VENDA"].ToString()) }),
						DetalheMotivoNaoVenda = linha["DETALHAMENTO_MOTIVO_NAO_VENDA"].ToString(),
						Confirmado = (linha["CONFIRMADO"].ToString() == "0" ? false : true),
						ValorBoletos = (linha["VALOR_TOTAL_BOLETOS"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL_BOLETOS"].ToString())),
						RealizouContratoVerbal = (linha["REALIZOU_CONTRATO_VERBAL"].ToString() == "0" ? false : true),
						NumeroART = linha["NUMERO_ART"].ToString(),
						ObservacaoEnvioART = linha["OBSERVACAO_ENVIO_ART"].ToString(),
						ObservacaoART = linha["OBSERVACAO_ART"].ToString(),
						PercentualDesconto = (linha["PERC_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["PERC_DESC"].ToString())),
						ValorDesconto = (linha["VLR_DESC"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VLR_DESC"].ToString())),
						ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
						EnviarPorCorreio = Convert.ToBoolean(linha["ENVIAR_POR_CORREIO"].ToString()),
						CobrarISS = (linha["COBRAR_ISS"].ToString() == "0" ? false : true),
						DataFechamento = (linha["DATA_FECHAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FECHAMENTO"].ToString())),
						DataInicioTreinamento = (linha["DATA_INICIO_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_INICIO_TREINAMENTO"].ToString())),
						DataFinalTreinamento = (linha["DATA_FIM_TREINAMENTO"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(linha["DATA_FIM_TREINAMENTO"].ToString())),
						InfoTreinamento = linha["INFO_DATA_TREINAMENTO"].ToString(),
						DetalheRetornoPedido = linha["DETALHAMENTO_RETORNO_PEDIDO"].ToString(),
						CobrarBoletos = (linha["COBRAR_BOLETOS"].ToString() == "0" ? false : true),
						temVistoria = (linha["TEM_VISTORIA"].ToString() == "0" ? false : true),
					});
				}
			}

			return listaPedidos;

		}

		public static List<Roteirizacao.ProdutosCategoria> BuscarPedidosProdutosCategoria(string codigoEstado, int? cidade, int? meso, int? micro, int? produto, int? codigoRede, out string mensagemErro)
		{

			List<Roteirizacao.ProdutosCategoria> lista = new List<Roteirizacao.ProdutosCategoria>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT CR.CODIGO, CR.DESCRICAO, COUNT(*) QUANTIDADE");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("	INNER JOIN ITENS_PEDIDOS AS IP ON IP.CODIGO_PEDIDO = CP.CODIGO");
			sql.AppendLine("	INNER JOIN PRODUTOS AS PR ON IP.CODIGO_PRODUTO = PR.CODIGO");
			sql.AppendLine("	INNER JOIN CATEGORIA_PRODUTO CR ON PR.CATEGORIA_PRODUTO = CR.CODIGO");
			sql.AppendLine("WHERE (CP.CODIGO_STATUS = 8 OR CP.CODIGO_STATUS = 15 OR CP.CODIGO_STATUS = 17) AND CP.ENVIAR_POR_CORREIO = 0");

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (cidade.HasValue && cidade > 0)
			{
				sql.AppendLine("	AND CI.CODIGO = '" + cidade + "'");
			}

			if (meso.HasValue && meso > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MESO = '" + meso + "'");
			}

			if (micro.HasValue && micro > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO = '" + micro + "'");
			}

			if (produto.HasValue && produto > 0)
			{
				sql.AppendLine("	AND IP.CODIGO_PRODUTO = '" + produto + "'");
			}

			if (codigoRede.HasValue && codigoRede > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_REDE = '" + codigoRede + "'");
			}

			sql.AppendLine("GROUP BY CR.CODIGO");
			sql.AppendLine("ORDER BY QUANTIDADE DESC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					lista.Add(new Roteirizacao.ProdutosCategoria()
					{
						categoria = linha["DESCRICAO"].ToString(),
						quantidade = Convert.ToInt32(linha["QUANTIDADE"].ToString())
					});
				}
			}

			return lista;

		}

		public static List<Roteirizacao.ProdutosCategoria> BuscarPedidosProdutos(string codigoEstado, int? cidade, int? meso, int? micro, int? produto, int? codigoRede, out string mensagemErro)
		{

			List<Roteirizacao.ProdutosCategoria> lista = new List<Roteirizacao.ProdutosCategoria>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT PR.CODIGO, PR.DESCRICAO, COUNT(*) QUANTIDADE, CR.DESCRICAO CATEGORIA");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS CP");
			sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("	INNER JOIN ITENS_PEDIDOS AS IP ON IP.CODIGO_PEDIDO = CP.CODIGO");
			sql.AppendLine("	INNER JOIN PRODUTOS AS PR ON IP.CODIGO_PRODUTO = PR.CODIGO");
			sql.AppendLine("	INNER JOIN CATEGORIA_PRODUTO CR ON PR.CATEGORIA_PRODUTO = CR.CODIGO");
			sql.AppendLine("WHERE (CP.CODIGO_STATUS = 8 OR CP.CODIGO_STATUS = 15 OR CP.CODIGO_STATUS = 17) AND CP.ENVIAR_POR_CORREIO = 0");

			if (!String.IsNullOrEmpty(codigoEstado))
			{
				sql.AppendLine("	AND CI.ESTADO = '" + codigoEstado + "'");
			}

			if (cidade.HasValue && cidade > 0)
			{
				sql.AppendLine("	AND CI.CODIGO = '" + cidade + "'");
			}

			if (meso.HasValue && meso > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MESO = '" + meso + "'");
			}

			if (micro.HasValue && micro > 0)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO = '" + micro + "'");
			}

			if (produto.HasValue && produto > 0)
			{
				sql.AppendLine("	AND IP.CODIGO_PRODUTO = '" + produto + "'");
			}

			if (codigoRede.HasValue && codigoRede > 0)
			{
				sql.AppendLine("	AND CL.CODIGO_REDE = '" + codigoRede + "'");
			}

			sql.AppendLine("GROUP BY PR.CODIGO");
			sql.AppendLine("ORDER BY QUANTIDADE DESC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					lista.Add(new Roteirizacao.ProdutosCategoria()
					{
						categoria = linha["CATEGORIA"].ToString(),
						produto = linha["DESCRICAO"].ToString(),
						quantidade = Convert.ToInt32(linha["QUANTIDADE"].ToString())
					});
				}
			}

			return lista;

		}

		#endregion
	}
}
