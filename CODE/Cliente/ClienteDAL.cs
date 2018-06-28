using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
    public class ClienteDAL
    {

		//INSERT
		public static bool InsertCliente(Cliente cliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO CLIENTES");
				sql.Append("	(TIPO_CLIENTE, CNPJ, RAZAO_SOCIAL, NOME_FANTASIA, INSCRICAO_ESTADUAL,");
				sql.Append("	CPF_CLIENTE, NOME_CLIENTE, RG_CLIENTE,");
				sql.Append("	BANDEIRA_POSTO, CODIGO_REDE, CODIGO_STATUS, EMAIL_PRINCIPAL, ATIVO, DATA_CADASTRO,");
				sql.Append("	NOME_CONTATO, CARGO_CONTATO,");
				sql.Append("	ENDERECO, DESCRICAO_BAIRRO, CEP, CODIGO_CIDADE, SIGLA_CAIXA_POSTAL, ");
				sql.Append("	ENDERECO_CORRESPONDENCIA, DESCRICAO_BAIRRO_CORRESPONDENCIA, CEP_CORRESPONDENCIA, CODIGO_CIDADE_CORRESPONDENCIA, SIGLA_REF_ENDERECO,");
				sql.Append("	PROPRIETARIO, RG_PROPRIETARIO, CPF_PROPRIETARIO,");
				sql.Append("	COORDENADOR, CARGO_COORDENADOR,");
				sql.Append("	ATIVIDADE, GRAU_RISCO, CNAE, GRUPO,");
				sql.Append("	HORARIO_FUNCIONAMENTO_SEG_SEXTA, HORARIO_FUNCIONAMENTO_FDS_FERIADOS)");
				sql.Append("	VALUES (");
				sql.Append("'" + cliente.TipoCliente + "', ");
				sql.Append("'" + cliente.CNPJ == null ? "" : cliente.CNPJ.RemoveMask() + ", ");
				sql.Append("'" + cliente.RazaoSocial + "', ");
				sql.Append("'" + cliente.NomeFantasia + "', ");
				sql.Append("'" + cliente.InscricaoEstadual + "', ");
				sql.Append("'" + (cliente.CPF == null ? "" : cliente.CPF.RemoveMask()) + "', ");
				sql.Append("'" + cliente.Nome + "', ");
				sql.Append("'" + cliente.RG + "', ");
				sql.Append("'" + (cliente.BandeiraPosto == null ? "" : cliente.BandeiraPosto.Codigo.ToString()) + "', ");
				sql.Append("'" + (cliente.RedePosto == null ? "" : cliente.RedePosto.Codigo.ToString()) + "', ");
				sql.Append("'" + cliente.CodigoStatus + "', ");
				sql.Append("'" + (cliente.EmailPrincipal == null ? "0" : cliente.EmailPrincipal.Codigo.ToString()) + "', ");
				sql.Append("'" + (cliente.Ativo ? "1" : "0") + "', ");
				sql.Append("'" + cliente.DataCadastro.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
				sql.Append("'" + cliente.NomeContato + "', ");
				sql.Append("'" + cliente.CargoContato + "', ");
				sql.Append("'" + cliente.Endereco + "', ");
				sql.Append("'" + cliente.Bairro + "', ");
				sql.Append("'" + (String.IsNullOrEmpty(cliente.CEP) ? "" : cliente.CEP.RemoveMask()) + "', ");
				sql.Append("'" + (cliente.Cidade == null ? "0" : cliente.Cidade.Codigo.ToString()) + "', ");
				sql.Append("'" + cliente.SiglaCaixaPostal + "', ");
				sql.Append("'" + cliente.EnderecoCorrespondencia + "', ");
				sql.Append("'" + cliente.BairroCorrespondencia + "', ");
				sql.Append("'" + (String.IsNullOrEmpty(cliente.CepCorrespondencia) ? null : cliente.CepCorrespondencia.RemoveMask()) + "', ");
				sql.Append("'" + (cliente.CidadeCorrespondencia == null ? "0" : cliente.CidadeCorrespondencia.Codigo.ToString()) + "', ");
				sql.Append("'" + cliente.ReferenciaEnderecoCorrespondencia + "', ");
				sql.Append("'" + cliente.Proprietario + "', ");
				sql.Append("'" + cliente.RgProprietario + "', ");
				sql.Append("'" + (cliente.CpfProprietario == null ? "" : cliente.CpfProprietario.RemoveMask()) + "', ");
				sql.Append("'" + cliente.Coordenador + "', ");
				sql.Append("'" + cliente.CargoCoordenador + "', ");
				sql.Append("'" + cliente.Atividade + "', ");
				sql.Append("'" + cliente.GrauRisco + "', ");
				sql.Append("'" + cliente.CNAE + "', ");
				sql.Append("'" + cliente.Grupo + "', ");
				sql.Append("'" + cliente.HorarioFuncionamentoSegSex + "', ");
				sql.Append("'" + cliente.HorarioFuncionamentoFDS + "'");
				sql.Append(")");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute_ReturnID();

				if (retorno > 0)
				{
					cliente.Codigo = retorno;
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o cliente. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE

		public static bool UpdateCliente(Cliente cliente, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CLIENTES");
				sql.Append("	SET");
				sql.Append("	TIPO_CLIENTE = '" + cliente.TipoCliente + "',");
				sql.Append("	CNPJ = '" + (cliente.CNPJ == null ? "" : cliente.CNPJ.RemoveMask()) + "',");
				sql.Append("	RAZAO_SOCIAL = '" + cliente.RazaoSocial + "',");
				sql.Append("	NOME_FANTASIA = '" + cliente.NomeFantasia + "',");
				sql.Append("	INSCRICAO_ESTADUAL = '" + cliente.InscricaoEstadual + "',");
				sql.Append("	CPF_CLIENTE = '" + (String.IsNullOrEmpty(cliente.CPF) ? "" : cliente.CPF.RemoveMask()) + "',");
				sql.Append("	NOME_CLIENTE = '" + cliente.Nome + "',");
				sql.Append("	RG_CLIENTE = '" + cliente.RG + "',");
				sql.Append("	BANDEIRA_POSTO = '" + (cliente.BandeiraPosto == null ? "" : cliente.BandeiraPosto.Codigo.ToString()) + "',");
				sql.Append("	CODIGO_REDE = '" + (cliente.RedePosto == null ? "" : cliente.RedePosto.Codigo.ToString()) + "',");
				sql.Append("	CODIGO_STATUS = '" + cliente.CodigoStatus + "',");
				sql.Append("	EMAIL_PRINCIPAL = '" + (cliente.EmailPrincipal == null ? "0" : cliente.EmailPrincipal.Codigo.ToString()) + "',");
				sql.Append("	ATIVO = '" + (cliente.Ativo ? 1 : 0) + "',");
				sql.Append("	NOME_CONTATO = '" + cliente.NomeContato + "',");
				sql.Append("	CARGO_CONTATO = '" + cliente.CargoContato + "',");
				sql.Append("	ENDERECO = '" + cliente.Endereco + "',");
				sql.Append("	DESCRICAO_BAIRRO = '" + cliente.Bairro + "',");
				sql.Append("	CEP = '" + (String.IsNullOrEmpty(cliente.CEP) ? "" : cliente.CEP.RemoveMask()) + "',");
				sql.Append("	CODIGO_CIDADE = '" + (cliente.Cidade == null ? "" : cliente.Cidade.Codigo.ToString()) + "',");
				sql.Append("	SIGLA_CAIXA_POSTAL = '" + cliente.SiglaCaixaPostal + "',");
				sql.Append("	ENDERECO_CORRESPONDENCIA = '" + cliente.EnderecoCorrespondencia + "',");
				sql.Append("	DESCRICAO_BAIRRO_CORRESPONDENCIA = '" + cliente.BairroCorrespondencia + "',");
				sql.Append("	CEP_CORRESPONDENCIA = '" + (String.IsNullOrEmpty(cliente.CepCorrespondencia) ? "" : cliente.CepCorrespondencia.RemoveMask()) + "',");
				sql.Append("	CODIGO_CIDADE_CORRESPONDENCIA = '" + (cliente.CidadeCorrespondencia == null ? "" : cliente.CidadeCorrespondencia.Codigo.ToString()) + "',");
				sql.Append("	SIGLA_REF_ENDERECO = '" + cliente.ReferenciaEnderecoCorrespondencia + "',");
				sql.Append("	PROPRIETARIO = '" + cliente.Proprietario + "',");
				sql.Append("	RG_PROPRIETARIO = '" + cliente.RgProprietario + "',");
				sql.Append("	CPF_PROPRIETARIO = '" + (cliente.CpfProprietario == null ? "" : cliente.CpfProprietario.RemoveMask()) + "',");
				sql.Append("	COORDENADOR = '" + cliente.Coordenador + "',");
				sql.Append("	CARGO_COORDENADOR = '" + cliente.CargoCoordenador + "',");
				sql.Append("	ATIVIDADE = '" + cliente.Atividade + "',");
				sql.Append("	GRAU_RISCO = '" + cliente.GrauRisco + "',");
				sql.Append("	CNAE = '" + cliente.CNAE + "',");
				sql.Append("	GRUPO = '" + cliente.Grupo + "',");
				sql.Append("	HORARIO_FUNCIONAMENTO_SEG_SEXTA = '" + cliente.HorarioFuncionamentoSegSex + "',");
				sql.Append("	HORARIO_FUNCIONAMENTO_FDS_FERIADOS = '" + cliente.HorarioFuncionamentoFDS + "'");
				sql.Append("	WHERE CODIGO = " + cliente.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o cliente. Contate o suporte!";
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static bool UpdateStatus(int codigoCliente, int CodigoStatus, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE CLIENTES");
				sql.Append("	SET");
				sql.Append("	CODIGO_STATUS = '" + CodigoStatus + "'");
				sql.Append("	WHERE CODIGO = " + codigoCliente);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o status do cliente. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o status do cliente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<Cliente> GetClientes(int Codigo, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente> listaClientes = new List<Cliente>();

			sql.Append("SELECT CL.*, CI_1.DESCRICAO AS CIDADE_CI1, CI_1.ESTADO AS ESTADO_CI1, CI_1.LATITUDE, CI_1.LONGITUDE, CI_2.DESCRICAO AS CIDADE_CI2, CI_2.ESTADO AS ESTADO_CI2, RE.DESCRICAO REDE FROM CLIENTES AS CL");
			sql.Append("	LEFT JOIN CIDADES AS CI_1 ON CL.CODIGO_CIDADE = CI_1.CODIGO");
			sql.Append("	LEFT JOIN CIDADES AS CI_2 ON CL.CODIGO_CIDADE = CI_2.CODIGO");
			sql.Append("	LEFT JOIN REDE AS RE ON CL.CODIGO_REDE = RE.CODIGO");
			sql.Append("	WHERE CL.CODIGO = " + Codigo);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaClientes.Add(new Cliente()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						TipoCliente = Convert.ToInt32(linha["TIPO_CLIENTE"].ToString()),
						CNPJ = linha["CNPJ"].ToString(),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						NomeFantasia = linha["NOME_FANTASIA"].ToString(),
						InscricaoEstadual = linha["INSCRICAO_ESTADUAL"].ToString(),
						CPF = linha["CPF_CLIENTE"].ToString(),
						Nome = linha["NOME_CLIENTE"].ToString(),
						RG = linha["RG_CLIENTE"].ToString(),
						BandeiraPosto = new BandeiraPosto() { Codigo = Convert.ToInt32(linha["BANDEIRA_POSTO"].ToString()) },
						RedePosto = new RedePosto() { Codigo = Convert.ToInt32(linha["CODIGO_REDE"].ToString()), Descricao = linha["REDE"].ToString() },
						CodigoStatus = Convert.ToInt32(linha["CODIGO_STATUS"].ToString()),
						EmailPrincipal = new EmailCliente() { Codigo = (linha["EMAIL_PRINCIPAL"].ToString() == "" ? -1 : Convert.ToInt32(linha["EMAIL_PRINCIPAL"].ToString())) },
						Ativo = Convert.ToBoolean(linha["ATIVO"].ToString()),
						DataCadastro = Convert.ToDateTime(linha["DATA_CADASTRO"].ToString()),
						NomeContato = linha["NOME_CONTATO"].ToString(),
						CargoContato = linha["CARGO_CONTATO"].ToString(),
						Endereco = linha["ENDERECO"].ToString(),
						Bairro = linha["DESCRICAO_BAIRRO"].ToString(),
						CEP = linha["CEP"].ToString(),
						Cidade = new Cidade() { Codigo = Convert.ToInt32(linha["CODIGO_CIDADE"].ToString()), Descricao = linha["CIDADE_CI1"].ToString(), Estado = linha["ESTADO_CI1"].ToString(), Latitude = linha["LATITUDE"].ToString().Replace(",","."), Longitude = linha["LONGITUDE"].ToString().Replace(",", ".") },
						SiglaCaixaPostal = linha["SIGLA_CAIXA_POSTAL"].ToString(),
						EnderecoCorrespondencia = linha["ENDERECO_CORRESPONDENCIA"].ToString(),
						BairroCorrespondencia = linha["DESCRICAO_BAIRRO_CORRESPONDENCIA"].ToString(),
						CepCorrespondencia = linha["CEP_CORRESPONDENCIA"].ToString(),
						CidadeCorrespondencia = new Cidade() { Codigo = Convert.ToInt32(linha["CODIGO_CIDADE_CORRESPONDENCIA"].ToString()), Descricao = linha["CIDADE_CI2"].ToString(), Estado = linha["ESTADO_CI2"].ToString() },
						ReferenciaEnderecoCorrespondencia = linha["SIGLA_REF_ENDERECO"].ToString(),
						Proprietario = linha["PROPRIETARIO"].ToString(),
						RgProprietario = linha["RG_PROPRIETARIO"].ToString(),
						CpfProprietario = linha["CPF_PROPRIETARIO"].ToString(),
						Coordenador = linha["COORDENADOR"].ToString(),
						CargoCoordenador = linha["CARGO_COORDENADOR"].ToString(),
						Atividade = linha["ATIVIDADE"].ToString(),
						GrauRisco = linha["GRAU_RISCO"].ToString(),
						CNAE = linha["CNAE"].ToString(),
						Grupo = linha["GRUPO"].ToString(),
						HorarioFuncionamentoSegSex = linha["HORARIO_FUNCIONAMENTO_SEG_SEXTA"].ToString(),
						HorarioFuncionamentoFDS = linha["HORARIO_FUNCIONAMENTO_FDS_FERIADOS"].ToString(),
					});
				}
			}


			return listaClientes;
		}

		public static List<Cliente.ClienteTela> GetClientes(int? codigoCliente, string RazaoSocial, string CNPJ, int? CodigoPedido, string NomeCliente,
										string CpfCliente, string Estado, int? CodigoCidade, int? CodigoMicro, int? CodigoRede,
										string Email, string Telefone, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente.ClienteTela> listaClientes = new List<Cliente.ClienteTela>();

			sql.AppendLine("SELECT CL.TIPO_CLIENTE, CL.CODIGO AS CODIGO_CLIENTE, CL.RAZAO_SOCIAL, CL.CNPJ, CL.NOME_CLIENTE, CL.CPF_CLIENTE, COALESCE(RE.DESCRICAO, '') AS REDE_POSTO, ");
			sql.AppendLine("	CI.DESCRICAO AS CIDADE, CI.ESTADO, CL.CODIGO_STATUS");
			sql.AppendLine("	FROM CLIENTES AS CL");
			sql.AppendLine("		LEFT JOIN REDE AS RE ON CL.CODIGO_REDE = RE.CODIGO");
			sql.AppendLine("		LEFT JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("		LEFT JOIN CABECALHOS_PEDIDOS AS CP ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		LEFT JOIN EMAILS_CLIENTES AS E ON E.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		LEFT JOIN CLIENTES_TELEFONES AS CT ON CT.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		LEFT JOIN TELEFONES AS TE ON TE.CODIGO = CT.CODIGO_TELEFONE");
			sql.AppendLine("	WHERE 1 = 1");

			if (codigoCliente.HasValue && codigoCliente > 0)
			{
				sql.AppendLine("	AND CL.CODIGO = " + codigoCliente);
			}

			if (!String.IsNullOrEmpty(RazaoSocial))
			{
				sql.AppendLine("	AND CL.RAZAO_SOCIAL LIKE '%" + RazaoSocial + "%'");
			}

			if (!String.IsNullOrEmpty(CNPJ))
			{
				sql.AppendLine("	AND CL.CNPJ = '" + CNPJ.RemoveMask() + "'");
			}

			if (CodigoPedido.HasValue)
			{
				sql.AppendLine("	AND CP.CODIGO= '" + CodigoPedido + "'");
			}

			if (!String.IsNullOrEmpty(NomeCliente))
			{
				sql.AppendLine("	AND CL.NOME_CLIENTE LIKE '$" + NomeCliente + "%'");
			}

			if (!String.IsNullOrEmpty(CpfCliente))
			{
				sql.AppendLine("	AND CL.CPF_CLIENTE = '" + CpfCliente.RemoveMask() + "'");
			}

			if (!String.IsNullOrEmpty(Estado))
			{
				sql.AppendLine("	AND CI.ESTADO= '" + Estado + "'");
			}

			if (CodigoCidade.HasValue)
			{
				sql.AppendLine("	AND CI.CODIGO= '" + CodigoCidade + "'");
			}

			if (CodigoMicro.HasValue)
			{
				sql.AppendLine("	AND CI.CODIGO_MICRO= '" + CodigoMicro + "'");
			}

			if (CodigoRede.HasValue)
			{
				sql.AppendLine("	AND CL.CODIGO_REDE= '" + CodigoRede + "'");
			}

			if (!String.IsNullOrEmpty(Email))
			{
				sql.AppendLine("	AND E.DESCRICAO LIKE '%" + Email + "%'");
			}

			if (!String.IsNullOrEmpty(Telefone))
			{
				sql.AppendLine("	AND TE.DESCRICAO LIKE '%" + Telefone.RemoveMaskTelefone() + "%'");
			}

			sql.AppendLine("	GROUP BY CL.CODIGO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaClientes.Add(new Cliente.ClienteTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()),
						TipoCliente = Convert.ToInt32(linha["TIPO_CLIENTE"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						NomeCliente = linha["NOME_CLIENTE"].ToString(),
						CPF = linha["CPF_CLIENTE"].ToString(),
						Rede = linha["REDE_POSTO"].ToString(),
						Cidade = linha["CIDADE"].ToString(),
						Estado = linha["ESTADO"].ToString(),
						Status = Convert.ToInt32(linha["CODIGO_STATUS"].ToString())
					});
				}
			}

			return listaClientes;
		}

		public static List<Cliente.ClienteTela> GetClienteResumido(string busca, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente.ClienteTela> listaClientes = new List<Cliente.ClienteTela>();

			sql.Append("SELECT CL.TIPO_CLIENTE, CL.CODIGO AS CODIGO_CLIENTE, CL.RAZAO_SOCIAL, CL.CNPJ, CL.NOME_CLIENTE, CL.CPF_CLIENTE, COALESCE(RE.DESCRICAO, '') AS REDE_POSTO, ");
			sql.Append("	CI.DESCRICAO AS CIDADE, CI.ESTADO, CL.CODIGO_STATUS");
			sql.Append("	FROM CLIENTES AS CL");
			sql.Append("		LEFT JOIN REDE AS RE ON CL.CODIGO_REDE = RE.CODIGO");
			sql.Append("		LEFT JOIN CIDADES AS CI ON CL.CODIGO_CIDADE = CI.CODIGO");

			if (!String.IsNullOrEmpty(busca))
			{
				sql.Append(" WHERE CL.RAZAO_SOCIAL LIKE CONCAT('%','"+busca+"','%')");
				sql.Append(" OR CL.CNPJ LIKE CONCAT('%','" + busca + "','%')");
				sql.Append(" OR CL.NOME_CLIENTE LIKE CONCAT('%','" + busca + "','%')");
				sql.Append(" OR CL.CPF_CLIENTE LIKE CONCAT('%','" + busca + "','%')");
			}

			sql.Append(" ORDER BY CL.RAZAO_SOCIAL, CL.NOME_CLIENTE");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaClientes.Add(new Cliente.ClienteTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO_CLIENTE"].ToString()),
						TipoCliente = Convert.ToInt32(linha["TIPO_CLIENTE"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						NomeCliente = linha["NOME_CLIENTE"].ToString(),
						CPF = linha["CPF_CLIENTE"].ToString(),
						Rede = linha["REDE_POSTO"].ToString(),
						Cidade = linha["CIDADE"].ToString(),
						Estado = linha["ESTADO"].ToString(),
						Status = Convert.ToInt32(linha["CODIGO_STATUS"].ToString())
					});
				}
			}

			return listaClientes;

		}

		public static List<Cliente.ProdutoExpirando> GetProdutosExpirando(int codigoCliente, out string mensagemErro)
		{

			List<Cliente.ProdutoExpirando> listaProdutosExpirando = new List<Cliente.ProdutoExpirando>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT PRODUTO, DATE_FORMAT(DATA_EXPIRACAO, '%d/%m/%Y') AS DATA_EXPIRACAO, CODIGO AS CODIGO_PEDIDO");
			sql.Append("	FROM ( SELECT IT.CODIGO_PEDIDO AS CODIGO, P.descricao as produto, IT.data_expiracao ");
			sql.Append("		FROM ITENS_PEDIDOS IT");
			sql.Append("			INNER JOIN CABECALHOS_PEDIDOS CB ON CB.CODIGO = IT.CODIGO_PEDIDO,");
			sql.Append("			PRODUTOS P");
			sql.Append("		WHERE CB.CODIGO_CLIENTE = '" + codigoCliente + "' AND ");
			sql.Append("			IT.CODIGO_PRODUTO = P.CODIGO AND");
			sql.Append("			date_format(IT.data_expiracao, '%y/%m') >= date_format(current_date(), '%y/%m') and");
			sql.Append("			CB.CODIGO_STATUS IN(" + ((int)StatusNegociacao.StatusPedido.NegocioFechado).ToString() + ", " +
															((int)StatusNegociacao.StatusPedido.Encerrada).ToString() + ", " +
															((int)StatusNegociacao.StatusPedido.Finalizado).ToString() + " ))" + " AS PRODUTOS");
			sql.Append("		GROUP BY PRODUTO");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaProdutosExpirando.Add(new Cliente.ProdutoExpirando()
					{
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
						DescricaoProduto = linha["PRODUTO"].ToString(),
						DataExpiracao = linha["DATA_EXPIRACAO"].ToString()

					});
				}
			}

			return listaProdutosExpirando;

		}

		public static List<Cliente.ProdutoExpirando> GetProdutosVencidos(int codigoCliente, out string mensagemErro)
		{

			List<Cliente.ProdutoExpirando> listaProdutosVencidos = new List<Cliente.ProdutoExpirando>();
			mensagemErro = "";

			string comandoSql = "select " +
								"   MAX(DATE_FORMAT(DATA_EXPIRACAO, '%d/%m/%Y')) as DATA_EXPIRACAO, " +
								"   produto AS PRODUTO, " +
								"   CODIGO as CODIGO_PEDIDO " +
								"from " +
								"   ( " +
								"       select  " +
								"           IT.CODIGO_PEDIDO AS CODIGO, " +
								"           P.descricao as produto, " +
								"           IT.data_expiracao " +
								"       from  " +
								"           ITENS_PEDIDOS IT " +
								"               INNER JOIN CABECALHOS_PEDIDOS CB ON CB.CODIGO = IT.CODIGO_PEDIDO, " +
								"           PRODUTOS P " +
								"       where  " +
								"           CB.codigo_cliente = " + codigoCliente + " and " +
								"           IT.CODIGO_PRODUTO = P.CODIGO AND " +
								"           date_format(IT.data_expiracao, '%y/%m/%d') <= date_format(current_date(), '%y/%m/%d') and " +
								"           NAO_EXIBIR_PRINCIPAL = '0' AND " +
								"           CB.CODIGO_STATUS IN(" + ((int)StatusNegociacao.StatusPedido.NegocioFechado).ToString() + ", " +
																	((int)StatusNegociacao.StatusPedido.Encerrada).ToString() + ", " +
																	((int)StatusNegociacao.StatusPedido.Finalizado).ToString() + " )" +
								"   ) PRODUTOS " +
								"group by " +
								"   produto ";

			Command cmd = new Command();
			cmd.CommandText = comandoSql;

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaProdutosVencidos.Add(new Cliente.ProdutoExpirando()
					{
						CodigoPedido = Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString()),
						DescricaoProduto = linha["PRODUTO"].ToString(),
						DataExpiracao = linha["DATA_EXPIRACAO"].ToString()

					});
				}
			}

			return listaProdutosVencidos;

		}

		public static List<ChaveValor> ObtemListaStatus()
		{
			List<ChaveValor> listaStatus = new List<ChaveValor>();

			listaStatus.Add(new ChaveValor("1", "Encerrado"));
			listaStatus.Add(new ChaveValor("2", "Ligar"));
			listaStatus.Add(new ChaveValor("3", "Negocio fechado"));
			listaStatus.Add(new ChaveValor("4", "Retornar"));
			listaStatus.Add(new ChaveValor("5", "Autorização Revogada"));
			listaStatus.Add(new ChaveValor("6", "Aguardando Ficha"));
			listaStatus.Add(new ChaveValor("7", "Marcar data"));

			return listaStatus;
		}

		public static int getMaxCodigoPedido(int codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente> listaClientes = new List<Cliente>();

			sql.Append("SELECT MAX(CODIGO) AS CODIGO_PEDIDO FROM CABECALHOS_PEDIDOS WHERE CODIGO_CLIENTE = " + codigoCliente);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				var linha = retorno.Rows[0];

				return Convert.ToInt32(linha["CODIGO_PEDIDO"].ToString());
			}

			return -1;
		}

		public static List<Cliente.ClienteTela> getClientesComProdutoExpirandoByAgenteVendas(int codigoAgenteVendas, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente.ClienteTela> listaClientes = new List<Cliente.ClienteTela>();

			sql.AppendLine("SELECT DISTINCT CL.*");
			sql.AppendLine("	FROM CLIENTES AS CL");
			sql.AppendLine("		INNER JOIN CABECALHOS_PEDIDOS AS CP ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		INNER JOIN ITENS_PEDIDOS AS IT ON IT.CODIGO_PEDIDO = CP.CODIGO");
			sql.AppendLine("	WHERE CL.AGENTE_RESPONSAVEL = " + codigoAgenteVendas);
			sql.AppendLine("		AND DATE_FORMAT(IT.DATA_EXPIRACAO, '%y/%m/%d') >= DATE_FORMAT(CURRENT_DATE(), '%y/%m/%d')");
			sql.AppendLine("		AND DATEDIFF(DATE_FORMAT(IT.DATA_EXPIRACAO, '%y/%m/%d'), DATE_FORMAT(CURRENT_DATE(), '%y/%m/%d')) > 0");
			sql.AppendLine("		AND DATEDIFF(DATE_FORMAT(IT.DATA_EXPIRACAO, '%y/%m/%d'), DATE_FORMAT(CURRENT_DATE(), '%y/%m/%d')) < 30");
			sql.AppendLine("		AND CP.CODIGO_STATUS IN(" + ((int)StatusNegociacao.StatusPedido.NegocioFechado).ToString() + ", " +
																((int)StatusNegociacao.StatusPedido.Encerrada).ToString() + ", " +
																((int)StatusNegociacao.StatusPedido.Finalizado).ToString() + " )");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaClientes.Add(new Cliente.ClienteTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						TipoCliente = Convert.ToInt32(linha["TIPO_CLIENTE"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						NomeCliente = linha["NOME_CLIENTE"].ToString(),
						CPF = linha["CPF_CLIENTE"].ToString(),
						Status = Convert.ToInt32(linha["CODIGO_STATUS"].ToString())
					});
				}
			}

			return listaClientes;
		}

		public static List<Cliente.ClienteRota> getClientesDetalheRota(int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			StringBuilder sql = new StringBuilder();
			List<Cliente.ClienteRota> listaClientes = new List<Cliente.ClienteRota>();

			sql.AppendLine("SELECT CL.CODIGO, CL.RAZAO_SOCIAL, CL.NOME_FANTASIA, CL.CNPJ, CL.ENDERECO, CL.DESCRICAO_BAIRRO,");
			sql.AppendLine("		CONCAT(CI.DESCRICAO, ' - ', CI.ESTADO) AS CIDADE,");
			sql.AppendLine("		(SELECT GROUP_CONCAT(CONCAT(TE.DESCRICAO, ' - ' ,TE.OBSERVACAO) SEPARATOR ' # ')");
			sql.AppendLine("			FROM CLIENTES_TELEFONES AS CT");
			sql.AppendLine("				LEFT JOIN TELEFONES AS TE ON CT.CODIGO_TELEFONE = TE.CODIGO");
			sql.AppendLine("			WHERE CT.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		) AS TELEFONES_CLIENTES,");
			sql.AppendLine("		(SELECT GROUP_CONCAT(CT.DESCRICAO SEPARATOR ' # ')");
			sql.AppendLine("			FROM EMAILS_CLIENTES AS CT");
			sql.AppendLine("			WHERE CT.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("		) AS EMAILS_CLIENTES");
			sql.AppendLine("FROM CABECALHOS_PEDIDOS CP");
			sql.AppendLine("	INNER JOIN CLIENTES CL ON CP.CODIGO_CLIENTE = CL.CODIGO");
			sql.AppendLine("	INNER JOIN CIDADES CI ON CL.CODIGO_CIDADE = CI.CODIGO");
			sql.AppendLine("WHERE CP.CODIGO = " + codigoPedido);

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaClientes.Add(new Cliente.ClienteRota()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						RazaoSocial = linha["RAZAO_SOCIAL"].ToString(),
						NomeFantasia = linha["NOME_FANTASIA"].ToString(),
						CNPJ = linha["CNPJ"].ToString(),
						Endereco = linha["ENDERECO"].ToString(),
						Bairro = linha["DESCRICAO_BAIRRO"].ToString(),
						Cidade = linha["CIDADE"].ToString(),
						Telefones = linha["TELEFONES_CLIENTES"].ToString(),
						Emails = linha["EMAILS_CLIENTES"].ToString()
					});
				}
			}

			return listaClientes;
		}

	}
}
