using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ContaBancaria
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoBanco { get; set; }

		public string Descricao { get; set; }

		public bool Ativo { get; set; }

		public int DigitoVerificador { get; set; }

		public int CodigoBeneficiario { get; set; }

		public int IncrementalBoletos { get; set; }

		public int CodigoAgencia { get; set; }

		public int DigitoVerificadorBeneficiario { get; set; }

		public string RazaoSocial { get; set; }

		public string CNPJ { get; set; }

		public int DigitoVerificadorAgencia { get; set; }

		public int CodigoCliente { get; set; }

		public decimal ValorPorBoleto { get; set; }

		public int IncrementalRemessa { get; set; }

		public string Endereco { get; set; }

		public Cidade Cidade { get; set; }

		public string DescricaoBairro { get; set; }

		public string Telefone { get; set; }

		public string NomeRepresentante { get; set; }

		public string CPF { get; set; }

		public int CodigoContaASC { get; set; }

		public int CodigoCondicaoASC { get; set; }

		#endregion
	}
}
