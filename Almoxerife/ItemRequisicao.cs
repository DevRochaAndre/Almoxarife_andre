using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     class ItemRequisicao
    {
        public int Id;
        public int RequisicaoId;
        public int MaterialId;
        public string MaterialDescricao;
        public int Quantidade;
        public int TipoMaterial;

        public ItemRequisicao() { }

        public ItemRequisicao(int materialId, string descricao, int quantidade, int tipoMaterial)
        {
            MaterialId = materialId;
            MaterialDescricao = descricao;
            Quantidade = quantidade;
            TipoMaterial = tipoMaterial;
        }
    }


}




