using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RegistroCorreio
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public int CodigoPedido { get; set; }

		public Cliente cliente { get; set; }

		public string CodigoPostagem { get; set; }

		public string Descricao { get; set; }

		public string Comentario { get; set; }

		public DateTime dataPostagem { get; set; }

		public int CodigoEmail { get; set; }

		#endregion

		#region Construtores

		public RegistroCorreio() {
			this.cliente = new Cliente();
		}

		#endregion

	}
}
