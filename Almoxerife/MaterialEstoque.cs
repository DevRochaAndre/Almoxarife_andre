using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class MaterialEstoque
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int TipoProduto { get; set; }

        public int QuantidadeEstoque { get; set; }
        public int QuantidadeEmpenhada { get; set; }
    }
}
