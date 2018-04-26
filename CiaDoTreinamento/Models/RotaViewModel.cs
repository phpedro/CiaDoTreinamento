using CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento
{
    public class RotaViewModel
    {
		#region Atributos e propriedades

		public Rota Rota { get; set; }

		public List<ItemRota> listaItensRota { get; set; }

		#endregion

		#region Construtores

		public RotaViewModel()
		{
			this.listaItensRota = new List<ItemRota>();
		}

		#endregion
	}
}
