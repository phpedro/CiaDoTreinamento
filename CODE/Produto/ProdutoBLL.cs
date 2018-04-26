using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ProdutoBLL
    {

		public bool insertProduto(Produto produto, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ProdutoDAL.insertProduto(produto, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateProduto(Produto produto, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ProdutoDAL.updateProduto(produto, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Produto.ProdutoTela> GetProdutosTela(out string mensagemErro, bool ativos = false)
		{
			mensagemErro = "";

			try
			{
				return ProdutoDAL.GetProdutosTela(out mensagemErro, ativos);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os produtos. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public Produto GetProdutoById(int? id, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ProdutoDAL.GetProdutoById(id, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Produto.Produto2ViaTela> GetProdutos2ViaTela(out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ProdutoDAL.GetProdutos2ViaTela(out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o produto. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
