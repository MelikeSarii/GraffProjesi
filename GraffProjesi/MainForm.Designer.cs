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
            this.dgvTopNodes = new System.Windows.Forms.DataGridView();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.colNodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDegree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // lstPeople
            // 
            this.lstPeople.FormattingEnabled = true;
            this.lstPeople.ItemHeight = 16;
            this.lstPeople.Location = new System.Drawing.Point(12, 81);
            this.lstPeople.Name = "lstPeople";
            this.lstPeople.Size = new System.Drawing.Size(173, 372);
            this.lstPeople.TabIndex = 0;
            this.lstPeople.SelectedIndexChanged += new System.EventHandler(this.lstPeople_SelectedIndexChanged);
            // 
            // btnKucukGraf
            // 
            this.btnKucukGraf.Location = new System.Drawing.Point(33, 12);
            this.btnKucukGraf.Name = "btnKucukGraf";
            this.btnKucukGraf.Size = new System.Drawing.Size(125, 23);
            this.btnKucukGraf.TabIndex = 1;
            this.btnKucukGraf.Text = "Küçük Graf";
            this.btnKucukGraf.UseVisualStyleBackColor = true;
            this.btnKucukGraf.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuyukGraf
            // 
            this.btnBuyukGraf.Location = new System.Drawing.Point(33, 41);
            this.btnBuyukGraf.Name = "btnBuyukGraf";
            this.btnBuyukGraf.Size = new System.Drawing.Size(125, 23);
            this.btnBuyukGraf.TabIndex = 2;
            this.btnBuyukGraf.Text = "Büyük Graf";
            this.btnBuyukGraf.UseVisualStyleBackColor = true;
            this.btnBuyukGraf.Click += new System.EventHandler(this.btnBuyukGraf_Click);
            // 
            // dgvTopNodes
            // 
            this.dgvTopNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopNodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNodeID,
            this.colDegree});
            this.dgvTopNodes.Location = new System.Drawing.Point(12, 459);
            this.dgvTopNodes.Name = "dgvTopNodes";
            this.dgvTopNodes.RowHeadersWidth = 51;
            this.dgvTopNodes.RowTemplate.Height = 24;
            this.dgvTopNodes.Size = new System.Drawing.Size(183, 150);
            this.dgvTopNodes.TabIndex = 3;
            this.dgvTopNodes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.Location = new System.Drawing.Point(207, 12);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(656, 441);
            this.pbCanvas.TabIndex = 4;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Click += new System.EventHandler(this.pbCanvas_Click_1);
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // colNodeID
            // 
            this.colNodeID.HeaderText = "Düğüm ID";
            this.colNodeID.MinimumWidth = 6;
            this.colNodeID.Name = "colNodeID";
            this.colNodeID.Width = 65;
            // 
            // colDegree
            // 
            this.colDegree.HeaderText = "Bağlantı Sayısı";
            this.colDegree.MinimumWidth = 6;
            this.colDegree.Name = "colDegree";
            this.colDegree.Width = 65;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 621);
            this.Controls.Add(this.pbCanvas);
            this.Controls.Add(this.dgvTopNodes);
            this.Controls.Add(this.btnBuyukGraf);
            this.Controls.Add(this.btnKucukGraf);
            this.Controls.Add(this.lstPeople);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPeople;
        private System.Windows.Forms.Button btnKucukGraf;
        private System.Windows.Forms.Button btnBuyukGraf;
        private System.Windows.Forms.DataGridView dgvTopNodes;
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDegree;
    }
}