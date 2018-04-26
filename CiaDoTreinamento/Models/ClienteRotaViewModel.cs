using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODE;

namespace CiaDoTreinamento.Models
{
    public class ClienteRotaViewModel
    {
		public Cliente.ClienteRota cliente { get; set; }
		
		public List<ItemPedido> listaItens { get; set; }

		public List<Atendimentos> atendimentos { get; set; }

		public List<string> telefones
		{
			get
			{
				return this.cliente.Telefones.Split('#').ToList();
			}
		}

		public List<string> emails
		{
			get
			{
				return this.cliente.Emails.Split('#').ToList();
			}
		}

		public ClienteRotaViewModel()
		{
			this.cliente = new Cliente.ClienteRota();
			this.listaItens = new List<ItemPedido>();
		}

	}
}
