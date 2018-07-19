using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stock.Web.Models
{
    public class UsuarioModel
    {
        internal static object ValidarUsuario(string login, string senha)
        {
            var ret = false;
            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = "Data Source=Partenon.grupoaec.com.br;Initial Catalog=MiracleClaro_BH_Teste;User Id=miracleclaro_bh_pass;Password=T5$MQ=AedccE";
            //    conexao.Open();
            //    using (var comando = new SqlCommand())
            //    {
            //        comando.Connection = conexao;
            //        comando.CommandText = $"select count(*) from usuario where login = {login} and senha={senha}";
            //        ret = ((int)comando.ExecuteScalar() > 0) ;
            //    }
            //}
            return true;
        }
    }
}