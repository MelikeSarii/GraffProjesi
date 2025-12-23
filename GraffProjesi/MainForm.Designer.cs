namespace GraffProjesi
{
    partial class MainForm
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
            this.lstPeople = new System.Windows.Forms.ListBox();
            this.btnKucukGraf = new System.Windows.Forms.Button();
            this.btnBuyukGraf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPeople
            // 
            this.lstPeople.FormattingEnabled = true;
            this.lstPeople.ItemHeight = 16;
            this.lstPeople.Location = new System.Drawing.Point(12, 5);
            this.lstPeople.Name = "lstPeople";
            this.lstPeople.Size = new System.Drawing.Size(173, 452);
            this.lstPeople.TabIndex = 0;
            this.lstPeople.SelectedIndexChanged += new System.EventHandler(this.lstPeople_SelectedIndexChanged);
            // 
            // btnKucukGraf
            // 
            this.btnKucukGraf.Location = new System.Drawing.Point(246, 154);
            this.btnKucukGraf.Name = "btnKucukGraf";
            this.btnKucukGraf.Size = new System.Drawing.Size(125, 23);
            this.btnKucukGraf.TabIndex = 1;
            this.btnKucukGraf.Text = "Küçük Graf";
            this.btnKucukGraf.UseVisualStyleBackColor = true;
            this.btnKucukGraf.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuyukGraf
            // 
            this.btnBuyukGraf.Location = new System.Drawing.Point(246, 205);
            this.btnBuyukGraf.Name = "btnBuyukGraf";
            this.btnBuyukGraf.Size = new System.Drawing.Size(125, 23);
            this.btnBuyukGraf.TabIndex = 2;
            this.btnBuyukGraf.Text = "Büyük Graf";
            this.btnBuyukGraf.UseVisualStyleBackColor = true;
            this.btnBuyukGraf.Click += new System.EventHandler(this.btnBuyukGraf_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBuyukGraf);
            this.Controls.Add(this.btnKucukGraf);
            this.Controls.Add(this.lstPeople);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPeople;
        private System.Windows.Forms.Button btnKucukGraf;
        private System.Windows.Forms.Button btnBuyukGraf;
    }
}