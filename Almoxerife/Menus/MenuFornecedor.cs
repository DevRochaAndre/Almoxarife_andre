using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class MenuFornecedor
    {
        public void Exibir()
        {


            string opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU DE CADASTROS ===");
                Console.WriteLine("1 - Cadastrar Fornecedor");
                Console.WriteLine("2 - Mostrar Fornecedores cadastrados");
                Console.WriteLine("3 - Alterar Cadastro de Fornecedores");
                Console.WriteLine("4 - Deletar Fornecedores");
                Console.WriteLine("B - Voltar");
                Console.Write("Selecione uma opção: ");

                opcao = Console.ReadLine().ToUpper();

                FornecedorDAO dao = new FornecedorDAO();


                switch (opcao)
                {
                    case "1":

                        Fornecedor f = new Fornecedor();
                        f.CadastrarFornecedor();
                        dao.Inserir(f);
                        Console.WriteLine("\nFornecedor cadastrado com sucesso!");
                        Console.ReadKey();

                        break;


                    case "2":

                        Console.WriteLine("Lista de Fornecedores Cadastrados");

                        dao.Mostrar();
                        Console.ReadKey();

                        break;

                    case "3":

                        Console.WriteLine("Alterar Dados Cadatrais de Fornecedores:");

                        dao.AlterarCadastro();
                        Console.ReadKey();

                        break;

                    case "4":

                        Console.WriteLine("Deletar Fornecedores");

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
    

