namespace SOAPAP.UI.FacturacionAnticipada
{
    partial class PeriodosAnticipados
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSimular = new System.Windows.Forms.Button();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMSI = new System.Windows.Forms.Label();
            this.checkPaymentTarget = new System.Windows.Forms.CheckBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lbldescuentoT = new System.Windows.Forms.Label();
            this.lblTextAnual = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textDescripcion = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboMesFin2 = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMesInicio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(23, 73);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Mes inicio";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 46);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.cobro;
            this.pictureBox1.Location = new System.Drawing.Point(222, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Periodos anticipados";
            // 
            // btnSimular
            // 
            this.btnSimular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnSimular.FlatAppearance.BorderSize = 0;
            this.btnSimular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimular.ForeColor = System.Drawing.Color.White;
            this.btnSimular.Location = new System.Drawing.Point(132, 351);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(96, 27);
            this.btnSimular.TabIndex = 4;
            this.btnSimular.Text = "Simular";
            this.btnSimular.UseVisualStyleBackColor = false;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.Green;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.Location = new System.Drawing.Point(254, 351);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(96, 27);
            this.btnGenerar.TabIndex = 4;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblMSI);
            this.panel2.Controls.Add(this.checkPaymentTarget);
            this.panel2.Controls.Add(this.lblDescuento);
            this.panel2.Controls.Add(this.lbldescuentoT);
            this.panel2.Controls.Add(this.lblTextAnual);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textDescripcion);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.comboMesFin2);
            this.panel2.Controls.Add(this.lblYear);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboMesInicio);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSimular);
            this.panel2.Controls.Add(this.btnGenerar);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 391);
            this.panel2.TabIndex = 5;
            // 
            // lblMSI
            // 
            this.lblMSI.ForeColor = System.Drawing.Color.Brown;
            this.lblMSI.Location = new System.Drawing.Point(158, 185);
            this.lblMSI.Name = "lblMSI";
            this.lblMSI.Size = new System.Drawing.Size(183, 40);
            this.lblMSI.TabIndex = 23;
            this.lblMSI.Text = "Con la promoción a MSI \r\nel descuento no aplica.\r\n\r\n";
            this.lblMSI.Visible = false;
            // 
            // checkPaymentTarget
            // 
            this.checkPaymentTarget.AutoSize = true;
            this.checkPaymentTarget.Location = new System.Drawing.Point(28, 199);
            this.checkPaymentTarget.Name = "checkPaymentTarget";
            this.checkPaymentTarget.Size = new System.Drawing.Size(124, 23);
            this.checkPaymentTarget.TabIndex = 22;
            this.checkPaymentTarget.Text = "Promoción MSI";
            this.checkPaymentTarget.UseVisualStyleBackColor = true;
            this.checkPaymentTarget.Visible = false;
            this.checkPaymentTarget.Click += new System.EventHandler(this.checkPaymentTarget_Click);
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.Location = new System.Drawing.Point(148, 144);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(41, 21);
            this.lblDescuento.TabIndex = 21;
            this.lblDescuento.Text = "Año";
            this.lblDescuento.Visible = false;
            // 
            // lbldescuentoT
            // 
            this.lbldescuentoT.AutoSize = true;
            this.lbldescuentoT.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldescuentoT.Location = new System.Drawing.Point(23, 139);
            this.lbldescuentoT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldescuentoT.Name = "lbldescuentoT";
            this.lbldescuentoT.Size = new System.Drawing.Size(117, 26);
            this.lbldescuentoT.TabIndex = 20;
            this.lbldescuentoT.Text = "Descuento:";
            this.lbldescuentoT.Visible = false;
            // 
            // lblTextAnual
            // 
            this.lblTextAnual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTextAnual.Location = new System.Drawing.Point(28, 62);
            this.lblTextAnual.Name = "lblTextAnual";
            this.lblTextAnual.Size = new System.Drawing.Size(313, 66);
            this.lblTextAnual.TabIndex = 19;
            this.lblTextAnual.Text = "En este apartado se va a facturar una deuda de 12 recibos, contemplando el mes de" +
    " enero hasta el mes de Diciembre del corriente año.";
            this.lblTextAnual.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 225);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 26);
            this.label4.TabIndex = 18;
            this.label4.Text = "Observaciones:";
            // 
            // textDescripcion
            // 
            this.textDescripcion.Location = new System.Drawing.Point(17, 254);
            this.textDescripcion.Multiline = true;
            this.textDescripcion.Name = "textDescripcion";
            this.textDescripcion.Size = new System.Drawing.Size(341, 88);
            this.textDescripcion.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Brown;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(11, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 27);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboMesFin2
            // 
            this.comboMesFin2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMesFin2.FormattingEnabled = true;
            this.comboMesFin2.Location = new System.Drawing.Point(181, 112);
            this.comboMesFin2.Name = "comboMesFin2";
            this.comboMesFin2.Size = new System.Drawing.Size(121, 27);
            this.comboMesFin2.TabIndex = 10;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(86, 172);
            this.lblYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(41, 21);
            this.lblYear.TabIndex = 9;
            this.lblYear.Text = "Año";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 169);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Año:";
            // 
            // comboMesInicio
            // 
            this.comboMesInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMesInicio.FormattingEnabled = true;
            this.comboMesInicio.Location = new System.Drawing.Point(28, 112);
            this.comboMesInicio.Name = "comboMesInicio";
            this.comboMesInicio.Size = new System.Drawing.Size(121, 27);
            this.comboMesInicio.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(195, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mes fin";
            // 
            // PeriodosAnticipados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(363, 391);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PeriodosAnticipados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PeriodosAnticipados_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboMesInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboMesFin2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDescripcion;
        private System.Windows.Forms.Label lblTextAnual;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lbldescuentoT;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.CheckBox checkPaymentTarget;
        private System.Windows.Forms.Label lblMSI;
    }
}