using CiaDoTreinamento.BancoDados;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CODE.BancoDados
{
	public class BD : IDisposable
	{
		public MySqlConnection Connection;

		public BD()
		{
			var connString = new ConnectionString("");
			Connection = new MySqlConnection(connString.ConnString);
		}

		public void Dispose()
		{
			Connection.Close();
		}
	}
}
