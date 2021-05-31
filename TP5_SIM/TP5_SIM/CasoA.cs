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
        private double finCaja1 = 1000;
        private double finCaja2 = 1100;
        private string estadoCaja1;
        private int colaCaja1;
        private string estadoCaja2;
        private int colaCaja2;




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

            //rnd
            double rnd;
            int opcion;

            IList<Object> filaActual = new Object[15];

            filaActual[0] = evento;
            filaActual[1] = reloj;
            filaActual[2] = rndLlegada;
            filaActual[3] = tiempoEntreLlegada;
            filaActual[4] = proximaLlegada;
            filaActual[5] = rndPago;
            filaActual[6] = formaPago;
            filaActual[7] = rndTiempoAtencion;
            filaActual[8] = tiempoAtencion;
            filaActual[9] = finCaja1;
            filaActual[10] = finCaja2;
            filaActual[11] = estadoCaja1;
            filaActual[12] = colaCaja1;
            filaActual[13] = estadoCaja2;
            filaActual[14] = colaCaja2;


            Random aleatorio = new Random();

            rnd = aleatorio.NextDouble();

            for(int i = 1; i <= Convert.ToInt32(txt_Minutos.Text); i++)
            {
                if(i == 1)
                {
                    evento = "Inicio";
                    reloj = 0.00;
                    filaActual[2] = Math.Round(aleatorio.NextDouble(), 2);
                    filaActual[3] = Math.Round(-media * Math.Log(1 - Convert.ToDouble(filaActual[2])), 2);
                    filaActual[4] = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[3]);
                    filaActual[9] = 0;
                    filaActual[10] = 0;
                    filaActual[11] = "Libre";
                    filaActual[13] = "Libre";

                }

                //filaActual[9] = 1000;
                //filaActual[10] = 1010;

                if (i > 1)
                {
                    //filaActual[9] = 1000;
                    //filaActual[10] = 1010;

                    if ( Convert.ToDouble(filaActual[4]) < Convert.ToDouble(filaActual[9]) && Convert.ToDouble(filaActual[4]) < Convert.ToDouble(filaActual[10]))
                    {
                        evento = "llegada_cliente";
                        reloj = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[4]);
                        filaActual[9] = 0;
                    }
                    else
                    {
                        evento = "llegada_clien";
                        reloj = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[4]);


                    }

                        
                    

                }


                if(i == 1)
                {
                    dgCasoA.Rows.Add(evento, reloj, filaActual[2], filaActual[3], filaActual[4], filaActual[5], filaActual[6], filaActual[7], filaActual[8], filaActual[9], filaActual[10], filaActual[11], filaActual[12], filaActual[13], filaActual[14]);
                }

                if (i > 1 && i >= desde && i <= desde + 100)
                {
                    dgCasoA.Rows.Add(evento, reloj, filaActual[2], filaActual[3], filaActual[4], filaActual[5], filaActual[6], filaActual[7], filaActual[8], filaActual[9], filaActual[10], filaActual[11], filaActual[12], filaActual[13], filaActual[14]);
                }
                if (i == minutos && minutos > 100)
                {
                    dgCasoA.Rows.Add(evento, reloj, filaActual[2], filaActual[3], filaActual[4], filaActual[5], filaActual[6], filaActual[7], filaActual[8], filaActual[9], filaActual[10], filaActual[11], filaActual[12], filaActual[13], filaActual[14]);
                }


            }

        }

        private int compararTiempos()
        {
            int i = 0;
            if (proximaLlegada > 0 && proximaLlegada < finCaja1 && proximaLlegada < finCaja2)
            {
                //Proximo evento es LlegadaConsulta
                i = 1;

            }
            if (finCaja1 > 0 && finCaja1 < proximaLlegada && finCaja1 < finCaja2)
            {
                //proximo evento es FinConsulta
                i = 2;
            }
            if (finCaja2 > 0 && finCaja2 < proximaLlegada && finCaja2 < finCaja1)
            {
                //proximo evento es LlegadaUrgencia
                i = 3;
            }

            return i;

        }
    }
}
