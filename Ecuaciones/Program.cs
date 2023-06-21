using Ecuaciones.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace std
{
    class Program
    {

        static void Main(string[] args)
        {
            Program p = new Program();
            Ecuacion ec;
            Nodo padre= new Nodo();

            Console.Write("Ingresa la ecuación a evaluar:  ");
            ec = new Ecuacion(Console.ReadLine());
            ec.deleteBS();
            ec.changeL();
            if(ec.contP() != 0)  {
                ec.purgarP();
                ec.addP();
                ec.joinParts();
            }

            Console.WriteLine("La ecuación se ha re-acondicionado para poder ser comprendida. El resultado obtenido es el siguiente:");
            Console.WriteLine(ec.ToString());

            //padre = ec.getNodoP();
            padre.inorder(ec.getNodoP());
            Console.WriteLine();
            padre.preorder(ec.getNodoP());

        }

    }
}
