using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class TelefoneClienteBLL
    {

		public bool InserTelefoneCliente(TelefoneCliente telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneClienteDAL.InserTelefoneCliente(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool UpdateTelefoneCliente(TelefoneCliente telefone, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneClienteDAL.UpdateTelefoneCliente(telefone, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool DeleteTelefoneCliente(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneClienteDAL.DeleteTelefoneCliente(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o telefone. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<TelefoneCliente> GetTelefonesCliente(int codigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return TelefoneClienteDAL.GetTelefonesCliente(codigoCliente, out mensagemErro);
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
