using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODE;

namespace CiaDoTreinamento.Models
{
    public class PedidoViewModel
    {
		public CabecalhoPedido cabecalhoPedido { get; set; }

		public List<ItemPedido> itensPedido { get; set; }
    }
}
