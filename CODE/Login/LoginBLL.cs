using System;
using System.Collections.Generic;
using System.Text;

namespace CODE
{
    public class LoginBLL
    {
		public static bool ValidaUsuario(string login, string senha, out Funcionario func, out string mensagemErro)
		{

			func = FuncionarioBLL.getFuncionario(login, out mensagemErro);

			if (func != null)
			{

				if (func.Senha != Uteis.GeraHashMD5(senha))
				{
					mensagemErro = "O usuário e/ou senha são inválidos!";
					return false;
				}

				if (!func.Ativo)
				{
					mensagemErro = "O usuário inátivo! Contate o administrador do sistema.";
					return false;
				}

				return true;
			}

			return false;
		}
	}
}
