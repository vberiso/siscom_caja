namespace SOAPAP.UI.ReportesForms
{
    partial class RepPadronWater
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
            this.btnExportar = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chxPorFechaContrato = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbxServicio = new System.Windows.Forms.ListBox();
            this.lbxColonia = new System.Windows.Forms.ListBox();
            this.lbxRuta = new System.Windows.Forms.ListBox();
            this.lbxTipoToma = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pgcRepPadronWater = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pgfCuenta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfNombre = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDomicilio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfColonia = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRuta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfContrato = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfEstatus = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfAdDesde = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfAdHasta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfAdeudo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfPago = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTipoToma = new DevExpress.XtraPivotGrid.PivotGridField();
            this.dataPadronWaterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcRepPadronWater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPadronWaterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.btnExportar);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel4);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel3);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.btnGenerar);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Location = new System.Drawing.Point(1, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 259);
            this.pnlHeader.TabIndex = 49;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.White;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(655, 58);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(133, 41);
            this.btnExportar.TabIndex = 56;
            this.btnExportar.Text = "EXPORTAR";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.numericUpDown1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(15, 211);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(770, 45);
            this.tableLayoutPanel4.TabIndex = 55;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(3, 21);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(179, 20);
            this.numericUpDown1.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Adeudo mayor a:";
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(208, 37);
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.82578F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.17422F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel3.Controls.Add(this.chxPorFechaContrato, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(212, 9);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(393, 25);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // chxPorFechaContrato
            // 
            this.chxPorFechaContrato.AutoSize = true;
            this.chxPorFechaContrato.Checked = true;
            this.chxPorFechaContrato.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxPorFechaContrato.Location = new System.Drawing.Point(3, 3);
            this.chxPorFechaContrato.Name = "chxPorFechaContrato";
            this.chxPorFechaContrato.Size = new System.Drawing.Size(156, 17);
            this.chxPorFechaContrato.TabIndex = 1;
            this.chxPorFechaContrato.Text = "Filtrar por fecha de contrato";
            this.chxPorFechaContrato.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lbxServicio, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbxColonia, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbxRuta, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbxTipoToma, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 105);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 100);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // lbxServicio
            // 
            this.lbxServicio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxServicio.FormattingEnabled = true;
            this.lbxServicio.Location = new System.Drawing.Point(389, 21);
            this.lbxServicio.Name = "lbxServicio";
            this.lbxServicio.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxServicio.Size = new System.Drawing.Size(187, 69);
            this.lbxServicio.TabIndex = 57;
            // 
            // lbxColonia
            // 
            this.lbxColonia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxColonia.FormattingEnabled = true;
            this.lbxColonia.Location = new System.Drawing.Point(196, 21);
            this.lbxColonia.Name = "lbxColonia";
            this.lbxColonia.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxColonia.Size = new System.Drawing.Size(187, 69);
            this.lbxColonia.TabIndex = 56;
            // 
            // lbxRuta
            // 
            this.lbxRuta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxRuta.FormattingEnabled = true;
            this.lbxRuta.Location = new System.Drawing.Point(3, 21);
            this.lbxRuta.Name = "lbxRuta";
            this.lbxRuta.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxRuta.Size = new System.Drawing.Size(187, 69);
            this.lbxRuta.TabIndex = 55;
            // 
            // lbxTipoToma
            // 
            this.lbxTipoToma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxTipoToma.FormattingEnabled = true;
            this.lbxTipoToma.Location = new System.Drawing.Point(582, 21);
            this.lbxTipoToma.Name = "lbxTipoToma";
            this.lbxTipoToma.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxTipoToma.Size = new System.Drawing.Size(188, 69);
            this.lbxTipoToma.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Seleccione la(s) colonia(s):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Seleccione la(s) ruta(s):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(389, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Estatus del servicio:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(582, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Tipo de toma:";
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(182, 35);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(451, 54);
            this.pcbIncomeByConcept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIncomeByConcept.TabIndex = 52;
            this.pcbIncomeByConcept.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 21);
            this.label5.TabIndex = 45;
            this.label5.Text = "Padron de agua";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.BackColor = System.Drawing.Color.White;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnGenerar.Location = new System.Drawing.Point(655, 12);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(133, 41);
            this.btnGenerar.TabIndex = 18;
            this.btnGenerar.Text = "GENERAR";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 12);
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
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // pgcRepPadronWater
            // 
            this.pgcRepPadronWater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcRepPadronWater.DataSource = this.dataPadronWaterBindingSource;
            this.pgcRepPadronWater.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfCuenta,
            this.pgfNombre,
            this.pgfDomicilio,
            this.pgfColonia,
            this.pgfRuta,
            this.pgfContrato,
            this.pgfEstatus,
            this.pgfAdDesde,
            this.pgfAdHasta,
            this.pgfAdeudo,
            this.pgfPago,
            this.pgfTipoToma});
            this.pgcRepPadronWater.Location = new System.Drawing.Point(1, 258);
            this.pgcRepPadronWater.Name = "pgcRepPadronWater";
            this.pgcRepPadronWater.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcRepPadronWater.Size = new System.Drawing.Size(800, 161);
            this.pgcRepPadronWater.TabIndex = 50;
            // 
            // pgfCuenta
            // 
            this.pgfCuenta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCuenta.AreaIndex = 0;
            this.pgfCuenta.Caption = "CUENTA";
            this.pgfCuenta.FieldName = "CUENTA";
            this.pgfCuenta.Name = "pgfCuenta";
            // 
            // pgfNombre
            // 
            this.pgfNombre.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfNombre.AreaIndex = 1;
            this.pgfNombre.Caption = "NOMBRE";
            this.pgfNombre.FieldName = "NOMBRE";
            this.pgfNombre.Name = "pgfNombre";
            // 
            // pgfDomicilio
            // 
            this.pgfDomicilio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDomicilio.AreaIndex = 2;
            this.pgfDomicilio.Caption = "DOMICILIO";
            this.pgfDomicilio.FieldName = "DOMICILIO";
            this.pgfDomicilio.Name = "pgfDomicilio";
            // 
            // pgfColonia
            // 
            this.pgfColonia.AreaIndex = 1;
            this.pgfColonia.Caption = "COLONIA";
            this.pgfColonia.FieldName = "COLONIA";
            this.pgfColonia.Name = "pgfColonia";
            // 
            // pgfRuta
            // 
            this.pgfRuta.AreaIndex = 0;
            this.pgfRuta.Caption = "RUTA";
            this.pgfRuta.FieldName = "RUTA";
            this.pgfRuta.Name = "pgfRuta";
            // 
            // pgfContrato
            // 
            this.pgfContrato.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfContrato.AreaIndex = 3;
            this.pgfContrato.Caption = "CONTRATO";
            this.pgfContrato.FieldName = "CONTRATO";
            this.pgfContrato.Name = "pgfContrato";
            // 
            // pgfEstatus
            // 
            this.pgfEstatus.AreaIndex = 2;
            this.pgfEstatus.Caption = "ESTATUS";
            this.pgfEstatus.FieldName = "ESTATUS";
            this.pgfEstatus.Name = "pgfEstatus";
            // 
            // pgfAdDesde
            // 
            this.pgfAdDesde.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfAdDesde.AreaIndex = 4;
            this.pgfAdDesde.Caption = "AD DESDE";
            this.pgfAdDesde.FieldName = "AD_DESDE";
            this.pgfAdDesde.Name = "pgfAdDesde";
            // 
            // pgfAdHasta
            // 
            this.pgfAdHasta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfAdHasta.AreaIndex = 5;
            this.pgfAdHasta.Caption = "AD HASTA";
            this.pgfAdHasta.FieldName = "AD_HASTA";
            this.pgfAdHasta.Name = "pgfAdHasta";
            // 
            // pgfAdeudo
            // 
            this.pgfAdeudo.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfAdeudo.AreaIndex = 1;
            this.pgfAdeudo.Caption = "ADEUDO";
            this.pgfAdeudo.FieldName = "ADEUDO";
            this.pgfAdeudo.Name = "pgfAdeudo";
            // 
            // pgfPago
            // 
            this.pgfPago.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfPago.AreaIndex = 0;
            this.pgfPago.Caption = "ULTIMO PAGO";
            this.pgfPago.FieldName = "CONTRATO_PAGO";
            this.pgfPago.Name = "pgfPago";
            // 
            // pgfTipoToma
            // 
            this.pgfTipoToma.AreaIndex = 3;
            this.pgfTipoToma.Caption = "TIPO TOMA";
            this.pgfTipoToma.FieldName = "TIPO_TOMA";
            this.pgfTipoToma.Name = "pgfTipoToma";
            // 
            // dataPadronWaterBindingSource
            // 
            this.dataPadronWaterBindingSource.DataSource = typeof(SOAPAP.Reportes.DataPadronWater);
            // 
            // RepPadronWater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pgcRepPadronWater);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepPadronWater";
            this.Text = "RepPadronWater";
            this.Load += new System.EventHandler(this.RepPadronWater_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcRepPadronWater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPadronWaterBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox chxPorFechaContrato;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lbxServicio;
        private System.Windows.Forms.ListBox lbxColonia;
        private System.Windows.Forms.ListBox lbxRuta;
        private System.Windows.Forms.ListBox lbxTipoToma;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExportar;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcRepPadronWater;
        private System.Windows.Forms.BindingSource dataPadronWaterBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCuenta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfNombre;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDomicilio;
        private DevExpress.XtraPivotGrid.PivotGridField pgfColonia;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRuta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfContrato;
        private DevExpress.XtraPivotGrid.PivotGridField pgfEstatus;
        private DevExpress.XtraPivotGrid.PivotGridField pgfAdDesde;
        private DevExpress.XtraPivotGrid.PivotGridField pgfAdHasta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfAdeudo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfPago;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTipoToma;
    }
}