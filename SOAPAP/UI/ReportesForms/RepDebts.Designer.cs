namespace SOAPAP.UI.ReportesForms
{
    partial class RepDebts
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chlbxColonia = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.label6 = new System.Windows.Forms.Label();
            this.srchColonia = new DevExpress.XtraEditors.SearchControl();
            this.cheColonia = new DevExpress.XtraEditors.CheckEdit();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pgcAdeudos = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataDebtsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pgfCuenta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfNombre = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDomicilio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDesde = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfHasta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRecAgua = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMontoAgua = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMontoDr = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRecargoDr = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMontoSan = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRecargoSan = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfColonia = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTipoPredio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfStatus = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRegion = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDescuento = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfPorcentaje = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chlbxColonia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srchColonia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheColonia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcAdeudos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDebtsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.windowsUIButtonPanel1);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pbBG);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1176, 158);
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
            this.windowsUIButtonPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButtonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowsUIButtonPanel1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(874, 0);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(302, 158);
            this.windowsUIButtonPanel1.TabIndex = 56;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chlbxColonia, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.srchColonia, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cheColonia, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(459, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(280, 123);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // chlbxColonia
            // 
            this.chlbxColonia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chlbxColonia.Cursor = System.Windows.Forms.Cursors.Default;
            this.chlbxColonia.Location = new System.Drawing.Point(3, 65);
            this.chlbxColonia.Name = "chlbxColonia";
            this.chlbxColonia.Size = new System.Drawing.Size(274, 55);
            this.chlbxColonia.TabIndex = 59;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "Colonia";
            // 
            // srchColonia
            // 
            this.srchColonia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.srchColonia.Client = this.chlbxColonia;
            this.srchColonia.Location = new System.Drawing.Point(3, 18);
            this.srchColonia.Name = "srchColonia";
            this.srchColonia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.srchColonia.Properties.Client = this.chlbxColonia;
            this.srchColonia.Size = new System.Drawing.Size(274, 20);
            this.srchColonia.TabIndex = 57;
            // 
            // cheColonia
            // 
            this.cheColonia.Location = new System.Drawing.Point(3, 43);
            this.cheColonia.Name = "cheColonia";
            this.cheColonia.Properties.Caption = "Todos";
            this.cheColonia.Size = new System.Drawing.Size(75, 19);
            this.cheColonia.TabIndex = 58;
            this.cheColonia.CheckedChanged += new System.EventHandler(this.cheColonia_CheckedChanged);
            // 
            // pbBG
            // 
            this.pbBG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBG.BackColor = System.Drawing.Color.Transparent;
            this.pbBG.Image = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.Location = new System.Drawing.Point(441, 7);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(321, 142);
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
            this.label4.Size = new System.Drawing.Size(77, 21);
            this.label4.TabIndex = 45;
            this.label4.Text = "Adeudos";
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
            // pgcAdeudos
            // 
            this.pgcAdeudos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcAdeudos.DataSource = this.dataDebtsBindingSource;
            this.pgcAdeudos.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfCuenta,
            this.pgfNombre,
            this.pgfDomicilio,
            this.pgfDesde,
            this.pgfHasta,
            this.pgfRecAgua,
            this.pgfMontoAgua,
            this.pgfMontoDr,
            this.pgfRecargoDr,
            this.pgfMontoSan,
            this.pgfRecargoSan,
            this.pgfTotal,
            this.pgfColonia,
            this.pgfTipoPredio,
            this.pgfStatus,
            this.pgfRegion,
            this.pgfCount,
            this.pgfDescuento,
            this.pgfPorcentaje});
            this.pgcAdeudos.Location = new System.Drawing.Point(1, 158);
            this.pgcAdeudos.Name = "pgcAdeudos";
            this.pgcAdeudos.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcAdeudos.Size = new System.Drawing.Size(1172, 264);
            this.pgcAdeudos.TabIndex = 51;
            // 
            // dataDebtsBindingSource
            // 
            this.dataDebtsBindingSource.DataSource = typeof(SOAPAP.Reportes.DataDebts);
            // 
            // pgfCuenta
            // 
            this.pgfCuenta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCuenta.AreaIndex = 2;
            this.pgfCuenta.Caption = "Cuenta";
            this.pgfCuenta.FieldName = "Cuenta";
            this.pgfCuenta.Name = "pgfCuenta";
            this.pgfCuenta.Width = 90;
            // 
            // pgfNombre
            // 
            this.pgfNombre.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfNombre.AreaIndex = 4;
            this.pgfNombre.Caption = "Nombre";
            this.pgfNombre.FieldName = "cliente";
            this.pgfNombre.Name = "pgfNombre";
            // 
            // pgfDomicilio
            // 
            this.pgfDomicilio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDomicilio.AreaIndex = 5;
            this.pgfDomicilio.Caption = "Domicilio";
            this.pgfDomicilio.FieldName = "DOMICILIO";
            this.pgfDomicilio.Name = "pgfDomicilio";
            // 
            // pgfDesde
            // 
            this.pgfDesde.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDesde.AreaIndex = 7;
            this.pgfDesde.Caption = "Desde";
            this.pgfDesde.FieldName = "DESDE";
            this.pgfDesde.Name = "pgfDesde";
            this.pgfDesde.Width = 90;
            // 
            // pgfHasta
            // 
            this.pgfHasta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfHasta.AreaIndex = 8;
            this.pgfHasta.Caption = "Hasta";
            this.pgfHasta.FieldName = "HASTA";
            this.pgfHasta.Name = "pgfHasta";
            this.pgfHasta.Width = 90;
            // 
            // pgfRecAgua
            // 
            this.pgfRecAgua.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfRecAgua.AreaIndex = 2;
            this.pgfRecAgua.Caption = "Recargos Agua";
            this.pgfRecAgua.FieldName = "Recarto_Ag";
            this.pgfRecAgua.Name = "pgfRecAgua";
            // 
            // pgfMontoAgua
            // 
            this.pgfMontoAgua.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMontoAgua.AreaIndex = 1;
            this.pgfMontoAgua.Caption = "Monto Agua";
            this.pgfMontoAgua.FieldName = "Monto_Ag";
            this.pgfMontoAgua.Name = "pgfMontoAgua";
            // 
            // pgfMontoDr
            // 
            this.pgfMontoDr.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMontoDr.AreaIndex = 3;
            this.pgfMontoDr.Caption = "Monto Dre";
            this.pgfMontoDr.FieldName = "Monto_Dr";
            this.pgfMontoDr.Name = "pgfMontoDr";
            // 
            // pgfRecargoDr
            // 
            this.pgfRecargoDr.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfRecargoDr.AreaIndex = 4;
            this.pgfRecargoDr.Caption = "Recargo Dre";
            this.pgfRecargoDr.FieldName = "Recargo_Dr";
            this.pgfRecargoDr.Name = "pgfRecargoDr";
            // 
            // pgfMontoSan
            // 
            this.pgfMontoSan.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfMontoSan.AreaIndex = 5;
            this.pgfMontoSan.Caption = "Monto San";
            this.pgfMontoSan.FieldName = "Monto_Sa";
            this.pgfMontoSan.Name = "pgfMontoSan";
            // 
            // pgfRecargoSan
            // 
            this.pgfRecargoSan.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfRecargoSan.AreaIndex = 6;
            this.pgfRecargoSan.Caption = "Recargo San";
            this.pgfRecargoSan.FieldName = "Recargo_Sa";
            this.pgfRecargoSan.Name = "pgfRecargoSan";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 7;
            this.pgfTotal.Caption = "Total";
            this.pgfTotal.FieldName = "Total_Debt";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // pgfColonia
            // 
            this.pgfColonia.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfColonia.AreaIndex = 0;
            this.pgfColonia.Caption = "Colonia";
            this.pgfColonia.FieldName = "COLONIA";
            this.pgfColonia.Name = "pgfColonia";
            // 
            // pgfTipoPredio
            // 
            this.pgfTipoPredio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfTipoPredio.AreaIndex = 6;
            this.pgfTipoPredio.Caption = "Tipo Predio";
            this.pgfTipoPredio.FieldName = "TipoPredio";
            this.pgfTipoPredio.Name = "pgfTipoPredio";
            // 
            // pgfStatus
            // 
            this.pgfStatus.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfStatus.AreaIndex = 3;
            this.pgfStatus.Caption = "Estado";
            this.pgfStatus.FieldName = "TipoEstadoServicio";
            this.pgfStatus.Name = "pgfStatus";
            this.pgfStatus.Width = 90;
            // 
            // pgfRegion
            // 
            this.pgfRegion.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfRegion.AreaIndex = 1;
            this.pgfRegion.Caption = "Region";
            this.pgfRegion.FieldName = "Region";
            this.pgfRegion.Name = "pgfRegion";
            this.pgfRegion.Width = 70;
            // 
            // pgfCount
            // 
            this.pgfCount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfCount.AreaIndex = 0;
            this.pgfCount.Caption = "Num";
            this.pgfCount.FieldName = "count";
            this.pgfCount.Name = "pgfCount";
            this.pgfCount.Width = 38;
            // 
            // pgfDescuento
            // 
            this.pgfDescuento.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDescuento.AreaIndex = 9;
            this.pgfDescuento.Caption = "Pob vul";
            this.pgfDescuento.FieldName = "Descuento";
            this.pgfDescuento.Name = "pgfDescuento";
            // 
            // pgfPorcentaje
            // 
            this.pgfPorcentaje.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfPorcentaje.AreaIndex = 10;
            this.pgfPorcentaje.Caption = "%";
            this.pgfPorcentaje.FieldName = "DescuentoPorcentaje";
            this.pgfPorcentaje.Name = "pgfPorcentaje";
            this.pgfPorcentaje.Width = 60;
            // 
            // RepDebts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 450);
            this.Controls.Add(this.pgcAdeudos);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepDebts";
            this.Text = "RepDebts";
            this.Load += new System.EventHandler(this.RepDebts_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chlbxColonia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srchColonia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheColonia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcAdeudos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDebtsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private DevExpress.XtraEditors.SearchControl srchColonia;
        private DevExpress.XtraEditors.CheckEdit cheColonia;
        private DevExpress.XtraEditors.CheckedListBoxControl chlbxColonia;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcAdeudos;
        private System.Windows.Forms.BindingSource dataDebtsBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCuenta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfNombre;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDomicilio;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDesde;
        private DevExpress.XtraPivotGrid.PivotGridField pgfHasta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRecAgua;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMontoAgua;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMontoDr;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRecargoDr;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMontoSan;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRecargoSan;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfColonia;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTipoPredio;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStatus;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRegion;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCount;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDescuento;
        private DevExpress.XtraPivotGrid.PivotGridField pgfPorcentaje;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
    }
}