using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{
    public class Arbol
    {
        public Nodo Raiz { get; set; }
        public Arbol()
        {
            Raiz = null;
        }
        public void Insertar(Gaseosas value)
        {
            if (Raiz == null)
            {
                Raiz = new Nodo(value);
                return;
            }
            Nodo actual = Raiz;
            Nodo padre = null;
            while (actual != null)
            {
                if (actual.Valores.Count == 3)
                {
                    if (padre == null)
                    {
                        Gaseosas k = actual.Pop(1);
                        Nodo nuevaRaiz = new Nodo(k);
                        Nodo[] newNodos = actual.Split();
                        nuevaRaiz.InsertarHijo(newNodos[0]);
                        nuevaRaiz.InsertarHijo(newNodos[1]);
                        Raiz = nuevaRaiz;
                        actual = nuevaRaiz;
                    }
                    else
                    {
                        Gaseosas k = actual.Pop(1);
                        if (k != null)
                        {
                            padre.Push(k);
                        }
                        Nodo[] nNodos = actual.Split();
                        int pos1 = padre.EncontrarPosicionHijo(nNodos[1].Valores[0].Nombre);
                        padre.InsertarHijo(nNodos[1]);

                        int posActual = padre.EncontrarPosicionHijo(value.Nombre);
                        actual = padre.GetHijo(posActual);

                    }
                }
                padre = actual;
                actual = actual.Traverse(value.Nombre);
                if (actual == null)
                {
                    padre.Push(value);
                }
            }
        }
        public Nodo Find(string k)
        {
            Nodo curr = Raiz;

            while (curr != null)
            {
                if (curr.HasKey(k) >= 0)
                {
                    return curr;
                }
                else
                {
                    int p = curr.EncontrarPosicionHijo(k);
                    curr = curr.GetHijo(p);
                }
            }

            return null;
        }

        public List<Gaseosas> Inorder()
        {
            Nodo n = Raiz;
            int a = 0;
            List<Gaseosas> items = new List<Gaseosas>();
            Tuple<Nodo, int> curr = new Tuple<Nodo, int>(n, a);
            Stack<Tuple<Nodo, int>> stack = new Stack<Tuple<Nodo, int>>();
            while (stack.Count > 0 || curr.Item1 != null)
            {
                if (curr.Item1 != null)
                {
                    stack.Push(curr);
                    Nodo leftChild = curr.Item1.GetHijo(curr.Item2);
                    curr = new Tuple<Nodo, int>(leftChild, a);
                }
                else
                {
                    curr = stack.Pop();
                    Nodo currNode = curr.Item1;

                    if (curr.Item2 < currNode.Valores.Count)
                    {
                        items.Add(currNode.Valores[0]);
                        curr = new Tuple<Nodo, int>(currNode, curr.Item2 + 1);
                    }
                    else
                    {
                        Nodo rightChild = currNode.GetHijo(curr.Item2 + 1);

                        curr = new Tuple<Nodo, int>(rightChild, curr.Item2 + 1);
                    }
                }
            }
            return items;
        }//Arreglo de todos los nombres de gaseosas.

    }
}

    

