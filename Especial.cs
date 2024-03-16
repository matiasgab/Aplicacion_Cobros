using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial_Matias_Lico
{
    sealed class Especial : Cobro,IDisposable,ICloneable
    {
        public Especial()
        {
                
        }
        public Especial(string pcodigo, string pnombre, DateTime pFechaVencimiento, decimal pImporteAcobrar, Cliente pCliente) : base(pcodigo, pnombre, pFechaVencimiento, pImporteAcobrar, pCliente)
        {

        }

        public override decimal CalculoAdicional()
        {
            TimeSpan diferenciaDeFechas = FechaVencimiento - DateTime.Today;
            int diferenciaDeDias = diferenciaDeFechas.Days * -1;

            if (diferenciaDeDias > 0)
            {
                decimal calculo = (((ImporteAcobrar * diferenciaDeDias) * 2) / 100) + 1000;

                AsignarImporteTotal(calculo + ImporteAcobrar);

                return calculo;
            }
            else
            {
                AsignarImporteTotal(ImporteAcobrar);
                return 0;
            }
        }

        bool _dispose = false;
        ~Especial()
        {
            if (!_dispose)
            {
                
                Console.WriteLine("El Cobro tipo Especial Finalizo con Codigo: " + Codigo + " con Nombre:" + NombreCobro + " y el Importe: " + ImporteAcobrar);
            }
        }
    }
}