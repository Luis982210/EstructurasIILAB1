using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{


	public class Nodo
	{
		public List<Nodo> Hijos { get; private set; }//Basicamente los hijos
		public List<Gaseosas> Valores { get; private set; }//Las llaves del arbol 
		public Nodo Padre { get; set; }


		public Nodo(Gaseosas gaseosa)
		{
			Valores = new List<Gaseosas>();
			Valores.Add(gaseosa);
			Hijos = new List<Nodo>();

		}


		public int HasKey(string k)
		{
			for (int i = 0; i < Valores.Count; i++)
			{
				if (Valores[i].Nombre.CompareTo(k) == 0)
				{
					return 1;
				}
			}
			return -1;
		}
		public void InsertarHijo(Nodo Hijo)
		{
			for (int x = 0; x < Hijos.Count; x++)
			{
				if (Hijos[x].Valores[0].Nombre.CompareTo(Hijo.Valores[0].Nombre) > 0)
				{
					Hijos.Insert(x, Hijo);
					return;
				}
			}


			Hijos.Add(Hijo);
			Hijo.Padre = this;
		}
		public bool EliminarHijo(Nodo n)
		{
			return Hijos.Remove(n);
		}
		public Nodo EliminarHijo(int position)
		{
			Nodo Hijo = null;
			if (Hijos.Count > position)
			{
				Hijo = Hijos[position];
				Hijo.Padre = null;
				Hijos.RemoveAt(position);
			}


			return Hijo;
		}
		public Nodo GetHijo(int position)
		{
			if (position < Hijos.Count)
			{
				return Hijos[position];
			}
			else
			{
				return null;
			}
		}
		public int EncontrarPosicionHijo(string k)
		{
			if (Valores.Count != 0)
			{
				string left = " ";
				for (int x = 0; x < Valores.Count; x++)
				{
					if (left.CompareTo(k) < 0 && k.CompareTo(Valores[x].Nombre) < 0)
					{
						return x;
					}
					else
					{
						left = Valores[x].Nombre;
					}
				}


				if (k.CompareTo(Valores[Valores.Count - 1].Nombre) > 0)
				{
					return Valores.Count;
				}
				else
				{
					return -1;
				}
			}
			else
			{
				return 0;
			}

		}
		public void Fuse(Nodo n1)
		{
			int totalValores = n1.Valores.Count;
			int totalHijos = n1.Hijos.Count;


			totalValores += this.Valores.Count;
			totalHijos += this.Hijos.Count;


			if (totalValores > 3)
			{
				throw new InvalidOperationException("Existen más de 3 valores");
			}




			if (totalHijos > 4)
			{
				throw new InvalidOperationException("Existen más de 4 valores");
			}




			for (int x = 0; x < n1.Valores.Count; x++)
			{
                Gaseosas k = n1.Valores[x];
				this.Push(k);
			}


			for (int x = Hijos.Count - 1; x >= 0; x--)
			{
				Nodo e = n1.EliminarHijo(x);
				this.InsertarHijo(e);
			}
		}


		public void Fuse(Nodo n1, Nodo n2)
		{
			int totalValores = n1.Valores.Count;
			int totalHijos = n1.Hijos.Count;


			totalValores += n2.Valores.Count;
			totalHijos += n2.Hijos.Count;
			totalValores += this.Valores.Count;
			totalHijos += this.Hijos.Count;


			if (totalValores > 3)
			{
				throw new InvalidOperationException("Total Valores of all nodes exceeded 3");
			}


			if (totalHijos > 4)
			{
				throw new InvalidOperationException("Total Hijos of all nodes exceeded 4");
			}


			this.Fuse(n1);
			this.Fuse(n2);
		}
		public Nodo[] Split()
		{
			if (Valores.Count != 2)
			{
				throw new InvalidOperationException(string.Format("This node has {0} Valores, can only split a 2 Valores node", Valores.Count));
			}


			Nodo newRight = new Nodo(Valores[1]);


			for (int x = 2; x < Hijos.Count; x++)
			{
				newRight.Hijos.Add(this.Hijos[x]);
			}


			for (int x = Hijos.Count - 1; x >= 2; x--)
			{
				this.Hijos.RemoveAt(x);
			}


			for (int x = 1; x < Valores.Count; x++)
			{
				Valores.RemoveAt(x);
			}


			return new Nodo[] { this, newRight };
		}


		public Gaseosas Pop(int position)
		{
			if (Valores.Count == 1)
			{
				throw new InvalidOperationException("Esta accion no se puede generar con un solo valor en el nodo");
			}


			if (position < Valores.Count)
			{
				Gaseosas k = Valores[position];
				Valores.RemoveAt(position);


				return k;
			}


			return null;
		}


		public void Push(Gaseosas k)
		{
			if (Valores.Count == 3)
			{
				throw new InvalidOperationException("Cannot push value into a 3 Valores node");
			}


			if (Valores.Count == 0)
			{
				Valores.Add(k);
			}
			else
			{
				string left = " ";
				for (int x = 0; x < Valores.Count; x++)
				{
					if (left.CompareTo(k) < 0 && k.Nombre.CompareTo(Valores[x]) < 0)
					{
						Valores.Insert(x, k);
						return;
					}
					else
					{
						left = Valores[x].Nombre;
					}
				}
				Valores.Add(k);
			}
		}


		public Nodo Traverse(string k)
		{
			int pos = EncontrarPosicionHijo(k);


			if (pos < Hijos.Count && pos > -1)
			{
				return Hijos[pos];
			}
			else
			{
				return null;
			}
		}
	}
}
