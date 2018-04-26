using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class CabecalhoOrcamento
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public Cliente Cliente { get; set; }

		public DateTime DataCriacao { get; set; }

		public Funcionario FuncionarioVendedor { get; set; }

		public CondicaoPagamento CondicaoPagamento { get; set; }

		public StatusOrcamento StatusOrcamento { get; set; }

		public int ValidadeOrcamento { get; set; }

		public decimal ValorOrcamento { get; set; }

		public string TelefoneContato { get; set; }

		public DateTime DataExpiracao { get; set; }

		#endregion

		#region Classes Aninhadas

		public class CabecalhoOrcamentoTela
		{
			public int Codigo { get; set; }

			public DateTime DataCadastro { get; set; }

			public DateTime DataExpiracao { get; set; }

			public string NomeVendedor { get; set; }

			public string StatusOrcamento { get; set; }

			public decimal ValorOrcamento { get; set; }
		}

		#endregion
	}
}
