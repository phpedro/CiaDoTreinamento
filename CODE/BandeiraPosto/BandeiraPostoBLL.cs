using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class BandeiraPostoBLL
    {
		public bool insertBandeiraPosto(BandeiraPosto bandeira, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return BandeiraPostoDAL.insertBandeiraPosto(bandeira, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateBandeiraPosto(BandeiraPosto bandeira, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return BandeiraPostoDAL.updateBandeiraPosto(bandeira, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteBandeiraPosto(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return BandeiraPostoDAL.deleteBandeiraPosto(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<BandeiraPosto> getBandeiras(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return BandeiraPostoDAL.getBandeiras(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
