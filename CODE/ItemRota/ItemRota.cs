using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemRota
    {
		#region Atributos e propriedades

		public Rota Rota { get; set; }

		public CabecalhoPedido CabecalhoPedido { get; set; }

		public Parceiro ParceiroSala { get; set; }

		public Parceiro ParceiroHotel { get; set; }

		public DateTime DataInicio { get; set; }

		public DateTime DataFim { get; set; }

		public string Observacao { get; set; }

		public bool Aprovado { get; set; }

		public string DataInicioFormatada
		{
			get
			{
				return this.DataInicio.ToString("dd/MM/yyyy HH:mm:ss");
			}
		}

		public string DataFimFormatada
		{
			get
			{
				return this.DataFim.ToString("dd/MM/yyyy HH:mm:ss");
			}
		}

		#endregion

		#region Construtores

		public ItemRota()
		{
			this.Rota = new Rota();
			this.CabecalhoPedido = new CabecalhoPedido();
		}

		#endregion
	}
}
