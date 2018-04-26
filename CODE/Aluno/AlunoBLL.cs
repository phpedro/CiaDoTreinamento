using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
	public class AlunoBLL
    {
		public bool InserAluno(Aluno aluno, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AlunoDAL.InserAluno(aluno, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível cadastrar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool UpdateAluno(Aluno aluno, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AlunoDAL.UpdateAluno(aluno, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível atualizar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public bool DeleteAluno(int codigo, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AlunoDAL.DeleteAluno(codigo, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível remover o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

		public List<Aluno.AlunoTela> GetAlunos(int? CodigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AlunoDAL.GetAlunos(CodigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public static string GetAlunosJson(int? CodigoCliente, out string mensagemErro)
		{
			mensagemErro = "";

			try
			{
				return AlunoDAL.GetAlunosJson(CodigoCliente, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return null;
			}
		}

		public bool GetAluno(int CodigoAluno, int CodigoCliente, out Aluno aluno, out string mensagemErro)
		{
			mensagemErro = "";
			aluno = null;
			try
			{
				return AlunoDAL.GetAluno(CodigoAluno, CodigoCliente, out aluno, out mensagemErro);
			}
			catch (Exception ex)
			{
				mensagemErro = "Não foi possível buscar o aluno. Contate o suporte!";
				Uteis.GravarLogErro(ex.TargetSite.Name, ex.Message);
				return false;
			}
		}

	}
}
