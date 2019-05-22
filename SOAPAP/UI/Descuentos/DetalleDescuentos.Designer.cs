namespace SOAPAP.UI.Descuentos
{
    partial class DetalleDescuentos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlHeaderBuscar = new System.Windows.Forms.Panel();
            this.pnlHeaderLeft = new System.Windows.Forms.Panel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvDiscounts = new System.Windows.Forms.DataGridView();
            this.pbBG = new System.Windows.Forms.PictureBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.pnlHeaderBuscar.SuspendLayout();
            this.pnlHeaderLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.pnlHeaderBuscar);
            this.panel1.Controls.Add(this.pnlHeaderLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 60);
            this.panel1.TabIndex = 0;
            // 
            // pnlHeaderBuscar
            // 
            this.pnlHeaderBuscar.Controls.Add(this.dateTimePicker1);
            this.pnlHeaderBuscar.Controls.Add(this.pbBG);
            this.pnlHeaderBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderBuscar.Location = new System.Drawing.Point(276, 0);
            this.pnlHeaderBuscar.Name = "pnlHeaderBuscar";
            this.pnlHeaderBuscar.Size = new System.Drawing.Size(868, 60);
            this.pnlHeaderBuscar.TabIndex = 50;
            // 
            // pnlHeaderLeft
            // 
            this.pnlHeaderLeft.Controls.Add(this.pbxIcon);
            this.pnlHeaderLeft.Controls.Add(this.lblTitulo);
            this.pnlHeaderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHeaderLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaderLeft.Name = "pnlHeaderLeft";
            this.pnlHeaderLeft.Size = new System.Drawing.Size(276, 60);
            this.pnlHeaderLeft.TabIndex = 48;
            // 
            // pbxIcon
            // 
            this.pbxIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbxIcon.Image = global::SOAPAP.Properties.Resources.cobro;
            this.pbxIcon.InitialImage = global::SOAPAP.Properties.Resources.movimientos;
            this.pbxIcon.Location = new System.Drawing.Point(26, 17);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Size = new System.Drawing.Size(20, 20);
            this.pbxIcon.TabIndex = 37;
            this.pbxIcon.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(207, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "Detalle de Descuentos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvDiscounts
            // 
            this.dgvDiscounts.AllowUserToAddRows = false;
            this.dgvDiscounts.AllowUserToDeleteRows = false;
            this.dgvDiscounts.AllowUserToResizeColumns = false;
            this.dgvDiscounts.AllowUserToResizeRows = false;
            this.dgvDiscounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiscounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiscounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDiscounts.Location = new System.Drawing.Point(0, 60);
            this.dgvDiscounts.MultiSelect = false;
            this.dgvDiscounts.Name = "dgvDiscounts";
            this.dgvDiscounts.ReadOnly = true;
            this.dgvDiscounts.RowHeadersVisible = false;
            this.dgvDiscounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiscounts.Size = new System.Drawing.Size(1144, 473);
            this.dgvDiscounts.TabIndex = 1;
            this.dgvDiscounts.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvDiscounts_CellMouseDoubleClick);
            // 
            // pbBG
            // 
            this.pbBG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBG.BackColor = System.Drawing.Color.Transparent;
            this.pbBG.Image = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.InitialImage = global::SOAPAP.Properties.Resources.bg;
            this.pbBG.Location = new System.Drawing.Point(77, 9);
            this.pbBG.Name = "pbBG";
            this.pbBG.Size = new System.Drawing.Size(303, 39);
            this.pbBG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBG.TabIndex = 43;
            this.pbBG.TabStop = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(115, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(236, 20);
            this.dateTimePicker1.TabIndex = 48;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.DateTimePicker1_ValueChanged);
            // 
            // DetalleDescuentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 533);
            this.Controls.Add(this.dgvDiscounts);
            this.Controls.Add(this.panel1);
            this.Name = "DetalleDescuentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de Descuentos";
            this.Load += new System.EventHandler(this.DetalleDescuentos_Load);
            this.panel1.ResumeLayout(false);
            this.pnlHeaderBuscar.ResumeLayout(false);
            this.pnlHeaderLeft.ResumeLayout(false);
            this.pnlHeaderLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlHeaderBuscar;
        private System.Windows.Forms.Panel pnlHeaderLeft;
        private System.Windows.Forms.PictureBox pbxIcon;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvDiscounts;
        private System.Windows.Forms.PictureBox pbBG;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}