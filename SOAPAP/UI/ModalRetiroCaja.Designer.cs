namespace SOAPAP.UI
{
    partial class ModalRetiroCaja
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
            this.pnlImagen = new System.Windows.Forms.Panel();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlText = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblCashBox = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRetiradoTransferencia = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCobradoTransferencia = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRetiradoTarjeta = new System.Windows.Forms.Label();
            this.lbltxtTransferencia = new System.Windows.Forms.Label();
            this.lblCobradoTarjeta = new System.Windows.Forms.Label();
            this.lbltxtfectivo = new System.Windows.Forms.Label();
            this.lblCobradoEfectivo = new System.Windows.Forms.Label();
            this.lblRetiradoCheque = new System.Windows.Forms.Label();
            this.lbltxtTarjeta = new System.Windows.Forms.Label();
            this.lblRetiradoEfectivo = new System.Windows.Forms.Label();
            this.lbltxtCheque = new System.Windows.Forms.Label();
            this.lblCobradoCheque = new System.Windows.Forms.Label();
            this.pnlImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.pnlText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImagen
            // 
            this.pnlImagen.Controls.Add(this.pbxIcon);
            this.pnlImagen.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImagen.Location = new System.Drawing.Point(0, 0);
            this.pnlImagen.Name = "pnlImagen";
            this.pnlImagen.Size = new System.Drawing.Size(410, 102);
            this.pnlImagen.TabIndex = 0;
            // 
            // pbxIcon
            // 
            this.pbxIcon.Image = global::SOAPAP.Properties.Resources.withdrawal;
            this.pbxIcon.Location = new System.Drawing.Point(151, 10);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Size = new System.Drawing.Size(85, 85);
            this.pbxIcon.TabIndex = 0;
            this.pbxIcon.TabStop = false;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 102);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(410, 35);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(410, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Retiro de Caja";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.comboBox1);
            this.pnlText.Controls.Add(this.lblCashBox);
            this.pnlText.Controls.Add(this.nudAmount);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Location = new System.Drawing.Point(0, 274);
            this.pnlText.Name = "pnlText";
            this.pnlText.Size = new System.Drawing.Size(411, 71);
            this.pnlText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(135, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tipo:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(197, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 4;
            // 
            // lblCashBox
            // 
            this.lblCashBox.AutoSize = true;
            this.lblCashBox.ForeColor = System.Drawing.Color.DimGray;
            this.lblCashBox.Location = new System.Drawing.Point(142, 5);
            this.lblCashBox.Name = "lblCashBox";
            this.lblCashBox.Size = new System.Drawing.Size(0, 16);
            this.lblCashBox.TabIndex = 3;
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(197, 9);
            this.nudAmount.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(120, 21);
            this.nudAmount.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importe:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnAceptar);
            this.pnlButtons.Controls.Add(this.btnCancelar);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 433);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(410, 42);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(106, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(97, 31);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(209, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 31);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            this.pnlContent.Controls.Add(this.pnlText);
            this.pnlContent.Controls.Add(this.pnlTitle);
            this.pnlContent.Controls.Add(this.pnlImagen);
            this.pnlContent.Controls.Add(this.pnlButtons);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(412, 477);
            this.pnlContent.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.04255F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.95744F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanel1.Controls.Add(this.lblRetiradoTransferencia, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCobradoTransferencia, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRetiradoTarjeta, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbltxtTransferencia, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblCobradoTarjeta, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbltxtfectivo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCobradoEfectivo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRetiradoCheque, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbltxtTarjeta, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblRetiradoEfectivo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbltxtCheque, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCobradoCheque, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 137);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 100);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lblRetiradoTransferencia
            // 
            this.lblRetiradoTransferencia.AutoSize = true;
            this.lblRetiradoTransferencia.BackColor = System.Drawing.Color.White;
            this.lblRetiradoTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetiradoTransferencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetiradoTransferencia.Location = new System.Drawing.Point(238, 80);
            this.lblRetiradoTransferencia.Name = "lblRetiradoTransferencia";
            this.lblRetiradoTransferencia.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblRetiradoTransferencia.Size = new System.Drawing.Size(169, 20);
            this.lblRetiradoTransferencia.TabIndex = 17;
            this.lblRetiradoTransferencia.Text = "$0.00";
            this.lblRetiradoTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(83, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cobrado";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCobradoTransferencia
            // 
            this.lblCobradoTransferencia.AutoSize = true;
            this.lblCobradoTransferencia.BackColor = System.Drawing.Color.White;
            this.lblCobradoTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCobradoTransferencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobradoTransferencia.Location = new System.Drawing.Point(83, 80);
            this.lblCobradoTransferencia.Name = "lblCobradoTransferencia";
            this.lblCobradoTransferencia.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblCobradoTransferencia.Size = new System.Drawing.Size(149, 20);
            this.lblCobradoTransferencia.TabIndex = 16;
            this.lblCobradoTransferencia.Text = "$0.00";
            this.lblCobradoTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(238, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Por Retirar";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRetiradoTarjeta
            // 
            this.lblRetiradoTarjeta.AutoSize = true;
            this.lblRetiradoTarjeta.BackColor = System.Drawing.Color.White;
            this.lblRetiradoTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetiradoTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetiradoTarjeta.Location = new System.Drawing.Point(238, 60);
            this.lblRetiradoTarjeta.Name = "lblRetiradoTarjeta";
            this.lblRetiradoTarjeta.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblRetiradoTarjeta.Size = new System.Drawing.Size(169, 20);
            this.lblRetiradoTarjeta.TabIndex = 12;
            this.lblRetiradoTarjeta.Text = "$0.00";
            this.lblRetiradoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltxtTransferencia
            // 
            this.lbltxtTransferencia.AutoSize = true;
            this.lbltxtTransferencia.BackColor = System.Drawing.Color.White;
            this.lbltxtTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtTransferencia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtTransferencia.Location = new System.Drawing.Point(3, 80);
            this.lbltxtTransferencia.Name = "lbltxtTransferencia";
            this.lbltxtTransferencia.Size = new System.Drawing.Size(74, 20);
            this.lbltxtTransferencia.TabIndex = 15;
            this.lbltxtTransferencia.Text = "Trasferecia:";
            this.lbltxtTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCobradoTarjeta
            // 
            this.lblCobradoTarjeta.AutoSize = true;
            this.lblCobradoTarjeta.BackColor = System.Drawing.Color.White;
            this.lblCobradoTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCobradoTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobradoTarjeta.Location = new System.Drawing.Point(83, 60);
            this.lblCobradoTarjeta.Name = "lblCobradoTarjeta";
            this.lblCobradoTarjeta.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblCobradoTarjeta.Size = new System.Drawing.Size(149, 20);
            this.lblCobradoTarjeta.TabIndex = 6;
            this.lblCobradoTarjeta.Text = "$0.00";
            this.lblCobradoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltxtfectivo
            // 
            this.lbltxtfectivo.AutoSize = true;
            this.lbltxtfectivo.BackColor = System.Drawing.Color.White;
            this.lbltxtfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtfectivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtfectivo.Location = new System.Drawing.Point(3, 20);
            this.lbltxtfectivo.Name = "lbltxtfectivo";
            this.lbltxtfectivo.Size = new System.Drawing.Size(74, 20);
            this.lbltxtfectivo.TabIndex = 1;
            this.lbltxtfectivo.Text = "Efectivo:";
            this.lbltxtfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCobradoEfectivo
            // 
            this.lblCobradoEfectivo.AutoSize = true;
            this.lblCobradoEfectivo.BackColor = System.Drawing.Color.White;
            this.lblCobradoEfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCobradoEfectivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobradoEfectivo.Location = new System.Drawing.Point(83, 20);
            this.lblCobradoEfectivo.Name = "lblCobradoEfectivo";
            this.lblCobradoEfectivo.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblCobradoEfectivo.Size = new System.Drawing.Size(149, 20);
            this.lblCobradoEfectivo.TabIndex = 4;
            this.lblCobradoEfectivo.Text = "$0.00";
            this.lblCobradoEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRetiradoCheque
            // 
            this.lblRetiradoCheque.AutoSize = true;
            this.lblRetiradoCheque.BackColor = System.Drawing.Color.White;
            this.lblRetiradoCheque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetiradoCheque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetiradoCheque.Location = new System.Drawing.Point(238, 40);
            this.lblRetiradoCheque.Name = "lblRetiradoCheque";
            this.lblRetiradoCheque.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblRetiradoCheque.Size = new System.Drawing.Size(169, 20);
            this.lblRetiradoCheque.TabIndex = 11;
            this.lblRetiradoCheque.Text = "$0.00";
            this.lblRetiradoCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltxtTarjeta
            // 
            this.lbltxtTarjeta.AutoSize = true;
            this.lbltxtTarjeta.BackColor = System.Drawing.Color.White;
            this.lbltxtTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtTarjeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtTarjeta.Location = new System.Drawing.Point(3, 60);
            this.lbltxtTarjeta.Name = "lbltxtTarjeta";
            this.lbltxtTarjeta.Size = new System.Drawing.Size(74, 20);
            this.lbltxtTarjeta.TabIndex = 3;
            this.lbltxtTarjeta.Text = "Tarjeta:";
            this.lbltxtTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRetiradoEfectivo
            // 
            this.lblRetiradoEfectivo.AutoSize = true;
            this.lblRetiradoEfectivo.BackColor = System.Drawing.Color.White;
            this.lblRetiradoEfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetiradoEfectivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetiradoEfectivo.Location = new System.Drawing.Point(238, 20);
            this.lblRetiradoEfectivo.Name = "lblRetiradoEfectivo";
            this.lblRetiradoEfectivo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblRetiradoEfectivo.Size = new System.Drawing.Size(169, 20);
            this.lblRetiradoEfectivo.TabIndex = 10;
            this.lblRetiradoEfectivo.Text = "$0.00";
            this.lblRetiradoEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltxtCheque
            // 
            this.lbltxtCheque.AutoSize = true;
            this.lbltxtCheque.BackColor = System.Drawing.Color.White;
            this.lbltxtCheque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtCheque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltxtCheque.Location = new System.Drawing.Point(3, 40);
            this.lbltxtCheque.Name = "lbltxtCheque";
            this.lbltxtCheque.Size = new System.Drawing.Size(74, 20);
            this.lbltxtCheque.TabIndex = 2;
            this.lbltxtCheque.Text = "Cheque:";
            this.lbltxtCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCobradoCheque
            // 
            this.lblCobradoCheque.AutoSize = true;
            this.lblCobradoCheque.BackColor = System.Drawing.Color.White;
            this.lblCobradoCheque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCobradoCheque.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobradoCheque.Location = new System.Drawing.Point(83, 40);
            this.lblCobradoCheque.Name = "lblCobradoCheque";
            this.lblCobradoCheque.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblCobradoCheque.Size = new System.Drawing.Size(149, 20);
            this.lblCobradoCheque.TabIndex = 5;
            this.lblCobradoCheque.Text = "$0.00";
            this.lblCobradoCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ModalRetiroCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(412, 477);
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ModalRetiroCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBoxForm";
            this.Load += new System.EventHandler(this.ModalRetiroCaja_Load);
            this.pnlImagen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImagen;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.PictureBox pbxIcon;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Label lblCashBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbltxtTarjeta;
        private System.Windows.Forms.Label lbltxtCheque;
        private System.Windows.Forms.Label lbltxtfectivo;
        private System.Windows.Forms.Label lblCobradoTarjeta;
        private System.Windows.Forms.Label lblCobradoCheque;
        private System.Windows.Forms.Label lblCobradoEfectivo;
        private System.Windows.Forms.Label lblRetiradoTarjeta;
        private System.Windows.Forms.Label lblRetiradoCheque;
        private System.Windows.Forms.Label lblRetiradoEfectivo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbltxtTransferencia;
        private System.Windows.Forms.Label lblCobradoTransferencia;
        private System.Windows.Forms.Label lblRetiradoTransferencia;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}