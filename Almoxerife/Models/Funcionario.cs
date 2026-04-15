using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoxerife
{
     public class Funcionario
    {
        public int Id;
        public string Nome;
        public string CPF;
        public int Matricula;
        public string Departamento;
        public string Cargo;

        public void CadastroFuncionario()
        {
            Console.WriteLine("Declare o nome");
            Nome = Console.ReadLine();
            Console.WriteLine("CPF");
            CPF = Console.ReadLine();
            Console.WriteLine("Insira matrícula");
            Matricula = int.Parse(Console.ReadLine());
            Console.WriteLine("Departamento");
            Departamento = Console.ReadLine();
            Console.WriteLine("Cargo");
            Cargo = Console.ReadLine();


            

        }
    }
}