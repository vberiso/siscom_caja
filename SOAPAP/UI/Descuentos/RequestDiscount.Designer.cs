namespace SOAPAP.UI.Descuentos
{
    partial class RequestDiscount
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grbFormDiscount = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcbPreview = new System.Windows.Forms.PictureBox();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.txtObservations = new System.Windows.Forms.TextBox();
            this.txtAmountDiscount = new System.Windows.Forms.TextBox();
            this.lblObservations = new System.Windows.Forms.Label();
            this.lblAmountDiscount = new System.Windows.Forms.Label();
            this.cmbTypeDescount = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.lblTypeDescount = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblFolio = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grbFormDiscount.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbPreview)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.panel2);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(624, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(79, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Solicitud de descuento";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(73, 55);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SOAPAP.Properties.Resources.descuento;
            this.pictureBox2.Location = new System.Drawing.Point(24, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 37);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SOAPAP.Properties.Resources.cruz_signo_remover;
            this.pictureBox1.Location = new System.Drawing.Point(548, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 15);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // grbFormDiscount
            // 
            this.grbFormDiscount.Controls.Add(this.groupBox1);
            this.grbFormDiscount.Controls.Add(this.lblLeyenda);
            this.grbFormDiscount.Controls.Add(this.txtObservations);
            this.grbFormDiscount.Controls.Add(this.txtAmountDiscount);
            this.grbFormDiscount.Controls.Add(this.lblObservations);
            this.grbFormDiscount.Controls.Add(this.lblAmountDiscount);
            this.grbFormDiscount.Controls.Add(this.cmbTypeDescount);
            this.grbFormDiscount.Controls.Add(this.txtAmount);
            this.grbFormDiscount.Controls.Add(this.txtFolio);
            this.grbFormDiscount.Controls.Add(this.lblTypeDescount);
            this.grbFormDiscount.Controls.Add(this.lblAmount);
            this.grbFormDiscount.Controls.Add(this.lblFolio);
            this.grbFormDiscount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbFormDiscount.Location = new System.Drawing.Point(7, 4);
            this.grbFormDiscount.Name = "grbFormDiscount";
            this.grbFormDiscount.Size = new System.Drawing.Size(606, 383);
            this.grbFormDiscount.TabIndex = 1;
            this.grbFormDiscount.TabStop = false;
            this.grbFormDiscount.Text = "Información";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(302, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 328);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Previsualizador";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 293);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 32);
            this.panel3.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOpen, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Enabled = false;
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(67)))), ((int)(((byte)(21)))));
            this.btnRemove.FlatAppearance.BorderSize = 2;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(149, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(140, 26);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Quitar";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnOpen.FlatAppearance.BorderSize = 2;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(140, 26);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Seleccionar";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pcbPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 306);
            this.panel1.TabIndex = 0;
            // 
            // pcbPreview
            // 
            this.pcbPreview.BackgroundImage = global::SOAPAP.Properties.Resources.folder;
            this.pcbPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbPreview.Location = new System.Drawing.Point(0, 0);
            this.pcbPreview.Name = "pcbPreview";
            this.pcbPreview.Size = new System.Drawing.Size(292, 306);
            this.pcbPreview.TabIndex = 11;
            this.pcbPreview.TabStop = false;
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeyenda.ForeColor = System.Drawing.Color.Gray;
            this.lblLeyenda.Location = new System.Drawing.Point(-3, 364);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(161, 16);
            this.lblLeyenda.TabIndex = 8;
            this.lblLeyenda.Text = "El monto es el calculo sin IVA";
            // 
            // txtObservations
            // 
            this.txtObservations.Location = new System.Drawing.Point(14, 246);
            this.txtObservations.Multiline = true;
            this.txtObservations.Name = "txtObservations";
            this.txtObservations.Size = new System.Drawing.Size(280, 115);
            this.txtObservations.TabIndex = 7;
            this.txtObservations.Validating += new System.ComponentModel.CancelEventHandler(this.TxtObservations_Validating);
            // 
            // txtAmountDiscount
            // 
            this.txtAmountDiscount.Location = new System.Drawing.Point(14, 197);
            this.txtAmountDiscount.Name = "txtAmountDiscount";
            this.txtAmountDiscount.Size = new System.Drawing.Size(280, 23);
            this.txtAmountDiscount.TabIndex = 7;
            this.txtAmountDiscount.Visible = false;
            this.txtAmountDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAmountDiscount_KeyPress);
            this.txtAmountDiscount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAmountDiscount_Validating);
            // 
            // lblObservations
            // 
            this.lblObservations.AutoSize = true;
            this.lblObservations.Location = new System.Drawing.Point(11, 226);
            this.lblObservations.Name = "lblObservations";
            this.lblObservations.Size = new System.Drawing.Size(94, 17);
            this.lblObservations.TabIndex = 6;
            this.lblObservations.Text = "Observaciones";
            // 
            // lblAmountDiscount
            // 
            this.lblAmountDiscount.AutoSize = true;
            this.lblAmountDiscount.Location = new System.Drawing.Point(11, 177);
            this.lblAmountDiscount.Name = "lblAmountDiscount";
            this.lblAmountDiscount.Size = new System.Drawing.Size(69, 17);
            this.lblAmountDiscount.TabIndex = 6;
            this.lblAmountDiscount.Text = "Descuento";
            this.lblAmountDiscount.Visible = false;
            // 
            // cmbTypeDescount
            // 
            this.cmbTypeDescount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDescount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeDescount.FormattingEnabled = true;
            this.cmbTypeDescount.Items.AddRange(new object[] {
            "Seleccionar Opcion",
            "Monto",
            "Porcentaje"});
            this.cmbTypeDescount.Location = new System.Drawing.Point(14, 144);
            this.cmbTypeDescount.Name = "cmbTypeDescount";
            this.cmbTypeDescount.Size = new System.Drawing.Size(280, 27);
            this.cmbTypeDescount.TabIndex = 5;
            this.cmbTypeDescount.SelectionChangeCommitted += new System.EventHandler(this.CmbTypeDescount_SelectionChangeCommitted);
            this.cmbTypeDescount.Validating += new System.ComponentModel.CancelEventHandler(this.CmbTypeDescount_Validating);
            // 
            // txtAmount
            // 
            this.txtAmount.Enabled = false;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(14, 93);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(280, 27);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAmount_Validating);
            // 
            // txtFolio
            // 
            this.txtFolio.Enabled = false;
            this.txtFolio.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(14, 41);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(280, 27);
            this.txtFolio.TabIndex = 3;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.TxtFolio_Validating);
            // 
            // lblTypeDescount
            // 
            this.lblTypeDescount.AutoSize = true;
            this.lblTypeDescount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeDescount.Location = new System.Drawing.Point(11, 125);
            this.lblTypeDescount.Name = "lblTypeDescount";
            this.lblTypeDescount.Size = new System.Drawing.Size(118, 17);
            this.lblTypeDescount.TabIndex = 0;
            this.lblTypeDescount.Text = "Tipo de Descuento";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(11, 74);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(47, 17);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "Monto";
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolio.Location = new System.Drawing.Point(11, 21);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(36, 17);
            this.lblFolio.TabIndex = 0;
            this.lblFolio.Text = "Folio";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnSend);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 448);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(624, 43);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(457, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(105, 28);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // pnlBackground
            // 
            this.pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackground.Controls.Add(this.pnlFooter);
            this.pnlBackground.Controls.Add(this.pnlContent);
            this.pnlBackground.Controls.Add(this.pnlHeader);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(626, 493);
            this.pnlBackground.TabIndex = 3;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.grbFormDiscount);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 55);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(624, 436);
            this.pnlContent.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // RequestDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(626, 493);
            this.Controls.Add(this.pnlBackground);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RequestDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RequestDiscount";
            this.Load += new System.EventHandler(this.RequestDiscount_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grbFormDiscount.ResumeLayout(false);
            this.grbFormDiscount.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbPreview)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlBackground.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox grbFormDiscount;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.ComboBox cmbTypeDescount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblTypeDescount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtObservations;
        private System.Windows.Forms.TextBox txtAmountDiscount;
        private System.Windows.Forms.Label lblObservations;
        private System.Windows.Forms.Label lblAmountDiscount;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pcbPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnOpen;
    }
}