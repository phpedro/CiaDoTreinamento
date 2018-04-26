using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class RegistroCorreioBLL
    {

		//INSERT
		public bool insertRegistroCorreio(RegistroCorreio registro, int[] codigoEmails, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{

				int codigo = RegistroCorreioDAL.insertRegistroCorreio(registro, out mensagemErro);

				if (codigo > 0)
				{
					registro.Codigo = codigo;

					foreach (int item in codigoEmails)
					{
						RegistroCorreioDAL.insertRegistroCorreioEmail(codigo, item, out mensagemErro);
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//UPDATE
		public bool updateRegistroCorreio(RegistroCorreio registro, int[] codigoEmails, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				RegistroCorreioDAL.deleteRegistroCorreioEmail((int)registro.Codigo, out mensagemErro);

				foreach (int item in codigoEmails)
				{
					RegistroCorreioDAL.insertRegistroCorreioEmail((int)registro.Codigo, item, out mensagemErro);
				}

				return RegistroCorreioDAL.updateRegistroCorreio(registro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//DELETE
		public bool deleteRegistroCorreio(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RegistroCorreioDAL.deleteRegistroCorreioEmail(codigo, out mensagemErro) && RegistroCorreioDAL.deleteRegistroCorreio(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o registro. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public string getDescricaoEmailsRegitroCorreio(int codigoRegistro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RegistroCorreioDAL.getDescricaoEmailsRegitroCorreio(codigoRegistro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os registros. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public RegistroCorreio getRegistroCorreioByCodigo(int codigoRegistro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RegistroCorreioDAL.getRegistroCorreioByCodigo(codigoRegistro, out mensagemErro)[0];
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os registros. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<RegistroCorreio> getRegistrosCorreio(string CNPJ, string razaoSocial, int? codigoCliente, string CPF, string nomeCliente,
															int? codigoPedido, DateTime? dataInicio, DateTime? dataFim, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RegistroCorreioDAL.getRegistrosCorreio(CNPJ, razaoSocial, codigoCliente, CPF, nomeCliente, codigoPedido, dataInicio, dataFim, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os registros. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public List<RegistroCorreioEmail> getEmailsRegistroCorreio(int codigoRegistro, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return RegistroCorreioDAL.getEmailsRegistroCorreio(codigoRegistro, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar os registros. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
