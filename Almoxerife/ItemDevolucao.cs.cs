using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    internal class ItemDevolucao
    {
        public int Id;
        public int DevolucaoId;
        public int MaterialCodigo;
        public string MaterialDescricao;
        public int Quantidade;
        public DateTime DataDevolucao;

        public ItemDevolucao() { }
    }
}
