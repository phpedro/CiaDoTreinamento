using CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento.Models
{
    public class CidadesRotaViewModel
    {
		public Cidade cidade { get; set; }

		public List<CabecalhoPedido> listaPedidos { get; set; }

		public int qtdePedidos { get; set; }

		public decimal valorTotal { get; set; }

		public int qtdePedidosVistoria { get; set; }

		public string valorTotalFormatado
		{
			get
			{
				return String.Format("{0:C}", this.valorTotal);
			}
		}

		public int qtdePedidosPendentes
		{
			get
			{
				if (this.listaPedidos != null)
				{
					return listaPedidos.Where(x => x.StatusNegociacao.CodigoStatus == 17).Count();
				}
				else
				{
					return 0;
				}
			}
		}

	}
}
