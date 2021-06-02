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
        private double reloj = 0.00;
        private double rndLlegada;
        private double tiempoEntreLlegada;
        private double proximaLlegada;
        private double rndPago;
        private string formaPago;
        private double rndTiempoAtencion;
        private double tiempoAtencion;
        private double finCaja1;
        private double finCaja2;
        private string estadoCaja1;
        private int colaCaja1 = 0;
        private string estadoCaja2;
        private int colaCaja2 = 0;
        private int cantTotalClientesAt = 0;
        private double tpoAcAtencionCli = 0;
        private double promTpoAtencionCli = 0;
        private double promClientesXMinuto = 0;




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
                return "Si";
            else
                return "No";
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

            IList<Object> filaActual = new Object[19];

            //evento = Convert.ToString(filaActual[0]);
            //reloj = Convert.ToDouble(filaActual[1]);
            //rndLlegada = Convert.ToDouble(filaActual[2]);
            //tiempoEntreLlegada = Convert.ToDouble(filaActual[3]);
            //proximaLlegada = Convert.ToDouble(filaActual[4]);
            //rndPago = Convert.ToDouble(filaActual[5]);
            //formaPago = Convert.ToString(filaActual[6]);
            //rndTiempoAtencion = Convert.ToDouble(filaActual[7]);
            //tiempoAtencion = Convert.ToDouble(filaActual[8]);
            //finCaja1 = Convert.ToDouble(filaActual[9]);
            //finCaja2 = Convert.ToDouble(filaActual[10]);
            //estadoCaja1 = Convert.ToString(filaActual[11]);
            //colaCaja1 = Convert.ToInt32(filaActual[12]);
            //estadoCaja2 = Convert.ToString(filaActual[13]);
            //colaCaja2 = Convert.ToInt32(filaActual[14]);
            //cantTotalClientesAt = Convert.ToInt32(filaActual[15]);
            //tpoAcAtencionCli = Convert.ToInt32(filaActual[16]);
            //promTpoAtencionCli = Convert.ToDouble(filaActual[17]);
            //promClientesXMinuto = Convert.ToDouble(filaActual[18]);

            //filaActual[0] = evento;
            //filaActual[1] = reloj;
            //filaActual[3] = tiempoEntreLlegada;
            //filaActual[4] = proximaLlegada;
            //filaActual[9] = finCaja1;
            //filaActual[10] = finCaja2;



            Random aleatorio = new Random();

            rnd = aleatorio.NextDouble();

            for(int i = 1; i <= Convert.ToInt32(txt_Minutos.Text); i++)
            {
                if(i == 1)
                {
                    filaActual[0] = "Inicio";
                    evento = Convert.ToString(filaActual[0]);
                    filaActual[1] = 0.00;
                    filaActual[2] = Math.Round(aleatorio.NextDouble(), 2);
                    rndLlegada = Convert.ToDouble(filaActual[2]);
                    filaActual[3] = Math.Round(-media * Math.Log(1 - rndLlegada), 2);
                    filaActual[4] = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[3]);
                    proximaLlegada = Convert.ToDouble(filaActual[4]);
                    //filaActual[5] = 0;
                    filaActual[6] = "";
                    //filaActual[7] = 0;
                    //filaActual[8] = 0;
                    filaActual[9] = 0;
                    finCaja1 = Convert.ToDouble(filaActual[9]);
                    filaActual[10] = 0;
                    finCaja2 = Convert.ToDouble(filaActual[10]);
                    filaActual[11] = "Libre";
                    filaActual[12] = 0;
                    filaActual[13] = "Libre";
                    filaActual[14] = 0;
                    filaActual[15] = 0;
                    filaActual[16] = 0;
                    filaActual[17] = 0;
                    filaActual[18] = 0;

                }

                

                if (i > 1)
                {

                    if (finCaja1 == 0 && finCaja2 == 0) /*&& estadoCaja1 == "Libre" && colaCaja1 < 5)*/
                    {
                        filaActual[0] = "llegada_cliente";
                        evento = Convert.ToString(filaActual[0]);
                        filaActual[1] = Convert.ToDouble(filaActual[4]);
                        filaActual[2] = Math.Round(aleatorio.NextDouble(), 2);
                        rndLlegada = Convert.ToDouble(filaActual[2]);
                        filaActual[3] = Math.Round(-media * Math.Log(1 - rndLlegada), 2);
                        filaActual[4] = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[3]);
                        proximaLlegada = Convert.ToDouble(filaActual[4]);
                        //veo si paga con tarjeta
                        filaActual[5] = Math.Round(aleatorio.NextDouble(), 2);
                        rndPago = Convert.ToDouble(filaActual[5]);
                        filaActual[6] = FormaPago(rndPago);
                        formaPago = Convert.ToString(filaActual[6]);
                        //calculo tiempo atencion
                        filaActual[7] = Math.Round(aleatorio.NextDouble(), 2);
                        rndTiempoAtencion = Convert.ToDouble(filaActual[7]);
                        filaActual[8] = (finAtencionB - finAtencionA) * rndTiempoAtencion + finAtencionA;

                        if (formaPago == "Si")
                        {
                            //rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                            filaActual[8] = Convert.ToDouble(filaActual[8]) + 2;
                        }
                        else
                        {
                            filaActual[8] = Convert.ToDouble(filaActual[8]);
                        }
                        filaActual[9] = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[8]); 
                        finCaja1 = Convert.ToDouble(filaActual[9]);
                        filaActual[10] = "";
                        finCaja2 = Convert.ToDouble(filaActual[10]);
                        filaActual[11] = "Ocupada";
                        filaActual[12] = 0;
                        filaActual[13] = "Cerrada";
                        filaActual[14] = 0;
                        filaActual[15] = 0;
                        filaActual[16] = 0;
                        filaActual[17] = 0;
                        filaActual[18] = 0;


                    }
                    if ( finCaja1!= 0 && finCaja2 == 0 && finCaja1 < proximaLlegada)
                    {
                        filaActual[0] = "finCaja1";
                        filaActual[1] = filaActual[9];
                        filaActual[2] = "";
                        filaActual[3] = "";
                        filaActual[4] = "";
                        //veo si paga con tarjeta
                        filaActual[5] = Math.Round(aleatorio.NextDouble(), 2);
                        rndPago = Convert.ToDouble(filaActual[5]);
                        filaActual[6] = FormaPago(rndPago);
                        formaPago = Convert.ToString(filaActual[6]);
                        //calculo tiempo atencion
                        filaActual[7] = Math.Round(aleatorio.NextDouble(), 2);
                        rndTiempoAtencion = Convert.ToDouble(filaActual[7]);
                        filaActual[8] = (finAtencionB - finAtencionA) * rndTiempoAtencion + finAtencionA;

                        if (formaPago == "Si")
                        {
                            //rndTiempoAtencion = Math.Round(aleatorio.NextDouble(), 2);
                            filaActual[8] = Convert.ToDouble(filaActual[8]) + 2;
                        }
                        else
                        {
                            filaActual[8] = Convert.ToDouble(filaActual[8]);
                        }
                        filaActual[9] = Convert.ToDouble(filaActual[1]) + Convert.ToDouble(filaActual[8]);
                        finCaja1 = Convert.ToDouble(filaActual[9]);
                        filaActual[10] = 0;
                        finCaja2 = Convert.ToDouble(filaActual[10]);
                        filaActual[11] = "Ocupada";
                        filaActual[12] = 0;
                        filaActual[13] = "Cerrada";
                        filaActual[14] = 0;
                        filaActual[15] = 0;
                        filaActual[16] = 0;
                        filaActual[17] = 0;
                        filaActual[18] = 0;
                    }




                }


                if (i >= 1 && i >= desde && i <= desde + 100)
                {
                    dgCasoA.Rows.Add(filaActual[0], filaActual[1], filaActual[2], filaActual[3], filaActual[4], filaActual[5], filaActual[6], filaActual[7], filaActual[8], finCaja1, finCaja2, filaActual[11], filaActual[12], filaActual[13], filaActual[14], filaActual[15], filaActual[16], filaActual[17], filaActual[18]);
                }
                if (i == minutos && minutos > 100)
                {
                    dgCasoA.Rows.Add(filaActual[0], filaActual[1], filaActual[2], filaActual[3], filaActual[4], filaActual[5], filaActual[6], filaActual[7], filaActual[8], filaActual[9], filaActual[10], filaActual[11], filaActual[12], filaActual[13], filaActual[14], filaActual[15], filaActual[16], filaActual[17], filaActual[18]);
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
