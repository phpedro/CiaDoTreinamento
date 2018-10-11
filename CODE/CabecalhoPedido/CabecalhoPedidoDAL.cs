using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class CabecalhoPedidoDAL
    {
		//CONSULTAS
		public static List<CabecalhoPedido.CabecalhoPedidoTela> GetPedidoByCliente(int? codigoCliente, out string mensagemErro)
		{
			List<CabecalhoPedido.CabecalhoPedidoTela> listaPedidos = new List<CabecalhoPedido.CabecalhoPedidoTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT CC.CODIGO, CC.DATA_CRIACAO, PE.NOME, SN.DESCRICAO, CC.VALOR_TOTAL");
			sql.Append("    FROM CABECALHOS_PEDIDOS AS CC");
			sql.Append("        LEFT JOIN PESSOAS AS PE ON CC.CODIGO_FUNCIONARIO_VENDEDOR = PE.CODIGO");
			sql.Append("        LEFT JOIN STATUS_NEGOCIACOES AS SN ON CC.CODIGO_STATUS = SN.CODIGO");
			sql.Append("	WHERE 1 = 1");

			if (codigoCliente != null && codigoCliente != 0)
			{
				sql.Append("	AND CC.CODIGO_CLIENTE = " + codigoCliente);
			}

			sql.Append("    ORDER BY CC.DATA_CRIACAO DESC");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaPedidos.Add(new CabecalhoPedido.CabecalhoPedidoTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						DataCadastro = Convert.ToDateTime(linha["DATA_CRIACAO"].ToString()),
						NomeVendedor = linha["NOME"].ToString(),
						StatusPedido = linha["DESCRICAO"].ToString(),
						ValorPedido = Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())
					});
				}
			}

			return listaPedidos;
		}

		public static List<CabecalhoPedido> GetPedidoByCodigo(int codigoPedido, out string mensagemErro)
		{
			List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * ");
			sql.Append("    FROM CABECALHOS_PEDIDOS");
			sql.Append("	WHERE 1 = 1");

			if (codigoPedido != 0)
			{
				sql.Append("	AND CODIGO = " + codigoPedido);
			}

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

		public static string GetProdutosVendidosResumido(int codigoPedido, out string mensagemErro)
		{
			string produtos = String.Empty;
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT PR.DESCRICAO ");
			sql.AppendLine("    FROM ITENS_PEDIDOS AS IP");
			sql.AppendLine("		LEFT JOIN PRODUTOS AS PR ON PR.CODIGO = IP.CODIGO_PRODUTO");
			sql.Append("	WHERE IP.CODIGO_PEDIDO = " + codigoPedido);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					produtos += linha["DESCRICAO"].ToString();
					produtos += " #-# ";
				}

				produtos = produtos.Substring(0, produtos.Length - 5);
			}

			return produtos;
		}

		public static List<CabecalhoPedido> BuscarPedidosNegadosPeloAdmVendas(int? codigoAgenteVendas, int? codigoInstrutor, string razaoSocial, int? codigoCidade, string codigoEstado,
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
			sql.AppendLine("WHERE CP.CODIGO_STATUS = 19");

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
				sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
			}

			if (dataFimFechamentoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dataFimFechamentoPedido).ToString("yyyy-MM-dd") + "'");
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

        public static List<CabecalhoPedido> BuscaPedidosAdmRota(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                        int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                        DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {

            List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
            StringBuilder sql = new StringBuilder();
            mensagemErro = "";

            sql.AppendLine("SELECT DISTINCT CP.*");
            sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
            sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
            sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
            sql.AppendLine("	INNER JOIN ITENS_PEDIDOS IP ON CP.CODIGO = IP.CODIGO_PEDIDO");
            sql.AppendLine("WHERE CP.CODIGO_STATUS = 13");
            sql.AppendLine("AND ENVIAR_POR_CORREIO = 0");

            if (ddlAgenteVendasFiltro.HasValue && ddlAgenteVendasFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + ddlAgenteVendasFiltro);
            }

            if (ddlInstrutorFiltro.HasValue && ddlInstrutorFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_INSTRUTOR	 = " + ddlInstrutorFiltro);
            }

            if (!String.IsNullOrEmpty(txtRazaoSocialFiltro))
            {
                sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%')");
            }

            if (ddlCidadesFiltro.HasValue && ddlCidadesFiltro > 0)
            {
                sql.AppendLine("	AND CL.CODIGO_CIDADE = " + ddlCidadesFiltro);
            }

            if (ddlEstadosFiltro.HasValue && ddlEstadosFiltro > 0)
            {
                sql.AppendLine("	AND CI.ESTADO = '" + ddlEstadosFiltro + "'");
            }

            if (dtpDataInicioFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dtpDataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (dtpDataFinalFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dtpDataFinalFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (txtCodigoPedidoFiltro.HasValue && txtCodigoPedidoFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO = '" + txtCodigoPedidoFiltro + "'");
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
                        FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
                        FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
                        ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),
                      
                    });
                }
            }

            return listaPedidos;

        }

        public static List<CabecalhoPedido> BuscaPedidosAdmVistoria(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                        int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                        DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {

            List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
            StringBuilder sql = new StringBuilder();
            mensagemErro = "";

            sql.AppendLine("SELECT DISTINCT CP.*");
            sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
            sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
            sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
            sql.AppendLine("	INNER JOIN ITENS_PEDIDOS IP ON CP.CODIGO = IP.CODIGO_PEDIDO");
            sql.AppendLine("WHERE CP.CODIGO_STATUS = 14");

            if (ddlAgenteVendasFiltro.HasValue && ddlAgenteVendasFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + ddlAgenteVendasFiltro);
            }

            if (ddlInstrutorFiltro.HasValue && ddlInstrutorFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_INSTRUTOR	 = " + ddlInstrutorFiltro);
            }

            if (!String.IsNullOrEmpty(txtRazaoSocialFiltro))
            {
                sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%')");
            }

            if (ddlCidadesFiltro.HasValue && ddlCidadesFiltro > 0)
            {
                sql.AppendLine("	AND CL.CODIGO_CIDADE = " + ddlCidadesFiltro);
            }

            if (ddlEstadosFiltro.HasValue && ddlEstadosFiltro > 0)
            {
                sql.AppendLine("	AND CI.ESTADO = '" + ddlEstadosFiltro + "'");
            }

            if (dtpDataInicioFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dtpDataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (dtpDataFinalFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dtpDataFinalFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (txtCodigoPedidoFiltro.HasValue && txtCodigoPedidoFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO = '" + txtCodigoPedidoFiltro + "'");
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
                        FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
                        FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
                        ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),

                    });
                }
            }

            return listaPedidos;

        }

        public static List<CabecalhoPedido> BuscaPedidosAdmCorreio(string txtCnpjFiltro, int? txtCodigoPedidoFiltro, string txtRazaoSocialFiltro, int? ddlAgenteVendasFiltro,
                                                int? ddlInstrutorFiltro, int? ddlEstadosFiltro, int? ddlCidadesFiltro, DateTime? dtpDataInicioFechamentoPedido,
                                                DateTime? dtpDataFinalFechamentoPedido, out string mensagemErro)
        {

            List<CabecalhoPedido> listaPedidos = new List<CabecalhoPedido>();
            StringBuilder sql = new StringBuilder();
            mensagemErro = "";

            sql.AppendLine("SELECT DISTINCT CP.*");
            sql.AppendLine("FROM CABECALHOS_PEDIDOS AS CP");
            sql.AppendLine("	INNER JOIN CLIENTES AS CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
            sql.AppendLine("	INNER JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
            sql.AppendLine("	INNER JOIN ITENS_PEDIDOS IP ON CP.CODIGO = IP.CODIGO_PEDIDO");
            sql.AppendLine("WHERE CP.CODIGO_STATUS = 13");
            sql.AppendLine("AND ENVIAR_POR_CORREIO = 1");

            if (ddlAgenteVendasFiltro.HasValue && ddlAgenteVendasFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_VENDEDOR = " + ddlAgenteVendasFiltro);
            }

            if (ddlInstrutorFiltro.HasValue && ddlInstrutorFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO_FUNCIONARIO_INSTRUTOR	 = " + ddlInstrutorFiltro);
            }

            if (!String.IsNullOrEmpty(txtRazaoSocialFiltro))
            {
                sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%') OR CL.NOME_CLIENTE LIKE CONCAT('%','" + txtRazaoSocialFiltro + "','%')");
            }

            if (ddlCidadesFiltro.HasValue && ddlCidadesFiltro > 0)
            {
                sql.AppendLine("	AND CL.CODIGO_CIDADE = " + ddlCidadesFiltro);
            }

            if (ddlEstadosFiltro.HasValue && ddlEstadosFiltro > 0)
            {
                sql.AppendLine("	AND CI.ESTADO = '" + ddlEstadosFiltro + "'");
            }

            if (dtpDataInicioFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO >= '" + Convert.ToDateTime(dtpDataInicioFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (dtpDataFinalFechamentoPedido.HasValue)
            {
                sql.AppendLine("	AND CP.DATA_FECHAMENTO <= '" + Convert.ToDateTime(dtpDataFinalFechamentoPedido).ToString("yyyy-MM-dd") + "'");
            }

            if (txtCodigoPedidoFiltro.HasValue && txtCodigoPedidoFiltro > 0)
            {
                sql.AppendLine("	AND CP.CODIGO = '" + txtCodigoPedidoFiltro + "'");
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
                        FuncionarioInstrutor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_INSTRUTOR"].ToString())),
                        FuncionarioVendedor = new Funcionario(Convert.ToInt32(linha["CODIGO_FUNCIONARIO_VENDEDOR"].ToString())),
                        ValorTotal = (linha["VALOR_TOTAL"].ToString() == "" ? 0 : Convert.ToDecimal(linha["VALOR_TOTAL"].ToString())),

                    });
                }
            }

            return listaPedidos;

        }


        //INSERT
        public static bool insertCabecalhoPedido(CabecalhoPedido cabecalho, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("INSERT INTO CABECALHOS_PEDIDOS");
				sql.AppendLine("	(CODIGO_CLIENTE, DATA_CRIACAO, CODIGO_CONDICAO, CODIGO_CONTA, CODIGO_FUNCIONARIO_INSTRUTOR, CODIGO_FUNCIONARIO_VENDEDOR,");
				sql.AppendLine("	CODIGO_STATUS, LOCAL_REALIZACAO, CODIGO_PARCEIRO_HOTEL, CODIGO_PARCEIRO_SALA, NUMERO_NOTA, CODIGO_MOTIVO_NAO_VENDA, DETALHAMENTO_MOTIVO_NAO_VENDA,");
				sql.AppendLine("	CONFIRMADO, VALOR_TOTAL_BOLETOS, REALIZOU_CONTRATO_VERBAL, NUMERO_ART, OBSERVACAO_ART, PERC_DESC, VLR_DESC, VALOR_TOTAL, ENVIAR_POR_CORREIO, COBRAR_ISS,");
				sql.AppendLine("	DATA_FECHAMENTO, DATA_INICIO_TREINAMENTO, DATA_FIM_TREINAMENTO, INFO_DATA_TREINAMENTO, DETALHAMENTO_RETORNO_PEDIDO, COBRAR_BOLETOS)");
				sql.AppendLine("	VALUES");
				sql.AppendLine("	('" + cabecalho.Cliente.Codigo + "', '" + cabecalho.DataCriacao.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + cabecalho.CondicaoPagamento.Codigo + "', '" + (cabecalho.ContaBancaria == null ? "0" : cabecalho.ContaBancaria.Codigo.ToString()) + "', '" + (cabecalho.FuncionarioInstrutor == null ? "1" : cabecalho.FuncionarioInstrutor.Codigo.ToString()) + "', '" + cabecalho.FuncionarioVendedor.Codigo +"',");
				sql.AppendLine("	'" + cabecalho.StatusNegociacao.CodigoStatus + "', '"+cabecalho.LocalRealizacao+"', '"+(cabecalho.ParceiroHotel == null ? "0" : cabecalho.ParceiroHotel.Codigo.ToString())+"','"+(cabecalho.ParceiraSalaTreinamento == null ? "0" : cabecalho.ParceiraSalaTreinamento.Codigo.ToString()) +"','"+cabecalho.NumeroNota+"','"+(cabecalho.MotivoNaoVenda == null ? "0" : cabecalho.MotivoNaoVenda.Codigo.ToString())+"', '"+cabecalho.DetalheMotivoNaoVenda+"',");
				sql.AppendLine("	'" + (cabecalho.Confirmado ? "1" : "0") + "', '" + cabecalho.ValorBoletos.ToString().Replace(",", ".") + "', '" + (cabecalho.RealizouContratoVerbal ? "1" : "0") + "', '" + cabecalho.NumeroART + "', '" + cabecalho.ObservacaoART + "', '" + cabecalho.PercentualDesconto.ToString().Replace(",",".") +"', '"+cabecalho.ValorDesconto.ToString().Replace(",",".")+"', '"+cabecalho.ValorTotal.ToString().Replace(",",".")+"', '"+(cabecalho.EnviarPorCorreio ? "1" : "0")+"', '"+(cabecalho.CobrarISS ? "1" : "0")+"',");
				sql.AppendLine("	'" + (cabecalho.DataFechamento == null ? "" : cabecalho.DataFechamento.ToString("yyyy-MM-dd HH:mm:ss")) + "', '" + (cabecalho.DataInicioTreinamento == null ? "" : cabecalho.DataInicioTreinamento.ToString("yyyy-MM-dd HH:mm:ss")) + "', '" + (cabecalho.DataFinalTreinamento == null ? "" : cabecalho.DataFinalTreinamento.ToString("yyyy-MM-dd HH:mm:ss")) + "', '"+cabecalho.InfoTreinamento+"','"+cabecalho.DetalheRetornoPedido+"','"+(cabecalho.CobrarBoletos ? "1" : "0")+"')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					cabecalho.Codigo = retorno;
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o pedido. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o pedido. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public static bool updateCabecalhoPedido(CabecalhoPedido cabecalho, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE CABECALHOS_PEDIDOS SET");
				sql.AppendLine("CODIGO_CLIENTE = '" + cabecalho.Cliente.Codigo + "' ,");
				sql.AppendLine("DATA_CRIACAO = '" + cabecalho.DataCriacao.ToString("yyyy-MM-dd HH:mm:ss") + "' ,");
				sql.AppendLine("CODIGO_CONDICAO = '" + cabecalho.CondicaoPagamento.Codigo + "' ,");
				sql.AppendLine("CODIGO_CONTA = '" + (cabecalho.ContaBancaria == null ? "0" : cabecalho.ContaBancaria.Codigo.ToString()) + "' ,");
				sql.AppendLine("CODIGO_FUNCIONARIO_INSTRUTOR = '" + (cabecalho.FuncionarioInstrutor == null ? "1" : cabecalho.FuncionarioInstrutor.Codigo.ToString()) + "' ,");
				sql.AppendLine("CODIGO_FUNCIONARIO_VENDEDOR = '" + cabecalho.FuncionarioVendedor.Codigo + "' ,");
				sql.AppendLine("CODIGO_STATUS = '" + cabecalho.StatusNegociacao.CodigoStatus + "' ,");
				sql.AppendLine("LOCAL_REALIZACAO = '" + cabecalho.LocalRealizacao + "' ,");
				sql.AppendLine("CODIGO_PARCEIRO_HOTEL = '" + (cabecalho.ParceiroHotel == null ? "0" : cabecalho.ParceiroHotel.Codigo.ToString()) + "' ,");
				sql.AppendLine("CODIGO_PARCEIRO_SALA = '" + (cabecalho.ParceiraSalaTreinamento == null ? "0" : cabecalho.ParceiraSalaTreinamento.Codigo.ToString()) + "' ,");
				sql.AppendLine("NUMERO_NOTA = '" + cabecalho.NumeroNota + "' ,");
				sql.AppendLine("CODIGO_MOTIVO_NAO_VENDA = '" + (cabecalho.MotivoNaoVenda == null ? "0" : cabecalho.MotivoNaoVenda.Codigo.ToString()) + "' ,");
				sql.AppendLine("DETALHAMENTO_MOTIVO_NAO_VENDA = '" + cabecalho.DetalheMotivoNaoVenda + "' ,");
				sql.AppendLine("CONFIRMADO = '" + (cabecalho.Confirmado ? "1" : "0") + "' ,");
				sql.AppendLine("VALOR_TOTAL_BOLETOS = '" + cabecalho.ValorBoletos.ToString().Replace(",", ".") + "' ,");
				sql.AppendLine("REALIZOU_CONTRATO_VERBAL = '" + (cabecalho.RealizouContratoVerbal ? "1" : "0") + "' ,");
				sql.AppendLine("NUMERO_ART = '" + cabecalho.NumeroART + "' ,");
				sql.AppendLine("OBSERVACAO_ART = '" + cabecalho.ObservacaoART + "' ,");
				sql.AppendLine("PERC_DESC = '" + cabecalho.PercentualDesconto.ToString().Replace(",", ".") + "' ,");
				sql.AppendLine("VLR_DESC = '" + cabecalho.ValorDesconto.ToString().Replace(",", ".") + "' ,");
				sql.AppendLine("VALOR_TOTAL = '" + cabecalho.ValorTotal.ToString().Replace(",", ".") + "' ,");
				sql.AppendLine("ENVIAR_POR_CORREIO = '" + (cabecalho.EnviarPorCorreio ? "1" : "0") + "' ,");
				sql.AppendLine("COBRAR_ISS = '" + (cabecalho.CobrarISS ? "1" : "0") + "', ");
				sql.AppendLine("DATA_FECHAMENTO = '" + (cabecalho.DataFechamento == null ? "" : cabecalho.DataFechamento.ToString("yyyy-MM-dd HH:mm:ss")) + "' ,");
				sql.AppendLine("DATA_INICIO_TREINAMENTO = '" + (cabecalho.DataInicioTreinamento == null ? "" : cabecalho.DataInicioTreinamento.ToString("yyyy-MM-dd HH:mm:ss")) + "' ,");
				sql.AppendLine("DATA_FIM_TREINAMENTO = '" + (cabecalho.DataFinalTreinamento == null ? "" : cabecalho.DataFinalTreinamento.ToString("yyyy-MM-dd HH:mm:ss")) + "' ,");
				sql.AppendLine("INFO_DATA_TREINAMENTO = '" + cabecalho.InfoTreinamento + "' ,");
				sql.AppendLine("DETALHAMENTO_RETORNO_PEDIDO = '" + cabecalho.DetalheRetornoPedido + "' ,");
				sql.AppendLine("COBRAR_BOLETOS = '" + (cabecalho.CobrarBoletos ? "1" : "0") + "'");
				sql.AppendLine("WHERE CODIGO = " + cabecalho.Codigo);

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

		public static bool updateEncargosItensPedidos(int codigoPedido, bool cobrarEncargos, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("UPDATE CABECALHOS_PEDIDOS SET COBRAR_ISS = '" + (cobrarEncargos ? "1" : "0") + "' WHERE CODIGO = " + codigoPedido + ";");

				if (cobrarEncargos)
				{
					sql.AppendLine("UPDATE ITENS_PEDIDOS IP");
					sql.AppendLine("LEFT JOIN PRODUTOS PR ON IP.CODIGO_PRODUTO = PR.CODIGO");
					sql.AppendLine("SET");
					sql.AppendLine("VALOR_ISS = (VALOR_FINAL * (PR.PERCENTUAL_ISS / 100)) * QUANTIDADE,");
					sql.AppendLine("SUBTOTAL = (VALOR_FINAL + (VALOR_FINAL * (PR.PERCENTUAL_ISS / 100))) * QUANTIDADE");
					sql.AppendLine("WHERE CODIGO_PEDIDO = " + codigoPedido);
				}
				else
				{
					sql.AppendLine("UPDATE ITENS_PEDIDOS SET");
					sql.AppendLine("VALOR_ISS = 0,");
					sql.AppendLine("SUBTOTAL = VALOR_FINAL * QUANTIDADE");
					sql.AppendLine("WHERE CODIGO_PEDIDO = " + codigoPedido);
				}
				
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
		
	}
}
