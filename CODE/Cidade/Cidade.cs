using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Cidade
    {
		#region Atributos e Propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public string TelefoneHospital { get; set; }

		public string TelefonePrefeitura { get; set; }

		public string Estado { get; set; }

		public Meso Meso { get; set; }

		public Micro Micro { get; set; }

		public string Latitude { get; set; }

		public string Longitude { get; set; }

		#endregion

		#region Construtores

		public Cidade()
		{
			this.Meso = new Meso();
			this.Micro = new Micro();
		}

		#endregion
	}
}
