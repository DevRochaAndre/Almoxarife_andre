using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class DevolucaoDAO
    {
        private Conexao conexao = new Conexao();
        private MaterialDAO mdao = new MaterialDAO();
        private RequisicaoDAO rdao = new RequisicaoDAO();

        public void Inserir(Devolucao dev)
        {
            var conn = conexao.Abrir();

            try
            {
                // 1. Inserir devolução
                string sqlDev = "INSERT INTO devolucao (NumeroRequisicao, DataDevolucao) " +
                                "VALUES (@NumeroRequisicao, @DataDevolucao)";

                MySqlCommand cmdDev = new MySqlCommand(sqlDev, conn);
                cmdDev.Parameters.AddWithValue("@NumeroRequisicao", dev.NumeroRequisicao);
                cmdDev.Parameters.AddWithValue("@DataDevolucao", dev.DataDevolucao);
                cmdDev.ExecuteNonQuery();

                int devolucaoId = (int)cmdDev.LastInsertedId;
                dev.Id = devolucaoId;

                // 2. Inserir cada item da devolução
                foreach (var item in dev.Itens)
                {
                    string sqlItem = "INSERT INTO itemdevolucao (DevolucaoId, MaterialCodigo, MaterialDescricao, Quantidade, DataDevolucao) " +
                                     "VALUES (@DevolucaoId, @MaterialCodigo, @MaterialDescricao, @Quantidade, @DataDevolucao)";

                    MySqlCommand cmdItem = new MySqlCommand(sqlItem, conn);
                    cmdItem.Parameters.AddWithValue("@DevolucaoId", devolucaoId);
                    cmdItem.Parameters.AddWithValue("@MaterialCodigo", item.MaterialCodigo);
                    cmdItem.Parameters.AddWithValue("@MaterialDescricao", item.MaterialDescricao);
                    cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                    cmdItem.Parameters.AddWithValue("@DataDevolucao", item.DataDevolucao);
                    cmdItem.ExecuteNonQuery();

                    // 3. Atualizar estoque
                    mdao.DevolverEstoque(item.MaterialCodigo, item.Quantidade); // Retira do empenho e volta ao estoque
                }

                // 4. Atualizar status da requisição
                bool todosDevolvidos = rdao.VerificarItensPendentes(dev.NumeroRequisicao);
                // Se houver itens ainda pendentes, status = Pendente; senão Finalizada
                rdao.AtualizarStatus(dev.NumeroRequisicao, todosDevolvidos ? "Pendente" : "Finalizada");

                Console.WriteLine("\nDevolução cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar devolução: {ex.Message}");
            }
        }
    }
}
    

