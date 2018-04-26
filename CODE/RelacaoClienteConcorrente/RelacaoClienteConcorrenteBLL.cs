using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class RelacaoClienteConcorrenteBLL
    {

		public bool insertRelacaoClienteConcorrente(RelacaoClienteConcorrente relacao, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteConcorrenteDAL.insertRelacaoClienteConcorrente(relacao, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteRelacaoClienteConcorrente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteConcorrenteDAL.deleteRelacaoClienteConcorrente(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<RelacaoClienteConcorrente> getConcorrentesByCliente(int? codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RelacaoClienteConcorrenteDAL.getConcorrentesByCliente(codigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a relação. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
