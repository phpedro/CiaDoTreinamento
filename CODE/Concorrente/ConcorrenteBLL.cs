using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ConcorrenteBLL
    {

		public bool insertConcorrente(Concorrente concorrente, List<TelefoneConcorrente.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ConcorrenteDAL.insertConcorrente(concorrente, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateConcorrente(Concorrente concorrente, List<TelefoneConcorrente.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ConcorrenteDAL.updateConcorrente(concorrente, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteConcorrente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ConcorrenteDAL.deleteConcorrente(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o concorrente. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Concorrente> getConcorrentes(int? codigo, string razao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ConcorrenteDAL.getConcorrentes(codigo, razao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os concorrentes. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<Concorrente> getConcorrentesByCliente(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ConcorrenteDAL.getConcorrentesByCliente(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os concorrentes. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}
	}
}
