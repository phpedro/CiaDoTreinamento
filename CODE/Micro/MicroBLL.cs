using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class MicroBLL
    {

		public bool insertMicro(Micro micro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MicroDAL.insertMicro(micro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateMicro(Micro micro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MicroDAL.updateMicro(micro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteMicro(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MicroDAL.deleteMicro(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Micro> getMicros(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return MicroDAL.getMicros(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a micro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
