using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemPedidoBLL
    {

		public bool insertItemPedido(ItemPedido item, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();

				if (!ItemPedidoDAL.insertItemPedido(item, out mensagemErro))
				{
					return false;
				}

				if (!cabecalhoPedidoBLL.updateCabecalhoPedidoTodo(item.CodigoPedido, out mensagemErro))
				{
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateItemPedido(ItemPedido item, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				CabecalhoPedidoBLL cabecalhoPedidoBLL = new CabecalhoPedidoBLL();

				if (!ItemPedidoDAL.updateItemPedido(item, out mensagemErro))
				{
					return false;
				}

				if (!cabecalhoPedidoBLL.updateCabecalhoPedidoTodo(item.CodigoPedido, out mensagemErro))
				{
					return false;
				}

				return true;

			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteItemPedido(int codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItemPedidoDAL.deleteItemPedido(codigoProduto, codigoPedido, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o item. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<ItemPedido> getItemPedido(int? codigoProduto, int codigoPedido, out string mensagemErro)
		{
			mensagemErro = "";
			string pendencias;

			try
			{
				List<ItemPedido> itens = ItemPedidoDAL.getItemPedido(codigoProduto, codigoPedido, out mensagemErro);

				foreach (ItemPedido item in itens)
				{
					item.temPendencia = this.TemPendencia(item, out pendencias, ref mensagemErro);

					if (item.temPendencia)
					{
						item.pendencias = pendencias;
					}
				}

				return itens;
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os itens. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public bool TemPendencia(ItemPedido item, out string listaPendencias, ref string mensagemErro)
		{
			listaPendencias = String.Empty;

			int quantidadeAlunosCurso = ItemPedidoDAL.quantidadeAlunosCurso((int)item.Produto.Codigo, item.CodigoPedido, out mensagemErro);

			if (item.Produto.TemCURSO || item.Produto.TemPAE || item.Produto.TemVISTORIARURAL || item.Produto.TemVISTORIAPASSAGEIROS || item.Produto.TemVISTORIAESCOLAR)
			{
				if (!item.DataInicioVigencia.HasValue)
				{
					listaPendencias += "A data de início da vigência não foi preenchida. <br>";
				}
			}

			if (item.Produto.TemCURSO)
			{
				if (quantidadeAlunosCurso < item.Quantidade)
				{
					listaPendencias += "A quantidade de alunos gravada é menor que a quantidade vendida. <br>";
				}
			}

			if (item.Produto.TemPASTACIPA)
			{
				if (quantidadeAlunosCurso <= 0)
				{
					listaPendencias += "É necessário informar ao menos um membro da comissão. <br>";
				}

				if (String.IsNullOrEmpty(item.NomeMembroRepresentanteCIPA))
				{
					listaPendencias += "É necessário informar o nome do membro representante da CIPA. <br>";
				}

				if (String.IsNullOrEmpty(item.NomeResponsavelCIPA))
				{
					listaPendencias += "É necessário informar o nome do responsável pela CIPA. <br>";
				}
			}

			if (item.Produto.TemPAE)
			{
				if (ItemPedidoDAL.quantidadeAlunosBrigada((int)item.Produto.Codigo, item.CodigoPedido, out mensagemErro) <= 0)
				{
					listaPendencias += "Não foi adicionado nenhum aluno na brigada de incêndio. <br>";
				}
			}

			if (item.Produto.TemNBR14276)
			{
				bool existe;

				if (!ItemPedidoDAL.verificaTemAlunosSemRG((int)item.Produto.Codigo, item.CodigoPedido, out existe, ref mensagemErro))
				{
					mensagemErro = "Erro ao verificar se existem alunos sem RG: " + mensagemErro;
					return false;
				}

				if (existe)
				{
					listaPendencias += "Existem alunos sem o RG cadastrado. <br>";
				}
			}

			if (item.CodigoMotivoPedido == 0)
			{
				listaPendencias += "Existem itens sem motivo de venda!";
			}

			return (listaPendencias.Length > 0 ? true : false);
		}

	}
}
