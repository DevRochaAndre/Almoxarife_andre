using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class Requisicao
    {
        public int Id;
        public int NumeroRequisicao;
        public DateTime DataSaida;
        public int FuncionarioId;
        public string MatriculaFuncionario;
        public string Status;

        public List<ItemRequisicao> Itens = new List<ItemRequisicao>();

        public Requisicao() { }

        public void AdicionarItem(ItemRequisicao item)
        {
            Itens.Add(item);
        }
    }
}
