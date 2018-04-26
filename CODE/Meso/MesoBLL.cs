using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class MesoBLL
    {

		public bool insertMeso(Meso meso, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MesoDAL.insertMeso(meso, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a meso. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateMeso(Meso meso, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MesoDAL.updateMeso(meso, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a meso. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteMeso(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MesoDAL.deleteMeso(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a meso. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Meso> getMesos(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MesoDAL.getMesos(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a meso. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
