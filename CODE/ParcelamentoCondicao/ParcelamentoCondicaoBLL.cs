using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ParcelamentoCondicaoBLL
    {

		public bool insertParcela(ParcelamentoCondicao parcela, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParcelamentoCondicaoDAL.insertParcela(parcela, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteParcela(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParcelamentoCondicaoDAL.deleteParcela(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAllParcelaByCondicao(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParcelamentoCondicaoDAL.deleteAllParcelaByCondicao(codigoCondicao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover as parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<ParcelamentoCondicao> getParcelas(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParcelamentoCondicaoDAL.getParcelas(codigoCondicao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<ParcelamentoCondicao.ParcelaTela> getParcelasTela(int codigoCondicao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParcelamentoCondicaoDAL.getParcelasTela(codigoCondicao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar as parcela. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
