using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class EmailClienteBLL
    {

		public bool insertEmail(EmailCliente email, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return EmailClienteDAL.insertEmail(email, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o email. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateEmail(EmailCliente email, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return EmailClienteDAL.updateEmail(email, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o email. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<EmailCliente> GetEmails(int codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return EmailClienteDAL.GetEmails(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o email. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
