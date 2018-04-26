using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace CiaDoTreinamento.BancoDados
{
    public class Command
    {
        private DbParameter[] dbParams;

        public string CommandText;
        public ConnectionString ConnectionString;

        public Command()
        {
            dbParams = new DbParameter[49];
            ConnectionString = new ConnectionString("connAcess");
        }

        private DbCommandGeneric GetCommand()
        {
            var dbCmd = new DbCommandGeneric();
			DAO dao = new DAO();

            dbCmd.DbCommand = dao.getCommand(ConnectionString);
            dbCmd.DbCommand.CommandText = CommandText;

            return dbCmd;
        }

        public DataTable GetData()
        {
			DAO dao = new DAO();
			DataTable dt = dao.getData(ConnectionString, CommandText);

			return dt;
        }

        public Object ExecuteScalar()
        {
			DAO dao = new DAO();
			return dao.ExecuteScalar(GetCommand());
        }

		public int Execute()
		{
			DAO dao = new DAO();
			return dao.Execute(GetCommand());
		}

		public int Execute_ReturnID() {
			DAO dao = new DAO();
			return dao.Execute_ReturnID(this.ConnectionString, GetCommand());
		}

	}
}
