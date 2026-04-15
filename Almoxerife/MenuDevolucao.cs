using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class MenuDevolucao
    {
        public void AbrirMenu()
        {
            Console.Clear();
            Console.Write("Número da Requisição: ");
            int numeroReq = int.Parse(Console.ReadLine());

            RequisicaoDAO rdao = new RequisicaoDAO();
            var itensRequisicao = rdao.ListarItensDevolutiveis(numeroReq); // retorna lista de ItemRequisicao tipo 2

            if (itensRequisicao.Count == 0)
            {
                Console.WriteLine("Nenhum item devolvível encontrado para esta requisição.");
                Console.ReadLine();
                return;
            }

            Devolucao dev = new Devolucao();
            dev.NumeroRequisicao = numeroReq;
            dev.DataDevolucao = DateTime.Today;

            bool devolverMais = true;

            while (devolverMais && itensRequisicao.Count > 0)
            {
                Console.WriteLine("\nItens disponíveis para devolução:");
                for (int i = 0; i < itensRequisicao.Count; i++)
                {
                    var item = itensRequisicao[i];
                    int quantidadeDevolvida = rdao.QuantidadeJaDevolvida(numeroReq, item.MaterialId);
                    int quantidadeDisponivel = item.Quantidade - quantidadeDevolvida;

                    Console.WriteLine($"{i + 1} - Código: {item.MaterialId}, Descrição: {item.MaterialDescricao}, Disponível: {quantidadeDisponivel}");
                }

                Console.Write("\nEscolha o item a devolver (número): ");
                int escolha = int.Parse(Console.ReadLine()) - 1;

                if (escolha < 0 || escolha >= itensRequisicao.Count)
                {
                    Console.WriteLine("Escolha inválida.");
                    continue;
                }

                var itemSelecionado = itensRequisicao[escolha];
                int jaDevolvido = rdao.QuantidadeJaDevolvida(numeroReq, itemSelecionado.MaterialId);
                int disponivel = itemSelecionado.Quantidade - jaDevolvido;

                Console.Write($"Quantidade a devolver (máx {disponivel}): ");
                int qtd = int.Parse(Console.ReadLine());

                if (qtd <= 0 || qtd > disponivel)
                {
                    Console.WriteLine("Quantidade inválida.");
                    continue;
                }

                ItemDevolucao novoItem = new ItemDevolucao
                {
                    MaterialCodigo = itemSelecionado.MaterialId,
                    MaterialDescricao = itemSelecionado.MaterialDescricao,
                    Quantidade = qtd,
                    DataDevolucao = DateTime.Today
                };

                dev.AdicionarItem(novoItem);

                Console.Write("Deseja devolver mais itens? (s/n): ");
                string resp = Console.ReadLine().ToLower();
                devolverMais = resp == "s";

                // Remove item se foi totalmente devolvido
                if (qtd == disponivel)
                    itensRequisicao.RemoveAt(escolha);
            }

            if (dev.Itens.Count > 0)
            {
                DevolucaoDAO ddao = new DevolucaoDAO();
                ddao.Inserir(dev);
            }

            Console.WriteLine("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }
    }
}

