using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RelacaoClienteConcorrente
    {

		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoCliente { get; set; }

		public Concorrente Concorrente { get; set; }

		public DateTime DataCadastro { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		#endregion

	}
}
