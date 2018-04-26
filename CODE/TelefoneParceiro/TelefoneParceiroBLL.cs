using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefoneParceiroBLL
    {
		public bool insertTelefoneParceiro(TelefoneParceiro telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				int retornoTelefone = TelefonesDAL.insertTelefone(telefone.Telefone, out mensagemErro);

				if (retornoTelefone != -1)
				{

					telefone.Telefone.Codigo = retornoTelefone;

					return TelefoneParceiroDAL.insertTelefoneParceiro(telefone, out mensagemErro);
				}
				else
				{
					mensagemErro = "Não foi possível cadastrar o telefone do parceiro. Contate o suporte!";
					return false;
				}

				
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteTelefoneParceiro(int codigoTelefone, int codigoParceiro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneParceiroDAL.deleteTelefoneParceiro(codigoTelefone, codigoParceiro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAllTelefoneParceiro(int codigoParceiro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneParceiroDAL.deleteAllTelefoneParceiro(codigoParceiro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os telefones do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<TelefoneParceiro.TelefoneTela> getTelefonesParceiroTela(int? codigoParceiro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneParceiroDAL.getTelefonesParceiroTela(codigoParceiro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones do parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
