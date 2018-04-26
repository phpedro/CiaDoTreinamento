using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class ItemOrcamentoBLL
    {
		public bool GetInsertItemOrcamento(ItemOrcamento itemOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItemOrcamentoDAL.GetInsertItemOrcamento(itemOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o item do orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool GetDeleteItemOrcamento(int codigoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItemOrcamentoDAL.GetDeleteItemOrcamento(codigoOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar remover os itens do orçamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<ItemOrcamento> getItensOrcamento(int codigoOrcamento, int? codigoProduto, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItemOrcamentoDAL.getItensOrcamento(codigoOrcamento, codigoProduto, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os itens. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<ItemOrcamento.ItemOrcamentoTela> getItensOrcamentoTela(int codigoOrcamento, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ItemOrcamentoDAL.getItensOrcamentoTela(codigoOrcamento, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os itens. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
