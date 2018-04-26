using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Parceiro
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public Cidade Cidade { get; set; }

		public string Descricao { get; set; }

		public string Endereco { get; set; }

		public string Observacao { get; set; }

		public string TipoParceiro { get; set; }

		public string Bairro { get; set; }

		public decimal Custo { get; set; }

		public string Responsavel { get; set; }

		public string Email { get; set; }

		public bool PassaCartao { get; set; }

		#endregion

		#region Construtores

		public Parceiro()
		{
			this.Cidade = new Cidade();
		}

		#endregion
	}
}
