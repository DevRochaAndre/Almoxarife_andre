using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class MaterialDAO
    {

        private Conexao conexao = new Conexao();


        public Material BuscarPorCodigo(int codigo)
        {
            var conn = conexao.Abrir();



            string sql = "SELECT * FROM material WHERE Codigo = @Codigo";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Material m = new Material();
                    m.Id = Convert.ToInt32(reader["Id"]);
                    m.Codigo = Convert.ToInt32(reader["Codigo"]);
                    m.Descricao = reader["Descricao"].ToString();
                    m.Categoria = reader["Categoria"].ToString();
                    m.Marca = reader["Marca"].ToString();
                    m.Modelo = reader["Modelo"].ToString();

                    if (reader["Validade"] != DBNull.Value)
                        m.Validade = Convert.ToDateTime(reader["Validade"]);
                    else
                        m.Validade = null;

                    m.TipoProduto = Convert.ToInt32(reader["TipoProduto"]);

                    return m;
                }
            }


            return null;
        }


        public void Inserir(Material m)
        {
            var conn = conexao.Abrir();




            string sql = @"INSERT INTO material (Codigo, Descricao, Categoria, Marca, Modelo, TipoProduto, Validade) VALUES (@Codigo, @Descricao, @Categoria, @Marca, @Modelo, @TipoProduto, @Validade)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Codigo", m.Codigo);
            cmd.Parameters.AddWithValue("@Descricao", m.Descricao);
            cmd.Parameters.AddWithValue("@Categoria", m.Categoria);
            cmd.Parameters.AddWithValue("@Marca", m.Marca);
            cmd.Parameters.AddWithValue("@Modelo", m.Modelo);
            cmd.Parameters.AddWithValue("@TipoProduto", m.TipoProduto);


            cmd.Parameters.AddWithValue("@Validade", m.Validade == DateTime.MinValue ? (object)DBNull.Value : m.Validade);

            cmd.ExecuteNonQuery();
        }
        public void AtualizarEstoque(int codigoMaterial, string descricaoMaterial, int quantidadeRecebida)
        {
            var conn = conexao.Abrir();
            if (conn == null)
            {
                Console.WriteLine("Erro: conexão com o banco não foi aberta.");
                return;
            }

            try
            {

                string sql = "UPDATE estoque SET QuantidadeEstoque = QuantidadeEstoque + @Quantidade WHERE MaterialCodigo = @Codigo";
                MySqlCommand cmdUpdate = new MySqlCommand(sql, conn);
                cmdUpdate.Parameters.AddWithValue("@Quantidade", quantidadeRecebida);
                cmdUpdate.Parameters.AddWithValue("@Codigo", codigoMaterial);

                int linhasAfetadas = cmdUpdate.ExecuteNonQuery();


                if (linhasAfetadas == 0)
                {
                    string sqlInsert = "INSERT INTO estoque (MaterialCodigo, Descricao, QuantidadeEstoque) VALUES (@Codigo, @Descricao, @Quantidade)";
                    MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, conn);
                    cmdInsert.Parameters.AddWithValue("@Codigo", codigoMaterial);
                    cmdInsert.Parameters.AddWithValue("@Descricao", descricaoMaterial);
                    cmdInsert.Parameters.AddWithValue("@Quantidade", quantidadeRecebida);
                    cmdInsert.ExecuteNonQuery();
                }

                Console.WriteLine("Estoque atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar estoque: {ex.Message}");
                Console.WriteLine("Pressione Enter para continuar...");
                Console.ReadLine();
            }
        }
        public void DarBaixaEstoque(int codigoMaterial, int quantidade)
        {
            var conn = conexao.Abrir();
            try
            {
                string sql = "UPDATE estoque SET QuantidadeEstoque = QuantidadeEstoque - @Quantidade WHERE MaterialCodigo = @Codigo";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Codigo", codigoMaterial);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao dar baixa no estoque: {ex.Message}");
            }

        }
        public void EmpenharEstoque(int codigoMaterial, int quantidade)
        {
            var conn = conexao.Abrir();
            try
            {
                string sql = "UPDATE estoque SET QuantidadeEstoque = QuantidadeEstoque - @Quantidade, QuantidadeEmpenhada = QuantidadeEmpenhada + @Quantidade WHERE MaterialCodigo = @Codigo";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Codigo", codigoMaterial);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao empenhar estoque: {ex.Message}");
            }
        }

        public void DevolverEstoque(int codigo, int quantidade)
        {
            var conn = conexao.Abrir();
            try
            {
                string sql = "UPDATE estoque SET QuantidadeEstoque = QuantidadeEstoque + @Quantidade, " +
                             "QuantidadeEmpenhada = QuantidadeEmpenhada - @Quantidade " +
                             "WHERE MaterialCodigo = @Codigo";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao devolver material no estoque: {ex.Message}");
            }
        }
        public void AtualizarMaterial(Material m)
        {
            var conn = conexao.Abrir();

            try
            {
                string sql = @"UPDATE material 
                       SET Descricao = @Descricao, 
                           Categoria = @Categoria, 
                           Marca = @Marca, 
                           Modelo = @Modelo, 
                           Validade = @Validade 
                       WHERE Codigo = @Codigo";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Descricao", m.Descricao);
                cmd.Parameters.AddWithValue("@Categoria", m.Categoria);
                cmd.Parameters.AddWithValue("@Marca", m.Marca);
                cmd.Parameters.AddWithValue("@Modelo", m.Modelo);
                cmd.Parameters.AddWithValue("@Validade", m.Validade == DateTime.MinValue ? (object)DBNull.Value : m.Validade);
                cmd.Parameters.AddWithValue("@Codigo", m.Codigo);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar material: {ex.Message}");
            }
        }
        public void DeletarMaterial(int codigo)
        {
            var conn = conexao.Abrir();

            try
            {
                string sql = "DELETE FROM material WHERE Codigo = @Codigo";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar material: {ex.Message}");
            }
        }
        public bool VerificarVinculoMaterial(int codigo)
        {
            var conn = conexao.Abrir();

            try
            {
                // Verificar estoque
                string sqlEstoque = "SELECT COUNT(*) FROM estoque WHERE MaterialCodigo = @Codigo";
                MySqlCommand cmdEstoque = new MySqlCommand(sqlEstoque, conn);
                cmdEstoque.Parameters.AddWithValue("@Codigo", codigo);
                long countEstoque = (long)cmdEstoque.ExecuteScalar();
                if (countEstoque > 0) return true;

                // Verificar item de requisição
                string sqlItemReq = "SELECT COUNT(*) FROM itemrequisicao WHERE MaterialCodigo = @Codigo";
                MySqlCommand cmdItemReq = new MySqlCommand(sqlItemReq, conn);
                cmdItemReq.Parameters.AddWithValue("@Codigo", codigo);
                long countItemReq = (long)cmdItemReq.ExecuteScalar();
                if (countItemReq > 0) return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar vinculo do material: {ex.Message}");
                return true; // Para segurança, impedir deleção se houver erro
            }

            return false; // sem vínculos, pode deletar
        }

    }
}




