using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class ContaBancariaBLL
    {

		public bool insertContaBancaria(ContaBancaria conta, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContaBancariaDAL.insertContaBancaria(conta, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool updateContaBancaria(ContaBancaria conta, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContaBancariaDAL.updateContaBancaria(conta, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool deleteContaBancaria(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContaBancariaDAL.deleteContaBancaria(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<ContaBancaria> getContas(int? codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return ContaBancariaDAL.getContas(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar a conta bancária. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
