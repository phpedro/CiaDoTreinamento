using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class FuncionarioBLL
    {

		public bool InserFuncionario(Funcionario func, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.InsertFuncionario(func, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}

		}

		public bool UpdateFuncionario(Funcionario func, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.UpdateFuncionario(func, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool DeleteFuncionario(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.DeleteFuncionario(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		//CONSULTAS
		public static List<Funcionario> getAllFuncionarios(out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.getAllFuncionarios(out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static Funcionario getFuncionario(string login, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.getFuncionario(login, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static Funcionario getFuncionarioByCodigo(int codigo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.getFuncionarioByCodigo(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static List<Funcionario> getFuncionarioByPerfil(int codigoPerfil, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.getFuncionarioByPerfil(codigoPerfil, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static Funcionario.MetaAgente getMetaAgente(int codigoAgente, DateTime inicioCiclo, DateTime fimCiclo, out string mensagemErro)
		{
			mensagemErro = "";
			try
			{
				return FuncionarioDAL.getMetaAgente(codigoAgente, inicioCiclo, fimCiclo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = ex.Message;
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

	}
}
