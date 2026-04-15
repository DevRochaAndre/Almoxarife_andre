using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class MenuEstoque
    {
        private MaterialEstoqueDAO edao = new MaterialEstoqueDAO();
        private MaterialDAO mdao = new MaterialDAO();

        public void AbrirMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("--- MENU ESTOQUE ---");
                Console.WriteLine($"Total de materiais cadastrados: {edao.ListarTodosEstoque().Count}");
                Console.WriteLine("1 - Listar todos os materiais");
                Console.WriteLine("2 - Consultar material por código");
                Console.WriteLine("3 - Listar materiais por tipo");
                Console.WriteLine("4 - Alterar material");
                Console.WriteLine("5 - Deletar material"); 
                Console.WriteLine("6 - Voltar ao menu principal");
                Console.Write("Escolha uma opção: ");
                string opc = Console.ReadLine();

                switch (opc)
                {
                    case "1":
                        ListarTodos();
                        break;
                    case "2":
                        ConsultarPorCodigo();
                        break;
                    case "3":
                        ListarPorTipo();
                        break;
                    case "4":
                        AlterarMaterial();
                        break;
                    case "5":
                        DeletarMaterial();
                        break;
                    case "6":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ListarTodos()
        {
            try
            {
                var lista = edao.ListarTodosEstoque();
                Console.WriteLine("\n--- Estoque Atual ---\n");
                foreach (var item in lista)
                {
                    Console.WriteLine($"Código: {item.Codigo}, Descrição: {item.Descricao}, Tipo: {(item.TipoProduto == 1 ? "Consumível" : "Devolvível")}, " +
                                      $"Disponível: {item.QuantidadeEstoque}, Empenhado: {item.QuantidadeEmpenhada}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar estoque: {ex.Message}");
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }

        private void ConsultarPorCodigo()
        {
            Console.Write("\nInforme o código do material: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido!");
                Console.ReadLine();
                return;
            }

            try
            {
                var item = edao.BuscarPorCodigoEstoque(codigo);
                if (item != null)
                {
                    Console.WriteLine($"\nCódigo: {item.Codigo}, Descrição: {item.Descricao}, Tipo: {(item.TipoProduto == 1 ? "Consumível" : "Devolvível")}, " +
                                      $"Disponível: {item.QuantidadeEstoque}, Empenhado: {item.QuantidadeEmpenhada}");
                }
                else
                {
                    Console.WriteLine("Material não encontrado no estoque.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar material: {ex.Message}");
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }

        private void ListarPorTipo()
        {
            Console.Write("\nInforme o tipo do material (1=Consumível, 2=Devolvível): ");
            if (!int.TryParse(Console.ReadLine(), out int tipo) || (tipo != 1 && tipo != 2))
            {
                Console.WriteLine("Tipo inválido!");
                Console.ReadLine();
                return;
            }

            try
            {
                var lista = edao.ListarPorTipoEstoque(tipo);
                Console.WriteLine($"\n--- Estoque do Tipo {(tipo == 1 ? "Consumível" : "Devolvível")} ---\n");
                foreach (var item in lista)
                {
                    Console.WriteLine($"Código: {item.Codigo}, Descrição: {item.Descricao}, Disponível: {item.QuantidadeEstoque}, Empenhado: {item.QuantidadeEmpenhada}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar por tipo: {ex.Message}");
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        private void AlterarMaterial()
        {
            Console.Write("\nInforme o código do material a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido!");
                Console.ReadLine();
                return;
            }

            var item = mdao.BuscarPorCodigo(codigo);
            if (item == null)
            {
                Console.WriteLine("Material não encontrado!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nAlterando material: {item.Descricao}");
            Console.Write("Nova descrição (Enter para manter): ");
            string descricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descricao)) item.Descricao = descricao;

            Console.Write("Nova categoria (Enter para manter): ");
            string categoria = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoria)) item.Categoria = categoria;

            Console.Write("Nova marca (Enter para manter): ");
            string marca = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(marca)) item.Marca = marca;

            Console.Write("Novo modelo (Enter para manter): ");
            string modelo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(modelo)) item.Modelo = modelo;

            Console.Write("Nova validade (yyyy-mm-dd ou Enter para manter): ");
            string val = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(val) && DateTime.TryParse(val, out DateTime validade))
            {
                item.Validade = validade;
            }

            mdao.AtualizarMaterial(item);
            Console.WriteLine("Material atualizado com sucesso!");
            Console.ReadLine();
        }

        private void DeletarMaterial()
        {
            Console.Write("\nInforme o código do material a deletar: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido!");
                Console.ReadLine();
                return;
            }

            var item = mdao.BuscarPorCodigo(codigo);
            if (item == null)
            {
                Console.WriteLine("Material não encontrado!");
                Console.ReadLine();
                return;
            }

            if (mdao.VerificarVinculoMaterial(codigo))
            {
                Console.WriteLine("Não é possível deletar material com estoque ou requisições vinculadas.");
                Console.ReadLine();
                return;
            }

            Console.Write($"Confirma deletar o material {item.Descricao}? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                mdao.DeletarMaterial(codigo);
                Console.WriteLine("Material deletado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }

            Console.ReadLine();
        }

       
    }
}