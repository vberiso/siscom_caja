namespace SOAPAP.UI.ReportesForms
{
    partial class RepIFB
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbSoloCancelados = new System.Windows.Forms.RadioButton();
            this.rdbMosCancelados = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chcbxOperador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.chcbxOficina = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.pnlHRigth = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.pnlHLeft = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pgcIFB = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataIncomeFromBoxBindingSource = new System.Windows.Forms.BindingSource();
            this.pgfFolio = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCuenta = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfNombre = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfRFC = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSubtotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDesc = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFecha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pfgEstatus = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFolioFiscal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfOficina = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfOperador = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfIva = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOficina.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            this.pnlHRigth.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcIFB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeFromBoxBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.tableLayoutPanel3);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.pbBG);
            this.pnlHeader.Controls.Add(this.pnlHRigth);
            this.pnlHeader.Controls.Add(this.pnlHLeft);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1027, 144);
            this.pnlHeader.TabIndex = 48;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.82578F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.17422F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel3.Controls.Add(this.rdbSoloCancelados, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdbMosCancelados, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(330, 116);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(393, 28);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // rdbSoloCancelados
            // 
            this.rdbSoloCancelados.AutoSize = true;
            this.rdbSoloCancelados.Location = new System.Drawing.Point(198, 3);
            this.rdbSoloCancelados.Name = "rdbSoloCancelados";
            this.rdbSoloCancelados.Size = new System.Drawing.Size(105, 17);
            this.rdbSoloCancelados.TabIndex = 1;
            this.rdbSoloCancelados.TabStop = true;
            this.rdbSoloCancelados.Text = "Solo Cancelados";
            this.rdbSoloCancelados.UseVisualStyleBackColor = true;
            // 
            // rdbMosCancelados
            // 
            this.rdbMosCancelados.AutoSize = true;
            this.rdbMosCancelados.Location = new System.Drawing.Point(3, 3);
            this.rdbMosCancelados.Name = "rdbMosCancelados";
            this.rdbMosCancelados.Size = new System.Drawing.Size(119, 17);
            this.rdbMosCancelados.TabIndex = 0;
            this.rdbMosCancelados.TabStop = true;
            this.rdbMosCancelados.Text = "Mostrar Cancelados";
            this.rdbMosCancelados.UseVisualStyleBackColor = true;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(328, 64);
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
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxArea, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chcbxOperador, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chcbxOficina, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(249, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 45);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Seleccione el área:";
            // 
            // cbxArea
            // 
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(3, 19);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(178, 21);
            this.cbxArea.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Seleccione el operador:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Seleccione la oficina:";
            // 
            // chcbxOperador
            // 
            this.chcbxOperador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chcbxOperador.EditValue = "";
            this.chcbxOperador.Location = new System.Drawing.Point(371, 19);
            this.chcbxOperador.Name = "chcbxOperador";
            this.chcbxOperador.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcbxOperador.Size = new System.Drawing.Size(180, 20);
            this.chcbxOperador.TabIndex = 57;
            // 
            // chcbxOficina
            // 
            this.chcbxOficina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chcbxOficina.EditValue = "";
            this.chcbxOficina.Location = new System.Drawing.Point(187, 19);
            this.chcbxOficina.Name = "chcbxOficina";
            this.chcbxOficina.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcbxOficina.Size = new System.Drawing.Size(178, 20);
            this.chcbxOficina.TabIndex = 58;
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(301, 62);
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
            this.pbBG.Location = new System.Drawing.Point(217, 7);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(623, 50);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 41;
            this.pbBG.TabStop = false;
            // 
            // pnlHRigth
            // 
            this.pnlHRigth.Controls.Add(this.panel1);
            this.pnlHRigth.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHRigth.Location = new System.Drawing.Point(846, 0);
            this.pnlHRigth.Name = "pnlHRigth";
            this.pnlHRigth.Size = new System.Drawing.Size(181, 144);
            this.pnlHRigth.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Controls.Add(this.btnGenerar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 144);
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
            this.pnlHLeft.Controls.Add(this.label5);
            this.pnlHLeft.Controls.Add(this.label1);
            this.pnlHLeft.Controls.Add(this.lblTitulo);
            this.pnlHLeft.Controls.Add(this.pictureBox1);
            this.pnlHLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlHLeft.Name = "pnlHLeft";
            this.pnlHLeft.Size = new System.Drawing.Size(211, 144);
            this.pnlHLeft.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(5, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 21);
            this.label5.TabIndex = 45;
            this.label5.Text = "ingresos de caja";
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
            this.lblTitulo.Location = new System.Drawing.Point(42, 7);
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
            // pgcIFB
            // 
            this.pgcIFB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgcIFB.DataSource = this.dataIncomeFromBoxBindingSource;
            this.pgcIFB.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfFolio,
            this.pgfCuenta,
            this.pgfNombre,
            this.pgfRFC,
            this.pgfSubtotal,
            this.pgfDesc,
            this.pgfTotal,
            this.pgfFecha,
            this.pfgEstatus,
            this.pgfFolioFiscal,
            this.pgfOficina,
            this.pgfOperador,
            this.pgfIva});
            this.pgcIFB.Location = new System.Drawing.Point(0, 142);
            this.pgcIFB.Name = "pgcIFB";
            this.pgcIFB.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcIFB.Size = new System.Drawing.Size(1027, 277);
            this.pgcIFB.TabIndex = 49;
            // 
            // dataIncomeFromBoxBindingSource
            // 
            this.dataIncomeFromBoxBindingSource.DataSource = typeof(SOAPAP.Reportes.DataIncomeFromBox);
            // 
            // pgfFolio
            // 
            this.pgfFolio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFolio.AreaIndex = 0;
            this.pgfFolio.Caption = "FOLIO";
            this.pgfFolio.FieldName = "folio_impresion";
            this.pgfFolio.Name = "pgfFolio";
            this.pgfFolio.Width = 189;
            // 
            // pgfCuenta
            // 
            this.pgfCuenta.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfCuenta.AreaIndex = 1;
            this.pgfCuenta.Caption = "CUENTA";
            this.pgfCuenta.FieldName = "CUENTA";
            this.pgfCuenta.Name = "pgfCuenta";
            // 
            // pgfNombre
            // 
            this.pgfNombre.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfNombre.AreaIndex = 2;
            this.pgfNombre.Caption = "NOMBRE";
            this.pgfNombre.FieldName = "cliente";
            this.pgfNombre.Name = "pgfNombre";
            // 
            // pgfRFC
            // 
            this.pgfRFC.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfRFC.AreaIndex = 3;
            this.pgfRFC.Caption = "RFC";
            this.pgfRFC.FieldName = "rfc";
            this.pgfRFC.Name = "pgfRFC";
            // 
            // pgfSubtotal
            // 
            this.pgfSubtotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSubtotal.AreaIndex = 0;
            this.pgfSubtotal.Caption = "SUBTOTAL";
            this.pgfSubtotal.FieldName = "SUBTOTAL";
            this.pgfSubtotal.Name = "pgfSubtotal";
            // 
            // pgfDesc
            // 
            this.pgfDesc.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfDesc.AreaIndex = 1;
            this.pgfDesc.Caption = "DESC";
            this.pgfDesc.FieldName = "DESC";
            this.pgfDesc.Name = "pgfDesc";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfTotal.AreaIndex = 3;
            this.pgfTotal.Caption = "TOTAL";
            this.pgfTotal.FieldName = "TOTAL_P";
            this.pgfTotal.Name = "pgfTotal";
            this.pgfTotal.Width = 112;
            // 
            // pgfFecha
            // 
            this.pgfFecha.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFecha.AreaIndex = 4;
            this.pgfFecha.Caption = "FECHA";
            this.pgfFecha.FieldName = "FECHA";
            this.pgfFecha.Name = "pgfFecha";
            // 
            // pfgEstatus
            // 
            this.pfgEstatus.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pfgEstatus.AreaIndex = 0;
            this.pfgEstatus.Caption = "ESTATUS";
            this.pfgEstatus.FieldName = "ESTATUS";
            this.pfgEstatus.Name = "pfgEstatus";
            // 
            // pgfFolioFiscal
            // 
            this.pgfFolioFiscal.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfFolioFiscal.AreaIndex = 5;
            this.pgfFolioFiscal.Caption = "FOLIO FISCAL";
            this.pgfFolioFiscal.FieldName = "folioTransaction";
            this.pgfFolioFiscal.Name = "pgfFolioFiscal";
            // 
            // pgfOficina
            // 
            this.pgfOficina.AreaIndex = 0;
            this.pgfOficina.Caption = "OFICINA";
            this.pgfOficina.FieldName = "branch_office";
            this.pgfOficina.Name = "pgfOficina";
            // 
            // pgfOperador
            // 
            this.pgfOperador.AreaIndex = 1;
            this.pgfOperador.Caption = "CAJERO";
            this.pgfOperador.FieldName = "cajero";
            this.pgfOperador.Name = "pgfOperador";
            // 
            // pgfIva
            // 
            this.pgfIva.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfIva.AreaIndex = 2;
            this.pgfIva.Caption = "IVA";
            this.pgfIva.FieldName = "iva";
            this.pgfIva.Name = "pgfIva";
            // 
            // RepIFB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 450);
            this.Controls.Add(this.pgcIFB);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepIFB";
            this.Text = "RepIFB";
            this.Load += new System.EventHandler(this.RepIFB_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOficina.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            this.pnlHRigth.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlHLeft.ResumeLayout(false);
            this.pnlHLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgcIFB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataIncomeFromBoxBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton rdbSoloCancelados;
        private System.Windows.Forms.RadioButton rdbMosCancelados;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Panel pnlHRigth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Panel pnlHLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcIFB;
        private System.Windows.Forms.BindingSource dataIncomeFromBoxBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFolio;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCuenta;
        private DevExpress.XtraPivotGrid.PivotGridField pgfNombre;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRFC;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSubtotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDesc;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFecha;
        private DevExpress.XtraPivotGrid.PivotGridField pfgEstatus;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFolioFiscal;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOperador;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOficina;
        private DevExpress.XtraPivotGrid.PivotGridField pgfOficina;
        private DevExpress.XtraPivotGrid.PivotGridField pgfOperador;
        private DevExpress.XtraPivotGrid.PivotGridField pgfIva;
    }
}