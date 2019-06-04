namespace SOAPAP.UI
{
    partial class ModalDetalleCaja
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlImagen = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlDataGrid = new System.Windows.Forms.Panel();
            this.dgvConceptosCobro = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalDescuento = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlImagen.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptosCobro)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlImagen
            // 
            this.pnlImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlImagen.Controls.Add(this.panel2);
            this.pnlImagen.Controls.Add(this.panel1);
            this.pnlImagen.Controls.Add(this.lblTotalDescuento);
            this.pnlImagen.Controls.Add(this.label1);
            this.pnlImagen.Controls.Add(this.lblStatus);
            this.pnlImagen.Controls.Add(this.lblTotal);
            this.pnlImagen.Controls.Add(this.lblTitle);
            this.pnlImagen.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImagen.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlImagen.ForeColor = System.Drawing.Color.White;
            this.pnlImagen.Location = new System.Drawing.Point(0, 0);
            this.pnlImagen.Name = "pnlImagen";
            this.pnlImagen.Size = new System.Drawing.Size(651, 116);
            this.pnlImagen.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(33, 98);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 16);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "label2";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(651, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = " Conceptos de deuda";
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(0, 83);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(651, 33);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "$0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(651, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Detalle";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnAceptar);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 330);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(651, 36);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(543, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(97, 31);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.pnlDataGrid);
            this.pnlContent.Controls.Add(this.pnlImagen);
            this.pnlContent.Controls.Add(this.pnlButtons);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(653, 368);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlDataGrid
            // 
            this.pnlDataGrid.Controls.Add(this.dgvConceptosCobro);
            this.pnlDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataGrid.Location = new System.Drawing.Point(0, 116);
            this.pnlDataGrid.Name = "pnlDataGrid";
            this.pnlDataGrid.Size = new System.Drawing.Size(651, 217);
            this.pnlDataGrid.TabIndex = 91;
            // 
            // dgvConceptosCobro
            // 
            this.dgvConceptosCobro.AllowUserToAddRows = false;
            this.dgvConceptosCobro.AllowUserToDeleteRows = false;
            this.dgvConceptosCobro.AllowUserToResizeColumns = false;
            this.dgvConceptosCobro.AllowUserToResizeRows = false;
            this.dgvConceptosCobro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConceptosCobro.BackgroundColor = System.Drawing.Color.White;
            this.dgvConceptosCobro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvConceptosCobro.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvConceptosCobro.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvConceptosCobro.ColumnHeadersHeight = 35;
            this.dgvConceptosCobro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvConceptosCobro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Description,
            this.OriginalAmount,
            this.Descuento,
            this.Amount,
            this.OnAccount,
            this.Total});
            this.dgvConceptosCobro.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConceptosCobro.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvConceptosCobro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConceptosCobro.GridColor = System.Drawing.Color.White;
            this.dgvConceptosCobro.Location = new System.Drawing.Point(0, 0);
            this.dgvConceptosCobro.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dgvConceptosCobro.Name = "dgvConceptosCobro";
            this.dgvConceptosCobro.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConceptosCobro.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvConceptosCobro.RowHeadersVisible = false;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvConceptosCobro.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvConceptosCobro.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvConceptosCobro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConceptosCobro.Size = new System.Drawing.Size(651, 217);
            this.dgvConceptosCobro.TabIndex = 90;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 160.1227F;
            this.Description.HeaderText = "Concepto";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // OriginalAmount
            // 
            this.OriginalAmount.DataPropertyName = "OriginalAmount";
            this.OriginalAmount.HeaderText = "Imp. Original";
            this.OriginalAmount.Name = "OriginalAmount";
            // 
            // Descuento
            // 
            this.Descuento.DataPropertyName = "AmountDiscount";
            this.Descuento.HeaderText = "Imp. Descuento";
            this.Descuento.Name = "Descuento";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.FillWeight = 200.8199F;
            this.Amount.HeaderText = "Importe";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // OnAccount
            // 
            this.OnAccount.DataPropertyName = "OnAccount";
            this.OnAccount.FillWeight = 103.8629F;
            this.OnAccount.HeaderText = "A Cuenta";
            this.OnAccount.Name = "OnAccount";
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.FillWeight = 60.93083F;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // lblTotalDescuento
            // 
            this.lblTotalDescuento.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalDescuento.Font = new System.Drawing.Font("Microsoft YaHei UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDescuento.Location = new System.Drawing.Point(0, 67);
            this.lblTotalDescuento.Name = "lblTotalDescuento";
            this.lblTotalDescuento.Size = new System.Drawing.Size(651, 16);
            this.lblTotalDescuento.TabIndex = 4;
            this.lblTotalDescuento.Text = "Total Sin Descuento: $0.00";
            this.lblTotalDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(6, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 15);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(17, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 15);
            this.panel2.TabIndex = 5;
            // 
            // ModalDetalleCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(653, 368);
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ModalDetalleCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBoxForm";
            this.Load += new System.EventHandler(this.ModalRetiroCaja_Load);
            this.pnlImagen.ResumeLayout(false);
            this.pnlImagen.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptosCobro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImagen;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dgvConceptosCobro;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.Label lblTotalDescuento;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}