using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class PessoaBLL
    {
		public static bool updateSenhaPessoa(int codigoPessoa, string senha)
		{
			try
			{
				return PessoaDAL.updateSenhaPessoa(codigoPessoa, senha);
			}
			catch (Exception ex)
			{
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public static Pessoa getPessoaByLogin(string login, out string mensagemErro)
		{
			Pessoa p = null;
			mensagemErro = "";
			try
			{
				p = PessoaDAL.getPessoaByLogin(login, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o funcionário. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}

			return p;
		}

	}
}
