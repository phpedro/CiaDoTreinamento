using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ParceiroBLL
    {

		public bool insertParceiro(Parceiro parceiro, List<TelefoneParceiro.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParceiroDAL.insertParceiro(parceiro, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateParceiro(Parceiro parceiro, List<TelefoneParceiro.TelefoneTela> telefones, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParceiroDAL.updateParceiro(parceiro, telefones, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteParceiro(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				TelefoneParceiroBLL BLL = new TelefoneParceiroBLL();
				if (BLL.deleteAllTelefoneParceiro(codigo, out mensagemErro))
				{
					return ParceiroDAL.deleteParceiro(codigo, out mensagemErro);
				}
				else
				{
					mensagemErro = "Não foi possível remover o parceiro. Contate o suporte!";
					return false;
				}

				
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Parceiro> getParceiros(int? codigo, string descricao, string estado, int? codigoCidade, string Tipo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParceiroDAL.getParceiros(codigo, descricao, estado, codigoCidade, Tipo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o parceiro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<string> getTiposParceiros(out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ParceiroDAL.getTiposParceiros(out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os tipos de parceiros. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
