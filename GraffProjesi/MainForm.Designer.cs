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
            this.label5 = new System.Windows.Forms.Label();
            this.btnWelshPowell = new System.Windows.Forms.Button();
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtTerminal = new System.Windows.Forms.RichTextBox();
            this.btnResetTerminal = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.flpLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnImportCsv = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblColoringTitle = new System.Windows.Forms.Label();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.dgvColoring = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnUpdateNode = new System.Windows.Forms.Button();
            this.btnDeleteEdge = new System.Windows.Forms.Button();
            this.btnAddEdge = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvColoring)).BeginInit();
            this.SuspendLayout();
            // 
            // lstPeople
            // 
            this.lstPeople.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstPeople.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lstPeople.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lstPeople.FormattingEnabled = true;
            this.lstPeople.ItemHeight = 18;
            this.lstPeople.Location = new System.Drawing.Point(121, 135);
            this.lstPeople.Name = "lstPeople";
            this.lstPeople.Size = new System.Drawing.Size(227, 184);
            this.lstPeople.TabIndex = 0;
            this.lstPeople.SelectedIndexChanged += new System.EventHandler(this.lstPeople_SelectedIndexChanged);
            // 
            // btnKucukGraf
            // 
            this.btnKucukGraf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnKucukGraf.Location = new System.Drawing.Point(237, 46);
            this.btnKucukGraf.Name = "btnKucukGraf";
            this.btnKucukGraf.Size = new System.Drawing.Size(126, 32);
            this.btnKucukGraf.TabIndex = 1;
            this.btnKucukGraf.Text = "Küçük Graf";
            this.btnKucukGraf.UseVisualStyleBackColor = true;
            this.btnKucukGraf.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBuyukGraf
            // 
            this.btnBuyukGraf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuyukGraf.Location = new System.Drawing.Point(108, 46);
            this.btnBuyukGraf.Name = "btnBuyukGraf";
            this.btnBuyukGraf.Size = new System.Drawing.Size(123, 32);
            this.btnBuyukGraf.TabIndex = 2;
            this.btnBuyukGraf.Text = "Büyük Graf";
            this.btnBuyukGraf.UseVisualStyleBackColor = true;
            this.btnBuyukGraf.Click += new System.EventHandler(this.btnBuyukGraf_Click);
            // 
            // dgvTopNodes
            // 
            this.dgvTopNodes.AllowUserToAddRows = false;
            this.dgvTopNodes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvTopNodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopNodes.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvTopNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopNodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNodeID,
            this.colDegree});
            this.dgvTopNodes.Location = new System.Drawing.Point(95, 141);
            this.dgvTopNodes.Name = "dgvTopNodes";
            this.dgvTopNodes.RowHeadersVisible = false;
            this.dgvTopNodes.RowHeadersWidth = 51;
            this.dgvTopNodes.RowTemplate.Height = 24;
            this.dgvTopNodes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvTopNodes.Size = new System.Drawing.Size(273, 175);
            this.dgvTopNodes.TabIndex = 5;
            // 
            // colNodeID
            // 
            this.colNodeID.HeaderText = "Düğüm ID";
            this.colNodeID.MinimumWidth = 4;
            this.colNodeID.Name = "colNodeID";
            this.colNodeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDegree
            // 
            this.colDegree.HeaderText = "Bağlantı Sayısı";
            this.colDegree.MinimumWidth = 4;
            this.colDegree.Name = "colDegree";
            this.colDegree.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pbCanvas
            // 
            this.pbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCanvas.Location = new System.Drawing.Point(478, 3);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(945, 1049);
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
            this.tlbMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1125F));
            this.tlbMain.Size = new System.Drawing.Size(1902, 1055);
            this.tlbMain.TabIndex = 5;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlRight.Controls.Add(this.flpRight);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(1429, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(470, 1049);
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
            this.flpRight.Size = new System.Drawing.Size(470, 1049);
            this.flpRight.TabIndex = 3;
            this.flpRight.WrapContents = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnWelshPowell);
            this.groupBox3.Controls.Add(this.btnConnectedComponents);
            this.groupBox3.Controls.Add(this.btnAStar);
            this.groupBox3.Controls.Add(this.btnDijkstra);
            this.groupBox3.Controls.Add(this.btnDFS);
            this.groupBox3.Controls.Add(this.btnBFS);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(467, 148);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(183, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Algoritmalar";
            // 
            // btnWelshPowell
            // 
            this.btnWelshPowell.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnWelshPowell.Location = new System.Drawing.Point(237, 118);
            this.btnWelshPowell.Name = "btnWelshPowell";
            this.btnWelshPowell.Size = new System.Drawing.Size(130, 30);
            this.btnWelshPowell.TabIndex = 5;
            this.btnWelshPowell.Text = "Renklendirme";
            this.btnWelshPowell.UseVisualStyleBackColor = true;
            this.btnWelshPowell.Click += new System.EventHandler(this.btnWelshPowell_Click);
            // 
            // btnConnectedComponents
            // 
            this.btnConnectedComponents.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConnectedComponents.Location = new System.Drawing.Point(101, 118);
            this.btnConnectedComponents.Name = "btnConnectedComponents";
            this.btnConnectedComponents.Size = new System.Drawing.Size(130, 30);
            this.btnConnectedComponents.TabIndex = 4;
            this.btnConnectedComponents.Text = "Bağlı Bileşen";
            this.btnConnectedComponents.UseVisualStyleBackColor = true;
            this.btnConnectedComponents.Click += new System.EventHandler(this.btnConnectedComponents_Click);
            // 
            // btnAStar
            // 
            this.btnAStar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAStar.Location = new System.Drawing.Point(237, 82);
            this.btnAStar.Name = "btnAStar";
            this.btnAStar.Size = new System.Drawing.Size(130, 30);
            this.btnAStar.TabIndex = 3;
            this.btnAStar.Text = "A*";
            this.btnAStar.UseVisualStyleBackColor = true;
            this.btnAStar.Click += new System.EventHandler(this.btnAStar_Click);
            // 
            // btnDijkstra
            // 
            this.btnDijkstra.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDijkstra.Location = new System.Drawing.Point(237, 46);
            this.btnDijkstra.Name = "btnDijkstra";
            this.btnDijkstra.Size = new System.Drawing.Size(130, 30);
            this.btnDijkstra.TabIndex = 2;
            this.btnDijkstra.Text = "Dijkstra";
            this.btnDijkstra.UseVisualStyleBackColor = true;
            this.btnDijkstra.Click += new System.EventHandler(this.btnDijkstra_Click);
            // 
            // btnDFS
            // 
            this.btnDFS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDFS.Location = new System.Drawing.Point(101, 82);
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(130, 30);
            this.btnDFS.TabIndex = 1;
            this.btnDFS.Text = "DFS";
            this.btnDFS.UseVisualStyleBackColor = true;
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            // 
            // btnBFS
            // 
            this.btnBFS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBFS.Location = new System.Drawing.Point(101, 46);
            this.btnBFS.Name = "btnBFS";
            this.btnBFS.Size = new System.Drawing.Size(130, 30);
            this.btnBFS.TabIndex = 0;
            this.btnBFS.Text = "BFS";
            this.btnBFS.UseVisualStyleBackColor = true;
            this.btnBFS.Click += new System.EventHandler(this.btnBFS_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.cmbEndNode);
            this.groupBox4.Controls.Add(this.cmbStartNode);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(3, 157);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(467, 83);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // cmbEndNode
            // 
            this.cmbEndNode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbEndNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbEndNode.FormattingEnabled = true;
            this.cmbEndNode.Location = new System.Drawing.Point(250, 36);
            this.cmbEndNode.Name = "cmbEndNode";
            this.cmbEndNode.Size = new System.Drawing.Size(150, 26);
            this.cmbEndNode.TabIndex = 3;
            this.cmbEndNode.SelectedIndexChanged += new System.EventHandler(this.cmbEndNode_SelectedIndexChanged);
            // 
            // cmbStartNode
            // 
            this.cmbStartNode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbStartNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbStartNode.FormattingEnabled = true;
            this.cmbStartNode.Location = new System.Drawing.Point(64, 36);
            this.cmbStartNode.Name = "cmbStartNode";
            this.cmbStartNode.Size = new System.Drawing.Size(150, 26);
            this.cmbStartNode.TabIndex = 2;
            this.cmbStartNode.SelectedIndexChanged += new System.EventHandler(this.cmbStartNode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(269, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hedef Düğümü:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(61, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Başlangıç Düğümü:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtTerminal);
            this.groupBox5.Controls.Add(this.btnResetTerminal);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox5.Location = new System.Drawing.Point(3, 246);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(467, 841);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(50, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "TERMİNAL";
            // 
            // txtTerminal
            // 
            this.txtTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTerminal.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTerminal.Location = new System.Drawing.Point(45, 34);
            this.txtTerminal.Name = "txtTerminal";
            this.txtTerminal.ReadOnly = true;
            this.txtTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtTerminal.Size = new System.Drawing.Size(404, 813);
            this.txtTerminal.TabIndex = 5;
            this.txtTerminal.Text = "";
            // 
            // btnResetTerminal
            // 
            this.btnResetTerminal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResetTerminal.AutoSize = true;
            this.btnResetTerminal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnResetTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnResetTerminal.Location = new System.Drawing.Point(365, 0);
            this.btnResetTerminal.Name = "btnResetTerminal";
            this.btnResetTerminal.Size = new System.Drawing.Size(69, 31);
            this.btnResetTerminal.TabIndex = 6;
            this.btnResetTerminal.Text = "Sıfırla";
            this.btnResetTerminal.UseVisualStyleBackColor = false;
            this.btnResetTerminal.Click += new System.EventHandler(this.btnResetTerminal_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlLeft.Controls.Add(this.flpLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(469, 1049);
            this.pnlLeft.TabIndex = 5;
            // 
            // flpLeft
            // 
            this.flpLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpLeft.AutoScroll = true;
            this.flpLeft.Controls.Add(this.groupBox1);
            this.flpLeft.Controls.Add(this.lstPeople);
            this.flpLeft.Controls.Add(this.label3);
            this.flpLeft.Controls.Add(this.groupBox2);
            this.flpLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLeft.Location = new System.Drawing.Point(0, -39);
            this.flpLeft.Name = "flpLeft";
            this.flpLeft.Size = new System.Drawing.Size(469, 1166);
            this.flpLeft.TabIndex = 4;
            this.flpLeft.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnExportCsv);
            this.groupBox1.Controls.Add(this.btnImportCsv);
            this.groupBox1.Controls.Add(this.btnKucukGraf);
            this.groupBox1.Controls.Add(this.btnBuyukGraf);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 126);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(182, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Graf Seçimi";
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExportCsv.Location = new System.Drawing.Point(108, 89);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(123, 32);
            this.btnExportCsv.TabIndex = 6;
            this.btnExportCsv.Text = "Veri Saklama";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnImportCsv
            // 
            this.btnImportCsv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImportCsv.Location = new System.Drawing.Point(237, 89);
            this.btnImportCsv.Name = "btnImportCsv";
            this.btnImportCsv.Size = new System.Drawing.Size(126, 32);
            this.btnImportCsv.TabIndex = 9;
            this.btnImportCsv.Text = "Veri Yükleme";
            this.btnImportCsv.UseVisualStyleBackColor = true;
            this.btnImportCsv.Click += new System.EventHandler(this.btnImportCsv_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(133, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Düğüm ve Kenar İşlemleri";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblColoringTitle);
            this.groupBox2.Controls.Add(this.btnAddNode);
            this.groupBox2.Controls.Add(this.dgvColoring);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.dgvTopNodes);
            this.groupBox2.Controls.Add(this.btnUpdateNode);
            this.groupBox2.Controls.Add(this.btnDeleteEdge);
            this.groupBox2.Controls.Add(this.btnAddEdge);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(3, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 782);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(152, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Merkezilik (Degree)";
            // 
            // lblColoringTitle
            // 
            this.lblColoringTitle.AutoSize = true;
            this.lblColoringTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblColoringTitle.Location = new System.Drawing.Point(93, 334);
            this.lblColoringTitle.Name = "lblColoringTitle";
            this.lblColoringTitle.Size = new System.Drawing.Size(255, 20);
            this.lblColoringTitle.TabIndex = 7;
            this.lblColoringTitle.Text = "Welsh–Powell Graf Renklendirme";
            // 
            // btnAddNode
            // 
            this.btnAddNode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddNode.Location = new System.Drawing.Point(67, 12);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(132, 27);
            this.btnAddNode.TabIndex = 0;
            this.btnAddNode.Text = "Düğüm Ekle";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dgvColoring
            // 
            this.dgvColoring.AllowUserToAddRows = false;
            this.dgvColoring.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvColoring.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColoring.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvColoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColoring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvColoring.Location = new System.Drawing.Point(95, 357);
            this.dgvColoring.Name = "dgvColoring";
            this.dgvColoring.RowHeadersVisible = false;
            this.dgvColoring.RowHeadersWidth = 51;
            this.dgvColoring.RowTemplate.Height = 24;
            this.dgvColoring.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvColoring.Size = new System.Drawing.Size(273, 404);
            this.dgvColoring.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Düğüm ID";
            this.Column1.MinimumWidth = 4;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Renk Kodu";
            this.Column2.MinimumWidth = 4;
            this.Column2.Name = "Column2";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnReset.Location = new System.Drawing.Point(253, 78);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(132, 27);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Temizle";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(67, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 27);
            this.button2.TabIndex = 1;
            this.button2.Text = "Düğüm Sil";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUpdateNode
            // 
            this.btnUpdateNode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUpdateNode.Location = new System.Drawing.Point(67, 78);
            this.btnUpdateNode.Name = "btnUpdateNode";
            this.btnUpdateNode.Size = new System.Drawing.Size(132, 27);
            this.btnUpdateNode.TabIndex = 2;
            this.btnUpdateNode.Text = "Düğüm Güncelle";
            this.btnUpdateNode.UseVisualStyleBackColor = true;
            this.btnUpdateNode.Click += new System.EventHandler(this.btnUpdateNode_Click);
            // 
            // btnDeleteEdge
            // 
            this.btnDeleteEdge.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDeleteEdge.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDeleteEdge.Location = new System.Drawing.Point(253, 45);
            this.btnDeleteEdge.Name = "btnDeleteEdge";
            this.btnDeleteEdge.Size = new System.Drawing.Size(132, 27);
            this.btnDeleteEdge.TabIndex = 4;
            this.btnDeleteEdge.Text = "Kenar Sil";
            this.btnDeleteEdge.UseVisualStyleBackColor = true;
            this.btnDeleteEdge.Click += new System.EventHandler(this.btnDeleteEdge_Click);
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddEdge.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddEdge.Location = new System.Drawing.Point(253, 12);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(132, 27);
            this.btnAddEdge.TabIndex = 3;
            this.btnAddEdge.Text = "Kenar Ekle";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            this.btnAddEdge.Click += new System.EventHandler(this.btnAddEdge_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1055);
            this.Controls.Add(this.tlbMain);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sosyal Ağ Analizi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.tlbMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.flpRight.ResumeLayout(false);
            this.flpRight.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.flpLeft.ResumeLayout(false);
            this.flpLeft.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColoring)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvColoring;
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
        private System.Windows.Forms.Button btnResetTerminal;
        private System.Windows.Forms.RichTextBox txtTerminal;
        private System.Windows.Forms.Button btnWelshPowell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblColoringTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDegree;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnImportCsv;
    }
}