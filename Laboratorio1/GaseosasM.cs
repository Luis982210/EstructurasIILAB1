using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio1
{
    public class GaseosasM
    {
        public Arbol nuevoArbol { get; set; }
        public List<string> nombres { get; set; }

        public GaseosasM()
        {
            nuevoArbol = new Arbol();
            nombres = new List<string>();
        }

        public bool verificar(string Nombre)
        {
            if (nombres.Contains(Nombre))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public List<Gaseosas> Get()
        {
            return nuevoArbol.Inorder(); 
        }

        public Gaseosas GetID(string nombre)
        {
            List<Gaseosas> lista = nuevoArbol.Inorder();
            foreach (var item in lista)
            {
                if (item.Nombre == nombre)
                {
                    return item;
                }
            }
            return null;
        }

        public void crearGaseosa(Gaseosas m)
        {
            nuevoArbol.Insertar(m);
        }
    }
    public interface IGaseosasM
    {
        Arbol nuevoArbol { get; set; }
        Gaseosas GetID(string gaseosa);
        List<Gaseosas> Get();
        bool verificar(string Nombre);
        void crearGaseosa(Gaseosas m);
    }

}

