using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Telefones
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public string Observacao { get; set; }

		#endregion

		#region Classes Aninhadas

		public class TelefoneTela
		{

			public int sequencia { get; set; }
			public string telefone { get; set; }
			public string responsavel { get; set; }
		}

		#endregion
	}
}
