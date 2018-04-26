using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemOrcamento
    {
		
		#region Atributos e propriedades

		public CabecalhoOrcamento cabecalhoOrcamento { get; set; }

		public Produto produto { get; set; }

		public int quantidade { get; set; }

		public decimal percentualDesconto { get; set; }

		public decimal subtotal { get; set; }

		public decimal acrescimo { get; set; }

		#endregion

		#region Classes Aninhadas

		public class ItemOrcamentoTela
		{
			public int produto_Codigo { get; set; }

			public string Nome { get; set; }

			public int Quantidade { get; set; }

			public decimal PercentualDesconto { get; set; }

			public decimal Subtotal { get; set; }

			public decimal Acrescimo { get; set; }

			public decimal valorUnitario { get; set; }
		}

		#endregion
	}
}
