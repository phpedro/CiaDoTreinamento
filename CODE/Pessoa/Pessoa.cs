using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Pessoa
    {
		#region Atributos e propriedades

		public virtual int? Codigo { get; set; }

		public string Nome { get; set; }

		public string Sexo { get; set; }

		public string Email { get; set; }

		public string Telefone { get; set; }

		public bool Ativo { get; set; }

		#endregion

	}
}
