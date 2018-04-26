using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODE
{
    public class Micro
    {

		#region Atributos e propriedades

		public int? Codigo { get; set; }

		public string Descricao { get; set; }

		public Funcionario FuncionarioResponsavel { get; set; }

		public Meso Meso { get; set; }

		#endregion

		#region Construtores

		public Micro() { }

		public Micro(int codigoMicro)
		{

			string mensagemErro;
			MicroBLL BLL = new MicroBLL();

			Micro microCorrente = BLL.getMicros(codigoMicro, "", out mensagemErro).Where(x => x.Codigo == codigoMicro).FirstOrDefault();

			this.Codigo = microCorrente.Codigo;
			this.Descricao = microCorrente.Descricao;
			this.Meso = microCorrente.Meso;
			this.FuncionarioResponsavel = microCorrente.FuncionarioResponsavel;
		}

		#endregion


	}
}
