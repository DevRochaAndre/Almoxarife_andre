using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class Devolucao
    {
        public int Id;
        public int NumeroRequisicao;              
        public DateTime DataDevolucao;

        public List<ItemDevolucao> Itens = new List<ItemDevolucao>();

        public Devolucao() { }

        public void AdicionarItem(ItemDevolucao item)
        {
            Itens.Add(item);
        }
    }
}
