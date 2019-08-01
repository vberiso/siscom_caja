namespace SOAPAP.UI
{
    partial class FindOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindOrders));
            this.pnpTiltle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.radioFolio = new System.Windows.Forms.RadioButton();
            this.radioFecha = new System.Windows.Forms.RadioButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlCalendar = new System.Windows.Forms.Panel();
            this.pbBuscar = new System.Windows.Forms.PictureBox();
            this.txtFolioSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnpTiltle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // pnpTiltle
            // 
            this.pnpTiltle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnpTiltle.Controls.Add(this.lblTitle);
            this.pnpTiltle.Controls.Add(this.radioFolio);
            this.pnpTiltle.Controls.Add(this.radioFecha);
            this.pnpTiltle.Controls.Add(this.radioGroup1);
            this.pnpTiltle.Controls.Add(this.btnClose);
            this.pnpTiltle.Controls.Add(this.lblTitulo);
            this.pnpTiltle.Controls.Add(this.pictureBox1);
            this.pnpTiltle.Controls.Add(this.pnlCalendar);
            this.pnpTiltle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnpTiltle.Location = new System.Drawing.Point(0, 0);
            this.pnpTiltle.Name = "pnpTiltle";
            this.pnpTiltle.Size = new System.Drawing.Size(960, 83);
            this.pnpTiltle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(208, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(75, 20);
            this.lblTitle.TabIndex = 64;
            this.lblTitle.Text = "Filtar por";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // radioFolio
            // 
            this.radioFolio.AutoSize = true;
            this.radioFolio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFolio.ForeColor = System.Drawing.Color.White;
            this.radioFolio.Location = new System.Drawing.Point(211, 47);
            this.radioFolio.Name = "radioFolio";
            this.radioFolio.Size = new System.Drawing.Size(62, 25);
            this.radioFolio.TabIndex = 63;
            this.radioFolio.Text = "Folio";
            this.radioFolio.UseVisualStyleBackColor = true;
            this.radioFolio.CheckedChanged += new System.EventHandler(this.radioFolio_CheckedChanged);
            // 
            // radioFecha
            // 
            this.radioFecha.AutoSize = true;
            this.radioFecha.Checked = true;
            this.radioFecha.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFecha.ForeColor = System.Drawing.Color.White;
            this.radioFecha.Location = new System.Drawing.Point(211, 21);
            this.radioFecha.Name = "radioFecha";
            this.radioFecha.Size = new System.Drawing.Size(68, 25);
            this.radioFecha.TabIndex = 62;
            this.radioFecha.TabStop = true;
            this.radioFecha.Text = "Fecha";
            this.radioFecha.UseVisualStyleBackColor = true;
            this.radioFecha.CheckedChanged += new System.EventHandler(this.radioFecha_CheckedChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(193, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Size = new System.Drawing.Size(100, 69);
            this.radioGroup1.TabIndex = 61;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(789, 28);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(159, 28);
            this.btnClose.TabIndex = 57;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(32, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(86, 25);
            this.lblTitulo.TabIndex = 52;
            this.lblTitulo.Text = "Ordenes";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.movimientos;
            this.pictureBox1.InitialImage = global::SOAPAP.Properties.Resources.movimientos;
            this.pictureBox1.Location = new System.Drawing.Point(9, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            // 
            // pnlCalendar
            // 
            this.pnlCalendar.Controls.Add(this.pbBuscar);
            this.pnlCalendar.Controls.Add(this.txtFolioSearch);
            this.pnlCalendar.Controls.Add(this.tableLayoutPanel1);
            this.pnlCalendar.Controls.Add(this.pbBG);
            this.pnlCalendar.Location = new System.Drawing.Point(320, 0);
            this.pnlCalendar.Name = "pnlCalendar";
            this.pnlCalendar.Size = new System.Drawing.Size(386, 83);
            this.pnlCalendar.TabIndex = 51;
            // 
            // pbBuscar
            // 
            this.pbBuscar.BackColor = System.Drawing.Color.White;
            this.pbBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBuscar.Image = global::SOAPAP.Properties.Resources.buscar_gris;
            this.pbBuscar.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbBuscar.InitialImage")));
            this.pbBuscar.Location = new System.Drawing.Point(324, 22);
            this.pbBuscar.Name = "pbBuscar";
            this.pbBuscar.Size = new System.Drawing.Size(26, 34);
            this.pbBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBuscar.TabIndex = 55;
            this.pbBuscar.TabStop = false;
            this.pbBuscar.Visible = false;
            this.pbBuscar.Click += new System.EventHandler(this.pbBuscar_Click);
            // 
            // txtFolioSearch
            // 
            this.txtFolioSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFolioSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioSearch.Location = new System.Drawing.Point(47, 25);
            this.txtFolioSearch.Multiline = true;
            this.txtFolioSearch.Name = "txtFolioSearch";
            this.txtFolioSearch.Size = new System.Drawing.Size(275, 33);
            this.txtFolioSearch.TabIndex = 53;
            this.txtFolioSearch.Tag = "Folio";
            this.txtFolioSearch.Visible = false;
            this.txtFolioSearch.TextChanged += new System.EventHandler(this.txtFolioSearch_TextChanged_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(44, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(295, 31);
            this.tableLayoutPanel1.TabIndex = 52;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(289, 20);
            this.dateTimePicker1.TabIndex = 46;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // pbBG
            // 
            this.pbBG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBG.BackColor = System.Drawing.Color.Transparent;
            this.pbBG.Image = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.Location = new System.Drawing.Point(23, 17);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(336, 48);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 51;
            this.pbBG.TabStop = false;
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AllowUserToResizeColumns = false;
            this.dgvOrders.AllowUserToResizeRows = false;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 83);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(960, 481);
            this.dgvOrders.TabIndex = 1;
            this.dgvOrders.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvOrders_CellPainting);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 564);
            this.panel1.TabIndex = 2;
            // 
            // FindOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 564);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.pnpTiltle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FindOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FindOrders";
            this.Load += new System.EventHandler(this.FindOrders_Load);
            this.Resize += new System.EventHandler(this.FindOrders_Resize);
            this.pnpTiltle.ResumeLayout(false);
            this.pnpTiltle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlCalendar.ResumeLayout(false);
            this.pnlCalendar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnpTiltle;
        private System.Windows.Forms.Panel pnlCalendar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioFolio;
        private System.Windows.Forms.RadioButton radioFecha;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.TextBox txtFolioSearch;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbBuscar;
    }
}