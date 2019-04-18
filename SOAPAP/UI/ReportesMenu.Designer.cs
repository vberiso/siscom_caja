namespace SOAPAP.UI
{
    partial class ReportesMenu
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
            this.button2 = new System.Windows.Forms.Button();
            this.btnIPC = new System.Windows.Forms.Button();
            this.btnCF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(70, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 57);
            this.button2.TabIndex = 19;
            this.button2.Text = "INGRESOS DE CAJA";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnIPC
            // 
            this.btnIPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIPC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.btnIPC.FlatAppearance.BorderSize = 0;
            this.btnIPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIPC.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnIPC.ForeColor = System.Drawing.Color.White;
            this.btnIPC.Location = new System.Drawing.Point(323, 52);
            this.btnIPC.Name = "btnIPC";
            this.btnIPC.Size = new System.Drawing.Size(145, 57);
            this.btnIPC.TabIndex = 20;
            this.btnIPC.Text = "INGRESOS POR CONCEPTO";
            this.btnIPC.UseVisualStyleBackColor = false;
            this.btnIPC.Click += new System.EventHandler(this.btnIPC_Click);
            // 
            // btnCF
            // 
            this.btnCF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.btnCF.FlatAppearance.BorderSize = 0;
            this.btnCF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCF.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.btnCF.ForeColor = System.Drawing.Color.White;
            this.btnCF.Location = new System.Drawing.Point(583, 52);
            this.btnCF.Name = "btnCF";
            this.btnCF.Size = new System.Drawing.Size(145, 57);
            this.btnCF.TabIndex = 21;
            this.btnCF.Text = "PADRON AGUA";
            this.btnCF.UseVisualStyleBackColor = false;
            this.btnCF.Click += new System.EventHandler(this.btnCF_Click);
            // 
            // ReportesMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 349);
            this.Controls.Add(this.btnCF);
            this.Controls.Add(this.btnIPC);
            this.Controls.Add(this.button2);
            this.Name = "ReportesMenu";
            this.Text = "ReportesMenu";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnIPC;
        private System.Windows.Forms.Button btnCF;
    }
}