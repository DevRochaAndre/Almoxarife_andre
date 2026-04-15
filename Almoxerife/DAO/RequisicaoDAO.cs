using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class RequisicaoDAO
    {
        private Conexao conexao = new Conexao();
        private MaterialDAO mdao = new MaterialDAO();

        public void Inserir(Requisicao req)
        {
            var conn = conexao.Abrir();

            try
            {
                string sql = "INSERT INTO requisicao (NumeroRequisicao, DataSaida, FuncionarioId, MatriculaFuncionario, Status) " + "VALUES (@NumeroRequisicao, @DataSaida, @FuncionarioId, @MatriculaFuncionario, @Status)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@NumeroRequisicao", req.NumeroRequisicao);
                cmd.Parameters.AddWithValue("@DataSaida", req.DataSaida);
                cmd.Parameters.AddWithValue("@FuncionarioId", req.FuncionarioId);
                cmd.Parameters.AddWithValue("@MatriculaFuncionario", req.MatriculaFuncionario);
                cmd.Parameters.AddWithValue("@Status", req.Status);
                cmd.ExecuteNonQuery();

                int requisicaoId = (int)cmd.LastInsertedId;
                req.Id = requisicaoId;


                foreach (var item in req.Itens)
                {
                    string sqlItem = "INSERT INTO itemrequisicao (RequisicaoId, MaterialCodigo, MaterialDescricao, Quantidade, TipoMaterial) " + "VALUES (@RequisicaoId, @MaterialCodigo, @MaterialDescricao, @Quantidade, @TipoMaterial)";

                    MySqlCommand cmdItem = new MySqlCommand(sqlItem, conn);

                    cmdItem.Parameters.AddWithValue("@RequisicaoId", requisicaoId);
                    cmdItem.Parameters.AddWithValue("@MaterialCodigo", item.MaterialId);
                    cmdItem.Parameters.AddWithValue("@MaterialDescricao", item.MaterialDescricao);
                    cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                    cmdItem.Parameters.AddWithValue("@TipoMaterial", item.TipoMaterial);
                    cmdItem.ExecuteNonQuery();

                    if (item.TipoMaterial == 1)
                    {
                        mdao.DarBaixaEstoque(item.MaterialId, item.Quantidade);

                    }
                    else if (item.TipoMaterial == 2)
                    {
                        mdao.EmpenharEstoque(item.MaterialId, item.Quantidade);
                    }

                }
                Console.WriteLine("\nRequisição cadastrada com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar requisição: {ex.Message}");
            }

        }
        public bool VerificarItensPendentes(int numeroRequisicao)
        {
            var conn = conexao.Abrir();
            try
            {
                string sql = @"SELECT COUNT(*) FROM itemrequisicao ir
                               JOIN requisicao r ON ir.RequisicaoId = r.Id
                               WHERE r.NumeroRequisicao = @NumeroRequisicao
                                 AND ir.TipoMaterial = 2
                                 AND ir.Quantidade > (
                                     SELECT IFNULL(SUM(idv.Quantidade),0)
                                     FROM itemdevolucao idv
                                     JOIN devolucao d ON idv.DevolucaoId = d.Id
                                     WHERE d.NumeroRequisicao = r.NumeroRequisicao
                                     AND idv.MaterialCodigo = ir.MaterialCodigo
                                 )";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NumeroRequisicao", numeroRequisicao);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // true = ainda existem itens pendentes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar itens pendentes: {ex.Message}");
                return true; // assume pendente em caso de erro
            }
        }
        public void AtualizarStatus(int numeroRequisicao, string status)
        {
            var conn = conexao.Abrir();
            try
            {
                string sql = "UPDATE requisicao SET Status = @Status WHERE NumeroRequisicao = @NumeroRequisicao";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@NumeroRequisicao", numeroRequisicao);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar status da requisição: {ex.Message}");
            }
        }
        public List<ItemRequisicao> ListarItensDevolutiveis(int numeroRequisicao)
        {
            var conn = conexao.Abrir();
            List<ItemRequisicao> itens = new List<ItemRequisicao>();

            try
            {
                string sql = @"SELECT ir.MaterialCodigo, ir.MaterialDescricao, ir.Quantidade
                               FROM itemrequisicao ir
                               JOIN requisicao r ON ir.RequisicaoId = r.Id
                               WHERE r.NumeroRequisicao = @NumeroRequisicao
                                 AND ir.TipoMaterial = 2";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NumeroRequisicao", numeroRequisicao);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemRequisicao item = new ItemRequisicao
                        {
                            MaterialId = reader.GetInt32("MaterialCodigo"),
                            MaterialDescricao = reader.GetString("MaterialDescricao"),
                            Quantidade = reader.GetInt32("Quantidade"),
                            TipoMaterial = 2
                        };
                        itens.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar itens devolvíveis: {ex.Message}");
            }

            return itens;
        }
        public int QuantidadeJaDevolvida(int numeroRequisicao, int materialCodigo)
        {
            var conn = conexao.Abrir();
            int quantidade = 0;

            try
            {
                string sql = @"SELECT IFNULL(SUM(idv.Quantidade),0) AS TotalDevolvido
                               FROM itemdevolucao idv
                               JOIN devolucao d ON idv.DevolucaoId = d.Id
                               WHERE d.NumeroRequisicao = @NumeroRequisicao
                                 AND idv.MaterialCodigo = @MaterialCodigo";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NumeroRequisicao", numeroRequisicao);
                cmd.Parameters.AddWithValue("@MaterialCodigo", materialCodigo);

                quantidade = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar quantidade já devolvida: {ex.Message}");
            }

            return quantidade;
        }
    }
}
