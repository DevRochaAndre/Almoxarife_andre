using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class ListarNotasMenu
    {
        public static void ListarNotas()
        {
            NotaFiscalDAO nfdao = new NotaFiscalDAO();
            var notas = nfdao.ListarNotas();

            foreach (var nf in notas)
            {
                Console.WriteLine($"\nNota Fiscal: {nf.NumeroNF} - Data: {nf.DataEntrada.ToShortDateString()} - Fornecedor: {nf.fornecedor.RazaoSocial}");
                Console.WriteLine("Itens:");
                foreach (var item in nf.Itens)
                {
                    Console.WriteLine($"  Código: {item.Material.Codigo} | Descrição: {item.Material.Descricao} | Quantidade: {item.Quantidade} | Data Entrada: {item.DataEntrada.ToShortDateString()}");
                }
            }

            Console.WriteLine("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }
    }
}
