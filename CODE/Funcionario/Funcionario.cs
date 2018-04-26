using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Funcionario: Pessoa
    {

		#region Atributos e propriedades

		public override int? Codigo { get; set; }

		public string Login { get; set; }

		public string Senha { get; set; }

		public string CPF { get; set; }

		public bool PODE_VER_RELATORIO_VENDAS { get; set; }

		public bool PODE_GERAR_BOLETO { get; set; }

		public bool PODE_GERAR_REMESSA_RETORNO { get; set; }

		public bool PODE_CONFIGURAR_DIREITOS { get; set; }

		public bool PODE_CADASTRAR_PRODUTOS { get; set; }

		public bool PODE_ALTERAR_CORREIO { get; set; }

		public Perfil Perfil { get; set; }

		public decimal META_VENDA_MENSAL { get; set; }

		#endregion

		#region Construtores

		public Funcionario() { }

		public Funcionario(int codigoFuncionario)
		{

			string mensagemErro = "";

			Funcionario func = FuncionarioDAL.getFuncionarioByCodigo(codigoFuncionario, out mensagemErro);

			this.Codigo = func.Codigo;
			this.Login = func.Login;
			this.Senha = func.Senha;
			this.CPF = func.CPF;
			this.Nome = func.Nome;
			this.Sexo = func.Sexo;
			this.Email = func.Email;
			this.Telefone = func.Telefone;
			this.Ativo = func.Ativo;
			this.PODE_VER_RELATORIO_VENDAS = func.PODE_VER_RELATORIO_VENDAS;
			this.PODE_GERAR_BOLETO = func.PODE_GERAR_BOLETO;
			this.PODE_GERAR_REMESSA_RETORNO = func.PODE_GERAR_REMESSA_RETORNO;
			this.PODE_CONFIGURAR_DIREITOS = func.PODE_CONFIGURAR_DIREITOS;
			this.PODE_CADASTRAR_PRODUTOS = func.PODE_CADASTRAR_PRODUTOS;
			this.PODE_ALTERAR_CORREIO = func.PODE_ALTERAR_CORREIO;
			this.Perfil = func.Perfil;

		}

		#endregion

		#region Classes Aninhadas

		public class MetaAgente
		{

			public int CodigoAgente { get; set; }

			public decimal Meta { get; set; }

			public decimal ValorTotal { get; set; }

		}

		#endregion

	}
}
