using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial_Matias_Lico
{
    abstract class Cobro :ICloneable,IComparable<Cobro>,IDisposable
    {
        private Cliente cliente;
        public event EventHandler<ImportePasadoEventArgs> ImportePasado;
        
        


        public Cobro()
        {

        }
        public Cobro(string pcodigo, string pNombreCobro, DateTime pFechaVencimiento, decimal pImporteAcobrar, Cliente pcliente)
        {
            Codigo = pcodigo; NombreCobro = pNombreCobro; FechaVencimiento = pFechaVencimiento; ImporteAcobrar = pImporteAcobrar; cliente = pcliente;
            _cobrocancelado = false;
        }

        public string Codigo { get; set; }
        public string NombreCobro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal ImporteAcobrar { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private bool _cobrocancelado;
        public void CancelacionCobro()
        {
            _cobrocancelado = true;
        }

        public bool SeCancela()
        {
            return _cobrocancelado;
        }

        public Cliente RetornaCliente()
        {
            return cliente;
        }

        public abstract decimal CalculoAdicional();

        public int CompareTo(Cobro cobro)
        {
            return ImporteAcobrar.CompareTo(cobro.ImporteAcobrar);
        }

        public override string ToString()
        {
            return $"{Codigo} {NombreCobro} {FechaVencimiento} {ImporteAcobrar}";
        }

        private decimal _TotalImporte;
        public decimal DevuelveTotalImporteCancelado()
        {
            return _TotalImporte;
        }

        public void AsignarImporteTotal(decimal pImporte)
        {
            _TotalImporte = pImporte;
        }

        bool _dispose = false;
        ~Cobro()
        {
            if (!_dispose)
            {
                
                Console.WriteLine("El Cobro ha Finalizado con el Codigo: " + Codigo + " con el Nombre:" + NombreCobro + " y el Importe: " + ImporteAcobrar);
            }
        }
        public void Dispose()
        {
            _dispose = true;
        }

    }
}