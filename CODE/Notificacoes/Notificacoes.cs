using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class Notificacoes
    {
		public int? Codigo { get; set; }

		public Funcionario FuncionarioCriador { get; set; }

		public Funcionario FuncionarioDestino { get; set; }

		public string Mensagem { get; set; }

		public string UrlRedirect { get; set; }

    }
}
