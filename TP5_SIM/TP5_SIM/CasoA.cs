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
        private double tiempoAtencion;
        private string estadoCaja1;
        private string estadoCaja2;
        private Evento proximoEvento;
        private int cantClientes;
        private int cantClientesAtendidos;
        private double totalTiempoAtendiendo;
        private double promTiempoAtendiendo;
        private double cantClientesAtendidosxMinuto;


        private List<int> colaA = new List<int>();
        private List<int> colaB = new List<int>();
        private List<double> tiemposInicioAt = new List<double>();
        private List<double> tiemposFinAt = new List<double>();
        private List<Evento> Eventos = new List<Evento>();
        Random aleatorio = new Random();
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
            clientesGrid.Rows.Clear();
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
            
            finAtencionA = 0;
            finAtencionB = 0;
            proximaLlegada = 0;
            cantClientes = 1;
            estadoCaja1 = "Libre";
            estadoCaja2 = "Cerrada";
            reloj = 0;
            colaA.Clear();
            colaB.Clear();
            tiemposInicioAt.Clear();
            tiemposFinAt.Clear();
            Eventos.Add(new Evento() { tiempo = 0, nombre = "Inicializacion", cliente = 0, caja = 0 });
            cantClientesAtendidos = 0;
            totalTiempoAtendiendo = 0;

            rnd = aleatorio.NextDouble();
            for (int i = 1; reloj <= minutos; i++)
            {
                proximoEvento = obtenerProxEvent(Eventos);

                //Inicialización
                if (proximoEvento.nombre == "Inicializacion")
                {
                    evento = "Inicialización";
                    reloj = proximoEvento.tiempo;
                    rndLlegada = obtenerRandom();
                    tiempoEntreLlegada = Math.Round(-media * Math.Log(1 - rndLlegada), 2);
                    proximaLlegada = reloj + tiempoEntreLlegada;
                    Eventos.Add(new Evento() { tiempo = proximaLlegada, nombre = "Llegada_cliente", cliente = 1, caja = 0 });
                    rndTiempoAtencion = 0;
                    tiempoAtencion = 0;
                    rndPago = 0;
                    formaPago = " ";

                }

                //Llegada de un cliente
                if (proximoEvento.nombre == "Llegada_cliente")
                {
                    cantClientes += 1;
                    evento = "Llegada_cliente_" + proximoEvento.cliente;
                    reloj = proximoEvento.tiempo;
                    rndLlegada = obtenerRandom();
                    if (rndLlegada == 1) {
                        rndLlegada = 0.99;
                    }
                    tiempoEntreLlegada = Math.Round(-media * Math.Log(1 - rndLlegada), 2);
                    proximaLlegada = reloj + tiempoEntreLlegada;
                    Eventos.Add(new Evento() { tiempo = proximaLlegada, nombre = "Llegada_cliente", cliente = cantClientes, caja = 0 });
                    rndTiempoAtencion = 0;
                    tiempoAtencion = 0;
                    rndPago = 0;
                    formaPago = " ";
                    agregarClienteTabla(proximoEvento.cliente);
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
                        rndTiempoAtencion = obtenerRandom();
                        rndPago = obtenerRandom();
                        formaPago = FormaPago(rndPago);
                        tiempoAtencion = 0.5 + rndTiempoAtencion;
                        agregarInicioAtencionTabla(proximoEvento.cliente, reloj);
                        Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = proximoEvento.cliente, caja = 1 });
                        finAtencionA = reloj + tiempoAtencion;
                        ingresaClienteColaA(proximoEvento.cliente);
                    }
                    if (cola == 2 && estadoCaja2 == "Libre")
                    {
                        rndTiempoAtencion = obtenerRandom();
                        rndPago = obtenerRandom();
                        formaPago = FormaPago(rndPago);
                        tiempoAtencion = 0.5 + rndTiempoAtencion;
                        if (formaPago == "Tarjeta")
                        {
                            tiempoAtencion += 2;
                        }
                        agregarInicioAtencionTabla(proximoEvento.cliente, reloj);
                        Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = proximoEvento.cliente, caja = 2 });
                        finAtencionB = reloj + tiempoAtencion;
                        ingresaClienteColaB(proximoEvento.cliente);
                    }
                    if (cola == 3 && estadoCaja2 == "Libre")
                    {
                        rndTiempoAtencion = obtenerRandom();
                        rndPago = obtenerRandom();
                        formaPago = FormaPago(rndPago);
                        tiempoAtencion = 0.5 + rndTiempoAtencion;
                        if (formaPago == "Tarjeta")
                        {
                            tiempoAtencion += 2;
                        }
                        agregarInicioAtencionTabla(colaB[0], reloj);
                        Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = colaB[0], caja = 2 });
                        finAtencionB = reloj + tiempoAtencion;
                        ingresaClienteColaB(proximoEvento.cliente);
                    }

                }

                //Fin de atención de un cliente
                if (proximoEvento.nombre == "Fin_Atencion_C") {
                    evento = proximoEvento.nombre + "_" + proximoEvento.cliente;
                    reloj = proximoEvento.tiempo;
                    rndLlegada = 0;
                    tiempoEntreLlegada = 0;
                    proximaLlegada = 0;
                    cantClientesAtendidos += 1;
                    agregarFinAtencionCliente(proximoEvento.cliente, reloj);
                    sumarTiempoAtencion(proximoEvento.cliente, reloj);
                    
                    if (proximoEvento.caja == 1)
                    {
                        finAtencionColaA();
                        if (colaA.Count != 0)
                        {
                            rndTiempoAtencion = obtenerRandom();
                            rndPago = obtenerRandom();
                            formaPago = FormaPago(rndPago);
                            tiempoAtencion = 0.5 + rndTiempoAtencion;
                            if (formaPago == "Tarjeta")
                            {
                                tiempoAtencion += 2;
                            }
                            agregarInicioAtencionTabla(colaA[0], reloj);
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
                            rndTiempoAtencion = obtenerRandom();
                            rndPago = obtenerRandom();
                            formaPago = FormaPago(rndPago);
                            tiempoAtencion = 0.5 + rndTiempoAtencion;
                            if (formaPago == "Tarjeta")
                            {
                                tiempoAtencion += 2;
                            }
                            agregarInicioAtencionTabla(colaB[0], reloj);
                            Eventos.Add(new Evento() { tiempo = reloj + tiempoAtencion, nombre = "Fin_Atencion_C", cliente = colaB[0], caja = 2 });
                            finAtencionB = reloj + tiempoAtencion;
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

                if (reloj >= desde && reloj <= hasta)
                {
                    dgCasoA.Rows.Add(evento, Math.Round(reloj,2), rndLlegada, tiempoEntreLlegada, Math.Round(proximaLlegada,2), rndPago, formaPago, rndTiempoAtencion, tiempoAtencion, Math.Round(finAtencionA,2), Math.Round(finAtencionB,2), estadoCaja1, string.Join(",", colaA), estadoCaja2, string.Join(",", colaB), cantClientesAtendidos, totalTiempoAtendiendo, promTiempoAtendiendo, cantClientesAtendidosxMinuto);
                }

                
                
            }

            int cli = 0;
            foreach (double tmp in tiemposFinAt)
            {
                clientesGrid.Rows.Add(cli + 1, tiemposInicioAt[cli], tiemposFinAt[cli]);
                cli++;
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
                if (estadoCaja1 == "Libre")
                {
                    estadoCaja2 = "Cerrada";
                }
            }
            
        }

        private int aQueColaIr()
        {
            if (estadoCaja2 == "Cerrada" && colaA.Count == 4) {
                estadoCaja2 = "Libre";
                colaB.Add(colaA.Last());
                colaA.RemoveAt(colaA.Count - 1);
                return 3;
            }
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

        private void agregarClienteTabla(int cliente) {
            //clientesGrid.Rows.Add(cliente);
            tiemposInicioAt.Add(0);
            tiemposFinAt.Add(0);
        }
        private void agregarInicioAtencionTabla(int cliente, double reloj)
        {
            tiemposInicioAt[cliente - 1] = reloj;
            
            //clientesGrid.Rows[cliente - 1].Cells[0].Value = cliente;
            //clientesGrid.Rows[cliente - 1].Cells[1].Value = reloj;
        }
        private void agregarFinAtencionCliente(int cliente, double reloj)
        {
            //DataGridViewRow fila = new DataGridViewRow();
            //fila = clientesGrid.Rows[cliente - 1];
            tiemposFinAt[cliente - 1] = reloj;
            //double tiempoInicio = Convert.ToDouble(fila.Cells[1].Value);
            //clientesGrid.Rows[cliente - 1].Cells[0].Value = cliente;
            //clientesGrid.Rows[cliente - 1].Cells[1].Value = tiempoInicio;
            //clientesGrid.Rows[cliente - 1].Cells[2].Value = reloj;
        }

        private double obtenerTiempoInicioAtencion(int cliente)
        {
            double tiempo;
            //tiempo = Convert.ToDouble(clientesGrid.Rows[cliente - 1].Cells[1].Value);
            tiempo = tiemposInicioAt[cliente - 1];
            return tiempo;
        }

        private void sumarTiempoAtencion(int cliente, double reloj) {
            double inicioT = obtenerTiempoInicioAtencion(cliente);
            double total = reloj - inicioT;
            totalTiempoAtendiendo += total;
            promTiempoAtendiendo = Math.Round(totalTiempoAtendiendo / cantClientesAtendidos, 2);
            calcularClientesxMinuto(reloj);
        }

        private void calcularClientesxMinuto(double reloj) {
            cantClientesAtendidosxMinuto = Math.Round(cantClientesAtendidos / reloj, 2);
        }
        private double obtenerRandom() { 
            double rnd = Math.Round(aleatorio.NextDouble(), 2);
            if (rnd == 1) {
                rnd = 0.99;
            }
            if (rnd == 0) {
                rnd = 0.01;
            }

            return rnd;
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
