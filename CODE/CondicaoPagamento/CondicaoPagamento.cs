using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class CondicaoPagamento
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public bool SolicitaConfirmacao { get; set; }

		#endregion
	}
}
