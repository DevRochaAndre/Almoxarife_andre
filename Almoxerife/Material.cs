using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class Material
    {
        public int Id;
        public int Codigo;
        public string Descricao;
        public string Categoria;
        public string Marca;
        public string Modelo;
        
        public DateTime? Validade { get; set; }
        public int TipoProduto;



    }
}

