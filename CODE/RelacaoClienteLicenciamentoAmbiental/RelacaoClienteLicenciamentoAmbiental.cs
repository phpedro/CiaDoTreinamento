using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RelacaoClienteLicenciamentoAmbiental
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoCliente { get; set; }

		public LicenciamentoAmbiental Concorrente { get; set; }

		public DateTime DataCadastro { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		#endregion
	}
}
