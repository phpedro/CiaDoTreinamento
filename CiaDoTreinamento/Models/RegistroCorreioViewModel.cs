using CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento.Models
{
    public class RegistroCorreioViewModel
    {
		public RegistroCorreio registroCorreio { get; set; }

		public List<RegistroCorreioEmail> listaEmails { get; set; }
    }
}
