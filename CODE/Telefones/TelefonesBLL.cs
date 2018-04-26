using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefonesBLL
    {

		public int insertTelefone(Telefones telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefonesDAL.insertTelefone(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return -1;
			}
		}

		public bool updateTelefone(Telefones telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefonesDAL.updateTelefone(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteTelefone(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefonesDAL.deleteTelefone(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Telefones> getTelefones(int? codigo, string descricao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefonesDAL.getTelefones(codigo, descricao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
