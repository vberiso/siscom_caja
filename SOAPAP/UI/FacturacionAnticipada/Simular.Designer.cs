namespace SOAPAP.UI.FacturacionAnticipada
{
    partial class Simular
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridViewServicios = new System.Windows.Forms.DataGridView();
            this.paelAnual = new System.Windows.Forms.Panel();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServicios)).BeginInit();
            this.paelAnual.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 46);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Simulación";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.paelAnual);
            this.panel2.Controls.Add(this.lblTotal);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblIva);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnAccept);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 282);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(524, 74);
            this.panel2.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(128, 40);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(51, 18);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(226, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Iva:";
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(332, 40);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(51, 18);
            this.lblIva.TabIndex = 7;
            this.lblIva.Text = "label3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Total con iva:";
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(412, 36);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(96, 27);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Ok";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click_1);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridViewServicios);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(524, 236);
            this.panel3.TabIndex = 5;
            // 
            // dataGridViewServicios
            // 
            this.dataGridViewServicios.AllowUserToAddRows = false;
            this.dataGridViewServicios.AllowUserToDeleteRows = false;
            this.dataGridViewServicios.AllowUserToResizeColumns = false;
            this.dataGridViewServicios.AllowUserToResizeRows = false;
            this.dataGridViewServicios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewServicios.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewServicios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewServicios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewServicios.ColumnHeadersHeight = 35;
            this.dataGridViewServicios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridViewServicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewServicios.GridColor = System.Drawing.Color.White;
            this.dataGridViewServicios.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewServicios.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dataGridViewServicios.Name = "dataGridViewServicios";
            this.dataGridViewServicios.ReadOnly = true;
            this.dataGridViewServicios.RowHeadersVisible = false;
            this.dataGridViewServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewServicios.Size = new System.Drawing.Size(524, 236);
            this.dataGridViewServicios.TabIndex = 0;
            // 
            // paelAnual
            // 
            this.paelAnual.Controls.Add(this.lblDescuento);
            this.paelAnual.Controls.Add(this.label6);
            this.paelAnual.Controls.Add(this.lblSubtotal);
            this.paelAnual.Controls.Add(this.label4);
            this.paelAnual.Dock = System.Windows.Forms.DockStyle.Top;
            this.paelAnual.Location = new System.Drawing.Point(0, 0);
            this.paelAnual.Name = "paelAnual";
            this.paelAnual.Size = new System.Drawing.Size(524, 35);
            this.paelAnual.TabIndex = 10;
            this.paelAnual.Visible = false;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.Location = new System.Drawing.Point(331, 14);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(51, 18);
            this.lblDescuento.TabIndex = 17;
            this.lblDescuento.Text = "label3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(225, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 16;
            this.label6.Text = "Descuento:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.Location = new System.Drawing.Point(130, 12);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(51, 18);
            this.lblSubtotal.TabIndex = 15;
            this.lblSubtotal.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "Sub Total:";
            // 
            // Simular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(524, 356);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Simular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Simular_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServicios)).EndInit();
            this.paelAnual.ResumeLayout(false);
            this.paelAnual.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridViewServicios;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel paelAnual;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label4;
    }
}