namespace SOAPAP.UI.ReportesForms
{
    partial class ReporteIBC
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPeriodoBusqueda = new System.Windows.Forms.ComboBox();
            this.cmbTypeReporte = new System.Windows.Forms.ComboBox();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.pnlHRigth = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlHLeft = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rvwReportes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            this.pnlHRigth.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.pbBG);
            this.pnlHeader.Controls.Add(this.pnlHRigth);
            this.pnlHeader.Controls.Add(this.pnlHLeft);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 118);
            this.pnlHeader.TabIndex = 47;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.56065F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.43935F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaFin, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaIni, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(217, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(396, 46);
            this.tableLayoutPanel2.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Fecha final";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(219, 22);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(166, 20);
            this.dtpFechaFin.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Fecha inicial";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(16, 22);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(175, 20);
            this.dtpFechaIni.TabIndex = 48;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.63659F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.36341F));
            this.tableLayoutPanel1.Controls.Add(this.cmbPeriodoBusqueda, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbTypeReporte, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(214, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 33);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // cmbPeriodoBusqueda
            // 
            this.cmbPeriodoBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbPeriodoBusqueda.BackColor = System.Drawing.Color.White;
            this.cmbPeriodoBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPeriodoBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPeriodoBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbPeriodoBusqueda.FormattingEnabled = true;
            this.cmbPeriodoBusqueda.Location = new System.Drawing.Point(220, 3);
            this.cmbPeriodoBusqueda.Name = "cmbPeriodoBusqueda";
            this.cmbPeriodoBusqueda.Size = new System.Drawing.Size(175, 28);
            this.cmbPeriodoBusqueda.TabIndex = 49;
            this.cmbPeriodoBusqueda.SelectionChangeCommitted += new System.EventHandler(this.cmbPeriodoBusqueda_SelectionChangeCommitted);
            // 
            // cmbTypeReporte
            // 
            this.cmbTypeReporte.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbTypeReporte.BackColor = System.Drawing.Color.White;
            this.cmbTypeReporte.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTypeReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeReporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbTypeReporte.FormattingEnabled = true;
            this.cmbTypeReporte.Location = new System.Drawing.Point(3, 3);
            this.cmbTypeReporte.Name = "cmbTypeReporte";
            this.cmbTypeReporte.Size = new System.Drawing.Size(211, 28);
            this.cmbTypeReporte.TabIndex = 48;
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(185, 62);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(454, 51);
            this.pcbIncomeByConcept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIncomeByConcept.TabIndex = 52;
            this.pcbIncomeByConcept.TabStop = false;
            // 
            // pbBG
            // 
            this.pbBG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBG.BackColor = System.Drawing.Color.Transparent;
            this.pbBG.Image = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.Location = new System.Drawing.Point(185, 7);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(454, 50);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 41;
            this.pbBG.TabStop = false;
            // 
            // pnlHRigth
            // 
            this.pnlHRigth.Controls.Add(this.panel1);
            this.pnlHRigth.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHRigth.Location = new System.Drawing.Point(619, 0);
            this.pnlHRigth.Name = "pnlHRigth";
            this.pnlHRigth.Size = new System.Drawing.Size(181, 118);
            this.pnlHRigth.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 118);
            this.panel1.TabIndex = 45;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(36, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 48);
            this.button2.TabIndex = 18;
            this.button2.Text = "GENERAR";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnlHLeft
            // 
            this.pnlHLeft.Controls.Add(this.label5);
            this.pnlHLeft.Controls.Add(this.label4);
            this.pnlHLeft.Controls.Add(this.label1);
            this.pnlHLeft.Controls.Add(this.lblTitulo);
            this.pnlHLeft.Controls.Add(this.pictureBox1);
            this.pnlHLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlHLeft.Name = "pnlHLeft";
            this.pnlHLeft.Size = new System.Drawing.Size(211, 118);
            this.pnlHLeft.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 21);
            this.label5.TabIndex = 46;
            this.label5.Text = "de conceptos";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(42, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 45;
            this.label4.Text = "Ingresos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 21);
            this.label1.TabIndex = 44;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(32, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(92, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Reportes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.reportes;
            this.pictureBox1.InitialImage = global::SOAPAP.Properties.Resources.reportes;
            this.pictureBox1.Location = new System.Drawing.Point(9, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // rvwReportes
            // 
            this.rvwReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvwReportes.Location = new System.Drawing.Point(0, 117);
            this.rvwReportes.Name = "rvwReportes";
            this.rvwReportes.Size = new System.Drawing.Size(800, 221);
            this.rvwReportes.TabIndex = 48;
            // 
            // ReporteIBC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.rvwReportes);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ReporteIBC";
            this.Text = "ReporteIBC";
            this.Load += new System.EventHandler(this.ReporteIBC_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            this.pnlHRigth.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlHLeft.ResumeLayout(false);
            this.pnlHLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbPeriodoBusqueda;
        private System.Windows.Forms.ComboBox cmbTypeReporte;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Panel pnlHRigth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlHLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Microsoft.Reporting.WinForms.ReportViewer rvwReportes;
    }
}