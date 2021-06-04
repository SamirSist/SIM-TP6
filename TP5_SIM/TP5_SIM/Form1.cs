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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_casoA_Click(object sender, EventArgs e)
        {
            CasoA1 ca = new CasoA1();
            ca.Show();

        }

        private void btn_casoB_Click(object sender, EventArgs e)
        {
            CasoB1 cb = new CasoB1();
            cb.Show();
        }
    }
}
