using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Stock.Web.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Preencha a descrição")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }


        internal static object ObterListaProduto()
        {
            var ret = new List<GrupoProdutoModel>();
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select id, nome, ativo from sistema";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel { Id = (int)reader["id"], Nome = reader["nome"].ToString(), Ativo = (bool)reader["ativo"] });
                    }
                }
            }
            return true;
        }

        internal static GrupoProdutoModel ObterGrupoProdutoPorId(int idGrupoProduto)
        {
            GrupoProdutoModel ret = null;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = $"select count(*) from sistema where login = @idGrupoProduto";
                    comando.Parameters.Add("@idGrupoProduto", SqlDbType.VarChar).Value = idGrupoProduto;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new GrupoProdutoModel { Id = (int)reader["id"], Nome = reader["nome"].ToString(), Ativo = (bool)reader["ativo"] };
                    }
                }
            }
            return ret;
        }

        internal static bool ExcluirGrupoProduto(int idGrupoProduto)
        {
            var ret = false;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = $"delete from sistema where idGrupoProduto = @idGrupoProduto";
                    comando.Parameters.Add("@idGrupoProduto", SqlDbType.Int).Value = idGrupoProduto;
                    ret = (comando.ExecuteNonQuery()) > 0;
                }
            }
            return ret;
        }

        public void Salvar()
        {
            var ret = 0;

            var model = ObterGrupoProdutoPorId(this.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
                conexao.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conexao;
                    if (model == null)
                    {
                        cmd.CommandText = $"insert into grupo-produto values(@Nome, @Ativo)";
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = this.Nome;
                        cmd.Parameters.Add("@Ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);
                        ret = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        cmd.CommandText = $"update grupo-produto set nome = @Nome, ativo = @Ativo where id = @id";
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = this.Nome;
                        cmd.Parameters.Add("@Ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }

            }
        }
    }
}