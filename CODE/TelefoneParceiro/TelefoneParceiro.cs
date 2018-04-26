using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class TelefoneParceiro
    {
		#region Atributos e propriedades

		public Telefones Telefone { get; set; }

		public Parceiro Parceiro { get; set; }

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
