using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class PerfilBLL
    {
		public bool insertPerfil(Perfil perfil, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return PerfilDAL.insertPerfil(perfil, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updatePerfil(Perfil perfil, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return PerfilDAL.updatePerfil(perfil, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deletePerfil(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return PerfilDAL.deletePerfil(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Perfil> getPerfis(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return PerfilDAL.getPerfis(codigo, descricao, out mensagemErro);
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
