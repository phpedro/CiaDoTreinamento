using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ParcelamentoCondicao
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoCondicao { get; set; }

		public bool EhAVista { get; set; }

		public int NumeroDiasPrazo { get; set; }

		#endregion

		#region ClassesAninhadas

		public class ParcelaTela
		{

			public int sequencia { get; set; }

			public int numeroDias { get; set; }
		}

		#endregion
	}
}
