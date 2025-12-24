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
            this.colNodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDegree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.tlbMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.flpRight = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnConnectedComponents = new System.Windows.Forms.Button();
            this.btnAStar = new System.Windows.Forms.Button();
            this.btnDijkstra = new System.Windows.Forms.Button();
            this.btnDFS = new System.Windows.Forms.Button();
            this.btnBFS = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbEndNode = new System.Windows.Forms.ComboBox();
            this.cmbStartNode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnResetTerminal = new System.Windows.Forms.Button();
            this.txtTerminal = new System.Windows.Forms.TextBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.flpLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteEdge = new System.Windows.Forms.Button();
            this.btnAddEdge = new System.Windows.Forms.Button();
            this.btnUpdateNode = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.tlbMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.flpRight.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.flpLeft.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lstPeople
            // 
            this.lstPeople.FormattingEnabled = true;
            this.lstPeople.ItemHeight = 16;
            this.lstPeople.Location = new System.Drawing.Point(3, 83);
            this.lstPeople.Name = "lstPeople";
            this.lstPeople.Size = new System.Drawing.Size(266, 100);
            this.lstPeople.TabIndex = 0;
            this.lstPeople.SelectedIndexChanged += new System.EventHandler(this.lstPeople_SelectedIndexChanged);
            // 
            // btnKucukGraf
            // 
            this.btnKucukGraf.Location = new System.Drawing.Point(127, 21);
            this.btnKucukGraf.Name = "btnKucukGraf";
            this.btnKucukGraf.Size = new System.Drawing.Size(126, 32);
            this.btnKucukGraf.TabIndex = 1;
            this.btnKucukGraf.Text = "Küçük Graf";
            this.btnKucukGraf.UseVisualStyleBackColor = true;
            this.btnKucukGraf.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuyukGraf
            // 
            this.btnBuyukGraf.Location = new System.Drawing.Point(0, 21);
            this.btnBuyukGraf.Name = "btnBuyukGraf";
            this.btnBuyukGraf.Size = new System.Drawing.Size(123, 32);
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
            this.dgvTopNodes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTopNodes.Location = new System.Drawing.Point(3, 416);
            this.dgvTopNodes.Name = "dgvTopNodes";
            this.dgvTopNodes.RowHeadersWidth = 51;
            this.dgvTopNodes.RowTemplate.Height = 24;
            this.dgvTopNodes.Size = new System.Drawing.Size(283, 127);
            this.dgvTopNodes.TabIndex = 3;
            this.dgvTopNodes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colNodeID
            // 
            this.colNodeID.HeaderText = "Düğüm ID";
            this.colNodeID.MinimumWidth = 6;
            this.colNodeID.Name = "colNodeID";
            this.colNodeID.Width = 90;
            // 
            // colDegree
            // 
            this.colDegree.HeaderText = "Bağlantı Sayısı";
            this.colDegree.MinimumWidth = 6;
            this.colDegree.Name = "colDegree";
            this.colDegree.Width = 90;
            // 
            // pbCanvas
            // 
            this.pbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCanvas.Location = new System.Drawing.Point(307, 3);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(602, 647);
            this.pbCanvas.TabIndex = 4;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Click += new System.EventHandler(this.pbCanvas_Click_1);
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseClick);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // tlbMain
            // 
            this.tlbMain.ColumnCount = 3;
            this.tlbMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlbMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlbMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlbMain.Controls.Add(this.pbCanvas, 1, 0);
            this.tlbMain.Controls.Add(this.pnlRight, 2, 0);
            this.tlbMain.Controls.Add(this.pnlLeft, 0, 0);
            this.tlbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.RowCount = 1;
            this.tlbMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlbMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlbMain.Size = new System.Drawing.Size(1216, 653);
            this.tlbMain.TabIndex = 5;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlRight.Controls.Add(this.flpRight);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(915, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(298, 647);
            this.pnlRight.TabIndex = 0;
            // 
            // flpRight
            // 
            this.flpRight.AutoScroll = true;
            this.flpRight.Controls.Add(this.groupBox3);
            this.flpRight.Controls.Add(this.groupBox4);
            this.flpRight.Controls.Add(this.groupBox5);
            this.flpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRight.Location = new System.Drawing.Point(0, 0);
            this.flpRight.Name = "flpRight";
            this.flpRight.Size = new System.Drawing.Size(298, 647);
            this.flpRight.TabIndex = 3;
            this.flpRight.WrapContents = false;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.btnConnectedComponents);
            this.groupBox3.Controls.Add(this.btnAStar);
            this.groupBox3.Controls.Add(this.btnDijkstra);
            this.groupBox3.Controls.Add(this.btnDFS);
            this.groupBox3.Controls.Add(this.btnBFS);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(279, 151);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Algoritmalar";
            // 
            // btnConnectedComponents
            // 
            this.btnConnectedComponents.Location = new System.Drawing.Point(75, 100);
            this.btnConnectedComponents.Name = "btnConnectedComponents";
            this.btnConnectedComponents.Size = new System.Drawing.Size(130, 30);
            this.btnConnectedComponents.TabIndex = 4;
            this.btnConnectedComponents.Text = "Bağlı Bileşen";
            this.btnConnectedComponents.UseVisualStyleBackColor = true;
            this.btnConnectedComponents.Click += new System.EventHandler(this.btnConnectedComponents_Click);
            // 
            // btnAStar
            // 
            this.btnAStar.Location = new System.Drawing.Point(143, 65);
            this.btnAStar.Name = "btnAStar";
            this.btnAStar.Size = new System.Drawing.Size(130, 30);
            this.btnAStar.TabIndex = 3;
            this.btnAStar.Text = "A*";
            this.btnAStar.UseVisualStyleBackColor = true;
            this.btnAStar.Click += new System.EventHandler(this.btnAStar_Click);
            // 
            // btnDijkstra
            // 
            this.btnDijkstra.Location = new System.Drawing.Point(143, 29);
            this.btnDijkstra.Name = "btnDijkstra";
            this.btnDijkstra.Size = new System.Drawing.Size(130, 30);
            this.btnDijkstra.TabIndex = 2;
            this.btnDijkstra.Text = "Dijkstra";
            this.btnDijkstra.UseVisualStyleBackColor = true;
            this.btnDijkstra.Click += new System.EventHandler(this.btnDijkstra_Click);
            // 
            // btnDFS
            // 
            this.btnDFS.Location = new System.Drawing.Point(7, 65);
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(130, 30);
            this.btnDFS.TabIndex = 1;
            this.btnDFS.Text = "DFS";
            this.btnDFS.UseVisualStyleBackColor = true;
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            // 
            // btnBFS
            // 
            this.btnBFS.Location = new System.Drawing.Point(7, 29);
            this.btnBFS.Name = "btnBFS";
            this.btnBFS.Size = new System.Drawing.Size(130, 30);
            this.btnBFS.TabIndex = 0;
            this.btnBFS.Text = "BFS";
            this.btnBFS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.cmbEndNode);
            this.groupBox4.Controls.Add(this.cmbStartNode);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(3, 160);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(270, 81);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // cmbEndNode
            // 
            this.cmbEndNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndNode.FormattingEnabled = true;
            this.cmbEndNode.Location = new System.Drawing.Point(143, 36);
            this.cmbEndNode.Name = "cmbEndNode";
            this.cmbEndNode.Size = new System.Drawing.Size(121, 24);
            this.cmbEndNode.TabIndex = 3;
            this.cmbEndNode.SelectedIndexChanged += new System.EventHandler(this.cmbEndNode_SelectedIndexChanged);
            // 
            // cmbStartNode
            // 
            this.cmbStartNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartNode.FormattingEnabled = true;
            this.cmbStartNode.Location = new System.Drawing.Point(16, 36);
            this.cmbStartNode.Name = "cmbStartNode";
            this.cmbStartNode.Size = new System.Drawing.Size(121, 24);
            this.cmbStartNode.TabIndex = 2;
            this.cmbStartNode.SelectedIndexChanged += new System.EventHandler(this.cmbStartNode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hedef Düğümü:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Başlangıç Düğümü:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnResetTerminal);
            this.groupBox5.Controls.Add(this.txtTerminal);
            this.groupBox5.Location = new System.Drawing.Point(3, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(284, 422);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Terminal";
            // 
            // btnResetTerminal
            // 
            this.btnResetTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetTerminal.AutoSize = true;
            this.btnResetTerminal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnResetTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnResetTerminal.Location = new System.Drawing.Point(195, 0);
            this.btnResetTerminal.Name = "btnResetTerminal";
            this.btnResetTerminal.Size = new System.Drawing.Size(69, 26);
            this.btnResetTerminal.TabIndex = 6;
            this.btnResetTerminal.Text = "Sıfırla";
            this.btnResetTerminal.UseVisualStyleBackColor = false;
            this.btnResetTerminal.Click += new System.EventHandler(this.btnResetTerminal_Click);
            // 
            // txtTerminal
            // 
            this.txtTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTerminal.Location = new System.Drawing.Point(3, 18);
            this.txtTerminal.Multiline = true;
            this.txtTerminal.Name = "txtTerminal";
            this.txtTerminal.ReadOnly = true;
            this.txtTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTerminal.Size = new System.Drawing.Size(278, 401);
            this.txtTerminal.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlLeft.Controls.Add(this.flpLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(298, 647);
            this.pnlLeft.TabIndex = 5;
            // 
            // flpLeft
            // 
            this.flpLeft.AutoScroll = true;
            this.flpLeft.Controls.Add(this.groupBox1);
            this.flpLeft.Controls.Add(this.lstPeople);
            this.flpLeft.Controls.Add(this.groupBox2);
            this.flpLeft.Controls.Add(this.dgvTopNodes);
            this.flpLeft.Controls.Add(this.dataGridView1);
            this.flpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLeft.Location = new System.Drawing.Point(0, 0);
            this.flpLeft.Name = "flpLeft";
            this.flpLeft.Size = new System.Drawing.Size(298, 647);
            this.flpLeft.TabIndex = 4;
            this.flpLeft.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnKucukGraf);
            this.groupBox1.Controls.Add(this.btnBuyukGraf);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graf Seçimi";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.btnDeleteEdge);
            this.groupBox2.Controls.Add(this.btnAddEdge);
            this.groupBox2.Controls.Add(this.btnUpdateNode);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.btnAddNode);
            this.groupBox2.Location = new System.Drawing.Point(3, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 221);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Düğüm & Kenar İşlemleri";
            // 
            // btnDeleteEdge
            // 
            this.btnDeleteEdge.Location = new System.Drawing.Point(65, 144);
            this.btnDeleteEdge.Name = "btnDeleteEdge";
            this.btnDeleteEdge.Size = new System.Drawing.Size(132, 27);
            this.btnDeleteEdge.TabIndex = 4;
            this.btnDeleteEdge.Text = "Kenar Sil";
            this.btnDeleteEdge.UseVisualStyleBackColor = true;
            this.btnDeleteEdge.Click += new System.EventHandler(this.btnDeleteEdge_Click);
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Location = new System.Drawing.Point(65, 115);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(132, 27);
            this.btnAddEdge.TabIndex = 3;
            this.btnAddEdge.Text = "Kenar Ekle";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            this.btnAddEdge.Click += new System.EventHandler(this.btnAddEdge_Click);
            // 
            // btnUpdateNode
            // 
            this.btnUpdateNode.Location = new System.Drawing.Point(65, 85);
            this.btnUpdateNode.Name = "btnUpdateNode";
            this.btnUpdateNode.Size = new System.Drawing.Size(132, 27);
            this.btnUpdateNode.TabIndex = 2;
            this.btnUpdateNode.Text = "Düğüm Güncelle";
            this.btnUpdateNode.UseVisualStyleBackColor = true;
            this.btnUpdateNode.Click += new System.EventHandler(this.btnUpdateNode_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 27);
            this.button2.TabIndex = 1;
            this.button2.Text = "Düğüm Sil";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(65, 173);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(132, 27);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Temizle";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(65, 22);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(132, 27);
            this.btnAddNode.TabIndex = 0;
            this.btnAddNode.Text = "Düğüm Ekle";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 549);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(283, 122);
            this.dataGridView1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 653);
            this.Controls.Add(this.tlbMain);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sosyal Ağ Analizi";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.tlbMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.flpRight.ResumeLayout(false);
            this.flpRight.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.flpLeft.ResumeLayout(false);
            this.flpLeft.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPeople;
        private System.Windows.Forms.Button btnKucukGraf;
        private System.Windows.Forms.Button btnBuyukGraf;
        private System.Windows.Forms.DataGridView dgvTopNodes;
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.TableLayoutPanel tlbMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.FlowLayoutPanel flpLeft;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteEdge;
        private System.Windows.Forms.Button btnAddEdge;
        private System.Windows.Forms.Button btnUpdateNode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDegree;
        private System.Windows.Forms.FlowLayoutPanel flpRight;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBFS;
        private System.Windows.Forms.Button btnConnectedComponents;
        private System.Windows.Forms.Button btnAStar;
        private System.Windows.Forms.Button btnDijkstra;
        private System.Windows.Forms.Button btnDFS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbStartNode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEndNode;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtTerminal;
        private System.Windows.Forms.Button btnResetTerminal;
    }
}