using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemPedido
    {

		#region Atributos e propriedades

		public Produto Produto { get; set; }

		public DateTime? DataInicioVigencia { get; set; }

		public decimal valorFinal { get; set; }

		public DateTime DataExpiracao { get; set; }

		public string ObservacaoGenerica { get; set; }

		public int CodigoPedido { get; set; }

		public decimal ValorDesconto { get; set; }

		public decimal Subtotal { get; set; }

		public decimal Quantidade { get; set; }

		public bool Confirmado { get; set; }

		public decimal ValorEncargos { get; set; }

		public int CodigoMotivoPedido { get; set; }

		public DateTime? DataInicioTreinamento { get; set; }

		public DateTime? DataFimTreinamento { get; set; }

		public string InformacoesParaTreinamento { get; set; }

		public DateTime dataInicioEntrega { get; set; }

		public DateTime dataFimEntrega { get; set; }


		//PENDÊNCIAS
		public bool temPendencia { get; set; }

		public string pendencias { get; set; }

		//PASTA CIPA
		public string NomeMembroRepresentanteCIPA { get; set; }

		public string NomeResponsavelCIPA { get; set; }

		#endregion

		#region Construtores

		public ItemPedido() {
			this.Produto = new Produto();
		}

		#endregion

	}
}
