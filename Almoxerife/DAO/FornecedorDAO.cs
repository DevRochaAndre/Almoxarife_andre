using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class FornecedorDAO
    {
        private Conexao conexao = new Conexao();

        public void Inserir(Fornecedor f)
        {
            var conn = conexao.Abrir();

            string sql = "INSERT INTO fornecedor (RazaoSocial, Fantasia, CNPJ, Contato, Logradouro) VALUES (@RazaoSocial, @Fantasia, @CNPJ, @Contato, @Logradouro)";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RazaoSocial", f.RazaoSocial);
                cmd.Parameters.AddWithValue("@Fantasia", f.Fantasia);
                cmd.Parameters.AddWithValue("@CNPJ", f.CNPJ);
                cmd.Parameters.AddWithValue("@Contato", f.Contato);
                cmd.Parameters.AddWithValue("@Logradouro", f.Logradouro);
                int linhas = cmd.ExecuteNonQuery();
                Console.WriteLine($"{linhas} Forncedores cadastrados");

            }


        }
        public void Mostrar()
        {
            var conn = conexao.Abrir();
            string sql = "SELECT Id, RazaoSocial, Fantasia, CNPJ, Contato, Logradouro FROM fornecedor";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string razaosocial = reader.GetString("RazaoSocial");
                    string fantasia = reader.GetString("Fantasia");
                    string CNPJ = reader.GetString("CNPJ");
                    string contato = reader.GetString("contato");
                    string logradouro = reader.GetString("logradouro");

                    Console.WriteLine($"ID: {id} RazaoSocial: {razaosocial} Fantasia: {fantasia} CNPJ : {CNPJ} Contato : {contato} Logradouro : {logradouro}");


                }
        }

        public void AlterarCadastro()
        {
            Console.WriteLine("Digite o ID do Paciente para Alterar os Dados:");
            int idParaAtualizar = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o novo Contato:");
            string NovoContato = Console.ReadLine();

            Console.WriteLine("Digite a novo Logradouro:");
            string NovoLogradouro = Console.ReadLine();

            try
            {
                var conn = conexao.Abrir();

                string sql = "UPDATE fornecedor SET Contato=@contato , Logradouro=@logradouro WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@logradouro", NovoContato);
                cmd.Parameters.AddWithValue("@contato", NovoLogradouro);
                cmd.Parameters.AddWithValue("@id", idParaAtualizar);
                int alterados = cmd.ExecuteNonQuery();
                Console.WriteLine($"{alterados} registro(s) atualizado(s).");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao atualizar o aluno!");
                Console.WriteLine($"Detalhes: {erro.Message}");
                Console.ReadKey();
            }
        }
        public void DeletarCadastro()
        {
            Console.WriteLine("Digite o ID do fornecedor que deseja excluir:");
            int idParaExcluir = int.Parse(Console.ReadLine());

            try
            {
                var conn = conexao.Abrir();
                string sql = "DELETE FROM fornecedor WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", idParaExcluir);
                int removidos = cmd.ExecuteNonQuery();
                Console.WriteLine($"{removidos} registro(s) excluído(s).");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao deletar o forncedor!");
                Console.WriteLine($"Detalhes: {erro.Message}");
                Console.ReadKey();
            }
        }
        public Fornecedor BuscarPorId(int id)
        {
            var conn = conexao.Abrir();
            string sql = "SELECT Id, RazaoSocial, CNPJ FROM fornecedor WHERE Id = @Id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    Fornecedor f = new Fornecedor();
                    f.Id = Convert.ToInt32(reader["Id"]);
                    f.RazaoSocial = reader["RazaoSocial"].ToString();
                    f.CNPJ = reader["CNPJ"].ToString();
                    return f;


                }

                return null;
            }

        }
    }
}
    

