namespace SOAPAP.UI.Messages
{
    partial class msgObservacionFactura
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxMensage = new System.Windows.Forms.TextBox();
            this.cbxUsoCFDI = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chbxEnviarCorreo = new System.Windows.Forms.CheckBox();
            this.tbxCorreo = new System.Windows.Forms.TextBox();
            this.lblMensajeCorreo = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(652, 44);
            this.pnlHeader.TabIndex = 44;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(141, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Observaciones";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Ingrese una observación para la factura:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbxMensage
            // 
            this.tbxMensage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMensage.Location = new System.Drawing.Point(12, 123);
            this.tbxMensage.Multiline = true;
            this.tbxMensage.Name = "tbxMensage";
            this.tbxMensage.Size = new System.Drawing.Size(628, 111);
            this.tbxMensage.TabIndex = 46;
            this.tbxMensage.TextChanged += new System.EventHandler(this.tbxMensage_TextChanged);
            // 
            // cbxUsoCFDI
            // 
            this.cbxUsoCFDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUsoCFDI.FormattingEnabled = true;
            this.cbxUsoCFDI.Location = new System.Drawing.Point(17, 72);
            this.cbxUsoCFDI.Name = "cbxUsoCFDI";
            this.cbxUsoCFDI.Size = new System.Drawing.Size(184, 23);
            this.cbxUsoCFDI.TabIndex = 48;
            this.cbxUsoCFDI.SelectedIndexChanged += new System.EventHandler(this.cbxUsoCFDI_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Tipo de uso CFDI:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(541, 279);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(97, 31);
            this.btnAceptar.TabIndex = 50;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chbxEnviarCorreo
            // 
            this.chbxEnviarCorreo.AutoSize = true;
            this.chbxEnviarCorreo.Location = new System.Drawing.Point(19, 252);
            this.chbxEnviarCorreo.Name = "chbxEnviarCorreo";
            this.chbxEnviarCorreo.Size = new System.Drawing.Size(101, 17);
            this.chbxEnviarCorreo.TabIndex = 51;
            this.chbxEnviarCorreo.Text = "Enviar correo a:";
            this.chbxEnviarCorreo.UseVisualStyleBackColor = true;
            this.chbxEnviarCorreo.Visible = false;
            // 
            // tbxCorreo
            // 
            this.tbxCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCorreo.Location = new System.Drawing.Point(15, 272);
            this.tbxCorreo.Name = "tbxCorreo";
            this.tbxCorreo.Size = new System.Drawing.Size(347, 23);
            this.tbxCorreo.TabIndex = 52;
            this.tbxCorreo.Visible = false;
            // 
            // lblMensajeCorreo
            // 
            this.lblMensajeCorreo.AutoSize = true;
            this.lblMensajeCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeCorreo.ForeColor = System.Drawing.Color.Red;
            this.lblMensajeCorreo.Location = new System.Drawing.Point(116, 251);
            this.lblMensajeCorreo.Name = "lblMensajeCorreo";
            this.lblMensajeCorreo.Size = new System.Drawing.Size(143, 15);
            this.lblMensajeCorreo.TabIndex = 53;
            this.lblMensajeCorreo.Text = "Debe ingresar un correo.";
            this.lblMensajeCorreo.Visible = false;
            // 
            // msgObservacionFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 317);
            this.ControlBox = false;
            this.Controls.Add(this.lblMensajeCorreo);
            this.Controls.Add(this.tbxCorreo);
            this.Controls.Add(this.chbxEnviarCorreo);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxUsoCFDI);
            this.Controls.Add(this.tbxMensage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "msgObservacionFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "msgObservacionFactura";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxMensage;
        private System.Windows.Forms.ComboBox cbxUsoCFDI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.CheckBox chbxEnviarCorreo;
        public System.Windows.Forms.TextBox tbxCorreo;
        private System.Windows.Forms.Label lblMensajeCorreo;
    }
}