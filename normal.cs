using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial_Matias_Lico
{
    sealed class Normal : Cobro,IDisposable,ICloneable
    {
        public Normal() : base()
        {

        }

        public Normal(string pcodigo, string pnombre, DateTime pFechaVencimiento,decimal pImporteAcobrar, Cliente pCliente) : base(pcodigo, pnombre, pFechaVencimiento,pImporteAcobrar,pCliente)
        {
       
        }

        public override decimal CalculoAdicional()
        {
            TimeSpan diferenciaDeFechas = FechaVencimiento - DateTime.Today;
            int diferenciaDeDias = diferenciaDeFechas.Days * -1;

            if (diferenciaDeDias > 0)
            {
                decimal calculo = (ImporteAcobrar * diferenciaDeDias) / 100;
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
        ~Normal()
        {
            if (!_dispose)
            {
                
                Console.WriteLine("El Cobro Normal Finalizo con Codigo: " + Codigo + "con Nombre:" + NombreCobro + " y el Importe: " + ImporteAcobrar);
            }
        }
    }
}