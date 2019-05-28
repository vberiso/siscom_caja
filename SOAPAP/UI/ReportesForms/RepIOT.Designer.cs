namespace SOAPAP.UI.ReportesForms
{
    partial class RepIOT
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
            this.chceMostrarDetalle = new DevExpress.XtraEditors.CheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chcbxArea = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chcbxOperador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pgdIOT = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pgfFolio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfNombre = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFecha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStatus = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfArea = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCajero = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDetalle = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMonto = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDesc = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSub = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfIva = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotaDt = new DevExpress.XtraPivotGrid.PivotGridField();
            this.dataIncomeOfTreasuryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chceMostrarDetalle.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgdIOT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeOfTreasuryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.chceMostrarDetalle);
            this.pnlHeader.Controls.Add(this.panel1);
            this.pnlHeader.Controls.Add(this.btnExportar);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.btnGenerar);
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.pbBG);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(935, 143);
            this.pnlHeader.TabIndex = 49;
            // 
            // chceMostrarDetalle
            // 
            this.chceMostrarDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chceMostrarDetalle.Location = new System.Drawing.Point(596, 119);
            this.chceMostrarDetalle.Name = "chceMostrarDetalle";
            this.chceMostrarDetalle.Properties.Caption = "Mostrar Detalle";
            this.chceMostrarDetalle.Size = new System.Drawing.Size(110, 19);
            this.chceMostrarDetalle.TabIndex = 54;
            this.chceMostrarDetalle.CheckedChanged += new System.EventHandler(this.chceMostrarDetalle_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 403);
            this.panel1.TabIndex = 50;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.White;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(780, 54);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(133, 39);
            this.btnExportar.TabIndex = 19;
            this.btnExportar.Text = "EXPORTAR";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaIni, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFechaFin, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(281, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(399, 46);
            this.tableLayoutPanel2.TabIndex = 53;
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
            this.dtpFechaIni.Location = new System.Drawing.Point(7, 22);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(185, 20);
            this.dtpFechaIni.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 3);
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
            this.dtpFechaFin.Location = new System.Drawing.Point(206, 22);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(186, 20);
            this.dtpFechaFin.TabIndex = 49;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.BackColor = System.Drawing.Color.White;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnGenerar.Location = new System.Drawing.Point(780, 9);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(133, 39);
            this.btnGenerar.TabIndex = 18;
            this.btnGenerar.Text = "GENERAR";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(17, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 21);
            this.label5.TabIndex = 46;
            this.label5.Text = "de Tesorería";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chcbxArea, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chcbxOperador, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(281, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 42);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // chcbxArea
            // 
            this.chcbxArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chcbxArea.EditValue = "";
            this.chcbxArea.Location = new System.Drawing.Point(202, 19);
            this.chcbxArea.Name = "chcbxArea";
            this.chcbxArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcbxArea.Size = new System.Drawing.Size(194, 20);
            this.chcbxArea.TabIndex = 53;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(202, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Area";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Operador";
            // 
            // chcbxOperador
            // 
            this.chcbxOperador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chcbxOperador.EditValue = "";
            this.chcbxOperador.Location = new System.Drawing.Point(3, 19);
            this.chcbxOperador.Name = "chcbxOperador";
            this.chcbxOperador.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcbxOperador.Size = new System.Drawing.Size(193, 20);
            this.chcbxOperador.TabIndex = 50;
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(252, 62);
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
            this.pbBG.Location = new System.Drawing.Point(252, 7);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(454, 50);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 41;
            this.pbBG.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(44, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 45;
            this.label4.Text = "Ingresos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.reportes;
            this.pictureBox1.InitialImage = global::SOAPAP.Properties.Resources.reportes;
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(29, 3);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(92, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Reportes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgdIOT
            // 
            this.pgdIOT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgdIOT.DataSource = this.dataIncomeOfTreasuryBindingSource;
            this.pgdIOT.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfFolio,
            this.pgfNombre,
            this.pgfFecha,
            this.pgfTotal,
            this.pgfStatus,
            this.pgfArea,
            this.pgfCajero,
            this.pgfDetalle,
            this.pgfMonto,
            this.pgfDesc,
            this.pgfSub,
            this.pgfIva,
            this.pgfTotaDt});
            this.pgdIOT.Location = new System.Drawing.Point(3, 161);
            this.pgdIOT.Name = "pgdIOT";
            this.pgdIOT.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgdIOT.Size = new System.Drawing.Size(929, 384);
            this.pgdIOT.TabIndex = 50;
            // 
            // pgfFolio
            // 
            this.pgfFolio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFolio.AreaIndex = 0;
            this.pgfFolio.Caption = "Folio";
            this.pgfFolio.FieldName = "Folio";
            this.pgfFolio.Name = "pgfFolio";
            // 
            // pgfNombre
            // 
            this.pgfNombre.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfNombre.AreaIndex = 1;
            this.pgfNombre.Caption = "Nombre";
            this.pgfNombre.FieldName = "Nombre";
            this.pgfNombre.Name = "pgfNombre";
            this.pgfNombre.Width = 300;
            // 
            // pgfFecha
            // 
            this.pgfFecha.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFecha.AreaIndex = 2;
            this.pgfFecha.Caption = "Fecha";
            this.pgfFecha.FieldName = "Fecha";
            this.pgfFecha.Name = "pgfFecha";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 0;
            this.pgfTotal.Caption = "Total";
            this.pgfTotal.FieldName = "TotalCS";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // pgfStatus
            // 
            this.pgfStatus.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfStatus.AreaIndex = 3;
            this.pgfStatus.Caption = "Estatus";
            this.pgfStatus.FieldName = "Estado";
            this.pgfStatus.Name = "pgfStatus";
            // 
            // pgfArea
            // 
            this.pgfArea.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pgfArea.AreaIndex = 0;
            this.pgfArea.Caption = "Area";
            this.pgfArea.FieldName = "Area";
            this.pgfArea.Name = "pgfArea";
            // 
            // pgfCajero
            // 
            this.pgfCajero.AreaIndex = 0;
            this.pgfCajero.Caption = "Cajero";
            this.pgfCajero.FieldName = "Cajero";
            this.pgfCajero.Name = "pgfCajero";
            // 
            // pgfDetalle
            // 
            this.pgfDetalle.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDetalle.AreaIndex = 4;
            this.pgfDetalle.Caption = "Detalle";
            this.pgfDetalle.FieldName = "Descripcion";
            this.pgfDetalle.Name = "pgfDetalle";
            // 
            // pgfMonto
            // 
            this.pgfMonto.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMonto.AreaIndex = 1;
            this.pgfMonto.Caption = "Monto";
            this.pgfMonto.FieldName = "Monto_dt_CS";
            this.pgfMonto.Name = "pgfMonto";
            // 
            // pgfDesc
            // 
            this.pgfDesc.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfDesc.AreaIndex = 3;
            this.pgfDesc.Caption = "Descuento";
            this.pgfDesc.FieldName = "Descuento_dt_CS";
            this.pgfDesc.Name = "pgfDesc";
            // 
            // pgfSub
            // 
            this.pgfSub.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSub.AreaIndex = 2;
            this.pgfSub.Caption = "Subtotal";
            this.pgfSub.FieldName = "Subtotal_dt_CS";
            this.pgfSub.Name = "pgfSub";
            // 
            // pgfIva
            // 
            this.pgfIva.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfIva.AreaIndex = 4;
            this.pgfIva.Caption = "IVA";
            this.pgfIva.FieldName = "iva_dt_CS";
            this.pgfIva.Name = "pgfIva";
            // 
            // pgfTotaDt
            // 
            this.pgfTotaDt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotaDt.AreaIndex = 5;
            this.pgfTotaDt.Caption = "Total";
            this.pgfTotaDt.FieldName = "Total_dt_CS";
            this.pgfTotaDt.Name = "pgfTotaDt";
            // 
            // dataIncomeOfTreasuryBindingSource
            // 
            this.dataIncomeOfTreasuryBindingSource.DataSource = typeof(SOAPAP.Reportes.DataIncomeOfTreasury);
            // 
            // RepIOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 576);
            this.Controls.Add(this.pgdIOT);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepIOT";
            this.Text = "RepIOT";
            this.Load += new System.EventHandler(this.RepIOT_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chceMostrarDetalle.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgdIOT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeOfTreasuryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxArea;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOperador;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private DevExpress.XtraPivotGrid.PivotGridControl pgdIOT;
        private System.Windows.Forms.BindingSource dataIncomeOfTreasuryBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFolio;
        private DevExpress.XtraPivotGrid.PivotGridField pgfNombre;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFecha;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStatus;
        private DevExpress.XtraPivotGrid.PivotGridField pgfArea;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCajero;
        private DevExpress.XtraEditors.CheckEdit chceMostrarDetalle;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDetalle;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMonto;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDesc;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSub;
        private DevExpress.XtraPivotGrid.PivotGridField pgfIva;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotaDt;
    }
}