using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class MenuCadastro
    {
        public void Exibir()
        {
          

            string opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU DE CADASTROS ===");
                Console.WriteLine("1 - Cadastrar Funcionário");
                Console.WriteLine("2 - Mostrar Funcionarios cadastrados");
                Console.WriteLine("3 - Alterar Cadastro de Funcionario");
                Console.WriteLine("4 - Deletar Funcionario");
                Console.WriteLine("B - Voltar");
                Console.Write("Selecione uma opção: ");

                opcao = Console.ReadLine().ToUpper();

                FuncionarioDAO dao = new FuncionarioDAO();


                switch (opcao)
                {
                    case "1":
                        
                         Funcionario f = new Funcionario();
                         f.CadastroFuncionario();
                         dao.Inserir(f);
                         Console.WriteLine("\nFuncionário cadastrado com sucesso!");
                         Console.ReadKey();

                         break;
                        

                    case "2":
                        
                        Console.WriteLine("Lista de Funcionario Cadastrados");

                        dao.Mostrar();
                        Console.ReadKey();

                        break;
                        
                    case "3":

                        Console.WriteLine("Alterar Dados Cadatrais:");

                        dao.AlterarCadastro();
                        Console.ReadKey();

                        break;

                    case "4":

                        Console.WriteLine("Deletar Funcionario");

                        dao.DeletarCadastro();
                        Console.ReadKey();

                        break;

                    case "B":

                        Console.WriteLine("saindo do programa");

                        break;

                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }

                if (opcao != "B")
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != "B");
        }
    }
}

