using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class LicenciamentoAmbientalBLL
    {

		public bool insertLicenciamentoAmbiental(LicenciamentoAmbiental licenciamento, List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return LicenciamentoAmbientalDAL.insertLicenciamentoAmbiental(licenciamento, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateLicenciamentoAmbiental(LicenciamentoAmbiental licenciamento, List<TelefoneLicenciamentoAmbiental.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return LicenciamentoAmbientalDAL.updateLicenciamentoAmbiental(licenciamento, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteLicenciamentoAmbiental(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return LicenciamentoAmbientalDAL.deleteLicenciamentoAmbiental(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<LicenciamentoAmbiental> getLicenciamentoAmbiental(int? codigo, string razao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return LicenciamentoAmbientalDAL.getLicenciamentoAmbiental(codigo, razao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a empresa de licenciamento ambiental. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
