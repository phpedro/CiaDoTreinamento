using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODE
{
    public class Cliente
    {
		#region Atributos e propriedades

		//Dados do Cliente

		public int? Codigo { get; set; }

		public int TipoCliente { get; set; } //1 - CNPJ / 2 - CPF

		//DADOS PESSOA JURÍDICA

		public string CNPJ { get; set; }

		public string RazaoSocial { get; set; }

		public string NomeFantasia { get; set; }

		public string InscricaoEstadual { get; set; }

		//DADOS PESSOA FÍSICA

		public string CPF { get; set; }

		public string Nome { get; set; }

		public string RG { get; set; }

		//DADOS COMPLEMENTARES

		public BandeiraPosto BandeiraPosto { get; set; }

		public RedePosto RedePosto { get; set; }

		public int CodigoStatus { get; set; }

		public bool Ativo { get; set; }

		public EmailCliente EmailPrincipal { get; set; }

		public DateTime DataCadastro { get; set; }

		//Dados do Contato

		public string NomeContato { get; set; }

		public string CargoContato { get; set; }

		//Dados de localização

		public string Endereco { get; set; }

		public string Bairro { get; set; }

		public string CEP { get; set; }

		public Cidade Cidade { get; set; }

		public string SiglaCaixaPostal { get; set; }

		//Dados para Correspondência

		public string EnderecoCorrespondencia { get; set; }

		public string BairroCorrespondencia { get; set; }

		public string CepCorrespondencia { get; set; }

		public Cidade CidadeCorrespondencia { get; set; }

		public string ReferenciaEnderecoCorrespondencia { get; set; }

		//Dados do proprietário

		public string Proprietario { get; set; }

		public string RgProprietario { get; set; }

		public string CpfProprietario { get; set; }

		//Dados para PAE

		public string Coordenador { get; set; }

		public string CargoCoordenador { get; set; }

		//Dados para PPRA

		public string Atividade { get; set; }

		public string GrauRisco { get; set; }

		public string CNAE { get; set; }

		public string Grupo { get; set; }

		//Dados para PCMSO

		public string HorarioFuncionamentoSegSex { get; set; }

		public string HorarioFuncionamentoFDS { get; set; }

		#endregion

		#region Construtores

		public Cliente() { }

		public Cliente(int codigoCliente)
		{
			ClienteBLL BLL = new ClienteBLL();
			string mensagemErro;

			Cliente cliente = BLL.GetClientes(codigoCliente, out mensagemErro).FirstOrDefault();

			if (cliente != null)
			{
				this.Codigo = cliente.Codigo;
				this.TipoCliente = cliente.TipoCliente;
				this.CNPJ = cliente.CNPJ;
				this.RazaoSocial = cliente.RazaoSocial;
				this.NomeFantasia = cliente.NomeFantasia;
				this.InscricaoEstadual = cliente.InscricaoEstadual;
				this.CPF = cliente.CPF;
				this.Nome = cliente.Nome;
				this.RG = cliente.RG;
				this.BandeiraPosto = cliente.BandeiraPosto;
				this.RedePosto = cliente.RedePosto;
				this.CodigoStatus = cliente.CodigoStatus;
				this.EmailPrincipal = cliente.EmailPrincipal;
				this.Ativo = cliente.Ativo;
				this.DataCadastro = cliente.DataCadastro;
				this.NomeContato = cliente.Nome;
				this.CargoContato = cliente.CargoContato;
				this.Endereco = cliente.Endereco;
				this.Bairro = cliente.Bairro;
				this.CEP = cliente.CEP;
				this.Cidade = cliente.Cidade;
				this.SiglaCaixaPostal = cliente.SiglaCaixaPostal;
				this.EnderecoCorrespondencia = cliente.EnderecoCorrespondencia;
				this.BairroCorrespondencia = cliente.BairroCorrespondencia;
				this.CepCorrespondencia = cliente.CepCorrespondencia;
				this.CidadeCorrespondencia = cliente.CidadeCorrespondencia;
				this.ReferenciaEnderecoCorrespondencia = cliente.ReferenciaEnderecoCorrespondencia;
				this.Proprietario = cliente.Proprietario;
				this.RgProprietario = cliente.RgProprietario;
				this.CpfProprietario = cliente.CpfProprietario;
				this.Coordenador = cliente.Coordenador;
				this.CargoCoordenador = cliente.CargoCoordenador;
				this.Atividade = cliente.Atividade;
				this.GrauRisco = cliente.GrauRisco;
				this.CNAE = cliente.CNAE;
				this.Grupo = cliente.Grupo;
				this.HorarioFuncionamentoSegSex = cliente.HorarioFuncionamentoSegSex;
				this.HorarioFuncionamentoFDS = cliente.HorarioFuncionamentoFDS;
			}
		}

		#endregion

		#region ClassesAninhadas

		public class ClienteTela
		{
			public int Codigo { get; set; }

			public int TipoCliente { get; set; }

			public string RazaoSocial { get; set; }

			public string CNPJ { get; set; }

			public string NomeCliente { get; set; }

			public string CPF { get; set; }

			public string Rede { get; set; }

			public string Cidade { get; set; }

			public string Estado { get; set; }

			public string ProximosExpirando { get; set; }

			public string Email { get; set; }

			public int Status { get; set; }

		}

		public class ProdutoExpirando
		{
			public int CodigoPedido { get; set; }

			public string DescricaoProduto { get; set; }

			public string DataExpiracao { get; set; }

		}

		public class ClienteRota
		{
			public int Codigo { get; set; }
			public string RazaoSocial { get; set; }
			public string NomeFantasia { get; set; }
			public string CNPJ { get; set; }
			public string Endereco { get; set; }
			public string Bairro { get; set; }
			public string Cidade { get; set; }
			public string Telefones { get; set; }
			public string Emails { get; set; }
		}

		#endregion
	}
}
