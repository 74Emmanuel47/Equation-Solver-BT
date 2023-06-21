using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecuaciones.Class
{
    class Ecuacion
    {
        //Atributos públicos
        public String oldEcua;
        public Nodo arbol;
        public String finEcua;

        //Atributos privados
        private List<int> variables;
        private List<List<char>> diffW;
        private List<String> miniE;
        private List<Nodo> nodos;
        private char[] ec;

        public Ecuacion(String cad)  {
            oldEcua = finEcua = cad;
            variables = new List<int>();
            diffW = new List<List<char>>();
            nodos = new List<Nodo>();
            arbol = new Nodo();
        }


        override
        public String ToString()
        { return oldEcua + "     ->      " + finEcua; }

        public Nodo getNodoP() { return arbol;  }

        /// <summary>
        /// Método que se encarga de eliminar los espacios en blanco de la ecuación.
        /// </summary>
        public void deleteBS()  {
            List<char> aux = oldEcua.ToList<char>();

            for (int i = 0; i < aux.Count; i++)  {
                if (aux[i] == ' ')  {
                    aux.RemoveAt(i);
                }
            }

            finEcua = new String(aux.ToArray());
        }


        /// <summary>
        /// Este método se encarga de hacer que las letras sean todas minisculas.
        /// </summary>
        public void changeL()  {
            List<char> auxE = finEcua.ToList();

            for (int i = 0; i < auxE.Count; i++)  {
                if ((auxE[i] + 32) >= 97 && (auxE[i] + 32) <= 122)
                    auxE[i] = (char)(auxE[i] + 32);
            }

            finEcua = new string(auxE.ToArray());
            diffW.Add(finEcua.ToList());
        }


        /// <summary>
        ///Método que se encarga de obtener los valores númericos de las variables
        ///introducidas en la ecuación. 
        /// </summary>
        public void setNumVariables()  {
            int cont = 0;

            foreach (char c in finEcua)  {
                if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122))  {
                    cont++;
                    Console.Write("Ingresa el valor de la variable " + c + ":");
                    variables.Add(int.Parse(Console.ReadLine()));
                    Console.Out.Flush();
                }
            }
        }


        /// <summary>
        /// Se encarga de leer la cantidad de parentésis que tiene la ecuación introducida por el usuario, con el fin de determinar
        /// si la ecuación está bien escrita o requiere formato.
        /// </summary>
        /// <returns>Regresa 0, en caso de que todo este bien equilibrado.
        /// Retorna 1 si hay mas operaciones que parentésis, por lo que es necesario agregar los operarios necesarios.
        /// Retorna 2 si hay más parentésis que operaciones, por lo que es necesario reingresar la ecuación o purgarla.
        /// </returns>
        public int contP()  {
            int i, j, k, response;
            i = j = k = response = 1;

            foreach (char c in finEcua)  {
                if (c == '(')
                    i++;
                if (c >= 42 && c <= 47)
                    j++;
                if (c == ')')
                    k++;
            }

            if (i == j && k == i)
                response = 0;
            else if (j > i && k == i)
                response = 1;
            else if (j < i && k == i)
                response = 2;

            return response;
        }

        /// <summary>
        /// Este método se encarga de recorrer la ecuación en busqueda de valores que se encuentren entre parentésis, para,
        /// posteriormente volverlos una sola variable. 
        /// </summary>
        /// <example>
        /// a*b - (c/d) + e     se convierte en        a*b - M + e
        /// </example>
        public void purgarP()
        {
            char[] operadores = { '/', '*', '+', '-' };
            int cO = 0;
            foreach (char c in operadores)
            {
                String aux = preProcess(c, cO);
                if (!finEcua.Equals(aux))
                {
                    finEcua = aux;
                    diffW.Add(aux.ToList());
                    cO++;
                }
            }
        }

        /// <summary>
        /// Recorre la ecuación buscando las operaciones para poder agruparlas en en suboperaciones.
        /// </summary>
        /// <example>
        /// a*b - c/d + e     ->         a*b - (c/d) + e     ->        a*b - M + e
        /// </example>
        public void addP()  {
            int k, pA, pC, cO;
            k = pA = pC = cO = 0;
            List<char> aux = finEcua.ToList();
            char[] operadores = { '/', '*', '+', '-' };

            while (contP() != 0)  {
                for (int i = 0; i < aux.Count; i++)  {
                    if ((aux[i] >= 65 && aux[i] <= 90) || (aux[i] >= 97 && aux[i] <= 122))  {

                        if (i < aux.Count - 2)  {
                            if (aux[i + 1] == operadores[k] && pA == 0)  {
                                aux.Insert(i, '(');
                                i++; pA++;
                            }
                        }

                        if (i > 0)  {
                            if (aux[i - 1] == operadores[k] && pC == 0)  {
                                aux.Insert(i + 1, ')');
                                i++; pC++;
                            }
                        }

                        if ((pC + pA) == 2)  {
                            diffW.Add(aux);
                            finEcua = new string(aux.ToArray());
                            aux = preProcess(operadores[k], cO).ToList();
                            pC = pA = 0; i = -1; cO++;
                        }
                    }
                }
                k++;
            }
        }


        /// <summary>
        /// Este método se encarga de reducir las operaciones que se encuentran encapsuladas.
        /// </summary>
        /// <param name="x">Es el operador que se va a utilizar como referencia para reducir las operaciones.</param>
        /// <param name="l">Letra que se asignará a la operación encapsulada.</param>
        /// <returns>Devuelve la ecuación en forma de cadena de texto.</returns>
        private String preProcess(char x, int l)  {
            int i, j;
            List<char> aux = finEcua.ToList();

            for (i = 0; i < aux.Count; i++)  {
                if (aux[i] == ')' && aux[i - 2] == x)  {
                    for (j = i; j > 0 && aux[j] != '('; j--)
                        aux.RemoveAt(j);

                    aux.RemoveAt(j);
                    aux.Insert(j, (char)(l + 65));
                    i = 0;
                }
            }

            return new string(aux.ToArray());
        }

        /// <summary>
        /// Este método se encarga de encapsular las mini ecuaciones para, de esta forma, crear la ecuación final (con todo y parétesis).
        /// </summary>
        private void generateFE()  {
            for (int j = 0; j < miniE[miniE.Count - 1].Length; j++)  {
                if (miniE[miniE.Count - 1][j] >= 65 && miniE[miniE.Count - 1][j] <= 90)  {
                    String ecuaM = miniE[ec.ToList().IndexOf(miniE[miniE.Count - 1][j])];   //Busca la ecuación correspondiente a la letra.
                    miniE[miniE.Count - 1] = miniE[miniE.Count - 1].Replace(miniE[miniE.Count - 1][j].ToString(), ecuaM);   //Sustituye el valor de la ecuacion encontrada en la posición J.
                }
                finEcua = miniE.Last();
            }
        }

        private void generateE()  {
            miniE = findME();
            ec = new char[miniE.Count];

            for (int i = 0; i < miniE.Count; i++)
                ec[i] = (char)(65 + i);
        }


        /// <summary>
        /// Recorre las diferentes formas que ha tomado la ecuación, buscando las Mini Ecuaciones (ME) que conforman el sistema, las cuales se pueden utilizar para formar 
        /// el árbol binario para resolver las ecuaciones.
        /// </summary>
        /// <returns>Regresa una lista de cadenas que contienen las Mini Ecuaciones encontradas en cada una de las diferentes formas (ecuaciones).</returns>
        private List<String> findME()
        {
            String p1, p2, auxC;
            List<String> subP = new List<string>();

            for (int i = 1; i < diffW.Count; i++)  {
                auxC = new string(diffW[i].ToArray());
                if (auxC.Contains("("))  {
                    p1 = auxC.Split('(')[1];
                    p2 = p1.Split(')')[0];
                    subP.Add('(' + p2 + ')');
                }
            }

            return subP;
        }

        /// <summary>
        /// Este método se encarga de crear el árbol binario en base a las mini ecuaciones generadas.
        /// </summary>
        public void joinParts()
        {
            int k;
            Nodo padre;

            //Ciclo para generar una lista de nodos en base a las mini-ecuaciones del arreglo miniE.
            for (int i = 0; i < miniE.Count; i++)  {
                padre = new Nodo(miniE[i].ToArray()[2].ToString());
                padre.hi = new Nodo(miniE[i].ToArray()[1].ToString());
                padre.hd = new Nodo(miniE[i].ToArray()[3].ToString());

                nodos.Add(padre);
            }
            //Unión de los nodos, en base a las mini ecuaciones.
            for (int i = 0; i < miniE.Count; i++)  {
                for (int j = k = 0; j < miniE[i].Length; j++)  {
                    if (miniE[i][j] >= 65 && miniE[i][j] <= 90)  {
                        int z = ec.ToList().IndexOf(miniE[i][j]);

                        if (k == 0)  {
                            nodos[i].hi = nodos[z];
                            k = 1;
                        }  else if (k == 1)  {
                            nodos[i].hd = nodos[z];
                            k = 0;
                        }

                    }
                }
            }

            arbol = nodos.Last();

        }

        private void cleanLs()  {
            diffW.Clear();
            nodos.Clear();
            miniE.Clear();

            diffW = null;
            nodos = null;
            miniE = null;
        }


        /// <summary>
        /// Recorre una cadena de texto, buscando un operador (*, /, -, +).
        /// </summary>
        /// <param name="operacion">Cadena de texto que debe poseer un operador.</param>
        /// <returns>Regresa el operador encontrado en la cadena de texto.</returns>
        private char getOperador(String operacion)  {
            char x = ' ';

            foreach (char c in operacion)  {
                if (c >= 42 && c <= 47)  {
                    x = c;
                    break;
                }
            }

            return x;
        }
    }
}