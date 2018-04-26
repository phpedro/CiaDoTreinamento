using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Rota
    {
		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public Funcionario Instrutor { get; set; }

		public DateTime DataInicio { get; set; }

		public DateTime DataFim { get; set; }

		public string Observacao { get; set; }

		public string _status
		{
			get
			{
				if (this.DataInicio > DateTime.Now)
				{
					return "Rota não iniciada";
				}
				else if (DataFim > DateTime.Now)
				{
					return "Rota em progresso";
				}
				else
				{
					return "Rota concluída";
				}
			}
		}

		#endregion

		#region Construtores

		public Rota()
		{
			this.Instrutor = new Funcionario();
		}

		#endregion
	}
}
