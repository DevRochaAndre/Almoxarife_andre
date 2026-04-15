using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class MenuRequisicao
    {
        public void AbrirMenu()
        {
            Console.Clear();
            Requisicao req = new Requisicao();
            FuncionarioDAO fdao = new FuncionarioDAO();
            fdao.Mostrar();

            Console.Write("Matrícula do funcionário: ");
            req.MatriculaFuncionario = Console.ReadLine();


            Console.Write("Número da Requisição: ");
            req.NumeroRequisicao = int.Parse(Console.ReadLine());


            req.DataSaida = DateTime.Today;

           
            var func = fdao.BuscarPorMatricula(req.MatriculaFuncionario);
            if (func != null) { 
                req.FuncionarioId = func.Id;

            Console.WriteLine("\n--- Funcionário Encontrado ---");
            Console.WriteLine($"Nome: {func.Nome}");
            Console.WriteLine($"Departamento: {func.Departamento}");
            Console.WriteLine($"Cargo: {func.Cargo}");
        
            }else
            {
                Console.WriteLine("Funcionário não encontrado!");
                Console.ReadLine();
                return;
            }

            req.Status = "Atendida";

            
            bool adicionarMais = true;
            MaterialDAO mdao = new MaterialDAO();
            while (adicionarMais)
            {
                ItemRequisicao item = new ItemRequisicao();

                Console.WriteLine("\n--- Adicionar Item ---");
                Console.Write("Código do material: ");
                int codigo = int.Parse(Console.ReadLine());

                var mat = mdao.BuscarPorCodigo(codigo);

                if (mat == null)
                {
                    Console.WriteLine("Material não encontrado!");
                    continue;
                }

                
                item.MaterialId = mat.Codigo;
                item.MaterialDescricao = mat.Descricao;
                item.TipoMaterial = mat.TipoProduto;

              
                Console.WriteLine($"Descrição: {mat.Descricao}");
                Console.WriteLine($"Tipo: {(mat.TipoProduto == 1 ? "Consumível" : "Devolvível")}");

                Console.Write("Quantidade: ");
                item.Quantidade = int.Parse(Console.ReadLine());

               
                req.AdicionarItem(item);

                Console.Write("Adicionar mais itens? (s/n): ");
                adicionarMais = Console.ReadLine().ToLower() == "s";
            }

           
            RequisicaoDAO rdao = new RequisicaoDAO();
            rdao.Inserir(req);

            Console.WriteLine("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }
    }
}

