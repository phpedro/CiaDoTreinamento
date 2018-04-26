using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class CabecalhoPedido
    {

		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public Cliente Cliente { get; set; }

		public DateTime DataCriacao { get; set; }

		public CondicaoPagamento CondicaoPagamento { get; set; }

		public ContaBancaria ContaBancaria { get; set; }

		public Funcionario FuncionarioInstrutor { get; set; }

		public Funcionario FuncionarioVendedor { get; set; }

		public StatusNegociacao StatusNegociacao { get; set; }

		public string LocalRealizacao { get; set; }

		public Parceiro ParceiroHotel { get; set; }

		public Parceiro ParceiraSalaTreinamento { get; set; }

		public string NumeroNota { get; set; }

		public MotivoNaoVenda MotivoNaoVenda { get; set; }

		public string DetalheMotivoNaoVenda { get; set; }

		public bool Confirmado { get; set; }

		public decimal ValorBoletos { get; set; }

		public bool RealizouContratoVerbal { get; set; }

		public string NumeroART { get; set; }

		public string ObservacaoEnvioART { get; set; }

		public string ObservacaoART { get; set; }

		public decimal PercentualDesconto { get; set; }

		public decimal ValorDesconto { get; set; }

		public decimal ValorTotal { get; set; }

		public bool EnviarPorCorreio { get; set; }

		public bool CobrarISS { get; set; }

		public DateTime DataFechamento { get; set; }

		public DateTime DataInicioTreinamento { get; set; }

		public DateTime DataFinalTreinamento { get; set; }

		public string InfoTreinamento { get; set; }

		public string DetalheRetornoPedido { get; set; }

		public bool CobrarBoletos { get; set; }

		public string valorTotalFormatado
		{
			get
			{
				return String.Format("{0:C}", this.ValorTotal);
			}
		}

		#endregion

		#region Contrutores

		public CabecalhoPedido(){ }

		public CabecalhoPedido(int codigoCliente, int codigoFuncionarioVendedor)
		{
			this.Cliente = new Cliente() { Codigo = codigoCliente };
			this.DataCriacao = DateTime.Now;
			this.CondicaoPagamento = new CondicaoPagamento() { Codigo = 1 };
			this.StatusNegociacao = new StatusNegociacao() { CodigoStatus = 4 };
			this.FuncionarioVendedor = new Funcionario(codigoFuncionarioVendedor);
			this.Confirmado = false;
			this.RealizouContratoVerbal = false;
			this.EnviarPorCorreio = false;
			this.CobrarISS = true;
			this.CobrarBoletos = true;
		}

		#endregion

		#region Classes Aninhadas

		public class CabecalhoPedidoTela
		{
			public int Codigo { get; set; }

			public DateTime DataCadastro { get; set; }

			public string NomeVendedor { get; set; }

			public string StatusPedido { get; set; }

			public decimal ValorPedido { get; set; }
		}

		#endregion
	}
}
