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

		public DateTime DataInicioColeta { get; set; }

		public DateTime DataFimColeta { get; set; }

		public string Observacao { get; set; }

		public bool Aprovado { get; set; }

		public string DataInicioFormatada
		{
			get
			{
				if (this.DataInicio != DateTime.MinValue)
				{
					return Convert.ToDateTime(this.DataInicio).ToString("dd/MM/yyyy HH:mm:ss");
				}
				else
				{
					return "";
				}
			}
		}

		public string DataFimFormatada
		{
			get
			{
				if (this.DataFim != DateTime.MinValue)
				{
					return Convert.ToDateTime(this.DataFim).ToString("dd/MM/yyyy HH:mm:ss");
				}
				else
				{
					return "";
				}
			}
		}

		public string DataInicioColetaFormatada
		{
			get
			{
				if (this.DataInicioColeta != DateTime.MinValue)
				{
					return Convert.ToDateTime(this.DataInicioColeta).ToString("dd/MM/yyyy HH:mm:ss");
				}
				else
				{
					return "";
				}
			}
		}

		public string DataFimColetaFormatada
		{
			get
			{
				if (this.DataFimColeta != DateTime.MinValue)
				{
					return Convert.ToDateTime(this.DataFimColeta).ToString("dd/MM/yyyy HH:mm:ss");
				}
				else
				{
					return "";
				}
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
