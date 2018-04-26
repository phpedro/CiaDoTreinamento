using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class Perfil
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public bool PodeAlterarValorItemPedido { get; set; }

		public bool PodeFazerPedidos { get; set; }

		public bool PodeAlterarPerfis { get; set; }

		public bool PodeConfirmarPedido { get; set; }

		public bool PodeGerarRemImpRetBoletos { get; set; }

		public bool PodeVisualizarRelatorios { get; set; }

		public bool PodeAlterarPercDescPedido { get; set; }

		public bool PodeVisualizarRelOutrosFuncionarios { get; set; }

		public bool PodeExportarDados { get; set; }

		public bool PodeFiltrarPedStsCli { get; set; }

		public bool PodeOcultarItemTelaPrincipal { get; set; }

		public bool PodeVenderSemISS { get; set; }

		public bool PodeManterProdutos { get; set; }

		public bool PodeManterCondicoesPagamento { get; set; }

		public bool PodeAlterarStatusBoletos { get; set; }

		public bool PodeCancelarPedidos { get; set; }

		public bool PodeAlterarPedidoAposFinalizado { get; set; }

		public bool PodeAlterarResponsavelCidade { get; set; }

		public bool PodeVenderCondRestrita { get; set; }

		public bool PodeAlterarNomeRede { get; set; }

		public bool PodeRegAtendimentoPedFechado { get; set; }

		public bool PodeExcluirTelCliente { get; set; }

		public bool PodeGerarDocumentos { get; set; }

		public bool PodeFinalziarPedidoComPendencia { get; set; }

		public bool PodeAlterarStatusPedido { get; set; }

		#endregion

		#region Construtores

		public Perfil()
		{

		}

		public Perfil(int codigoPerfil)
		{
			PerfilBLL perfilBLL = new PerfilBLL();
			string mensagemErro;

			Perfil perfil = perfilBLL.getPerfis(codigoPerfil, null, out mensagemErro).Find(x => x.Codigo == codigoPerfil);

			this.Codigo = perfil.Codigo;
			this.Descricao = perfil.Descricao;
			this.PodeAlterarValorItemPedido = perfil.PodeAlterarValorItemPedido;
			this.PodeFazerPedidos = perfil.PodeFazerPedidos;
			this.PodeAlterarPerfis = perfil.PodeAlterarPerfis;
			this.PodeConfirmarPedido = perfil.PodeConfirmarPedido;

		}

		#endregion

	}
}
