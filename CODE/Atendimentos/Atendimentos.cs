using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Atendimentos
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public Funcionario Funcionario { get; set; }

		public int CodigoPedido { get; set; }

		public string Descricao { get; set; }

		public DateTime? DataRegistro { get; set; }

		public Enumeradores.Tipo tipo { get; set; }

		public string _dataRegistroFormatada
		{
			get
			{
				if (this.DataRegistro == null)
				{
					return "";
				}
				else
				{
					return Convert.ToDateTime(this.DataRegistro).ToString("dd/MM/yyyy HH:mm");
				}
			}
		}

		#endregion
	}
}
