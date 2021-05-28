
namespace TP5_SIM
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_casoA = new System.Windows.Forms.Button();
            this.btn_casoB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_casoA
            // 
            this.btn_casoA.Location = new System.Drawing.Point(193, 67);
            this.btn_casoA.Name = "btn_casoA";
            this.btn_casoA.Size = new System.Drawing.Size(249, 85);
            this.btn_casoA.TabIndex = 0;
            this.btn_casoA.Text = "CASO A";
            this.btn_casoA.UseVisualStyleBackColor = true;
            this.btn_casoA.Click += new System.EventHandler(this.btn_casoA_Click);
            // 
            // btn_casoB
            // 
            this.btn_casoB.Location = new System.Drawing.Point(193, 223);
            this.btn_casoB.Name = "btn_casoB";
            this.btn_casoB.Size = new System.Drawing.Size(249, 83);
            this.btn_casoB.TabIndex = 1;
            this.btn_casoB.Text = "CASO B";
            this.btn_casoB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 402);
            this.Controls.Add(this.btn_casoB);
            this.Controls.Add(this.btn_casoA);
            this.Name = "Form1";
            this.Text = "TP5-SIM";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_casoA;
        private System.Windows.Forms.Button btn_casoB;
    }
}

