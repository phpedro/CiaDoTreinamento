using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class Aluno : Pessoa
    {
		#region Atributos e propriedades

		public Cliente Cliente { get; set; }

		public string Cargo { get; set; }

		public int CargoBrigada { get; set; }

		public string Rg { get; set; }

		public string CPF { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		#endregion

		#region Classes Aninhadas

		public class AlunoTela
		{

			public int? Codigo { get; set; }

			public string Nome { get; set; }

			public string Cargo { get; set; }

			public bool Ativo { get; set; }

			public Enumeradores.Tipo tipo { get; set; }
		}

		#endregion
	}
}
