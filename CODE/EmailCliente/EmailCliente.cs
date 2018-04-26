using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class EmailCliente
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public int Cliente { get; set; }

		public DateTime DataCadastro { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		#endregion

	}
}
