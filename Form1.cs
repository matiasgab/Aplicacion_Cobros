using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace _2doParcial_Matias_Lico
{
    public partial class Form1 : Form
    {
        Institucion institucion;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            institucion = new Institucion();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.MultiSelect = false;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView5.MultiSelect = false;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnAltaCliente_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente auxCliente = new Cliente();

                string legajoIn = Interaction.InputBox("Ingresar Legajo: ");
                ValidarValueVacios(legajoIn, "Legajo");
               
                auxCliente.Legajo = legajoIn;
                if (institucion.CorroborarExistenciaCliente(auxCliente))
                {
                    throw new Exception("El legajo ingresado ya existe en sistema");
                }
                string nombre = Interaction.InputBox("Ingresar Nombre: ");
                ValidarValueVacios(nombre, "nombre");
                auxCliente.Nombre = nombre;

                institucion.AltaCliente(auxCliente);

                Mostrar(dataGridView1, institucion.DevuelveListaCliente());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ValidarValueVacios(string input, string campo)
        {
            if (input.Length == 0)
            {
                throw new Exception("El campo " + campo + " no debe estar vacio");
            }
        }
        private void Mostrar(DataGridView Dgv, Object pO)
        {
            Dgv.DataSource = null;
            Dgv.DataSource = pO;
        }

        private void btnBajaCliente_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    Cliente _cliente = institucion.DevolverCLiente(DevolverSeleccionCliente());
                    if (_cliente._ListaCobros == null || _cliente._ListaCobros.Count == 0)
                    {
                        institucion.BajaCliente(_cliente);
                        Mostrar(dataGridView1, institucion.RetornaListClienteClon());
                    }
                    else
                    {
                        throw new Exception("El cliente no se podra dar de baja ya que posee cobros pendientes ");
                    }
                }
                else
                {
                    throw new Exception("No hay clientes cargados para ejecutar la baja ");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private Cliente DevolverSeleccionCliente()
        {
            return dataGridView1.SelectedRows[0].DataBoundItem as Cliente;
        }

        private void btnModificacionCliente_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    Cliente _cliente = institucion.DevolverCLiente(DevolverSeleccionCliente());

                    string nombre = Interaction.InputBox("Ingresar nombre: ");
                    ValidarValueVacios(nombre, "nombre");
                    _cliente.Nombre = nombre;

                    institucion.ModificarCliente(_cliente);

                    Mostrar(dataGridView1, institucion.RetornaListClienteClon());

                }
                else
                {
                    throw new Exception("No hay datos para ser modificados! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAltaCobro_Click(object sender, EventArgs e)
        {

            try
            {
                Cobro _cobro = null;

                if (dataGridView1.Rows.Count > 0)
                {
                    Cliente _cliente = DevolverSeleccionCliente();

                    if (!institucion.PagoSuperaAlLimite(_cliente))
                    {
                        string tipoDeCobro = Interaction.InputBox("Ingresar 1 si el tipo de cobro es NORMAL o 2 si es ESPECIAL");
                        int tNumero = 0;

                        if (Information.IsNumeric(tipoDeCobro))
                        {
                            tNumero = int.Parse(tipoDeCobro);

                            if (tNumero != 0 && tNumero <= 2)
                            {
                                switch (tNumero)
                                {
                                    case 1:
                                        _cobro = new Normal();
                                        break;

                                    case 2:
                                        _cobro = new Especial();
                                        break;
                                }
                                string codigo = Interaction.InputBox("Ingresar el Codigo del Cobro: ");
                                ValidarValueVacios(codigo, "Codigo");

                                _cobro.Codigo = codigo;

                                if (institucion.CorroborarExistenciaCobro(_cobro))
                                {
                                    throw new Exception("Existe un cobro con el mismo, favor de ingresar otro");
                                }



                                string nombre = Interaction.InputBox("Ingresar Nombre: ");
                                ValidarValueVacios(nombre, "Nombre");
                                _cobro.NombreCobro = nombre;


                                string fechaVencimiento = Interaction.InputBox("Ingresar la fecha de vencimiento: ");
                                ValidarValueVacios(fechaVencimiento, "Fecha Vencimiento");

                                if (Information.IsDate(fechaVencimiento))
                                {
                                    _cobro.FechaVencimiento = Convert.ToDateTime(fechaVencimiento);
                                }

                                string importe = Interaction.InputBox("Ingresar el Importe: ");
                                ValidarValueVacios(importe, "Importe");

                                if (Information.IsNumeric(importe))
                                {
                                    _cobro.ImporteAcobrar = decimal.Parse(importe);
                                }

                                if (!institucion.CorroborarExistenciaCobro(_cobro))
                                {
                                    institucion.AltaCobro(_cobro, _cliente);
                                }
                                else
                                {
                                    throw new Exception("Existe un otro Cobro con el mismo Legajo, ingresar un nuevo");
                                }

                                Mostrar(dataGridView1, institucion.RetornaListClienteClon());
                                Mostrar(dataGridView2, institucion.RetornaListCobroClon());
                                Mostrar(dataGridView4, RetornaCobrosMayoraMenor(true));
                                raBtnMenAMayor.Checked = true;
                            }
                            else
                            {
                                throw new Exception("Debe ingresar un tipo de Cobro entre los valores 1 y 2 ");
                            }
                        }
                        else
                        {
                            throw new Exception("Debe ingresar un tipo de Cobro valido ");
                        }
                    }
                    else
                    {
                        throw new Exception("El Cliente seleccionado tiene pendiente dos cobros sin saldar, no podra darse de alta un nuevo cobro hasta abonar lo deudado");
                    }
                }
                else
                {
                    throw new Exception("Si no existen clientes no puede darse de alta un cobro ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private IEnumerable<Cobro> RetornaCobrosMayoraMenor(bool pMayor)
        {

            Cliente _cliente = institucion.DevolverCLiente(DevolverSeleccionCliente());
            List<Cobro> _cobros = _cliente._ListaCobros;

            if (pMayor)
            {
                _cobros.Sort();
            }
            else
            {
                _cobros.Reverse();
            }


            return _cobros;
        }
        private IEnumerable<Cobro> DevuelveListaCobrosPagadosPorCliente()
        {
            var cobros = (from Cobro c in institucion.RetornaListaCobros()
                          where c.RetornaCliente().Legajo == DevolverSeleccionCliente().Legajo && c.SeCancela() == true
                          select c
            );

            return cobros.ToList();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0)
                {
                    Cobro _cobro = institucion.DevolverCobro(RetornaCobroSeleccionado());
                    Cliente _cliente = DevolverSeleccionCliente();

                    if (!_cobro.SeCancela())
                    {

                        if (_cobro.RetornaCliente().Legajo == _cliente.Legajo)
                        {
                            decimal recargo = _cobro.CalculoAdicional();
                            decimal totalAbonar = _cobro.ImporteAcobrar + recargo;

                            if (totalAbonar >= 10000)
                            {
                                _cobro.ImportePasado += ImportePasadoEventArgs;
                                ImportePasadoEventArgs(this, new ImportePasadoEventArgs(totalAbonar));
                            }

                            MessageBox.Show("El importe a pagar es : $" + _cobro.ImporteAcobrar + " recargo: $" + recargo + " con un total de: $" + (_cobro.ImporteAcobrar + recargo));

                            institucion.PagoDeCobro(_cobro);
                            MuestraListaDeCobrosCancelados();
                            MuestraListaDeCobrosCanceladosCLiente();
                        }
                        else
                        {
                            throw new Exception(" El Cobro que se selecciono no puede pagarse porque pertenece al cliente " + _cobro.RetornaCliente().Nombre + "con legajo: " + _cobro.RetornaCliente().Legajo);
                        }
                    }
                    else
                    {
                        throw new Exception("El cobro que intenta cancelar ya se encuentra cancelado!");
                    }
                }
                else
                {
                    throw new Exception("Las grilla de clientes o de Cobros se encuentran vacias");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MuestraListaDeCobrosCancelados()
        {
            var CancelacionDeCobros = (from Cobro c in institucion.RetornaListaCobros()
                                    where c.SeCancela() == true
                                    select new
                                    {
                                        NombreCliente = c.RetornaCliente().Nombre,
                                        ImporteTotalCancelado = c.DevuelveTotalImporteCancelado()
                                    });

            dataGridView5.DataSource = CancelacionDeCobros.ToList();
        }

        private void MuestraListaDeCobrosCanceladosCLiente()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                var CancelacionDeCobros = (from Cobro c in institucion.RetornaListaCobros()
                                        where c.SeCancela() == true && c.RetornaCliente().Legajo == DevolverSeleccionCliente().Legajo
                                        select new
                                        {
                                            Tipo = c.GetType().Name,
                                            Código = c.Codigo,
                                            Nombre = c.NombreCobro,
                                            FechaVencimiento = c.FechaVencimiento,
                                            Importe = c.ImporteAcobrar,
                                            ImporteCancelado = c.DevuelveTotalImporteCancelado()
                                        });

                dataGridView3.DataSource = CancelacionDeCobros.ToList();
            }
        }

        private Cobro RetornaCobroSeleccionado()
        {
            return dataGridView2.SelectedRows[0].DataBoundItem as Cobro;
        }

        private void ImportePasadoEventArgs(object sender, ImportePasadoEventArgs _e)
        {
            try
            {
                MessageBox.Show($"El cobro que intenta realizar esta superando los  $10.000 pesos, el total a Cobrar es : $ {_e.TotalImporte}");
            }
            catch (Exception)
            {

            }
        }

        private void raBtnMayAMen_CheckedChanged(object sender, EventArgs e)
        {
            Mostrar(dataGridView4, RetornaCobrosMayoraMenor(true));
        }

        private void raBtnMenAMayor_CheckedChanged(object sender, EventArgs e)
        {
            Mostrar(dataGridView4, RetornaCobrosMayoraMenor(false));
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                Mostrar(dataGridView3, DevuelveListaCobrosPagadosPorCliente());
                Mostrar(dataGridView4, RetornaCobrosMayoraMenor(true));
                MuestraListaDeCobrosCancelados();
                MuestraListaDeCobrosCanceladosCLiente();
                raBtnMenAMayor.Checked = true;

            }
            catch (Exception)
            {

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            this.Close();

        }
    }
}
