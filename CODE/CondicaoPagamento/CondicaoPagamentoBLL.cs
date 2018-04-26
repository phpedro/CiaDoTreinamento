using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class CondicaoPagamentoBLL
    {

		public bool insertCondicaoPagamento(CondicaoPagamento condicao, List<ParcelamentoCondicao.ParcelaTela> parcelas, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CondicaoPagamentoDAL.insertCondicaoPagamento(condicao, parcelas, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateCondicaoPagamento(CondicaoPagamento condicao, List<ParcelamentoCondicao.ParcelaTela> parcelas, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CondicaoPagamentoDAL.updateCondicaoPagamento(condicao, parcelas, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteCondicaoPagamento(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CondicaoPagamentoDAL.deleteCondicaoPagamento(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a condição de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<CondicaoPagamento> getCondicoes(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return CondicaoPagamentoDAL.getCondicoes(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as condições de pagamento. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
