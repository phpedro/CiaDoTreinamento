using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class TelefoneCliente : Telefones
	{
		#region Atributos e propriedades

		public Cliente cliente { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		#endregion
	}
}
