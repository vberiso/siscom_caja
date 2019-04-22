namespace SOAPAP.UI.ReportesForms
{
    partial class RepCollection
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
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxOperador = new System.Windows.Forms.ComboBox();
            this.pnlHRigth = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.pnlHLeft = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pgcCollection = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pgfDescripcion = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSubtotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDescuento = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.dataCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlHRigth.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgcCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCollectionBindingSource)).BeginInit();
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
            this.pnlHeader.Size = new System.Drawing.Size(800, 124);
            this.pnlHeader.TabIndex = 49;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.38596F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.61404F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaFin, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaIni, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(215, 64);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(399, 46);
            this.tableLayoutPanel2.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 3);
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
            this.dtpFechaFin.Location = new System.Drawing.Point(224, 22);
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
            this.dtpFechaIni.Location = new System.Drawing.Point(13, 22);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(190, 20);
            this.dtpFechaIni.TabIndex = 48;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.63659F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.36341F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxOperador, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(214, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 45);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(220, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Seleccione el operador:";
            // 
            // cbxOperador
            // 
            this.cbxOperador.FormattingEnabled = true;
            this.cbxOperador.Location = new System.Drawing.Point(220, 19);
            this.cbxOperador.Name = "cbxOperador";
            this.cbxOperador.Size = new System.Drawing.Size(176, 21);
            this.cbxOperador.TabIndex = 54;
            // 
            // pnlHRigth
            // 
            this.pnlHRigth.Controls.Add(this.panel1);
            this.pnlHRigth.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHRigth.Location = new System.Drawing.Point(619, 0);
            this.pnlHRigth.Name = "pnlHRigth";
            this.pnlHRigth.Size = new System.Drawing.Size(181, 124);
            this.pnlHRigth.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Controls.Add(this.btnGenerar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 124);
            this.panel1.TabIndex = 45;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.White;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(36, 67);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(133, 48);
            this.btnExportar.TabIndex = 19;
            this.btnExportar.Text = "EXPORTAR";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.BackColor = System.Drawing.Color.White;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnGenerar.Location = new System.Drawing.Point(36, 13);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(133, 48);
            this.btnGenerar.TabIndex = 18;
            this.btnGenerar.Text = "GENERAR";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // pnlHLeft
            // 
            this.pnlHLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHLeft.Controls.Add(this.label5);
            this.pnlHLeft.Controls.Add(this.label1);
            this.pnlHLeft.Controls.Add(this.lblTitulo);
            this.pnlHLeft.Controls.Add(this.pictureBox1);
            this.pnlHLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlHLeft.Name = "pnlHLeft";
            this.pnlHLeft.Size = new System.Drawing.Size(211, 144);
            this.pnlHLeft.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(18, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 21);
            this.label5.TabIndex = 45;
            this.label5.Text = "Recaudación";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // pgcCollection
            // 
            this.pgcCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcCollection.DataSource = this.dataCollectionBindingSource;
            this.pgcCollection.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfDescripcion,
            this.pgfSubtotal,
            this.pgfDescuento,
            this.pgfTotal});
            this.pgcCollection.Location = new System.Drawing.Point(0, 121);
            this.pgcCollection.Name = "pgcCollection";
            this.pgcCollection.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcCollection.Size = new System.Drawing.Size(800, 295);
            this.pgcCollection.TabIndex = 50;
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(188, 62);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(451, 51);
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
            // pgfDescripcion
            // 
            this.pgfDescripcion.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDescripcion.AreaIndex = 0;
            this.pgfDescripcion.Caption = "CONCEPTO";
            this.pgfDescripcion.FieldName = "DESCRIPCION";
            this.pgfDescripcion.Name = "pgfDescripcion";
            this.pgfDescripcion.Width = 491;
            // 
            // pgfSubtotal
            // 
            this.pgfSubtotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSubtotal.AreaIndex = 0;
            this.pgfSubtotal.Caption = "SUBTOTAL";
            this.pgfSubtotal.FieldName = "SUBTOTAL";
            this.pgfSubtotal.Name = "pgfSubtotal";
            // 
            // pgfDescuento
            // 
            this.pgfDescuento.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfDescuento.AreaIndex = 1;
            this.pgfDescuento.Caption = "DESCUENTO";
            this.pgfDescuento.FieldName = "DESCUENTO";
            this.pgfDescuento.Name = "pgfDescuento";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 2;
            this.pgfTotal.Caption = "TOTAL";
            this.pgfTotal.FieldName = "TOTAL";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // dataCollectionBindingSource
            // 
            this.dataCollectionBindingSource.DataSource = typeof(SOAPAP.Reportes.DataCollection);
            // 
            // RepCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pgcCollection);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepCollection";
            this.Text = "RepCollection";
            this.Load += new System.EventHandler(this.RepCollection_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlHRigth.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlHLeft.ResumeLayout(false);
            this.pnlHLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgcCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCollectionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxOperador;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Panel pnlHRigth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Panel pnlHLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcCollection;
        private System.Windows.Forms.BindingSource dataCollectionBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDescripcion;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSubtotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDescuento;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
    }
}