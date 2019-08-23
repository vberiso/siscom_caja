namespace SOAPAP.UI.ReportesForms
{
    partial class RepINAAyunt
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.windowsUIButtonPanel1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pgcINA = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataIncomeNewAccountsAyuntBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pgfColonia = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfAño = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTipoVivienda = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfObtAgua = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfObtDrenaje = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCuenta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCliente = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRecMonto = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcINA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeNewAccountsAyuntBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.windowsUIButtonPanel1);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1076, 113);
            this.pnlHeader.TabIndex = 51;
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
            windowsUIButtonImageOptions3.Image = global::SOAPAP.Properties.Resources.imprimir;
            toolTipTitleItem3.Text = "Imprimir";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Genera un PDF listo para imprimir";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, superToolTip1, true, false, true, "GE", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Exportar", -1, true, superToolTip2, true, false, true, "EX", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, superToolTip3, true, false, true, "PR", -1, false)});
            this.windowsUIButtonPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButtonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowsUIButtonPanel1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(836, 0);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(240, 113);
            this.windowsUIButtonPanel1.TabIndex = 61;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 21);
            this.label1.TabIndex = 58;
            this.label1.Text = "Nuevos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 21);
            this.label5.TabIndex = 57;
            this.label5.Text = "Fraccionamientos";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(321, 14);
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
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(294, 12);
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
            this.lblTitulo.Location = new System.Drawing.Point(72, 12);
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
            // pgcINA
            // 
            this.pgcINA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcINA.DataSource = this.dataIncomeNewAccountsAyuntBindingSource;
            this.pgcINA.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfColonia,
            this.pgfAño,
            this.pgfTipoVivienda,
            this.pgfObtAgua,
            this.pgfObtDrenaje,
            this.pgfCount,
            this.pgfCuenta,
            this.pgfCliente,
            this.pgfRecMonto});
            this.pgcINA.Location = new System.Drawing.Point(1, 113);
            this.pgcINA.Name = "pgcINA";
            this.pgcINA.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcINA.Size = new System.Drawing.Size(1075, 303);
            this.pgcINA.TabIndex = 52;
            // 
            // dataIncomeNewAccountsAyuntBindingSource
            // 
            this.dataIncomeNewAccountsAyuntBindingSource.DataSource = typeof(SOAPAP.Reportes.DataIncomeNewAccountsAyunt);
            // 
            // pgfColonia
            // 
            this.pgfColonia.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfColonia.AreaIndex = 0;
            this.pgfColonia.Caption = "Fraccionamiento";
            this.pgfColonia.FieldName = "COLONIA";
            this.pgfColonia.Name = "pgfColonia";
            this.pgfColonia.Width = 200;
            // 
            // pgfAño
            // 
            this.pgfAño.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pgfAño.AreaIndex = 0;
            this.pgfAño.Caption = "Año Creacion";
            this.pgfAño.FieldName = "CONTRATO_AÑO";
            this.pgfAño.Name = "pgfAño";
            // 
            // pgfTipoVivienda
            // 
            this.pgfTipoVivienda.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfTipoVivienda.AreaIndex = 1;
            this.pgfTipoVivienda.Caption = "Tipo de vivienda";
            this.pgfTipoVivienda.FieldName = "TipoViviendas";
            this.pgfTipoVivienda.Name = "pgfTipoVivienda";
            this.pgfTipoVivienda.Width = 150;
            // 
            // pgfObtAgua
            // 
            this.pgfObtAgua.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfObtAgua.AreaIndex = 1;
            this.pgfObtAgua.Caption = "Obtenida de Predial";
            this.pgfObtAgua.FieldName = "PredialMonto";
            this.pgfObtAgua.Name = "pgfObtAgua";
            // 
            // pgfObtDrenaje
            // 
            this.pgfObtDrenaje.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfObtDrenaje.AreaIndex = 2;
            this.pgfObtDrenaje.Caption = "Obtenida de limpia";
            this.pgfObtDrenaje.FieldName = "LimpiaMonto";
            this.pgfObtDrenaje.Name = "pgfObtDrenaje";
            // 
            // pgfCount
            // 
            this.pgfCount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfCount.AreaIndex = 0;
            this.pgfCount.Caption = "Numero viviendas";
            this.pgfCount.FieldName = "count";
            this.pgfCount.Name = "pgfCount";
            // 
            // pgfCuenta
            // 
            this.pgfCuenta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCuenta.AreaIndex = 2;
            this.pgfCuenta.Caption = "Cuenta";
            this.pgfCuenta.FieldName = "CUENTA";
            this.pgfCuenta.Name = "pgfCuenta";
            // 
            // pgfCliente
            // 
            this.pgfCliente.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCliente.AreaIndex = 3;
            this.pgfCliente.Caption = "Cliente";
            this.pgfCliente.FieldName = "CLIENTE";
            this.pgfCliente.Name = "pgfCliente";
            // 
            // pgfRecMonto
            // 
            this.pgfRecMonto.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfRecMonto.AreaIndex = 3;
            this.pgfRecMonto.Caption = "Obtenida de Recargos";
            this.pgfRecMonto.FieldName = "RecargoMonto";
            this.pgfRecMonto.Name = "pgfRecMonto";
            // 
            // RepINAAyunt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 450);
            this.Controls.Add(this.pgcINA);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepINAAyunt";
            this.Text = "RepINAAyunt";
            this.Load += new System.EventHandler(this.RepINAAyunt_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcINA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeNewAccountsAyuntBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcINA;
        private DevExpress.XtraPivotGrid.PivotGridField pgfColonia;
        private DevExpress.XtraPivotGrid.PivotGridField pgfAño;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTipoVivienda;
        private DevExpress.XtraPivotGrid.PivotGridField pgfObtAgua;
        private DevExpress.XtraPivotGrid.PivotGridField pgfObtDrenaje;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCount;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCuenta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCliente;
        private System.Windows.Forms.BindingSource dataIncomeNewAccountsAyuntBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRecMonto;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
    }
}