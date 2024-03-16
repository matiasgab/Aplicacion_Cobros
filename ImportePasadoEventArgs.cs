using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial_Matias_Lico
{
    class ImportePasadoEventArgs : EventArgs
    {
        decimal _TotalImporte;

        public ImportePasadoEventArgs(decimal pTotalImporte)
        {
            _TotalImporte = pTotalImporte;
        }

        public decimal TotalImporte { get { return _TotalImporte; } }

    }
}
