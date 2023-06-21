using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecuaciones.Class
{
    class Nodo
    {
        public string element { get; set; }
        public Nodo hd { get; set; }
        public Nodo hi { get; set; }

        public Nodo() { }

        public Nodo(String x){
            element = x;
            hi = null;
            hd = null;
        }

        //Método que imprime el árbol InOrden
        public void inorder(Nodo root)
        {
            if (root != null)
            {
                inorder(root.hi);
                Console.Write(root.element + " ");
                inorder(root.hd);
            }
        }

        //Método que imprime el árbol en PreOrden
        public void preorder(Nodo root)
        {
            if(root != null){
                Console.Write(root.element + " ");
                preorder(root.hi);  //Se mueve a lado izquierdo 
                preorder(root.hd);  //Se mueve a lado derecho
            }
        }

    }
}
