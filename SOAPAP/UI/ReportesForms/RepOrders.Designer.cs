namespace SOAPAP.UI.ReportesForms
{
    partial class RepOrders
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.windowsUIButtonPanel1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chcbxArea = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chcbxOperador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pgcOrders = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataOrdersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pgfFolio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pfgFecha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDivision = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDescripcion = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfOficina = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMonto = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDesc = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSub = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfIva = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfEstado = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCajero = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFechaPago = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfcount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataOrdersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.windowsUIButtonPanel1);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pbBG);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(936, 124);
            this.pnlHeader.TabIndex = 50;
            // 
            // windowsUIButtonPanel1
            // 
            this.windowsUIButtonPanel1.BackColor = System.Drawing.Color.Transparent;
            windowsUIButtonImageOptions1.Image = global::SOAPAP.Properties.Resources.buscar;
            toolTipTitleItem1.Text = "Generar";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Carga los ingresos corespondientes a los filtros seleccionados.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            windowsUIButtonImageOptions2.Image = global::SOAPAP.Properties.Resources.file;
            toolTipTitleItem2.Text = "Exportar";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Exporta los ingresos correspondientes a los filtros seleccionados.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, superToolTip1, true, false, true, "GE", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Exportar", -1, true, superToolTip2, true, false, true, "EX", -1, false)});
            this.windowsUIButtonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowsUIButtonPanel1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(696, 0);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(240, 124);
            this.windowsUIButtonPanel1.TabIndex = 62;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(56, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 59;
            this.label4.Text = "Ordenes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(275, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 42);
            this.tableLayoutPanel1.TabIndex = 58;
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
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Usuario";
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
            // pbBG
            // 
            this.pbBG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBG.BackColor = System.Drawing.Color.Transparent;
            this.pbBG.Image = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.Location = new System.Drawing.Point(246, 70);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(454, 50);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 57;
            this.pbBG.TabStop = false;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(275, 14);
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
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(246, 12);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(451, 54);
            this.pcbIncomeByConcept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIncomeByConcept.TabIndex = 52;
            this.pcbIncomeByConcept.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(84, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Reporte";
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
            // pgcOrders
            // 
            this.pgcOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcOrders.DataSource = this.dataOrdersBindingSource;
            this.pgcOrders.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfFolio,
            this.pfgFecha,
            this.pgfDivision,
            this.pgfDescripcion,
            this.pgfOficina,
            this.pgfMonto,
            this.pgfDesc,
            this.pgfSub,
            this.pgfIva,
            this.pgfTotal,
            this.pgfEstado,
            this.pgfCajero,
            this.pgfFechaPago,
            this.pgfcount});
            this.pgcOrders.Location = new System.Drawing.Point(0, 124);
            this.pgcOrders.Name = "pgcOrders";
            this.pgcOrders.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcOrders.Size = new System.Drawing.Size(936, 287);
            this.pgcOrders.TabIndex = 51;
            // 
            // dataOrdersBindingSource
            // 
            this.dataOrdersBindingSource.DataSource = typeof(SOAPAP.Reportes.DataOrders);
            // 
            // pgfFolio
            // 
            this.pgfFolio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFolio.AreaIndex = 0;
            this.pgfFolio.Caption = "Folio";
            this.pgfFolio.FieldName = "folio";
            this.pgfFolio.Name = "pgfFolio";
            // 
            // pfgFecha
            // 
            this.pfgFecha.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pfgFecha.AreaIndex = 1;
            this.pfgFecha.Caption = "Fecha creación";
            this.pfgFecha.FieldName = "FechaOS";
            this.pfgFecha.Name = "pfgFecha";
            // 
            // pgfDivision
            // 
            this.pgfDivision.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDivision.AreaIndex = 2;
            this.pgfDivision.Caption = "Division";
            this.pgfDivision.FieldName = "Division";
            this.pgfDivision.Name = "pgfDivision";
            // 
            // pgfDescripcion
            // 
            this.pgfDescripcion.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDescripcion.AreaIndex = 3;
            this.pgfDescripcion.Caption = "Descripción";
            this.pgfDescripcion.FieldName = "description";
            this.pgfDescripcion.Name = "pgfDescripcion";
            // 
            // pgfOficina
            // 
            this.pgfOficina.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfOficina.AreaIndex = 4;
            this.pgfOficina.Caption = "Oficina";
            this.pgfOficina.FieldName = "branch_office";
            this.pgfOficina.Name = "pgfOficina";
            // 
            // pgfMonto
            // 
            this.pgfMonto.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMonto.AreaIndex = 1;
            this.pgfMonto.Caption = "Monto";
            this.pgfMonto.FieldName = "MONTO";
            this.pgfMonto.Name = "pgfMonto";
            // 
            // pgfDesc
            // 
            this.pgfDesc.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfDesc.AreaIndex = 2;
            this.pgfDesc.Caption = "Descuento";
            this.pgfDesc.FieldName = "DESCUENTO";
            this.pgfDesc.Name = "pgfDesc";
            // 
            // pgfSub
            // 
            this.pgfSub.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSub.AreaIndex = 3;
            this.pgfSub.Caption = "Subtotal";
            this.pgfSub.FieldName = "SUBTOTAL";
            this.pgfSub.Name = "pgfSub";
            // 
            // pgfIva
            // 
            this.pgfIva.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfIva.AreaIndex = 4;
            this.pgfIva.Caption = "IVA";
            this.pgfIva.FieldName = "IVA";
            this.pgfIva.Name = "pgfIva";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 5;
            this.pgfTotal.Caption = "Total";
            this.pgfTotal.FieldName = "TOTAL";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // pgfEstado
            // 
            this.pgfEstado.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfEstado.AreaIndex = 5;
            this.pgfEstado.Caption = "Estado";
            this.pgfEstado.FieldName = "Estado";
            this.pgfEstado.Name = "pgfEstado";
            // 
            // pgfCajero
            // 
            this.pgfCajero.AreaIndex = 0;
            this.pgfCajero.Caption = "Cajero";
            this.pgfCajero.FieldName = "cajero";
            this.pgfCajero.Name = "pgfCajero";
            // 
            // pgfFechaPago
            // 
            this.pgfFechaPago.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFechaPago.AreaIndex = 6;
            this.pgfFechaPago.Caption = "Fecha pago";
            this.pgfFechaPago.FieldName = "FechaPago";
            this.pgfFechaPago.Name = "pgfFechaPago";
            // 
            // pgfcount
            // 
            this.pgfcount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfcount.AreaIndex = 0;
            this.pgfcount.Caption = "Elems";
            this.pgfcount.FieldName = "count";
            this.pgfcount.Name = "pgfcount";
            // 
            // RepOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 450);
            this.Controls.Add(this.pgcOrders);
            this.Controls.Add(this.pnlHeader);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "RepOrders";
            this.Text = "RepOrders";
            this.Load += new System.EventHandler(this.RepOrders_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataOrdersBindingSource)).EndInit();
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
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcOrders;
        private System.Windows.Forms.BindingSource dataOrdersBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFolio;
        private DevExpress.XtraPivotGrid.PivotGridField pfgFecha;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDivision;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDescripcion;
        private DevExpress.XtraPivotGrid.PivotGridField pgfOficina;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMonto;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDesc;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSub;
        private DevExpress.XtraPivotGrid.PivotGridField pgfIva;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfEstado;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxArea;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOperador;
        private System.Windows.Forms.PictureBox pbBG;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCajero;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFechaPago;
        private DevExpress.XtraPivotGrid.PivotGridField pgfcount;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
    }
}