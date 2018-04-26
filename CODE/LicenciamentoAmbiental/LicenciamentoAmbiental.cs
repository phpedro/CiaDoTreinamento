using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class LicenciamentoAmbiental
    {
		#region Atributos e Propriedades

		public int? Codigo { get; set; }

		public string RazaoSocial { get; set; }

		public string CNPJ { get; set; }

		public Cidade Cidade { get; set; }

		public string Endereco { get; set; }

		public string Bairro { get; set; }

		public string CEP { get; set; }

		public DateTime DataCadastro { get; set; }

		public string Descricao { get; set; }

		#endregion
	}
}
