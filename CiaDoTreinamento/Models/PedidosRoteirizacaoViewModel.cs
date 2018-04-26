using CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento
{
    public class PedidosRoteirizacaoViewModel
    {
		public string cidade { get; set; }

		public string latitude { get; set; }

		public string longitude { get; set; }

		public string meso { get; set; }

		public string corMeso { get; set; }

		public List<CabecalhoPedido> listaPedidos { get; set; }

		public int _quantidadePedidos
		{
			get
			{
				return listaPedidos.Count;
			}
		}

		public PedidosRoteirizacaoViewModel()
		{
			this.listaPedidos = new List<CabecalhoPedido>();
		}
	}
}
