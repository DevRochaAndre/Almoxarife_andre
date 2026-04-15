using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class NotaFiscalDAO
    {
        private Conexao conexao = new Conexao();

        public void Inserir(NotaFiscal nf)
        {
            var conn = conexao.Abrir();

            MaterialDAO mdao = new MaterialDAO();
            NotaFiscalDAO nfdao = new NotaFiscalDAO();

            try
            {

                string sql = "INSERT INTO notafiscal (NumeroNF, DataEntrada, FornecedorId) VALUES (@NumeroNF, @DataEntrada, @FornecedorId)";


                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@NumeroNF", nf.NumeroNF);
                cmd.Parameters.AddWithValue("@DataEntrada", nf.DataEntrada);
                cmd.Parameters.AddWithValue("@FornecedorId", nf.fornecedor.Id);
                

                cmd.ExecuteNonQuery();

                nf.Id = (int)cmd.LastInsertedId;

                

                foreach (var item in nf.Itens)
                {
                    mdao.AtualizarEstoque(item.Material.Codigo, item.Material.Descricao, item.Quantidade);

                    nfdao.InserirItemNF(nf.Id, item.Material.Codigo, item.Material.Descricao, item.Quantidade, nf.DataEntrada);


                }
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro ao inserir nota fiscal: {erro.Message}");
            }

        }
        public List<NotaFiscal> ListarNotas()
        {
            var conn = conexao.Abrir();
            List<NotaFiscal> notas = new List<NotaFiscal>();

            try
            {
                
                string sqlNotas = @"SELECT nf.Id AS NotaId, nf.NumeroNF, nf.DataEntrada, f.Id AS FornecedorId, f.RazaoSocial FROM notafiscal nf JOIN fornecedor f ON f.Id = nf.FornecedorId";
                MySqlCommand cmd = new MySqlCommand(sqlNotas, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NotaFiscal nf = new NotaFiscal();
                        nf.Id = Convert.ToInt32(reader["NotaId"]);
                        nf.NumeroNF = Convert.ToInt32(reader["NumeroNF"]);
                        nf.DataEntrada = Convert.ToDateTime(reader["DataEntrada"]);

                        nf.fornecedor = new Fornecedor
                        {
                            Id = Convert.ToInt32(reader["FornecedorId"]),
                            RazaoSocial = reader["RazaoSocial"].ToString()
                        };

                        nf.Itens = new List<ItemNF>();

                        notas.Add(nf);
                    }
                }

                
                foreach (var nf in notas)
                {
                    string sqlItens = @"SELECT MaterialCodigo, Descricao, Quantidade, DataEntrada FROM itemnf WHERE NotaFiscalId = @NFId";

                    MySqlCommand cmdItens = new MySqlCommand(sqlItens, conn);
                    cmdItens.Parameters.AddWithValue("@NFId", nf.Id);

                    using (var readerItens = cmdItens.ExecuteReader())
                    {
                        while (readerItens.Read())
                        {
                            ItemNF item = new ItemNF();
                            

                            item.Material = new Material()
                            {
                                Codigo = Convert.ToInt32(readerItens["MaterialCodigo"]),
                                Descricao = readerItens["Descricao"].ToString()
                            };

                            item.Quantidade = Convert.ToInt32(readerItens["Quantidade"]);
                            item.DataEntrada = Convert.ToDateTime(readerItens["DataEntrada"]);


                            nf.Itens.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar notas: {ex.Message}");
                Console.WriteLine($"Detalhes: {ex.Message}");
                Console.ReadKey();
            }

            return notas;
        }
        public void InserirItemNF(int notaId, int codigoMaterial, string descricao, int quantidade, DateTime dataEntrada)
        {
            var conn = conexao.Abrir();
            string sql = "INSERT INTO itemnf (NotaFiscalId, MaterialCodigo, Quantidade, Descricao, DataEntrada) " +
                         "VALUES (@NotaId, @Codigo, @Quantidade, @Descricao, @DataEntrada)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NotaId", notaId);
            cmd.Parameters.AddWithValue("@Codigo", codigoMaterial);
            cmd.Parameters.AddWithValue("@Quantidade", quantidade);
            cmd.Parameters.AddWithValue("@Descricao", descricao);
            cmd.Parameters.AddWithValue("@DataEntrada", dataEntrada);
            cmd.ExecuteNonQuery();
        }

    }
}
