using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefoneLicenciamentoAmbientalBLL
    {

		public bool insertTelefoneLicenciamento(TelefoneLicenciamentoAmbiental telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.insertTelefoneLicenciamento(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateTelefoneLicenciamento(TelefoneLicenciamentoAmbiental telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.updateTelefoneLicenciamento(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteTelefoneLicenciamento(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.deleteTelefoneLicenciamento(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAllTelefoneLicenciamento(int codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.deleteAllTelefoneLicenciamento(codigoConcorrente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<TelefoneLicenciamentoAmbiental> getTelefonesLicenciamento(int? codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.getTelefonesLicenciamento(codigoConcorrente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<TelefoneLicenciamentoAmbiental.TelefoneTela> getTelefonesLicenciamentoTela(int? codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneLicenciamentoAmbientalDAL.getTelefonesLicenciamentoTela(codigoConcorrente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}
	}
}
