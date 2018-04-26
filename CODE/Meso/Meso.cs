using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODE
{
    public class Meso
    {

		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		#endregion

		#region Construtores

		public Meso() { }

		public Meso(int codigoMeso)
		{

			string mensagemErro;
			MesoBLL BLL = new MesoBLL();

			Meso mesoCorrente = BLL.getMesos(codigoMeso, "", out mensagemErro).Where(x => x.Codigo == codigoMeso).FirstOrDefault();

			this.Codigo = mesoCorrente.Codigo;
			this.Descricao = mesoCorrente.Descricao;
		}

		#endregion

	}
}
