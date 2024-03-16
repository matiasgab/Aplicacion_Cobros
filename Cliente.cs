using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2doParcial_Matias_Lico
{
    sealed class Cliente : ICloneable, IDisposable //revisar visualizacion de clases...
    {
        public List<Cobro> _ListaCobros;

        public Cliente()
        {

        }
        public Cliente(string pLegajo, string pNombre)
        {
            Legajo = pLegajo; Nombre = pNombre; _ListaCobros = new List<Cobro>();
        }

        public string Legajo { get; set; }
        public string Nombre { get; set; }

        public void AddCobro(Cobro pCobro)
        {
            _ListaCobros.Add(pCobro);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

      
        bool _dispose = false;
        ~Cliente()
        {
            if (!_dispose)
            {
                
                Console.WriteLine("El objeto Cliente se encuetra finalizado con Legajo: " + Legajo + " y Nombre: " + Nombre);
            }
        }
        
        public void Dispose()
        {
            _dispose= true;
        }
    }
}