namespace SOAPAP
{
    partial class TerminalFront
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbBranchOffice = new System.Windows.Forms.ComboBox();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pbCubo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMacAdress = new System.Windows.Forms.Label();
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbxSucursal = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tlpTerminal = new System.Windows.Forms.TableLayoutPanel();
            this.gboxFondo = new System.Windows.Forms.GroupBox();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCubo)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlTitulo.SuspendLayout();
            this.gbxSucursal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tlpTerminal.SuspendLayout();
            this.gboxFondo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(125, 147);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(137, 40);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbBranchOffice
            // 
            this.cmbBranchOffice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranchOffice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbBranchOffice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranchOffice.FormattingEnabled = true;
            this.cmbBranchOffice.Location = new System.Drawing.Point(6, 18);
            this.cmbBranchOffice.Name = "cmbBranchOffice";
            this.cmbBranchOffice.Size = new System.Drawing.Size(212, 28);
            this.cmbBranchOffice.TabIndex = 47;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtitulo.Location = new System.Drawing.Point(0, 75);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(746, 93);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "La MAC ADRESS de este equipo en la red,\r\nque se dará de alta";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.pbCubo);
            this.pnlHeader.Controls.Add(this.lblSubtitulo);
            this.pnlHeader.Controls.Add(this.panel1);
            this.pnlHeader.Controls.Add(this.pnlTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(746, 216);
            this.pnlHeader.TabIndex = 15;
            // 
            // pbCubo
            // 
            this.pbCubo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbCubo.Image = global::SOAPAP.Properties.Resources.cubo_alfa;
            this.pbCubo.Location = new System.Drawing.Point(0, 75);
            this.pbCubo.Name = "pbCubo";
            this.pbCubo.Size = new System.Drawing.Size(184, 93);
            this.pbCubo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCubo.TabIndex = 49;
            this.pbCubo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMacAdress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(746, 48);
            this.panel1.TabIndex = 12;
            // 
            // lblMacAdress
            // 
            this.lblMacAdress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMacAdress.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMacAdress.ForeColor = System.Drawing.Color.White;
            this.lblMacAdress.Location = new System.Drawing.Point(0, 0);
            this.lblMacAdress.Name = "lblMacAdress";
            this.lblMacAdress.Size = new System.Drawing.Size(746, 48);
            this.lblMacAdress.TabIndex = 49;
            this.lblMacAdress.Text = "###";
            this.lblMacAdress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(746, 75);
            this.pnlTitulo.TabIndex = 11;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(746, 75);
            this.lblTitulo.TabIndex = 10;
            this.lblTitulo.Text = "ALTA DE TERMINAL";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxSucursal
            // 
            this.gbxSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxSucursal.Controls.Add(this.pictureBox4);
            this.gbxSucursal.Controls.Add(this.cmbBranchOffice);
            this.gbxSucursal.Location = new System.Drawing.Point(10, 3);
            this.gbxSucursal.Name = "gbxSucursal";
            this.gbxSucursal.Size = new System.Drawing.Size(252, 55);
            this.gbxSucursal.TabIndex = 27;
            this.gbxSucursal.TabStop = false;
            this.gbxSucursal.Text = "SUCURSAL";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::SOAPAP.Properties.Resources.store;
            this.pictureBox4.Location = new System.Drawing.Point(224, 20);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 23);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 27;
            this.pictureBox4.TabStop = false;
            // 
            // tlpTerminal
            // 
            this.tlpTerminal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpTerminal.ColumnCount = 1;
            this.tlpTerminal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.65149F));
            this.tlpTerminal.Controls.Add(this.gboxFondo, 0, 1);
            this.tlpTerminal.Controls.Add(this.gbxSucursal, 0, 0);
            this.tlpTerminal.Controls.Add(this.btnGuardar, 0, 2);
            this.tlpTerminal.Location = new System.Drawing.Point(231, 6);
            this.tlpTerminal.Name = "tlpTerminal";
            this.tlpTerminal.RowCount = 3;
            this.tlpTerminal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTerminal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpTerminal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpTerminal.Size = new System.Drawing.Size(265, 192);
            this.tlpTerminal.TabIndex = 14;
            // 
            // gboxFondo
            // 
            this.gboxFondo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxFondo.Controls.Add(this.nudAmount);
            this.gboxFondo.Controls.Add(this.pictureBox1);
            this.gboxFondo.Location = new System.Drawing.Point(10, 76);
            this.gboxFondo.Name = "gboxFondo";
            this.gboxFondo.Size = new System.Drawing.Size(252, 55);
            this.gboxFondo.TabIndex = 48;
            this.gboxFondo.TabStop = false;
            this.gboxFondo.Text = "MAXIMO FONDO DE CAJA";
            // 
            // nudAmount
            // 
            this.nudAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAmount.Location = new System.Drawing.Point(6, 20);
            this.nudAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(212, 26);
            this.nudAmount.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.money;
            this.pictureBox1.Location = new System.Drawing.Point(224, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tlpTerminal);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 216);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(746, 201);
            this.pnlContent.TabIndex = 16;
            // 
            // TerminalFront
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(746, 417);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Name = "TerminalFront";
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCubo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlTitulo.ResumeLayout(false);
            this.gbxSucursal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tlpTerminal.ResumeLayout(false);
            this.gboxFondo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.ComboBox cmbBranchOffice;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblMacAdress;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pbCubo;
        private System.Windows.Forms.GroupBox gbxSucursal;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TableLayoutPanel tlpTerminal;
        private System.Windows.Forms.GroupBox gboxFondo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Panel pnlContent;
    }
}