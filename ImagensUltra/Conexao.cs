using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ImagensUltra
{
    public class Conexao
    {
        SqlConnection conection;
        public Conexao()
        {
            conection = new SqlConnection();
            conection.ConnectionString = @"Data Source=WIDDIAN;Initial Catalog=Imagens;Integrated Security=True";

        }
        public SqlConnection Conectar()
        {
            if (conection.State == System.Data.ConnectionState.Closed)
            {
                conection.Open();
            }
            return conection;


        }
        public void Desconectar()
        {
            if (conection.State == System.Data.ConnectionState.Open)
            {
                conection.Close();
            }


        }

    }
}
