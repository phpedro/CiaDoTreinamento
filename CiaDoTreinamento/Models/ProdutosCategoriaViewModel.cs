using CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento.Models
{
    public class ProdutosCategoriaViewModel
    {
		public string Categoria { get; set; }
		public List<Roteirizacao.ProdutosCategoria> listaProdutos { get; set; }

		public int qtdeProdutos
		{
			get
			{
				return (listaProdutos == null ? 0 : listaProdutos.Count);
			}
		}
    }
}
