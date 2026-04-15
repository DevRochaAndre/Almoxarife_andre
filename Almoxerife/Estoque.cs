using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class Estoque
    {
        public int CodigoMaterial;     
        public string Descricao;       
        public int Quantidade;         
        public int Empenhado;          

        public int Disponivel
        {
            get { return Quantidade - Empenhado; }
        }
    }

}

