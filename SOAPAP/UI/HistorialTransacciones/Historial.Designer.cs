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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chcbxOperador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.pcbIncomeByConcept = new System.Windows.Forms.PictureBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pgcHistorial = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dataHistorialBindingSource = new System.Windows.Forms.BindingSource();
            this.pgfMetodo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfCajero = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfType = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfFecha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfDetalle = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfSubtotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcbxOperador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIncomeByConcept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgcHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataHistorialBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.btnExportar);
            this.pnlHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeader.Controls.Add(this.pcbIncomeByConcept);
            this.pnlHeader.Controls.Add(this.btnCargar);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 106);
            this.pnlHeader.TabIndex = 50;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.White;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(655, 50);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(133, 39);
            this.btnExportar.TabIndex = 56;
            this.btnExportar.Text = "EXPORTAR";
            this.btnExportar.UseVisualStyleBackColor = false;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(208, 23);
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
            this.pcbIncomeByConcept.Location = new System.Drawing.Point(182, 20);
            this.pcbIncomeByConcept.Name = "pcbIncomeByConcept";
            this.pcbIncomeByConcept.Size = new System.Drawing.Size(451, 54);
            this.pcbIncomeByConcept.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIncomeByConcept.TabIndex = 52;
            this.pcbIncomeByConcept.TabStop = false;
            // 
            // btnCargar
            // 
            this.btnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargar.BackColor = System.Drawing.Color.White;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnCargar.ForeColor = System.Drawing.Color.Black;
            this.btnCargar.Location = new System.Drawing.Point(655, 7);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(133, 39);
            this.btnCargar.TabIndex = 18;
            this.btnCargar.Text = "CARGAR";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
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
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pgcHistorial, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 314);
            this.tableLayoutPanel1.TabIndex = 51;
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
            this.pgfDetalle,
            this.pgfSubtotal});
            this.pgcHistorial.Location = new System.Drawing.Point(403, 3);
            this.pgcHistorial.Name = "pgcHistorial";
            this.pgcHistorial.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pgcHistorial.Size = new System.Drawing.Size(394, 308);
            this.pgcHistorial.TabIndex = 0;
            // 
            // dataHistorialBindingSource
            // 
            this.dataHistorialBindingSource.DataSource = typeof(SOAPAP.UI.HistorialTransacciones.DataHistorial);
            // 
            // pgfMetodo
            // 
            this.pgfMetodo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfMetodo.AreaIndex = 3;
            this.pgfMetodo.Caption = "Metodo";
            this.pgfMetodo.FieldName = "MetodoPago";
            this.pgfMetodo.Name = "pgfMetodo";
            // 
            // pgfCajero
            // 
            this.pgfCajero.AreaIndex = 0;
            this.pgfCajero.Caption = "Cajero";
            this.pgfCajero.FieldName = "Cajero";
            this.pgfCajero.Name = "pgfCajero";
            // 
            // pgfType
            // 
            this.pgfType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfType.AreaIndex = 0;
            this.pgfType.Caption = "Tipo Op";
            this.pgfType.FieldName = "TypeTransactionName";
            this.pgfType.Name = "pgfType";
            // 
            // pgfFecha
            // 
            this.pgfFecha.AreaIndex = 1;
            this.pgfFecha.Caption = "Fecha";
            this.pgfFecha.FieldName = "FechaTransaction";
            this.pgfFecha.Name = "pgfFecha";
            // 
            // pgfTotal
            // 
            this.pgfTotal.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfTotal.AreaIndex = 1;
            this.pgfTotal.Caption = "Total";
            this.pgfTotal.FieldName = "Total";
            this.pgfTotal.Name = "pgfTotal";
            // 
            // pgfDetalle
            // 
            this.pgfDetalle.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfDetalle.AreaIndex = 2;
            this.pgfDetalle.Caption = "Detalle";
            this.pgfDetalle.FieldName = "Detalle";
            this.pgfDetalle.Name = "pgfDetalle";
            // 
            // pgfSubtotal
            // 
            this.pgfSubtotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfSubtotal.AreaIndex = 0;
            this.pgfSubtotal.Caption = "Subtotal";
            this.pgfSubtotal.FieldName = "Subtotal";
            this.pgfSubtotal.Name = "pgfSubtotal";
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            ((System.ComponentModel.ISupportInitialize)(this.pgcHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataHistorialBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.PictureBox pcbIncomeByConcept;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcHistorial;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcbxOperador;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMetodo;
        private System.Windows.Forms.BindingSource dataHistorialBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCajero;
        private DevExpress.XtraPivotGrid.PivotGridField pgfType;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFecha;
        private DevExpress.XtraPivotGrid.PivotGridField pgfTotal;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDetalle;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSubtotal;
    }
}