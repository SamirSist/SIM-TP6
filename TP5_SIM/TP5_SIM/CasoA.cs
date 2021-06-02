using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP5_SIM
{
    public partial class CasoA : Form
    {

        private double media;
        private double finAtencionA;
        private double finAtencionB;
        private int minutos;
        private int desde;
        private int hasta;
        private string evento;
        private double reloj;
        private double rndLlegada;
        private double tiempoEntreLlegada;
        private double proximaLlegada;
        private double rndPago;
        private string formaPago;
        private double rndTiempoAtencion;
        private double proximaAtencion;
        private double tiempoAtencion;
        private double finCaja1;
        private double finCaja2;
        private string estadoCaja1;
        private int colaCaja1;
        private string estadoCaja2;
        private int colaCaja2;
        private Evento proximoEvento;
        private int cantClientes;
        private int actClienteA;
        private int actClienteB;
        private List<int> colaA = new List<int>();
        private List<int> colaB = new List<int>();
        private List<Evento> Eventos = new List<Evento>();
        public CasoA()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CasoA_Load(object sender, EventArgs e)
        {
            txt_media.Text = Convert.ToString(1);
            txt_finAtencion_A.Text = Convert.ToString(0.5);
            txt_FinAtencion_B.Text = Convert.ToString(1.5);
            txt_Minutos.Text = Convert.ToString(50);
            txt_Desde.Text = Convert.ToString(1);
            txt_Hasta.Text = Convert.ToString(20);
        }

        private string FormaPago(double rnd)
        {
            if (rnd < 0.20)
                return "Tarjeta";
            else
                return "Contado";
        }

        private void btn_simular_Click(object sender, EventArgs e)
        {
            dgCasoA.Rows.Clear();

            //asigno parametros
            media = Convert.ToDouble(txt_media.Text);
            finAtencionA = Convert.ToDouble(txt_finAtencion_A.Text);
            finAtencionB = Convert.ToDouble(txt_FinAtencion_B.Text);
            desde = Convert.ToInt32(txt_Desde.Text);
            hasta = Convert.ToInt32(txt_Hasta.Text);
            minutos = Convert.ToInt32(txt_Minutos.Text);
            //rnd
            double rnd;
            int opcion;


            Eventos.Clear();
            Random aleatorio = new Random();
            finAtencionA = 0;
            finAtencionB = 0;
            proximaLlegada = 0;
            cantClientes = 1;
            estadoCaja1 = "Libre";
            estadoCaja2 = "Cerrada";
            colaCaja1 = 0;
            colaCaja2 = 0;
            reloj = 0;
            Eventos.Add(new Evento() { tiempo = 0, nombre = "Inicializacion", cliente = 0, caja = 0 });

            rnd = aleatorio.NextDouble();
            for (int i = 1; reloj <= minutos; i++)
            {
                proximoEvento = obtenerProxEvent(Eventos);

                //Inicialización
                if (proximoEvento.nombre == "Inicializacion")
                {
                    evento = "Inicialización";
                    reloj = proximoEvento.tiempo;
                    rndLlegada = Math.Round(aleatorio.NextDouble(), 2);
                    tiempoEntreLlegada = Math.Round(-media * Math.Log(Math.E, 1 - rndLlegada), 2);
                    proximaLlegada = reloj + tiempoEntreLlegada;
                    Eventos.Add(new Evento() { tiempo = proximaLlegada, nombre = "Llegada_cliente", cliente = 1, caja = 0 });
                    rndTiempoAtencion = 0;
                    tiempoAtencion = 0;
                    rndPago = 0;
                    formaPago = " ";

                }

                if (proximoEvento.nombre == "Llegada_cliente")
                {
                    cantClientes += 1;
                    evento = "Llegada_cliente_" + proximoEvento.cliente;
                    reloj = proximoEvento.tiempo;
                    rndLlegada = Math.Round(aleatorio.NextDouble(), 2);
                    tiempoEntreLlegada = Math.Round(-media * Math.Log(Math.E, 1 - rndLlegada), 2);
                    proximaLlegada = reloj + tiempoEntreLlegada;
                    Eventos.Add(new Evento() { tiempo = proximaLlegada, nombre = "Llegada_cliente", cliente = cantClientes, caja = 0 });
                    rndTiempoAtencion = 0;
                    tiempoAtencion = 0;
                    rndPago = 0;
                    formaPago = " ";

                    int cola = aQueColaIr();
                    if (estadoCaja1 == "AC" && (estadoCaja2 == "AC" || estadoCaja2 == "Cerrada"))
                    {
                        if (cola == 1)
                        {
                            ingresaClienteColaA(proximoEvento.cliente);
                        }
                        if (cola == 2)
                        {
                            ingresaClienteColaB(proximoEvento.cliente);
                        }
                    }
                    if (cola == 1 && estadoCaja1 == "Libre") {
                        rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                        rndPago = Math.Round(aleatorio.NextDouble(), 2);
                        formaPago = FormaPago(rndPago);
                        tiempoAtencion = 0.5 + rndTiempoAtencion;
                        if (formaPago == "Tarjeta")
                        {
                            tiempoAtencion += 2;
                        }
                        Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = proximoEvento.cliente, caja = 1 });
                        finAtencionA = reloj + tiempoAtencion;
                        ingresaClienteColaA(proximoEvento.cliente);
                    }
                    if (cola == 2 && estadoCaja2 == "Libre")
                    {
                        rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                        rndPago = Math.Round(aleatorio.NextDouble(), 2);
                        formaPago = FormaPago(rndPago);
                        tiempoAtencion = 0.5 + rndTiempoAtencion;
                        if (formaPago == "Tarjeta")
                        {
                            tiempoAtencion += 2;
                        }
                        Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = proximoEvento.cliente, caja = 2 });
                        finAtencionA = reloj + tiempoAtencion;
                        ingresaClienteColaB(proximoEvento.cliente);
                    }

                }

                if (proximoEvento.nombre == "Fin_Atencion_C") {
                    evento = proximoEvento.nombre + " " + proximoEvento.cliente;
                    reloj = proximoEvento.tiempo;
                    rndLlegada = 0;
                    tiempoEntreLlegada = 0;
                    proximaLlegada = 0;

                    if (proximoEvento.caja == 1)
                    {
                        finAtencionColaA();
                        if (colaA.Count != 0)
                        {
                            rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                            rndPago = Math.Round(aleatorio.NextDouble(), 2);
                            formaPago = FormaPago(rndPago);
                            tiempoAtencion = 0.5 + rndTiempoAtencion;
                            if (formaPago == "Tarjeta")
                            {
                                tiempoAtencion += 2;
                            }

                            Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = colaA[0], caja = 1 });
                            finAtencionA = reloj + tiempoAtencion;
                        }
                        else
                        {
                            finAtencionColaA();
                            rndTiempoAtencion = 0;
                            tiempoAtencion = 0;
                            rndPago = 0;
                            formaPago = " ";
                        }
                    }
                    if (proximoEvento.caja == 2)
                    {
                        finAtencionColaB();
                        if (colaB.Count != 0)
                        {
                            rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                            rndPago = Math.Round(aleatorio.NextDouble(), 2);
                            formaPago = FormaPago(rndPago);
                            tiempoAtencion = 0.5 + rndTiempoAtencion;
                            if (formaPago == "Tarjeta")
                            {
                                tiempoAtencion += 2;
                            }

                            Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = colaB[0], caja = 2 });
                            finAtencionA = reloj + tiempoAtencion;
                        }
                        else
                        {
                            finAtencionColaB();
                            rndTiempoAtencion = 0;
                            tiempoAtencion = 0;
                            rndPago = 0;
                            formaPago = " ";
                        }
                    }
                }

                dgCasoA.Rows.Add(evento, reloj, rndLlegada, tiempoEntreLlegada, proximaLlegada, rndPago, formaPago, rndTiempoAtencion, tiempoAtencion, finAtencionA, finAtencionB, estadoCaja1, string.Join(",", colaA), estadoCaja2, string.Join(",", colaB), cantClientes, 0, 0);

            }



        }

        private void ingresaClienteColaA(int numC) {

            if (estadoCaja1 == "Libre")
            {
                estadoCaja1 = "AC";
            }

            colaA.Add(numC);

        }

        private void ingresaClienteColaB(int numC)
        {
            if (estadoCaja2 == "Libre")
            {
                estadoCaja2 = "AC";
            }
            colaB.Add(numC);

        }

        private void finAtencionColaA()
        {
            int temp;
            if (colaA.Count == 0)
            {
                temp = -1;
            }
            else
            {
                temp = colaA[0];
                colaA.RemoveAt(0);
            }

            if (colaA.Count == 0)
            {
                estadoCaja1 = "Libre";
            }
        }

        private void finAtencionColaB()
        {
            int temp;
            if (colaB.Count == 0)
            {
                temp = -1;
            }
            else
            {
                temp = colaB[0];
                colaB.RemoveAt(0);
            }
            if (colaB.Count == 0)
            {
                estadoCaja2 = "Libre";
            }
        }

        private int aQueColaIr()
        {
            if (estadoCaja2 == "Cerrada")
            {
                return 1;
            }
            if (colaA.Count > colaB.Count && estadoCaja2 == "AC" || estadoCaja2 == "Libre") {
                return 2;
            }
            if (colaA.Count <= colaB.Count)
            {
                return 1;
            }
            return 1;
            

        }

        class Evento
        {
            public double tiempo { get; set; }
            public string nombre { get; set; }

            public int cliente { get; set; }

            public int caja { get; set; }
        }

        private Evento obtenerProxEvent(List<Evento> Eventos) {
            Evento tempEvent = Eventos[0];
            int i;
            i = 0;
            int j;
            j = 0;
            foreach (Evento ev in Eventos) {
               
                if (ev.tiempo < tempEvent.tiempo) {
                    tempEvent = ev;
                    j = i;
                }
                i += 1;
            }
            Eventos.RemoveAt(j);
            return tempEvent;
        }
    }
}
