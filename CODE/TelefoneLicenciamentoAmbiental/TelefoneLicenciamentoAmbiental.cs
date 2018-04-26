using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class TelefoneLicenciamentoAmbiental
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoConcorrente { get; set; }

		public string Descricao { get; set; }

		public string Responsavel { get; set; }

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
