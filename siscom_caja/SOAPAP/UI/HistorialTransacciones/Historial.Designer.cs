namespace SOAPAP.UI.HistorialTransacciones
{
    partial class Historial
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.windowsUIButtonPanel1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chcbxOperador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pgcHistorial = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataHistorialBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pgfMetodo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCajero = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfType = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFecha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfOficina = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfHora = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDetalle = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMonto = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDescuento = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSubtotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfIva = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.pgfSerial = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCuenta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCliente = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgcHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataHistorialBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.windowsUIButtonPanel1);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(964, 106);
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
            windowsUIButtonImageOptions3.EnableTransparency = true;
            windowsUIButtonImageOptions3.Image = global::SOAPAP.Properties.Resources.imprimir;
            windowsUIButtonImageOptions3.Location = DevExpress.XtraBars.Docking2010.ImageLocation.BeforeText;
            toolTipTitleItem3.Text = "Imprimir";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Genera y abre un PDF listo para imprimir.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, superToolTip1, true, false, true, "GE", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Exportar", -1, true, superToolTip2, true, false, true, "EX", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "imprimit", -1, true, superToolTip3, true, false, true, "PR", -1, false)});
            this.windowsUIButtonPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButtonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowsUIButtonPanel1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(776, 0);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(188, 106);
            this.windowsUIButtonPanel1.TabIndex = 58;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.38596F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.61404F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel2.Controls.Add(this.chcbxOperador, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFecha, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(290, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(399, 46);
            this.tableLayoutPanel2.TabIndex = 53;
            // 
            // chcbxOperador
            // 
            this.chcbxOperador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chcbxOperador.EditValue = "";
            this.chcbxOperador.Location = new System.Drawing.Point(219, 22);
            this.chcbxOperador.Name = "chcbxOperador";
            this.chcbxOperador.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcbxOperador.Size = new System.Drawing.Size(177, 20);
            this.chcbxOperador.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(13, 22);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(190, 20);
            this.dtpFecha.TabIndex = 48;
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(264, 20);
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
            this.lblTitulo.Size = new System.Drawing.Size(87, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Historial";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.Historial;
            this.pictureBox1.InitialImage = global::SOAPAP.Properties.Resources.reportes;
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(964, 457);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pgcHistorial);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 222);
            this.panel1.TabIndex = 1;
            // 
            // pgcHistorial
            // 
            this.pgcHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcHistorial.DataSource = this.dataHistorialBindingSource;
            this.pgcHistorial.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfMetodo,
            this.pgfCajero,
            this.pgfType,
            this.pgfFecha,
            this.pgfTotal,
            this.pgfOficina,
            this.pgfHora,
            this.pgfDetalle,
            this.pgfMonto,
            this.pgfDescuento,
            this.pgfSubtotal,
            this.pgfIva,
            this.pgfSerial,
            this.pgfCuenta,
            this.pgfCliente});
            this.pgcHistorial.Location = new System.Drawing.Point(3, 3);
            this.pgcHistorial.Name = "pgcHistorial";
            this.pgcHistorial.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcHistorial.Size = new System.Drawing.Size(952, 216);
            this.pgcHistorial.TabIndex = 0;
            // 
            // dataHistorialBindingSource
            // 
            this.dataHistorialBindingSource.DataSource = typeof(SOAPAP.UI.HistorialTransacciones.DataHistorial);
            // 
            // pgfMetodo
            // 
            this.pgfMetodo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfMetodo.AreaIndex = 2;
            this.pgfMetodo.Caption = "Metodo";
            this.pgfMetodo.FieldName = "TipoPago";
            this.pgfMetodo.Name = "pgfMetodo";
            // 
            // pgfCajero
            // 
            this.pgfCajero.AreaIndex = 1;
            this.pgfCajero.Caption = "Cajero";
            this.pgfCajero.FieldName = "cajero";
            this.pgfCajero.Name = "pgfCajero";
            // 
            // pgfType
            // 
            this.pgfType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfType.AreaIndex = 1;
            this.pgfType.Caption = "Tipo Op";
            this.pgfType.FieldName = "TypeTransactionName";
            this.pgfType.Name = "pgfType";
            this.pgfType.Width = 132;
            // 
            // pgfFecha
            // 
            this.pgfFecha.AreaIndex = 2;
            this.pgfFecha.Caption = "Fecha";
            this.pgfFecha.FieldName = "Fecha";
            this.pgfFecha.Name = "pgfFecha";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 4;
            this.pgfTotal.Caption = "Total";
            this.pgfTotal.FieldName = "TotalConSigno_dt";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // pgfOficina
            // 
            this.pgfOficina.AreaIndex = 3;
            this.pgfOficina.Caption = "Oficina";
            this.pgfOficina.FieldName = "Oficina";
            this.pgfOficina.Name = "pgfOficina";
            // 
            // pgfHora
            // 
            this.pgfHora.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfHora.AreaIndex = 0;
            this.pgfHora.Caption = "Hora";
            this.pgfHora.FieldName = "Hora";
            this.pgfHora.Name = "pgfHora";
            // 
            // pgfDetalle
            // 
            this.pgfDetalle.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDetalle.AreaIndex = 3;
            this.pgfDetalle.Caption = "Detalle";
            this.pgfDetalle.FieldName = "Descripcion_dt";
            this.pgfDetalle.Name = "pgfDetalle";
            // 
            // pgfMonto
            // 
            this.pgfMonto.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMonto.AreaIndex = 0;
            this.pgfMonto.Caption = "Monto";
            this.pgfMonto.FieldName = "MontoConSigno_dt";
            this.pgfMonto.Name = "pgfMonto";
            // 
            // pgfDescuento
            // 
            this.pgfDescuento.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfDescuento.AreaIndex = 1;
            this.pgfDescuento.Caption = "Descuento";
            this.pgfDescuento.FieldName = "DescuentoConSigno_dt";
            this.pgfDescuento.Name = "pgfDescuento";
            // 
            // pgfSubtotal
            // 
            this.pgfSubtotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSubtotal.AreaIndex = 2;
            this.pgfSubtotal.Caption = "Subtotal";
            this.pgfSubtotal.FieldName = "SubtotalConSigno_dt";
            this.pgfSubtotal.Name = "pgfSubtotal";
            // 
            // pgfIva
            // 
            this.pgfIva.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfIva.AreaIndex = 3;
            this.pgfIva.Caption = "IVA";
            this.pgfIva.FieldName = "ivaConSigno_dt";
            this.pgfIva.Name = "pgfIva";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.chartControl1);
            this.panel2.Location = new System.Drawing.Point(3, 231);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(958, 223);
            this.panel2.TabIndex = 2;
            // 
            // chartControl1
            // 
            this.chartControl1.DataBindings = null;
            this.chartControl1.DataSource = this.pgcHistorial;
            xyDiagram1.AxisX.Title.Text = "Hora Tipo Op Metodo Detalle";
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Text = "Monto Descuento Subtotal IVA Total";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Legend.MaxHorizontalPercentage = 30D;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(12, 16);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesDataMember = "Series";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.SeriesTemplate.ArgumentDataMember = "Arguments";
            this.chartControl1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartControl1.SeriesTemplate.ValueDataMembersSerializable = "Values";
            this.chartControl1.Size = new System.Drawing.Size(937, 200);
            this.chartControl1.TabIndex = 0;
            // 
            // pgfSerial
            // 
            this.pgfSerial.AreaIndex = 0;
            this.pgfSerial.Caption = "Serial";
            this.pgfSerial.FieldName = "Serial";
            this.pgfSerial.Name = "pgfSerial";
            // 
            // pgfCuenta
            // 
            this.pgfCuenta.AreaIndex = 4;
            this.pgfCuenta.Caption = "Cuenta";
            this.pgfCuenta.FieldName = "account";
            this.pgfCuenta.Name = "pgfCuenta";
            // 
            // pgfCliente
            // 
            this.pgfCliente.AreaIndex = 5;
            this.pgfCliente.Caption = "Cliente";
            this.pgfCliente.FieldName = "Contribuyente";
            this.pgfCliente.Name = "pgfCliente";
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 593);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Historial";
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.Historial_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pgcHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataHistorialBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcHistorial;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOperador;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMetodo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCajero;
        private DevExpress.XtraPivotGrid.PivotGridField pgfType;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFecha;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfOficina;
        private DevExpress.XtraPivotGrid.PivotGridField pgfHora;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDetalle;
        private System.Windows.Forms.BindingSource dataHistorialBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMonto;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDescuento;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSubtotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfIva;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSerial;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCuenta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCliente;
    }
}