using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class AmbienteBLL
    {
		//INSERT
		public bool insertAmbiente(Ambiente ambiente, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return AmbienteDAL.insertAmbiente(ambiente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public bool updateAmbiente(Ambiente ambiente, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return AmbienteDAL.updateAmbiente(ambiente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public bool deleteAmbiente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return AmbienteDAL.deleteAmbiente(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public List<Ambiente> getAmbientes(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";
			List<Ambiente> listaAmbientes = null;
			try
			{
				listaAmbientes = AmbienteDAL.getAmbientes(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return listaAmbientes;
		}

	}
}
