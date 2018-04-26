using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CiaDoTreinamento.BancoDados
{
    public class ConnectionString
    {
        public string ConnString { get; set; }
        public string ProviderName { get; set; }

        public ConnectionString(string connStringName)
        {
			this.ConnString = "Server=localhost;Database=bd_ciatreinamento;Uid=root;Pwd=masterkey;SslMode=none;Pooling=false;";
			this.ProviderName = "MySql.Data.MySqlClient";
		}
    }
}
