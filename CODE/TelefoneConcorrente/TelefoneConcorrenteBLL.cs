using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefoneConcorrenteBLL
    {

		public bool insertTelefoneConcorrente(TelefoneConcorrente telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.insertTelefoneConcorrente(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateTelefoneConcorrente(TelefoneConcorrente telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.updateTelefoneConcorrente(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteTelefoneConcorrente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.deleteTelefoneConcorrente(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteAllTelefoneConcorrente(int codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.deleteAllTelefoneConcorrente(codigoConcorrente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<TelefoneConcorrente> getTelefonesConcorrente(int? codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.getTelefonesConcorrente(codigoConcorrente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os telefones. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<TelefoneConcorrente.TelefoneTela> getTelefonesConcorrenteTela(int? codigoConcorrente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneConcorrenteDAL.getTelefonesConcorrenteTela(codigoConcorrente, out mensagemErro);
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
