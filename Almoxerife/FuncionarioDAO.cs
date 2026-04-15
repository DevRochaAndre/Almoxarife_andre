using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Almoxerife
{
    public class FuncionarioDAO
    {
        private Conexao conexao = new Conexao();

        public void Inserir(Funcionario f)
        {
            var conn = conexao.Abrir();

            string sql = "INSERT INTO funcionario (Nome, CPF, Matricula, Departamento, Cargo) VALUES (@Nome, @CPF, @Matricula, @Departamento, @Cargo)";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", f.Nome);
                cmd.Parameters.AddWithValue("@CPF", f.CPF);
                cmd.Parameters.AddWithValue("@Matricula", f.Matricula);
                cmd.Parameters.AddWithValue("@Departamento", f.Departamento);
                cmd.Parameters.AddWithValue("@Cargo", f.Cargo);
                int linhas = cmd.ExecuteNonQuery();
                Console.WriteLine($"{linhas} Funcionario(s) cadastrado(s).");

            }


        }
        public void Mostrar()
        {
            var conn = conexao.Abrir();
            string sql = "SELECT Id, Nome, CPF, Matricula, Departamento, Cargo FROM funcionario";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string nome = reader.GetString("Nome");
                    string CPF = reader.GetString("CPF");
                    int Matricula = Convert.ToInt32(reader["Matricula"]);
                    string departamento = reader.GetString("Departamento");
                    string Cargo = reader.GetString("Cargo");

                    Console.WriteLine($"ID: {id} Nome: {nome} CPF: {CPF} Matricula : {Matricula} Departamento : {departamento} Cargo : {Cargo}");


                }
        }

        public void AlterarCadastro()
        {
            Console.WriteLine("Digite o ID do Paciente para Alterar os Dados:");
            int idParaAtualizar = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o novo Departamento:");
            string NovoDepartamento = Console.ReadLine();

            Console.WriteLine("Digite a nova Cargo:");
            string NovoCargo = Console.ReadLine();

            try
            {
                var conn = conexao.Abrir();

                string sql = "UPDATE funcionario SET Departamento=@departamento , Cargo=@cargo WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@departamento", NovoDepartamento);
                cmd.Parameters.AddWithValue("@cargo", NovoCargo);
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
            Console.WriteLine("Digite o ID funcionario que deseja excluir:");
            int idParaExcluir = int.Parse(Console.ReadLine());

            try
            {
                var conn = conexao.Abrir();
                string sql = "DELETE FROM funcionario WHERE Id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", idParaExcluir);
                int removidos = cmd.ExecuteNonQuery();
                Console.WriteLine($"{removidos} registro(s) excluído(s).");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Ocorreu um erro ao deletar o funcionario!");
                Console.WriteLine($"Detalhes: {erro.Message}");
                Console.ReadKey();
            }
        }
        public Funcionario BuscarPorMatricula(string matricula)
        {
            var conn = conexao.Abrir();
            if (conn == null) return null;

            try
            {
                string sql = "SELECT Id, Nome, Matricula, CPF, Departamento, Cargo FROM funcionario WHERE Matricula = @Matricula";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Matricula", matricula);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Funcionario f = new Funcionario();
                        f.Id = Convert.ToInt32(reader["Id"]);
                        f.Nome = reader["Nome"].ToString();
                        f.Matricula = Convert.ToInt32(reader["Matricula"]);
                        f.CPF = reader["CPF"].ToString();
                        f.Departamento = reader["Departamento"].ToString();
                        f.Cargo = reader["Cargo"].ToString();
                        return f;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar funcionário: {ex.Message}");
            }

            return null; 
        }
    }
}
    
