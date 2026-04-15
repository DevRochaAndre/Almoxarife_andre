using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class MaterialEstoqueDAO
    {
        private Conexao conexao = new Conexao();
        public List<MaterialEstoque> ListarTodosEstoque()
        {
            var lista = new List<MaterialEstoque>();
            var conn = conexao.Abrir();

            string sql = @"SELECT m.Codigo, m.Descricao, m.TipoProduto,
                          e.QuantidadeEstoque, e.QuantidadeEmpenhada
                   FROM material m
                   JOIN estoque e ON m.Codigo = e.MaterialCodigo";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new MaterialEstoque
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Descricao = reader.GetString("Descricao"),
                        TipoProduto = reader.GetInt32("TipoProduto"),
                        QuantidadeEstoque = reader.GetInt32("QuantidadeEstoque"),
                        QuantidadeEmpenhada = reader.GetInt32("QuantidadeEmpenhada")
                    });
                }
            }
            return lista;
        }

        public List<MaterialEstoque> ListarPorTipoEstoque(int tipo)
        {
            var lista = new List<MaterialEstoque>();
            var conn = conexao.Abrir();

            string sql = @"SELECT m.Codigo, m.Descricao, m.TipoProduto,
                          e.QuantidadeEstoque, e.QuantidadeEmpenhada
                   FROM material m
                   JOIN estoque e ON m.Codigo = e.MaterialCodigo
                   WHERE m.TipoProduto = @Tipo";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Tipo", tipo);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new MaterialEstoque
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Descricao = reader.GetString("Descricao"),
                        TipoProduto = reader.GetInt32("TipoProduto"),
                        QuantidadeEstoque = reader.GetInt32("QuantidadeEstoque"),
                        QuantidadeEmpenhada = reader.GetInt32("QuantidadeEmpenhada")
                    });
                }
            }
            return lista;
        }

        public MaterialEstoque BuscarPorCodigoEstoque(int codigo)
        {
            var conn = conexao.Abrir();

            string sql = @"SELECT m.Codigo, m.Descricao, m.TipoProduto, 
                          e.QuantidadeEstoque, e.QuantidadeEmpenhada
                   FROM material m
                   JOIN estoque e ON m.Codigo = e.MaterialCodigo
                   WHERE m.Codigo = @Codigo";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Codigo", codigo);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new MaterialEstoque
                    {
                        Codigo = reader.GetInt32("Codigo"),
                        Descricao = reader.GetString("Descricao"),
                        TipoProduto = reader.GetInt32("TipoProduto"),
                        QuantidadeEstoque = reader.GetInt32("QuantidadeEstoque"),
                        QuantidadeEmpenhada = reader.GetInt32("QuantidadeEmpenhada")
                    };
                }
            }

            return null; 
        }
    }
}
