using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Produto
    {
		#region Propriedades e Atributos

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public decimal ValorPorPessoa { get; set; }

		public int MesesVigencia { get; set; }

		public string NomeImagem { get; set; }

		public string NomeModeloProposta { get; set; }

		public string NomeModeloCertificado { get; set; }

		public string NomeModeloRecibo { get; set; }

		public string NomeModeloVerso { get; set; }

		public string NomeModeloListaPresenca { get; set; }

		public bool Ativo { get; set; }

		public string ArgumentacaoVenda { get; set; }

		public decimal PercentualIIS { get; set; }

		public int CargaHoraria { get; set; }

		public CategoriaProduto CategoriaProduto { get; set; }

		public int NumeroReferenciaNotaFiscal { get; set; }

		public int ProdutoRef2Via { get; set; }

		public bool TemPAE { get; set; }

		public bool TemPPRA { get; set; }

		public bool TemCURSO { get; set; }

		public bool TemPC0405 { get; set; }

		public bool TemNR20 { get; set; }

		public bool TemPCMSO { get; set; }

		public bool TemNBR14276 { get; set; }

		public bool TemPASTACIPA { get; set; }

		public bool TemVISTORIARURAL { get; set; }

		public bool TemVISTORIAESCOLAR { get; set; }

		public bool TemVISTORIAPASSAGEIROS { get; set; }

		public bool TemVISTORIA { get; set; }

		public bool TemPRONTUARIONR20 { get; set; }

		#endregion

		#region Construtores

		public Produto() { }

		public Produto(int codigoProduto)
		{
			ProdutoBLL produtoBLL = new ProdutoBLL();
			string mensagemErro;
			Produto produto = produtoBLL.GetProdutoById(codigoProduto, out mensagemErro);

			this.Codigo = produto.Codigo;
			this.Descricao = produto.Descricao;
			this.ValorPorPessoa = produto.ValorPorPessoa;
			this.MesesVigencia = produto.MesesVigencia;
			this.NomeImagem = produto.NomeImagem;
			this.NomeModeloProposta = produto.NomeModeloProposta;
			this.NomeModeloCertificado = produto.NomeModeloCertificado;
			this.NomeModeloRecibo = produto.NomeModeloRecibo;
			this.NomeModeloVerso = produto.NomeModeloVerso;
			this.NomeModeloListaPresenca = produto.NomeModeloListaPresenca;
			this.Ativo = produto.Ativo;
			this.ArgumentacaoVenda = produto.ArgumentacaoVenda;
			this.PercentualIIS = produto.PercentualIIS;
			this.CargaHoraria = produto.CargaHoraria;
			this.CategoriaProduto = produto.CategoriaProduto;
			this.NumeroReferenciaNotaFiscal = produto.NumeroReferenciaNotaFiscal;
			this.ProdutoRef2Via = produto.ProdutoRef2Via;
			this.TemPAE = produto.TemPAE;
			this.TemPPRA = produto.TemPPRA;
			this.TemCURSO = produto.TemCURSO;
			this.TemPC0405 = produto.TemPC0405;
			this.TemNR20 = produto.TemNR20;
			this.TemPC0405 = produto.TemPCMSO;
			this.TemNBR14276 = produto.TemNBR14276;
			this.TemPASTACIPA = produto.TemPASTACIPA;
			this.TemVISTORIARURAL = produto.TemVISTORIARURAL;
			this.TemVISTORIAESCOLAR = produto.TemVISTORIAESCOLAR;
			this.TemVISTORIAPASSAGEIROS = produto.TemVISTORIAPASSAGEIROS;
			this.TemVISTORIA = produto.TemVISTORIA;
			this.TemPRONTUARIONR20 = produto.TemPRONTUARIONR20;
		}

		#endregion

		#region Classes Aninhadas

		public class ProdutoTela
		{
			public int Codigo { get; set; }

			public string Descricao { get; set; }

			public Decimal ValorPorPessoa { get; set; }

			public int MesesVigencia { get; set; }

			public int CargaHoraria { get; set; }

			public bool Ativo { get; set; }

			public string CategoriaProduto { get; set; }

			public string _valorPorPessoaFormatado
			{
				get
				{
					return String.Format("{0:C}", this.ValorPorPessoa);
				}
			}
		}

		public class Produto2ViaTela
		{
			public int Codigo { get; set; }

			public string Descricao { get; set; }
		}

		#endregion
	}
}
