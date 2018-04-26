using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class StatusNegociacao
    {
		#region Enumerators

		public enum StatusPedido
		{
			//Legado
			Iniciada = 1,
			NegocioFechado = 2,
			Encerrada = 3,

			//Atuais
			Criado = 4,
			AtualizandoDadosClientes = 5,
			InserindoItens = 6,
			AtualizandoDadosFinais = 7,
			Fechado = 8,
			BoletosGerados = 9,
			NotaFiscalInformada = 10,
			Finalizado = 11,
			Cancelado = 12,
			AguardandoGeracaoDodumentos = 13,
			AguardandoGeracaoDodumentosVistoria = 14,
			PedidoNegadoPeloAdministrativo = 15,
			Orçamento = 16,
			PendenteRota = 17,
			PendenteVistoria = 18

		}

		#endregion Enumerators

		#region Atributos e propriedades

		public int? CodigoStatus { get; set; }

		public string Descricao { get; set; }

		public string Cor { get; set; }

		#endregion

		#region Construtores

		public StatusNegociacao() { }

		public StatusNegociacao(int codigoStatus)
		{
			StatusNegociacaoBLL BLL = new StatusNegociacaoBLL();
			string mensagemErro;

			StatusNegociacao status = BLL.getStatusNegociacao(codigoStatus, null, out mensagemErro)[0];

			this.CodigoStatus = status.CodigoStatus;
			this.Descricao = status.Descricao;
			this.Cor = status.Cor;

		}

		#endregion

	}
}
