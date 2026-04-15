using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
    class Fornecedor
    {
        public int Id;
        public string RazaoSocial;
        public string Fantasia;
        public string CNPJ;
        public string Contato;
        public string Logradouro;
      

        public void CadastrarFornecedor()
        {
            Console.WriteLine("Razão Social:");
            RazaoSocial = Console.ReadLine();

            Console.WriteLine("Nome Fantasia:");
            Fantasia = Console.ReadLine();

            Console.WriteLine("CNPJ:");
            CNPJ = Console.ReadLine();

            Console.WriteLine("Contato:");
            Contato = Console.ReadLine();

            Console.WriteLine("Endereço:");
            Logradouro = Console.ReadLine();

            Console.WriteLine("\n Fornecedor cadastrado com sucesso!\n");
        }
    }

}