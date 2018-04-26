using CiaDoTreinamento.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CODE
{
	public class ProdutoDAL
    {

		//INSERT 
		public static bool insertProduto(Produto produto, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("INSERT INTO PRODUTOS");
				sql.Append("	(DESCRICAO, VALOR_POR_PESSOA, MESES_VIGENCIA, NOME_MODELO_PROPOSTA, NOME_MODELO_CERTIFICADO, NOME_MODELO_VERSO, ");
				sql.Append("	NOME_MODELO_LISTA_PRESENCA, TEM_PAE, TEM_PPRA, TEM_CURSO, TEM_PC_04_05, ATIVO, ");
				sql.Append("	ARGUMENTACAO_VENDA, TEM_NR20, PERCENTUAL_ISS, TEM_PCMSO, CARGA_HORARIA, TEM_NBR_14276, ");
				sql.Append("	TEM_PASTA_CIPA, TEM_VISTORIA_RURAL, TEM_VISTORIA_ESCOLAR, TEM_VISTORIA_PASSAGEIROS, TEM_VISTORIA, TEM_PRONTUARIO_NR20, ");
				sql.Append("	CATEGORIA_PRODUTO, PROD_REF_2VIA, IMAGEM) ");
				sql.Append("	VALUES");
				sql.Append("	('" + produto.Descricao + "','" + produto.ValorPorPessoa.ToString().Replace(",", ".") + "', '" + produto.MesesVigencia + "', '" + produto.NomeModeloProposta + "', '" + produto.NomeModeloCertificado + "', '" + produto.NomeModeloVerso + "', ");
				sql.Append("	'" + produto.NomeModeloListaPresenca + "','" + (produto.TemPAE ? 1 : 0) + "', '" + (produto.TemPPRA ? 1 : 0) + "', '" + (produto.TemCURSO ? 1 : 0) + "', '" + (produto.TemPC0405 ? 1 : 0) + "', '" + (produto.Ativo ? 1 : 0) + "', ");
				sql.Append("	'" + produto.ArgumentacaoVenda + "','" + (produto.TemNR20 ? 1 : 0) + "', '" + produto.PercentualIIS.ToString().Replace(",", ".") + "', '" + (produto.TemPCMSO ? 1 : 0) + "', '" + produto.CargaHoraria + "', '" + (produto.TemNBR14276 ? 1 : 0) + "', ");
				sql.Append("	'" + (produto.TemPASTACIPA ? 1 : 0) + "','" + (produto.TemVISTORIARURAL ? 1 : 0) + "', '" + (produto.TemVISTORIAESCOLAR ? 1 : 0) + "', '" + (produto.TemVISTORIAPASSAGEIROS ? 1 : 0) + "', '" + (produto.TemVISTORIA ? 1 : 0) + "', '" + (produto.TemPRONTUARIONR20 ? 1 : 0) + "', ");
				sql.Append("    '" + produto.CategoriaProduto.Codigo + "','" + produto.ProdutoRef2Via + "', '" + produto.NomeImagem + "')");

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o produto. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{

				mensagemErro = "Não foi possível cadastrar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//UPDATE
		public static bool updateProduto(Produto produto, out string mensagemErro)
		{

			mensagemErro = "";

			try
			{
				Command cmd = new Command();
				StringBuilder sql = new StringBuilder();

				sql.Append("UPDATE PRODUTOS");
				sql.Append("	SET");
				sql.Append("	DESCRICAO = '" + produto.Descricao + "',");
				sql.Append("	VALOR_POR_PESSOA = '" + produto.ValorPorPessoa.ToString().Replace(",", ".") + "',");
				sql.Append("	MESES_VIGENCIA = '" + produto.MesesVigencia + "',");
				sql.Append("	NOME_MODELO_PROPOSTA = '" + produto.NomeModeloProposta + "',");
				sql.Append("	NOME_MODELO_CERTIFICADO = '" + produto.NomeModeloCertificado + "',");
				sql.Append("	NOME_MODELO_VERSO = '" + produto.NomeModeloVerso + "',");
				sql.Append("	NOME_MODELO_LISTA_PRESENCA = '" + produto.NomeModeloListaPresenca + "',");
				sql.Append("	TEM_PAE = '" + (produto.TemPAE ? 1 : 0) + "',");
				sql.Append("	TEM_PPRA = '" + (produto.TemPPRA ? 1 : 0) + "',");
				sql.Append("	TEM_CURSO = '" + (produto.TemCURSO ? 1 : 0) + "',");
				sql.Append("	TEM_PC_04_05 = '" + (produto.TemPC0405 ? 1 : 0) + "',");
				sql.Append("	ATIVO = '" + (produto.Ativo ? 1 : 0) + "',");
				sql.Append("	ARGUMENTACAO_VENDA = '" + produto.ArgumentacaoVenda + "',");
				sql.Append("	TEM_NR20 = '" + (produto.TemNR20 ? 1 : 0) + "',");
				sql.Append("	PERCENTUAL_ISS = '" + produto.PercentualIIS.ToString().Replace(",", ".") + "',");
				sql.Append("	TEM_PCMSO = '" + (produto.TemPCMSO ? 1 : 0) + "',");
				sql.Append("	CARGA_HORARIA = '" + produto.CargaHoraria + "',");
				sql.Append("	TEM_NBR_14276 = '" + (produto.TemNBR14276 ? 1 : 0) + "',");
				sql.Append("	TEM_PASTA_CIPA = '" + (produto.TemPASTACIPA ? 1 : 0) + "',");
				sql.Append("	TEM_VISTORIA_RURAL = '" + (produto.TemVISTORIARURAL ? 1 : 0) + "',");
				sql.Append("	TEM_VISTORIA_ESCOLAR = '" + (produto.TemVISTORIAESCOLAR ? 1 : 0) + "',");
				sql.Append("	TEM_VISTORIA_PASSAGEIROS = '" + (produto.TemVISTORIAPASSAGEIROS ? 1 : 0) + "',");
				sql.Append("	TEM_VISTORIA = '" + (produto.TemVISTORIA ? 1 : 0) + "',");
				sql.Append("	TEM_PRONTUARIO_NR20 = '" + (produto.TemPRONTUARIONR20 ? 1 : 0) + "',");
				sql.Append("	CATEGORIA_PRODUTO = '" + produto.CategoriaProduto.Codigo + "',");
				sql.Append("	IMAGEM = '" + produto.NomeImagem + "',");
				sql.Append("	PROD_REF_2VIA = '" + produto.ProdutoRef2Via + "'");
				sql.Append("	WHERE CODIGO = " + produto.Codigo);

				cmd.CommandText = sql.ToString();

				int retorno = cmd.Execute();

				if (retorno > 0)
				{
					return true;
				}
				else
				{
					mensagemErro = "Não foi possível atualizar o produto. Contate o suporte!";
					return false;
				}

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		//CONSULTAR
		public static List<Produto.ProdutoTela> GetProdutosTela(out string mensagemErro, bool ativos)
		{
			List<Produto.ProdutoTela> listaProdutos = new List<Produto.ProdutoTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.AppendLine("SELECT PR.CODIGO, PR.DESCRICAO, PR.VALOR_POR_PESSOA, PR.MESES_VIGENCIA, PR.CARGA_HORARIA, PR.ATIVO, CP.DESCRICAO AS CATEGORIA_PRODUTO");
			sql.AppendLine("    FROM bd_ciatreinamento.produtos AS PR");
			sql.AppendLine("    LEFT JOIN categoria_produto as CP ON PR.CATEGORIA_PRODUTO = CP.CODIGO");

			if (ativos)
			{
				sql.AppendLine("WHERE PR.ATIVO = '1'");
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaProdutos.Add(new Produto.ProdutoTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString(),
						ValorPorPessoa = Convert.ToDecimal(linha["VALOR_POR_PESSOA"].ToString()),
						MesesVigencia = Convert.ToInt32(linha["MESES_VIGENCIA"].ToString()),
						CargaHoraria = (String.IsNullOrEmpty(linha["CARGA_HORARIA"].ToString()) ? 0 : Convert.ToInt32(linha["CARGA_HORARIA"].ToString())),
						Ativo = (linha["ATIVO"].ToString() == "1" ? true : false),
						CategoriaProduto = linha["CATEGORIA_PRODUTO"].ToString()
					});
				}
			}

			return listaProdutos;
		}

		public static Produto GetProdutoById(int? id, out string mensagemErro)
		{
			Produto produto = new Produto();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM PRODUTOS");

			if (id.HasValue && id != 0)
			{
				sql.Append("    WHERE CODIGO = " + id);
			}

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				var linha = retorno.Rows[0];

				produto.Codigo = Convert.ToInt32(linha["CODIGO"].ToString());
				produto.Descricao = linha["DESCRICAO"].ToString();
				produto.ValorPorPessoa = Convert.ToDecimal(linha["VALOR_POR_PESSOA"].ToString());
				produto.MesesVigencia = Convert.ToInt32(linha["MESES_VIGENCIA"].ToString());
				produto.NomeImagem = linha["IMAGEM"].ToString();
				produto.NomeModeloProposta = linha["NOME_MODELO_PROPOSTA"].ToString();
				produto.NomeModeloCertificado = linha["NOME_MODELO_CERTIFICADO"].ToString();
				produto.NomeModeloRecibo = linha["NOME_MODELO_RECIBO"].ToString();
				produto.NomeModeloVerso = linha["NOME_MODELO_VERSO"].ToString();
				produto.NomeModeloListaPresenca = linha["NOME_MODELO_LISTA_PRESENCA"].ToString();
				produto.TemPAE = (linha["TEM_PAE"].ToString() == "1" ? true : false);
				produto.TemPPRA = (linha["TEM_PPRA"].ToString() == "1" ? true : false);
				produto.TemCURSO = (linha["TEM_CURSO"].ToString() == "1" ? true : false);
				produto.TemPC0405 = (linha["TEM_PC_04_05"].ToString() == "1" ? true : false);
				produto.Ativo = (linha["ATIVO"].ToString() == "1" ? true : false);
				produto.ArgumentacaoVenda = linha["ARGUMENTACAO_VENDA"].ToString();
				produto.TemNR20 = (linha["TEM_NR20"].ToString() == "1" ? true : false);
				produto.PercentualIIS = Convert.ToDecimal(linha["PERCENTUAL_ISS"].ToString());
				produto.TemPCMSO = (linha["TEM_PCMSO"].ToString() == "1" ? true : false);
				produto.CargaHoraria = (linha["CARGA_HORARIA"].ToString() == "" ? 0 : Convert.ToInt32(linha["CARGA_HORARIA"].ToString()));
				produto.TemNBR14276 = (linha["TEM_NBR_14276"].ToString() == "1" ? true : false);
				produto.TemPASTACIPA = (linha["TEM_PASTA_CIPA"].ToString() == "1" ? true : false);
				produto.TemVISTORIARURAL = (linha["TEM_VISTORIA_RURAL"].ToString() == "1" ? true : false);
				produto.TemVISTORIAESCOLAR = (linha["TEM_VISTORIA_ESCOLAR"].ToString() == "1" ? true : false);
				produto.TemVISTORIAPASSAGEIROS = (linha["TEM_VISTORIA_PASSAGEIROS"].ToString() == "1" ? true : false);
				produto.TemVISTORIA = (linha["TEM_VISTORIA"].ToString() == "1" ? true : false);
				produto.TemPRONTUARIONR20 = (linha["TEM_PRONTUARIO_NR20"].ToString() == "1" ? true : false);
				produto.CategoriaProduto = new CategoriaProduto() { Codigo = Convert.ToInt32(linha["CATEGORIA_PRODUTO"].ToString()) };
				produto.ProdutoRef2Via = (linha["PROD_REF_2VIA"].ToString() == "" ? 0 : Convert.ToInt32(linha["PROD_REF_2VIA"]));

			}

			return produto;
		}

		public static List<Produto.Produto2ViaTela> GetProdutos2ViaTela(out string mensagemErro)
		{
			List<Produto.Produto2ViaTela> listaProdutos = new List<Produto.Produto2ViaTela>();
			StringBuilder sql = new StringBuilder();
			mensagemErro = "";

			sql.Append("SELECT * FROM PRODUTOS");
			sql.Append("    WHERE CATEGORIA_PRODUTO = 4");

			Command cmd = new Command();
			cmd.CommandText = sql.ToString();

			DataTable retorno = cmd.GetData();

			if (retorno.Rows.Count > 0)
			{
				foreach (DataRow linha in retorno.Rows)
				{
					listaProdutos.Add(new Produto.Produto2ViaTela()
					{
						Codigo = Convert.ToInt32(linha["CODIGO"].ToString()),
						Descricao = linha["DESCRICAO"].ToString()
					});
				}
			}

			return listaProdutos;
		}

	}
}
