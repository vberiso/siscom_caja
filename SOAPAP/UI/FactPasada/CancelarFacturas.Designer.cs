namespace SOAPAP.UI.FactPasada
{
    partial class CancelarFacturas
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
            this.tableLayoutComprobante = new System.Windows.Forms.TableLayoutPanel();
            this.lblComprobante = new System.Windows.Forms.Label();
            this.tbxComprobante = new System.Windows.Forms.TextBox();
            this.tableLayoutFecha = new System.Windows.Forms.TableLayoutPanel();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutUsuarios = new System.Windows.Forms.TableLayoutPanel();
            this.cbxUsuarios = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tlpFecha = new System.Windows.Forms.TableLayoutPanel();
            this.cbxTipoBusqueda = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.IdTransaction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioTransaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folioImpresionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuentaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Signo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HaveInvoice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cancelar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ActualizaPdf = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Enviar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Descargar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.transactionMovimientosCajaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pdfVwrDetalle = new DevExpress.XtraPdfViewer.PdfViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutComprobante.SuspendLayout();
            this.tableLayoutFecha.SuspendLayout();
            this.tableLayoutUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tlpFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionMovimientosCajaBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.tableLayoutComprobante);
            this.pnlHeader.Controls.Add(this.tableLayoutFecha);
            this.pnlHeader.Controls.Add(this.tableLayoutUsuarios);
            this.pnlHeader.Controls.Add(this.pictureBox2);
            this.pnlHeader.Controls.Add(this.tlpFecha);
            this.pnlHeader.Controls.Add(this.btnBuscar);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1016, 133);
            this.pnlHeader.TabIndex = 52;
            // 
            // tableLayoutComprobante
            // 
            this.tableLayoutComprobante.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutComprobante.BackColor = System.Drawing.Color.White;
            this.tableLayoutComprobante.ColumnCount = 1;
            this.tableLayoutComprobante.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutComprobante.Controls.Add(this.lblComprobante, 0, 0);
            this.tableLayoutComprobante.Controls.Add(this.tbxComprobante, 0, 1);
            this.tableLayoutComprobante.Location = new System.Drawing.Point(315, 66);
            this.tableLayoutComprobante.Name = "tableLayoutComprobante";
            this.tableLayoutComprobante.RowCount = 2;
            this.tableLayoutComprobante.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutComprobante.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutComprobante.Size = new System.Drawing.Size(410, 44);
            this.tableLayoutComprobante.TabIndex = 58;
            this.tableLayoutComprobante.Visible = false;
            // 
            // lblComprobante
            // 
            this.lblComprobante.AutoSize = true;
            this.lblComprobante.Location = new System.Drawing.Point(3, 3);
            this.lblComprobante.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblComprobante.Name = "lblComprobante";
            this.lblComprobante.Size = new System.Drawing.Size(70, 13);
            this.lblComprobante.TabIndex = 50;
            this.lblComprobante.Text = "Comprobante";
            // 
            // tbxComprobante
            // 
            this.tbxComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxComprobante.Location = new System.Drawing.Point(2, 18);
            this.tbxComprobante.Margin = new System.Windows.Forms.Padding(2);
            this.tbxComprobante.Name = "tbxComprobante";
            this.tbxComprobante.Size = new System.Drawing.Size(406, 23);
            this.tbxComprobante.TabIndex = 51;
            // 
            // tableLayoutFecha
            // 
            this.tableLayoutFecha.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutFecha.BackColor = System.Drawing.Color.White;
            this.tableLayoutFecha.ColumnCount = 1;
            this.tableLayoutFecha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutFecha.Controls.Add(this.dtpFecha, 0, 1);
            this.tableLayoutFecha.Controls.Add(this.label2, 0, 0);
            this.tableLayoutFecha.Location = new System.Drawing.Point(530, 66);
            this.tableLayoutFecha.Name = "tableLayoutFecha";
            this.tableLayoutFecha.RowCount = 2;
            this.tableLayoutFecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutFecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutFecha.Size = new System.Drawing.Size(199, 46);
            this.tableLayoutFecha.TabIndex = 57;
            this.tableLayoutFecha.Visible = false;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(3, 22);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(193, 20);
            this.dtpFecha.TabIndex = 48;
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
            // tableLayoutUsuarios
            // 
            this.tableLayoutUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutUsuarios.BackColor = System.Drawing.Color.White;
            this.tableLayoutUsuarios.ColumnCount = 1;
            this.tableLayoutUsuarios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutUsuarios.Controls.Add(this.cbxUsuarios, 0, 1);
            this.tableLayoutUsuarios.Controls.Add(this.label3, 0, 0);
            this.tableLayoutUsuarios.Location = new System.Drawing.Point(314, 66);
            this.tableLayoutUsuarios.Name = "tableLayoutUsuarios";
            this.tableLayoutUsuarios.RowCount = 2;
            this.tableLayoutUsuarios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutUsuarios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutUsuarios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutUsuarios.Size = new System.Drawing.Size(209, 46);
            this.tableLayoutUsuarios.TabIndex = 56;
            this.tableLayoutUsuarios.Visible = false;
            // 
            // cbxUsuarios
            // 
            this.cbxUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUsuarios.FormattingEnabled = true;
            this.cbxUsuarios.Location = new System.Drawing.Point(3, 22);
            this.cbxUsuarios.Name = "cbxUsuarios";
            this.cbxUsuarios.Size = new System.Drawing.Size(203, 21);
            this.cbxUsuarios.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Usuario";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::SOAPAP.Properties.Resources.bg;
            this.pictureBox2.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pictureBox2.Location = new System.Drawing.Point(280, 65);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(480, 51);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 55;
            this.pictureBox2.TabStop = false;
            // 
            // tlpFecha
            // 
            this.tlpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tlpFecha.BackColor = System.Drawing.Color.White;
            this.tlpFecha.ColumnCount = 1;
            this.tlpFecha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpFecha.Controls.Add(this.cbxTipoBusqueda, 0, 1);
            this.tlpFecha.Controls.Add(this.label1, 0, 0);
            this.tlpFecha.Location = new System.Drawing.Point(376, 13);
            this.tlpFecha.Name = "tlpFecha";
            this.tlpFecha.RowCount = 2;
            this.tlpFecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpFecha.Size = new System.Drawing.Size(284, 46);
            this.tlpFecha.TabIndex = 53;
            // 
            // cbxTipoBusqueda
            // 
            this.cbxTipoBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTipoBusqueda.FormattingEnabled = true;
            this.cbxTipoBusqueda.Location = new System.Drawing.Point(3, 22);
            this.cbxTipoBusqueda.Name = "cbxTipoBusqueda";
            this.cbxTipoBusqueda.Size = new System.Drawing.Size(278, 21);
            this.cbxTipoBusqueda.TabIndex = 56;
            this.cbxTipoBusqueda.SelectedValueChanged += new System.EventHandler(this.cbxTipoBusqueda_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Tipo Busqueda:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(861, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(133, 39);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pcbIncomeByConcept
            // 
            this.pcbIncomeByConcept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pcbIncomeByConcept.BackColor = System.Drawing.Color.Transparent;
            this.pcbIncomeByConcept.Image = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(320, 12);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(388, 51);
            this.pcbIncomeByConcept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIncomeByConcept.TabIndex = 52;
            this.pcbIncomeByConcept.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(30, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 21);
            this.label4.TabIndex = 45;
            this.label4.Text = "cancelar facturas";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
            this.lblTitulo.Size = new System.Drawing.Size(115, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Facturación";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.AllowUserToResizeColumns = false;
            this.dgvMovimientos.AllowUserToResizeRows = false;
            this.dgvMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovimientos.AutoGenerateColumns = false;
            this.dgvMovimientos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMovimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdTransaction,
            this.FolioTransaccion,
            this.IdPayment,
            this.operacionDataGridViewTextBoxColumn,
            this.folioImpresionDataGridViewTextBoxColumn,
            this.horaDataGridViewTextBoxColumn,
            this.totalDataGridViewTextBoxColumn,
            this.cuentaDataGridViewTextBoxColumn,
            this.clienteDataGridViewTextBoxColumn,
            this.Signo,
            this.HaveInvoice,
            this.Cancelar,
            this.ActualizaPdf,
            this.Enviar,
            this.Descargar});
            this.dgvMovimientos.DataSource = this.transactionMovimientosCajaBindingSource;
            this.dgvMovimientos.Location = new System.Drawing.Point(0, 136);
            this.dgvMovimientos.MultiSelect = false;
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.RowHeadersWidth = 51;
            this.dgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.Size = new System.Drawing.Size(1012, 200);
            this.dgvMovimientos.TabIndex = 53;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // IdTransaction
            // 
            this.IdTransaction.DataPropertyName = "IdTransaction";
            this.IdTransaction.HeaderText = "IdTransaction";
            this.IdTransaction.MinimumWidth = 6;
            this.IdTransaction.Name = "IdTransaction";
            this.IdTransaction.ReadOnly = true;
            this.IdTransaction.Visible = false;
            this.IdTransaction.Width = 125;
            // 
            // FolioTransaccion
            // 
            this.FolioTransaccion.DataPropertyName = "FolioTransaccion";
            this.FolioTransaccion.HeaderText = "FolioTransaccion";
            this.FolioTransaccion.MinimumWidth = 6;
            this.FolioTransaccion.Name = "FolioTransaccion";
            this.FolioTransaccion.ReadOnly = true;
            this.FolioTransaccion.Visible = false;
            this.FolioTransaccion.Width = 125;
            // 
            // IdPayment
            // 
            this.IdPayment.DataPropertyName = "IdPayment";
            this.IdPayment.HeaderText = "IdPayment";
            this.IdPayment.MinimumWidth = 6;
            this.IdPayment.Name = "IdPayment";
            this.IdPayment.ReadOnly = true;
            this.IdPayment.Visible = false;
            this.IdPayment.Width = 125;
            // 
            // operacionDataGridViewTextBoxColumn
            // 
            this.operacionDataGridViewTextBoxColumn.DataPropertyName = "Operacion";
            this.operacionDataGridViewTextBoxColumn.HeaderText = "Operacion";
            this.operacionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.operacionDataGridViewTextBoxColumn.Name = "operacionDataGridViewTextBoxColumn";
            this.operacionDataGridViewTextBoxColumn.ReadOnly = true;
            this.operacionDataGridViewTextBoxColumn.Width = 125;
            // 
            // folioImpresionDataGridViewTextBoxColumn
            // 
            this.folioImpresionDataGridViewTextBoxColumn.DataPropertyName = "FolioImpresion";
            this.folioImpresionDataGridViewTextBoxColumn.HeaderText = "Folio";
            this.folioImpresionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.folioImpresionDataGridViewTextBoxColumn.Name = "folioImpresionDataGridViewTextBoxColumn";
            this.folioImpresionDataGridViewTextBoxColumn.ReadOnly = true;
            this.folioImpresionDataGridViewTextBoxColumn.Width = 125;
            // 
            // horaDataGridViewTextBoxColumn
            // 
            this.horaDataGridViewTextBoxColumn.DataPropertyName = "Hora";
            this.horaDataGridViewTextBoxColumn.HeaderText = "Hora";
            this.horaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.horaDataGridViewTextBoxColumn.Name = "horaDataGridViewTextBoxColumn";
            this.horaDataGridViewTextBoxColumn.ReadOnly = true;
            this.horaDataGridViewTextBoxColumn.Width = 125;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.ReadOnly = true;
            this.totalDataGridViewTextBoxColumn.Width = 125;
            // 
            // cuentaDataGridViewTextBoxColumn
            // 
            this.cuentaDataGridViewTextBoxColumn.DataPropertyName = "Cuenta";
            this.cuentaDataGridViewTextBoxColumn.HeaderText = "Cuenta";
            this.cuentaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cuentaDataGridViewTextBoxColumn.Name = "cuentaDataGridViewTextBoxColumn";
            this.cuentaDataGridViewTextBoxColumn.ReadOnly = true;
            this.cuentaDataGridViewTextBoxColumn.Width = 125;
            // 
            // clienteDataGridViewTextBoxColumn
            // 
            this.clienteDataGridViewTextBoxColumn.DataPropertyName = "Cliente";
            this.clienteDataGridViewTextBoxColumn.HeaderText = "Cliente";
            this.clienteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clienteDataGridViewTextBoxColumn.Name = "clienteDataGridViewTextBoxColumn";
            this.clienteDataGridViewTextBoxColumn.ReadOnly = true;
            this.clienteDataGridViewTextBoxColumn.Width = 125;
            // 
            // Signo
            // 
            this.Signo.DataPropertyName = "Signo";
            this.Signo.HeaderText = "Signo";
            this.Signo.MinimumWidth = 6;
            this.Signo.Name = "Signo";
            this.Signo.ReadOnly = true;
            this.Signo.Visible = false;
            this.Signo.Width = 125;
            // 
            // HaveInvoice
            // 
            this.HaveInvoice.DataPropertyName = "HaveInvoice";
            this.HaveInvoice.HeaderText = "HaveInvoice";
            this.HaveInvoice.MinimumWidth = 6;
            this.HaveInvoice.Name = "HaveInvoice";
            this.HaveInvoice.ReadOnly = true;
            this.HaveInvoice.Visible = false;
            this.HaveInvoice.Width = 125;
            // 
            // Cancelar
            // 
            this.Cancelar.HeaderText = "Cancelar";
            this.Cancelar.MinimumWidth = 6;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.ReadOnly = true;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseColumnTextForButtonValue = true;
            this.Cancelar.Width = 125;
            // 
            // ActualizaPdf
            // 
            this.ActualizaPdf.HeaderText = "Actualiza Pdf";
            this.ActualizaPdf.MinimumWidth = 6;
            this.ActualizaPdf.Name = "ActualizaPdf";
            this.ActualizaPdf.ReadOnly = true;
            this.ActualizaPdf.Text = "Actualiza";
            this.ActualizaPdf.UseColumnTextForButtonValue = true;
            this.ActualizaPdf.Width = 125;
            // 
            // Enviar
            // 
            this.Enviar.HeaderText = "Enviar Mail";
            this.Enviar.MinimumWidth = 6;
            this.Enviar.Name = "Enviar";
            this.Enviar.ReadOnly = true;
            this.Enviar.Text = "Enviar";
            this.Enviar.UseColumnTextForButtonValue = true;
            this.Enviar.Width = 125;
            // 
            // Descargar
            // 
            this.Descargar.HeaderText = "Descargar";
            this.Descargar.MinimumWidth = 6;
            this.Descargar.Name = "Descargar";
            this.Descargar.ReadOnly = true;
            this.Descargar.Text = "Descarga";
            this.Descargar.UseColumnTextForButtonValue = true;
            this.Descargar.Width = 125;
            // 
            // transactionMovimientosCajaBindingSource
            // 
            this.transactionMovimientosCajaBindingSource.DataSource = typeof(SOAPAP.Model.TransactionMovimientosCaja);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "Facturar";
            this.dataGridViewButtonColumn1.MinimumWidth = 6;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Text = "Facturar";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 125;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.HeaderText = "Actualiza Pdf";
            this.dataGridViewButtonColumn2.MinimumWidth = 6;
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.ReadOnly = true;
            this.dataGridViewButtonColumn2.Text = "Actualiza";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn2.Width = 125;
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.HeaderText = "Enviar Mail";
            this.dataGridViewButtonColumn3.MinimumWidth = 6;
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.ReadOnly = true;
            this.dataGridViewButtonColumn3.Text = "Enviar";
            this.dataGridViewButtonColumn3.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn3.Width = 125;
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.HeaderText = "Descargar";
            this.dataGridViewButtonColumn4.MinimumWidth = 6;
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            this.dataGridViewButtonColumn4.ReadOnly = true;
            this.dataGridViewButtonColumn4.Text = "Descarga";
            this.dataGridViewButtonColumn4.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn4.Width = 125;
            // 
            // pdfVwrDetalle
            // 
            this.pdfVwrDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfVwrDetalle.Location = new System.Drawing.Point(0, 0);
            this.pdfVwrDetalle.Name = "pdfVwrDetalle";
            this.pdfVwrDetalle.Size = new System.Drawing.Size(1009, 197);
            this.pdfVwrDetalle.TabIndex = 55;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pdfVwrDetalle);
            this.panel1.Location = new System.Drawing.Point(3, 342);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1009, 197);
            this.panel1.TabIndex = 57;
            // 
            // CancelarFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 563);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvMovimientos);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CancelarFacturas";
            this.Text = "CancelarFacturas";
            this.Load += new System.EventHandler(this.CancelarFacturas_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutComprobante.ResumeLayout(false);
            this.tableLayoutComprobante.PerformLayout();
            this.tableLayoutFecha.ResumeLayout(false);
            this.tableLayoutFecha.PerformLayout();
            this.tableLayoutUsuarios.ResumeLayout(false);
            this.tableLayoutUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tlpFecha.ResumeLayout(false);
            this.tlpFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionMovimientosCajaBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFecha;
        private System.Windows.Forms.TableLayoutPanel tableLayoutUsuarios;
        private System.Windows.Forms.ComboBox cbxUsuarios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TableLayoutPanel tlpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ComboBox cbxTipoBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutComprobante;
        private System.Windows.Forms.Label lblComprobante;
        private System.Windows.Forms.TextBox tbxComprobante;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.BindingSource transactionMovimientosCajaBindingSource;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTransaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioTransaccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn operacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioImpresionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuentaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Signo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HaveInvoice;
        private System.Windows.Forms.DataGridViewButtonColumn Cancelar;
        private System.Windows.Forms.DataGridViewButtonColumn ActualizaPdf;
        private System.Windows.Forms.DataGridViewButtonColumn Enviar;
        private System.Windows.Forms.DataGridViewButtonColumn Descargar;
        private System.Windows.Forms.PrintDialog printDialog1;
        private DevExpress.XtraPdfViewer.PdfViewer pdfVwrDetalle;
        private System.Windows.Forms.Panel panel1;
    }
}