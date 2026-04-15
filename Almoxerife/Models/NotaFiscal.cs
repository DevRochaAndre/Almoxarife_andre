using Almoxerife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class NotaFiscal
    {
        public int Id;
        public int NumeroNF;
        public DateTime DataEntrada;
        public Fornecedor fornecedor;
        public List<ItemNF> Itens = new List<ItemNF>();

        public void RegistrarNota()
        {
            Console.WriteLine("Número da Nota Fiscal:");
            NumeroNF = int.Parse(Console.ReadLine());

            Console.WriteLine("Data de Entrada (Ex: 10/11/2025):");
            DataEntrada = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("ID do Fornecedor:");
            int idFornecedor = int.Parse(Console.ReadLine());

            FornecedorDAO fdao = new FornecedorDAO();
            fornecedor = fdao.BuscarPorId(idFornecedor);

            MaterialDAO mdao = new MaterialDAO();
            NotaFiscalDAO nfdao = new NotaFiscalDAO();

            nfdao.Inserir(this);

            bool adicionarMais = true;
            

            while (adicionarMais)
            {
                Console.WriteLine("\n--- Cadastro de Item da Nota ---");

                Console.WriteLine("Código do Material (numérico):");
                int codigoMaterial = int.Parse(Console.ReadLine());

                
                Material material = mdao.BuscarPorCodigo(codigoMaterial);

                if (material == null)
                {
                    
                    material = new Material();
                    material.Codigo = codigoMaterial;

                    Console.WriteLine("Descrição:");
                    material.Descricao = Console.ReadLine();

                    Console.WriteLine("Categoria:");
                    material.Categoria = Console.ReadLine();

                    Console.WriteLine("Marca:");
                    material.Marca = Console.ReadLine();

                    Console.WriteLine("Modelo:");
                    material.Modelo = Console.ReadLine();

                    Console.WriteLine("Validade (Enter se não tiver):");
                    string dataVal = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(dataVal))
                        material.Validade = null;
                    else
                        material.Validade = DateTime.Parse(dataVal);

                    Console.WriteLine("Tipo do Produto (1 - Consumível / 2 - Retornável):");
                    material.TipoProduto = int.Parse(Console.ReadLine());

                    mdao.Inserir(material); 
                }

                Console.WriteLine("Quantidade Recebida:");
                int quantidade = int.Parse(Console.ReadLine());

                
                ItemNF item = new ItemNF();
                item.Material = material;

                Itens.Add(item);

                
                mdao.AtualizarEstoque(material.Codigo, material.Descricao, item.Quantidade);

                nfdao.InserirItemNF(this.Id, material.Codigo, material.Descricao, quantidade,DataEntrada);

                Console.WriteLine("Adicionar mais itens? (s/n)");
                adicionarMais = Console.ReadLine().ToLower() == "s";
            }


            Console.WriteLine("\nNota cadastrada com sucesso!");
        }
    }
}
