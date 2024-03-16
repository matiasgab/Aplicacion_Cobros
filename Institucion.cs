using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2doParcial_Matias_Lico
{
    class Institucion : IDisposable
    {
        List<Cobro> _ListaCobros;
        List<Cliente> _ListaClientes;

        public Institucion()
        {
            _ListaCobros = new List<Cobro>();
            _ListaClientes = new List<Cliente>();
        }

        public void AltaCliente(Cliente cliente)
        {

            try
            {
                if (cliente != null)
                {
                    _ListaClientes.Add(new Cliente(cliente.Legajo, cliente.Nombre));
                }
                else { throw new Exception("El cliente no puede ser nulo"); }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void BajaCliente(Cliente cliente)
        {
            try
            {

                if (cliente != null)
                {
                    if (CorroborarExistenciaCliente(cliente))
                    {
                        _ListaClientes.Remove(_ListaClientes.Find(x => x.Legajo == cliente.Legajo));
                    }

                    else { throw new Exception(" El cliente con legajo" + cliente.Legajo + "que intenta dar de baja no existe"); }

                }else
                { throw new Exception("El cliente no puede ser Null!"); }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarCliente (Cliente cliente)
        {

            try
            {

                if (cliente!= null)
                {
                    if (CorroborarExistenciaCliente(cliente))
                    {
                        Cliente _cliente = _ListaClientes.Find(x => x.Legajo == cliente.Legajo);
                        _cliente.Nombre = cliente.Nombre;
                        _cliente.Legajo = cliente.Legajo;
                    }
                    else
                    {
                        throw new Exception(" El cliente" + cliente.Legajo + " que intenta modificar no existe en la Institucion" );
                    }
                }
                else
                {
                    throw new Exception("El cliente que intenta modificar no puede ser Null!");
                }

            }
            catch (Exception ex)
            {
              throw new Exception(ex.Message);
            }

        }

        public void AltaCobro(Cobro pCObro, Cliente pcliente)
        {

            try
            {
                if (pCObro != null && pcliente != null)
                {
                    string TipoDeCobro = pCObro.GetType().Name;

                    if(TipoDeCobro == "Normal")
                    {
                        _ListaCobros.Add(new Normal(pCObro.Codigo, pCObro.NombreCobro, pCObro.FechaVencimiento, pCObro.ImporteAcobrar, DevolverCLiente(pcliente)));
                    }else if(TipoDeCobro == "Especial")
                    {
                        _ListaCobros.Add(new Normal(pCObro.Codigo, pCObro.NombreCobro, pCObro.FechaVencimiento, pCObro.ImporteAcobrar, DevolverCLiente(pcliente)));
                    }

                    Cliente cliente = DevolverCLiente(pcliente);
                    cliente.AddCobro(DevolverCobro(pCObro));
                }
                else
                {
                    throw new Exception("El COBRO o CLIENTE que se intenta igresar son NULL!");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool CorroborarExistenciaCliente(Cliente cliente)
        {
            return _ListaClientes.Exists(x => x.Legajo == cliente.Legajo);
        }

        public bool CorroborarExistenciaCobro(Cobro cobro)
        {
            return _ListaCobros.Exists(x => x.Codigo == cobro.Codigo);
        }

        public List<Cliente> DevuelveListaCliente()
        {
            List<Cliente> lCliente = new List<Cliente>();

            foreach (Cliente c in _ListaClientes)
            {
                lCliente.Add((Cliente)c.Clone());
            }

            return lCliente;
        }

        public Cliente DevolverCLiente(Cliente pCliente)
        {
            return _ListaClientes.Find(x => x.Legajo == pCliente.Legajo);
        }

        public List<Cliente> RetornaListClienteClon()
        {
            List<Cliente> _listaclientes = new List<Cliente>();
            foreach (Cliente _c in _ListaClientes)
            {
                _listaclientes.Add((Cliente)_c.Clone());
            }

            return _listaclientes;
        }

        public Cobro DevolverCobro(Cobro pCobro)
        {
            return _ListaCobros.Find(x => x.Codigo == pCobro.Codigo);
        }

        public void PagoDeCobro (Cobro cobro)
        {

            try
            {
                Cobro _cobro = _ListaCobros.Find(x => x.Codigo == cobro.Codigo);
                _cobro.CancelacionCobro();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool PagoSuperaAlLimite (Cliente pcliente)
        {
            Cliente _cliente = _ListaClientes.Find(x => x.Legajo == pcliente.Legajo);
            int pendiente = 0;

            foreach (Cobro c in _cliente._ListaCobros)
            {
                if(c.ImporteAcobrar > 0)
                {
                    pendiente += 1;
                }

            }
            if(pendiente == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
                }

        public List<Cobro>RetornaListCobroClon()
        {
            List<Cobro> ListaClientes = new List<Cobro>();

            foreach (Cobro _c in _ListaCobros)
            {
                ListaClientes.Add((Cobro)_c.Clone());
            }

            return ListaClientes;
        }

        public List<Cobro> RetornaListaCobros()
        {
            return _ListaCobros.ToList();
        }

        bool _dispose = false;
        ~Institucion()
        {
            if (!_dispose)
            {
               
                Console.WriteLine("El objeto Institucion ha finalizado");
            }
        }
        public void Dispose()
        {
            _dispose = true;
        }
    }
}