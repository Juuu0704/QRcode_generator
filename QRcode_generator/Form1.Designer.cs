namespace QRcode_generator
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.QRpicture = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BtnAperçu = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.QRpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(676, 403);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(75, 23);
            this.BtnGenerate.TabIndex = 0;
            this.BtnGenerate.Text = "Generate";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // QRpicture
            // 
            this.QRpicture.Location = new System.Drawing.Point(316, 180);
            this.QRpicture.Name = "QRpicture";
            this.QRpicture.Size = new System.Drawing.Size(100, 50);
            this.QRpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.QRpicture.TabIndex = 1;
            this.QRpicture.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(316, 75);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // BtnAperçu
            // 
            this.BtnAperçu.Location = new System.Drawing.Point(676, 347);
            this.BtnAperçu.Name = "BtnAperçu";
            this.BtnAperçu.Size = new System.Drawing.Size(75, 39);
            this.BtnAperçu.TabIndex = 4;
            this.BtnAperçu.Text = "Aperçu pour impression";
            this.BtnAperçu.UseVisualStyleBackColor = true;
            this.BtnAperçu.Click += new System.EventHandler(this.BtnAperçu_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnAperçu);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.QRpicture);
            this.Controls.Add(this.BtnGenerate);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.QRpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.PictureBox QRpicture;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnAperçu;
        //private System.Windows.Forms.PrintDialog printDialog1;
    }
}

