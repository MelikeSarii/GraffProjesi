using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using GraffProjesi;          // Graph sınıfı için
using System.IO;                 // gerekirse


namespace GraffProjesi
{
    public partial class MainForm : Form
    {
        private Dictionary<int, PointF> _nodePositions = new Dictionary<int, PointF>(); // Düğüm koordinatları
        private int _draggedNodeId = -1; // Şu an sürüklenen düğümün ID'si
        private bool _isDragging = false; // Sürükleme işlemi yapılıyor mu?

        private Graph _graph;
        private List<Person> _people;

        private Dictionary<int, int> _nodeColors; // Welsh–Powell renk sonucu: <NodeId, ColorIndex>
        private bool _isColoringActive = false;


        public MainForm()
        {
            InitializeComponent();
            SetupColoringGrid();
        }

        // Form açılır açılmaz KÜÇÜK grafı yükle
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv","Küçük Graf");
        }

        private enum EditMode
        {
            None,
            AddNode,
            DeleteNode,
            UpdateNode,
            AddEdge,
            DeleteEdge,
            SelectStart,
            SelectTarget
        }

        private EditMode _currentMode = EditMode.None;

        private void LoadData(string graphFile, string peopleFile, string description)
        {
            _startNodeId = null;
            _targetNodeId = null;

            _nodeColors = null;
            _isColoringActive = false;

            dgvColoring.Rows.Clear();
            try
            {
                _graph = new Graph();
                _graph.LoadFromFile(graphFile);

                _people = CsvPeopleLoader.Load(peopleFile);
                FillPeopleList();

                MessageBox.Show(
                    $"{description} başarıyla yüklendi.\n" +
                    $"Kişi sayısı: {_people.Count}",
                    "Yükleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{description} yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            CalculateRandomPositions();
            UpdateTopNodesTable();
            pbCanvas.Invalidate();

        }
        //düğümleri rastgele yerleştrimek için
        private void CalculateRandomPositions()
        {
            _nodePositions.Clear();
            if (_people == null || _people.Count == 0) return;

            // Panel boyutu henüz oluşmamışsa varsayılan bir değer ver
            int canvasWidth = pbCanvas.Width > 0 ? pbCanvas.Width : 600;
            int canvasHeight = pbCanvas.Height > 0 ? pbCanvas.Height : 400;

            Random rng = new Random();
            int margin = 50;
            int width = canvasWidth - (margin * 2);
            int height = canvasHeight - (margin * 2);

            foreach (var person in _people)
            {
                float x = rng.Next(margin, margin + width);
                float y = rng.Next(margin, margin + height);
                _nodePositions[person.Id] = new PointF(x, y);
            }
            pbCanvas.Invalidate();
        }

        // DÜĞÜM ÇİZİMİ: Bu metot sadece pbCanvas için çizim yapar
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_nodePositions == null || _nodePositions.Count == 0)
                return;

            // 1️ KENARLARI ÇİZ
            foreach (var nodePos in _nodePositions)
            {
                int fromId = nodePos.Key;
                var neighbors = _graph.GetNeighbors(fromId);

                foreach (var toId in neighbors)
                {
                    if (_nodePositions.ContainsKey(toId))
                    {
                        g.DrawLine(
                            Pens.Gray,
                            _nodePositions[fromId],
                            _nodePositions[toId]
                        );
                    }
                }
            }
            // 2️ DÜĞÜMLERİ ÇİZ (RENKLİ)
            foreach (var node in _nodePositions)
            {
                Color fillColor = Color.SkyBlue; // Varsayılan

                // Welsh–Powell renklendirme varsa uygula
                if (_nodeColors != null && _nodeColors.ContainsKey(node.Key))
                {
                    fillColor = GetColorByIndex(_nodeColors[node.Key]);
                }

                // Başlangıç ve hedef her zaman baskın renkte
                if (node.Key == _startNodeId)
                    fillColor = Color.LightGreen;
                else if (node.Key == _targetNodeId)
                    fillColor = Color.Orange;

                using (Brush brush = new SolidBrush(fillColor))
                {
                    g.FillEllipse(
                        brush,
                        node.Value.X - 15,
                        node.Value.Y - 15,
                        30,
                        30
                    );
                }
                g.DrawEllipse(
                    Pens.Black,
                    node.Value.X - 15,
                    node.Value.Y - 15,
                    30,
                    30
                );
                g.DrawString(  // ID yaz
                    node.Key.ToString(),
                    this.Font,
                    Brushes.Black,
                    node.Value.X - 8,
                    node.Value.Y - 8
                );
            }
        }

        private void UpdateTopNodesTable()
            {
            if (_graph == null) return;// Eğer graf yüklenmemişse işlem yapma

            var top5 = _graph.GetTop5DegreeNodes(); // Graph.cs içinde yazdığın metodu çağırarak en iyi 5 düğümü al
            dgvTopNodes.Rows.Clear(); // Tablodaki eski verileri temizle

            foreach (var node in top5)   // Listeyi değil nodeleri tabloya satır satır ekle
            { // Düğümün bağlantı sayısını (degree) Graph üzerinden alıyoruz
                int nodeId = node.Id;                 
                int degree = _graph.GetDegree(node);  

                dgvTopNodes.Rows.Add(nodeId, degree); 
            }
        }

        private void WriteToTerminal(string text) // Terminali otomatik aşağıya çekmek için
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            txtTerminal.AppendText(text + Environment.NewLine);
            txtTerminal.SelectionStart = txtTerminal.Text.Length;
            txtTerminal.ScrollToCaret();
        }

        // DÜĞÜM EKLEME
        private int _nodeCounter; // yeni eklenecek düğümlerin id numarası
        private void AddNodeAt(Point location)
        {
            if (_graph == null || _people == null)
                return;

            var result = AskNodeInfo();
            if (result == null)
                return;

            string name = result.Value.name;
            double aktiflik = result.Value.aktiflik;

            // ID'yi belirle: Eğer liste boşsa 1, değilse son kişiden devam ettir
            int id = (_people != null && _people.Any()) ? _people.Max(p => p.Id) + 1 : 1;

            // Aynı isim + aktiflik kontrolü
            bool exists = _people.Any(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                _graph.GetNode(p.Id)?.Aktiflik == aktiflik
            );

            if (exists)
            {
                MessageBox.Show(
                    "Aynı isim ve aktifliğe sahip bir düğüm zaten mevcut.",
                    "Ekleme Engellendi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            _graph.AddNode(id, aktiflik, 0, 0);  // Graph'a ekle
            
            _nodePositions[id] = location; // Çiz
    
            _people.Add(new Person // Listeye ekle
            {
                Id = id,
                Name = name
            });

            FillPeopleList();
            pbCanvas.Invalidate();
            UpdateNodeCombos();
            WriteToTerminal($"Düğüm {id} - {name} eklendi.\r\n");
        }
        private (string name, double aktiflik, int etkilesim)? AskNodeInfo() // Yeni düğüme bilgi
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 270;
                prompt.Text = "Yeni Düğüm";

                Label lblName = new Label()
                {
                    Left = 10,
                    Top = 10,
                    Text = "Kişi adı:"
                };

                TextBox txtName = new TextBox()
                {
                    Left = 10,
                    Top = 35,
                    Width = 260
                };

                Label lblAktiflik = new Label()
                {
                    Left = 10,
                    Top = 70,
                    Text = "Aktiflik (0–1):"
                };

                TextBox txtAktiflik = new TextBox()
                {
                    Left = 10,
                    Top = 95,
                    Width = 260,
                    Text = "0.5"
                };

                Label lblEtkilesim = new Label()
                {
                    Left = 10,
                    Top = 130,
                    Text = "Etkileşim (1–10):"
                };

                TextBox txtEtkilesim = new TextBox()
                {
                    Left = 10,
                    Top = 155,
                    Width = 260,
                    Text = "5"
                };

                Button btnOk = new Button()
                {
                    Text = "Tamam",
                    Left = 200,
                    Width = 70,
                    Top = 190,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.Add(lblName);
                prompt.Controls.Add(txtName);
                prompt.Controls.Add(lblAktiflik);
                prompt.Controls.Add(txtAktiflik);
                prompt.Controls.Add(lblEtkilesim);
                prompt.Controls.Add(txtEtkilesim);
                prompt.Controls.Add(btnOk);

                prompt.AcceptButton = btnOk;
                prompt.StartPosition = FormStartPosition.CenterParent;

                if (prompt.ShowDialog() != DialogResult.OK)
                    return null;

                // 🔍 Validasyonlar
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("İsim boş olamaz.");
                    return null;
                }

                if (!double.TryParse(
                        txtAktiflik.Text.Replace(',', '.'),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out double aktiflik)
                    || aktiflik < 0 || aktiflik > 1)
                {
                    MessageBox.Show("Aktiflik 0 ile 1 arasında olmalıdır.");
                    return null;
                }

                if (!int.TryParse(txtEtkilesim.Text, out int etkilesim)
                    || etkilesim < 1 || etkilesim > 10)
                {
                    MessageBox.Show("Etkileşim 1 ile 10 arasında tam sayı olmalıdır.");
                    return null;
                }

                return (
                    txtName.Text.Trim(),
                    aktiflik,
                    etkilesim
                );
            }
        }



        // DÜĞÜM SİLME
        private void DeleteNode(int nodeId)
        {
            var result = MessageBox.Show(
                $"Düğüm {nodeId} silinsin mi?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;
            //Listeden ismi bul
            var person = _people.FirstOrDefault(p => p.Id == nodeId);
            string name = person?.Name ?? "Bilinmeyen";

            // Graph içinden sil
            _graph.RemoveNode(nodeId);

            // Canvas pozisyonu sil
            _nodePositions.Remove(nodeId);

            // Listeden sil
            // var person = _people.FirstOrDefault(p => p.Id == nodeId);
            if (person != null)
                _people.Remove(person);

            FillPeopleList();
            UpdateTopNodesTable();
            pbCanvas.Invalidate();
            UpdateNodeCombos(); //Hedef düğüm seçimi listelerini günceller
            WriteToTerminal($"Düğüm {nodeId} - {name} silindi.\r\n");
        }

        // DÜĞÜM GÜNCELLEME
        private void UpdateNode(int nodeId)
        {
            var person = _people.FirstOrDefault(p => p.Id == nodeId);
            if (person == null) return;

            Node node = _graph.GetNode(nodeId);
            if (node == null) return;

            var result = AskNodeInfoForUpdate(
                person.Name,
                node.Aktiflik,
                node.Etkilesim
            );
            if (result == null)
                return;

            string oldName = person.Name;
            person.Name = result.Value.name;
            node.Aktiflik = result.Value.aktiflik;
            node.Etkilesim = result.Value.etkilesim;

            FillPeopleList();
            UpdateNodeCombos();
            pbCanvas.Invalidate();
            WriteToTerminal(
                $"{nodeId} güncellendi → " +
                $"İsim: {oldName} → {person.Name}, " +
                $"Aktiflik: {node.Aktiflik:0.##}, " +
                $"Etkileşim: {node.Etkilesim:0}"
            );
        }
            // İsim, aktiflik ve etkileşim düzenleme
        private (string name, double aktiflik, double etkilesim)? AskNodeInfoForUpdate(
        string defaultName,
        double defaultAktiflik,
        double defaultEtkilesim)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 320;
                prompt.Height = 260;
                prompt.Text = "Düğüm Güncelle";

                Label lblName = new Label() { Left = 10, Top = 10, Text = "İsim:" };
                TextBox txtName = new TextBox()
                {
                    Left = 10,
                    Top = 30,
                    Width = 280,
                    Text = defaultName
                };

                Label lblAktiflik = new Label() { Left = 10, Top = 65, Text = "Aktiflik (0–1):" };
                TextBox txtAktiflik = new TextBox()
                {
                    Left = 10,
                    Top = 85,
                    Width = 280,
                    Text = defaultAktiflik.ToString("0.##")
                };

                Label lblEtkilesim = new Label() { Left = 10, Top = 120, Text = "Etkileşim (1–10):" };
                TextBox txtEtkilesim = new TextBox()
                {
                    Left = 10,
                    Top = 140,
                    Width = 280,
                    Text = defaultEtkilesim.ToString("0")
                };

                Button btnOk = new Button()
                {
                    Text = "Güncelle",
                    Left = 210,
                    Width = 80,
                    Top = 180,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.AddRange(new Control[]
                {
            lblName, txtName,
            lblAktiflik, txtAktiflik,
            lblEtkilesim, txtEtkilesim,
            btnOk
                });

                prompt.AcceptButton = btnOk;
                prompt.StartPosition = FormStartPosition.CenterParent;

                if (prompt.ShowDialog() != DialogResult.OK)
                    return null;

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("İsim boş olamaz.");
                    return null;
                }

                if (!double.TryParse(txtAktiflik.Text, out double aktiflik) ||
                    aktiflik < 0 || aktiflik > 1)
                {
                    MessageBox.Show("Aktiflik 0 ile 1 arasında olmalıdır.");
                    return null;
                }

                if (!double.TryParse(txtEtkilesim.Text, out double etkilesim) ||
                    etkilesim < 0 || etkilesim > 10)
                {
                    MessageBox.Show("Etkileşim 0 ile 10 arasında olmalıdır.");
                    return null;
                }

                return (txtName.Text.Trim(), aktiflik, etkilesim);
            }
        }


        // EDGE EKLEME
        private int? _selectedEdgeNodeId = null; // Seçili node'yi tutar
        private void HandleAddEdgeClick(int nodeId)
        {
            // İlk düğüm seçimi
            if (_selectedEdgeNodeId == null)
            {
                _selectedEdgeNodeId = nodeId;
                WriteToTerminal($"1. düğüm seçildi: {nodeId}\r\n");
                return;
            }

            // Aynı düğümse iptal
            if (_selectedEdgeNodeId == nodeId)
            {
                WriteToTerminal("Aynı düğüm seçilemez.\r\n");
                return;
            }

            int fromId = _selectedEdgeNodeId.Value;
            int toId = nodeId;

            // Edge zaten varsa
            if (_graph.HasEdge(fromId, toId))
            {
                WriteToTerminal($"{fromId} ↔ {toId} bağlantısı zaten var.\r\n");
                _selectedEdgeNodeId = null;
                return;
            }

            _graph.AddEdge(fromId, toId); // Edge ekle
            UpdateTopNodesTable(); // top5 tablosunu güncelle
            pbCanvas.Invalidate(); // canvası güncelle
            WriteToTerminal($"{fromId} ↔ {toId} bağlantısı eklendi.\r\n");
            _selectedEdgeNodeId = null; // seçimi sıfırla
        }

        // EDGE SİLME
        private const float EDGE_CLICK_TOLERANCE = 6f;
        private (int fromId, int toId)? GetEdgeAtPoint(Point p)
        {
            foreach (var from in _nodePositions)
            {
                int fromId = from.Key;
                PointF p1 = from.Value;

                foreach (int toId in _graph.GetNeighbors(fromId))
                {
                    // Aynı edge iki kere gelmesin
                    if (fromId >= toId) continue;

                    if (!_nodePositions.ContainsKey(toId)) continue;

                    PointF p2 = _nodePositions[toId];

                    if (DistancePointToLine(p, p1, p2) <= EDGE_CLICK_TOLERANCE)
                        return (fromId, toId);
                }
            }
            return null;
        }
        private float DistancePointToLine(Point p, PointF a, PointF b) // Nokta-Çizgi mesafesi
        {
            float A = p.X - a.X;
            float B = p.Y - a.Y;
            float C = b.X - a.X;
            float D = b.Y - a.Y;

            float dot = A * C + B * D;
            float lenSq = C * C + D * D;
            float param = (lenSq != 0) ? dot / lenSq : -1;

            float xx, yy;

            if (param < 0)
            {
                xx = a.X;
                yy = a.Y;
            }
            else if (param > 1)
            {
                xx = b.X;
                yy = b.Y;
            }
            else
            {
                xx = a.X + param * C;
                yy = a.Y + param * D;
            }
            float dx = p.X - xx;
            float dy = p.Y - yy;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        private void DeleteEdge(int fromId, int toId)
        {
            _graph.RemoveEdge(fromId, toId);

            UpdateTopNodesTable();
            pbCanvas.Invalidate();

            WriteToTerminal($"{fromId} ↔ {toId} bağlantısı silindi.\r\n");
        }

        // Başlangıç - Hedef düğümü seçimi
        private int? _startNodeId = null;
        private int? _targetNodeId = null;
        private void UpdateNodeCombos()
        {
            cmbStartNode.Items.Clear();
            cmbEndNode.Items.Clear();

            if (_people == null) return;

            foreach (var p in _people)
            {
                var item = new ComboNodeItem
                {
                    Id = p.Id,
                    Text = $"{p.Id} - {p.Name}"
                };

                cmbStartNode.Items.Add(item);
                cmbEndNode.Items.Add(item);
            }
        }

        // Küçük graf butonu
        private void btnLoadSmall_Click(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv");
        }

        // Büyük graf butonu
        private void btnLoadBig_Click(object sender, EventArgs e)
        {
            LoadData("graph_buyuk.txt", "kisiler_buyuk.csv");
        }

        // Ortak yükleme metodu
        private void LoadData(string graphFile, string peopleFile)
        {
            _startNodeId = null;
            _targetNodeId = null;

            _nodeColors = null;
            _isColoringActive = false;

            dgvColoring.Rows.Clear();

            try
            {
                _graph = new Graph();
                _graph.LoadFromFile(graphFile);

                _people = CsvPeopleLoader.Load(peopleFile);

                FillPeopleList();
                CalculateRandomPositions(); // <--- VERİ YÜKLENİNCE KONUMLARI HESAPLA
                UpdateTopNodesTable(); // Tabloyu en güncel verilere göre doldur

                MessageBox.Show(
                    $"{graphFile} dosyasından graf başarıyla yüklendi.\n" +
                    $"Kişi sayısı: {_people.Count}",
                    "Yükleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Veriler yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            _nodeCounter = _people.Max(p => p.Id) + 1;
            UpdateNodeCombos();
        }

        // ListBox ve ComboBox'ları doldur
        private void FillPeopleList()
        {
            lstPeople.Items.Clear();

            if (_people == null || _people.Count == 0)
            {
                lstPeople.Items.Add("Kişi bulunamadı.");
                return;
            }

            foreach (var p in _people)
            {
                lstPeople.Items.Add($"{p.Id} - {p.Name}");
            }

            cmbStartNode.Items.Clear();
            cmbEndNode.Items.Clear();

            // Boş seçimler
            cmbStartNode.Items.Add(new ComboNodeItem { Id = -1, Text = "-- Seçiniz --" });
            cmbEndNode.Items.Add(new ComboNodeItem { Id = -1, Text = "-- Seçiniz --" });

            foreach (var p in _people)
            {
                var item = new ComboNodeItem
                {
                    Id = p.Id,
                    Text = $"{p.Id} - {p.Name}"
                };

                cmbStartNode.Items.Add(item);
                cmbEndNode.Items.Add(item);
            }
        }

        // ListBox'tan kişi seçilince detay göster
        private void lstPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex == -1 || _people == null)
                return;

            var selectedText = lstPeople.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedText))
                return;

            var idPart = selectedText.Split('-')[0].Trim();
            if (!int.TryParse(idPart, out int id))
                return;

            var person = _people.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return;

            MessageBox.Show(
                $"Id: {person.Id}\nAd Soyad: {person.Name}",
                "Kişi Bilgisi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FillColoringTable() // Düğüm boyama
        {
            dgvColoring.Rows.Clear();

            if (_nodeColors == null)
                return;

            foreach (var pair in _nodeColors)
            {
                dgvColoring.Rows.Add(
                    pair.Key,
                    pair.Value
                );
            }
        }


        // Düğüme basınca bilgi gösterme
        private const float NODE_RADIUS = 15f; //düğüm yarıçapı
        private int? GetNodeAtPoint(Point p) //düğümün neresine basarsan bas
        {
            foreach (var kvp in _nodePositions)
            {
                float dx = p.X - kvp.Value.X;
                float dy = p.Y - kvp.Value.Y;

                if (dx * dx + dy * dy <= NODE_RADIUS * NODE_RADIUS)
                    return kvp.Key;
            }

            return null;
        }

        private void SetEditMode(EditMode mode) // Edit modu ayarları:
        {
            _currentMode = mode;

            // Renkleri sıfırla
            btnAddNode.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;  //düğüm sil butonu
            btnAddEdge.BackColor = SystemColors.Control;
            btnDeleteEdge.BackColor = SystemColors.Control;
            
            // Terminal temizle
            // txtTerminal.Clear();

            switch (mode)
            {
                case EditMode.AddNode:
                    btnAddNode.BackColor = Color.LightGreen;
                    WriteToTerminal("Düğüm ekleme modu aktif.\r\n");
                    break;

                case EditMode.DeleteNode:
                    button2.BackColor = Color.IndianRed;
                    WriteToTerminal("Düğüm silme modu aktif.\r\n");
                    break;

                case EditMode.AddEdge:
                    btnAddEdge.BackColor = Color.LightBlue;
                    WriteToTerminal("Kenar ekleme modu aktif.\r\n");
                    break;

                case EditMode.DeleteEdge:
                    btnDeleteEdge.BackColor = Color.OrangeRed;
                    WriteToTerminal("Kenar silme modu aktif.\r\n");
                    break;

                case EditMode.None:
                default:
                    WriteToTerminal("Düzenleme modu kapalı.\r\n");
                    break;
            }
        }
        private void SetupColoringGrid()
        {
            dgvColoring.Columns.Clear();
            dgvColoring.Rows.Clear();

            dgvColoring.AutoGenerateColumns = false;
            dgvColoring.AllowUserToAddRows = false;
            dgvColoring.ReadOnly = true;

            // Node ID sütunu
            dgvColoring.Columns.Add("NodeId", "Düğüm ID");

            // Renk Numarası sütunu
            dgvColoring.Columns.Add("ColorNo", "Renk Kodu");
        }



        // ------- Butonlar: --------

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv", "Küçük graf");

        }

        private void btnBuyukGraf_Click(object sender, EventArgs e)
        {
            LoadData("graph_buyuk.txt", "kisiler_buyuk.csv", "Büyük graf");

        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var node in _nodePositions)
            {
                // Fare koordinatları ile düğüm merkezi arasındaki mesafeyi ölçüyoruz (Öklid uzaklığı)
                float mesafe = (float)Math.Sqrt(Math.Pow(e.X - node.Value.X, 2) + Math.Pow(e.Y - node.Value.Y, 2));

                // Eğer fare düğümün (30px çapında varsayalım) içindeyse o düğümü tut
                if (mesafe < 20)
                {
                    _draggedNodeId = node.Key;
                    _isDragging = true;
                    break;
                }
            }
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedNodeId != -1)
            {
                // Tutulan düğümün konumunu farenin güncel konumu yap
                _nodePositions[_draggedNodeId] = new PointF(e.X, e.Y);

                // Canvas'ı "geçersiz kıl" (Invalidate), bu sayede Paint olayı tetiklenir ve ekran yeniden çizilir
                pbCanvas.Invalidate();
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            _draggedNodeId = -1;
        }

        // Clickler:

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }

        private void pbCanvas_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Modu ve seçimleri sıfırla
            SetEditMode(EditMode.None);
            _draggedNodeId = -1;
            _isDragging = false;
            _selectedEdgeNodeId = null;

            // Bellekteki verileri temizle
            if (_graph != null) _graph.Clear();
            if (_people != null) _people.Clear();
            _nodePositions.Clear(); // Ekrandaki düğüm konumlarını siler

            _startNodeId = null; // Seçimleri sıfırlar
            _targetNodeId = null;
            cmbStartNode.SelectedIndex = 0;
            cmbEndNode.SelectedIndex = 0;
            dgvColoring.Rows.Clear();


            // Arayüz bileşenlerini temizle
            lstPeople.Items.Clear();
            dgvTopNodes.Rows.Clear();
            txtTerminal.Clear();
            WriteToTerminal("Sistem tamamen sıfırlandı. Yeni graf yükleyebilir veya düğüm ekleyebilirsiniz.\r\n");

            // Canvas'ı yeniden çizdir (boş)
            pbCanvas.Invalidate();
            UpdateNodeCombos(); // Listeleri sıfırlar
            _startNodeId = null;
            _targetNodeId = null; // Seçimleri sıfırlar
            _nodeColors = null; // Rekleri sıfırlar
            _isColoringActive = false;
            MessageBox.Show("Tüm veriler ve çizim alanı temizlendi.", "Sıfırla", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void cmbStartNode_SelectedIndexChanged(object sender, EventArgs e) // Start düğümü seçilsin
        {
            if (cmbStartNode.SelectedItem == null)
                return;

            ComboNodeItem selected = (ComboNodeItem)cmbStartNode.SelectedItem;
            _startNodeId = selected.Id;

            pbCanvas.Invalidate();
        }

        private void cmbEndNode_SelectedIndexChanged(object sender, EventArgs e) // End düğümü seçilsin
        {
            if (cmbEndNode.SelectedItem == null)
                return;

            ComboNodeItem selected = (ComboNodeItem)cmbEndNode.SelectedItem;
            _targetNodeId = selected.Id;

            pbCanvas.Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e) //Düğüm ekle butonu
        {
            if (_currentMode == EditMode.AddNode)
                SetEditMode(EditMode.None);
            else
                SetEditMode(EditMode.AddNode);
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (_currentMode == EditMode.AddNode) // Ekleme modundaysa ekle
            {
                AddNodeAt(e.Location);
            }
            else if (_currentMode == EditMode.DeleteNode) // Silme modundaysa sil
            {
                int? nodeId = GetNodeAtPoint(e.Location);

                if (nodeId.HasValue) //Tıklanan yerde düğüm varsa
                {
                    DeleteNode(nodeId.Value);
                }
                else
                {
                    WriteToTerminal("Tıklanan yerde düğüm yok.\r\n");
                }
            }
            else if (_currentMode == EditMode.UpdateNode)
            {
                int? nodeId = GetNodeAtPoint(e.Location);
                if (nodeId.HasValue)
                    UpdateNode(nodeId.Value);
                else
                    WriteToTerminal("Tıklanan yerde düğüm yok.\r\n");
            }
            else if (_currentMode == EditMode.AddEdge)
            {
                int? nodeId = GetNodeAtPoint(e.Location);

                if (!nodeId.HasValue)
                {
                    WriteToTerminal("Bir düğüme tıklayın.\r\n");
                    return;
                }
                HandleAddEdgeClick(nodeId.Value);
            }
            else if (_currentMode == EditMode.DeleteEdge)
            {   // düğüme mi tıklandı?
                int? nodeId = GetNodeAtPoint(e.Location);
                if (nodeId.HasValue)
                {
                    WriteToTerminal("Kenar silmek için çizgiye tıklayın.\r\n");
                    return;
                }
                // edge kontrolü
                var edge = GetEdgeAtPoint(e.Location);
                if (edge.HasValue)
                {
                    DeleteEdge(edge.Value.fromId, edge.Value.toId);
                }
                else
                {
                    WriteToTerminal("Tıklanan yerde kenar yok.\r\n");
                }
            }
            else if (_currentMode == EditMode.None) // BİLGİ GÖSTERME
            {
                int? nodeId = GetNodeAtPoint(e.Location);
                if (!nodeId.HasValue)
                    return;

                var person = _people.FirstOrDefault(p => p.Id == nodeId.Value);
                Node node = _graph.GetNode(nodeId.Value);

                if (person == null || node == null)
                    return;

                int degree = _graph.GetDegree(node);
                double interaction = node.Etkilesim;

                // Komşu düğümleri al
                var neighbors = _graph.GetNeighbors(node.Id);

                string neighborText = neighbors.Count > 0
                    ? string.Join(", ", neighbors)
                    : "Yok";

                // Terminal çıktısı
                WriteToTerminal("\n- DÜĞÜM BİLGİLERİ -");
                WriteToTerminal($"ID              : {node.Id}");
                WriteToTerminal($"Ad Soyad        : {person.Name}");
                WriteToTerminal($"Aktiflik        : {node.Aktiflik:F2}");
                WriteToTerminal($"Etkileşim       : {interaction}");
                WriteToTerminal($"Bağlantı Sayısı : {degree}");
                WriteToTerminal($"Komşu Düğümler  : {neighborText}");
                WriteToTerminal("---------------------------");
            }
        }

        private void button2_Click(object sender, EventArgs e) //Düğüm sil
        {
            if (_currentMode == EditMode.DeleteNode)
                SetEditMode(EditMode.None);
            else
                SetEditMode(EditMode.DeleteNode);
        }

        private void btnUpdateNode_Click(object sender, EventArgs e)
        {
            if (_currentMode == EditMode.UpdateNode)
                SetEditMode(EditMode.None);
            else
                SetEditMode(EditMode.UpdateNode);
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            if (_currentMode == EditMode.AddEdge)
                SetEditMode(EditMode.None);
            else
                SetEditMode(EditMode.AddEdge);

            _selectedEdgeNodeId = null;
        }

        private void btnDeleteEdge_Click(object sender, EventArgs e)
        {
            if (_currentMode == EditMode.DeleteEdge)
                SetEditMode(EditMode.None);
            else
                SetEditMode(EditMode.DeleteEdge);
        }

        private void btnResetTerminal_Click(object sender, EventArgs e)
        {
            btnAddNode.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;  //düğüm sil butonu
            btnUpdateNode.BackColor = SystemColors.Control;
            btnAddEdge.BackColor = SystemColors.Control;
            btnDeleteEdge.BackColor = SystemColors.Control;
            btnReset.BackColor = SystemColors.Control;

            // Başlangıç & hedef seçimlerini sıfırla
            if (cmbStartNode.Items.Count > 0)
                cmbStartNode.SelectedIndex = 0;

            if (cmbEndNode.Items.Count > 0)
                cmbEndNode.SelectedIndex = 0;
            txtTerminal.Clear();
        }
        // ALGORİTMA BUTONLARI
        private void btnBFS_Click(object sender, EventArgs e)
        {
            if (cmbStartNode.SelectedItem == null)
            {
                WriteToTerminal("Lütfen başlangıç düğümü seçiniz.\n\n");
                return;
            }

            ComboNodeItem selected = (ComboNodeItem)cmbStartNode.SelectedItem;
            int startId = selected.Id;

            BFSAlgorithm bfs = new BFSAlgorithm(_graph);
            bfs.Execute(startId);

            WriteToTerminal("BFS ile erişilebilen kullanıcılar:\n");
            WriteToTerminal($"Başlangıç düğümü: {selected.Text}\n\n");

            foreach (int id in bfs.Result)
            {
                var p = _people.FirstOrDefault(x => x.Id == id);
                if (p != null)
                    WriteToTerminal($"{p.Id} \n"); // - {p.Name} eklenebilir
            }

            WriteToTerminal($"Toplam erişilen kullanıcı sayısı: {bfs.Result.Count}\n\n");
        }
        private void btnDFS_Click(object sender, EventArgs e)
        {
            if (cmbStartNode.SelectedItem == null)
            {
                WriteToTerminal("Lütfen başlangıç düğümü seçiniz.\n");
                return;
            }
            ComboNodeItem selected = (ComboNodeItem)cmbStartNode.SelectedItem;
            int startId = selected.Id;

            DFSAlgorithm dfs = new DFSAlgorithm(_graph);
            dfs.Execute(startId);

            WriteToTerminal("\nDFS ile erişilebilen kullanıcılar:\n");
            WriteToTerminal($"Başlangıç düğümü: {selected.Text}\n\n");

            foreach (int id in dfs.Result)
            {
                var p = _people.FirstOrDefault(x => x.Id == id);
                WriteToTerminal($"{id} - {p?.Name}\n");
            }

            WriteToTerminal($"Toplam erişilen kullanıcı sayısı: {dfs.Result.Count}\n");
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            if (cmbStartNode.SelectedItem == null) // Başlangıç düğümü seçili mi kontrol et
            {
                WriteToTerminal("Lütfen başlangıç düğümü seçiniz.");
                return;
            }

            var startItem = (ComboNodeItem)cmbStartNode.SelectedItem;  // Seçilen başlangıç düğümünü al
            ComboNodeItem endItem = cmbEndNode.SelectedItem as ComboNodeItem;

            var dijkstra = new DijkstraAlgorithm(_graph);  // Dijkstra algoritmasını çalıştır
            dijkstra.Execute(startItem.Id);
            //txtTerminal.Clear();
            WriteToTerminal("");
            WriteToTerminal("- Dijkstra Algoritması -");
            WriteToTerminal("");

            //  İKİ DÜĞÜM SEÇİLİ İSE
            if (endItem != null)
            {
                WriteToTerminal($"Başlangıç: {startItem.Text}");
                WriteToTerminal($"Hedef    : {endItem.Text}");

                var path = dijkstra.GetPath(endItem.Id);

                if (path.Count <= 1)
                {
                    WriteToTerminal("Sonuç: Yol bulunamadı.\n");
                    return;
                }

                WriteToTerminal("Sonuç: En Kısa Yol");
                WriteToTerminal("Yol: " + string.Join(" -> ", path));
                WriteToTerminal($"Toplam Maliyet: {dijkstra.Distances[endItem.Id]:F2}");
            }
            //  SADECE BAŞLANGIÇ VAR İSE
            else
            {
                WriteToTerminal($"Başlangıç: {startItem.Text}");

                foreach (var kvp in dijkstra.Distances.OrderBy(x => x.Value))
                {
                    var person = _people.FirstOrDefault(p => p.Id == kvp.Key);

                    if (double.IsInfinity(kvp.Value))
                        WriteToTerminal($"{kvp.Key} - {person?.Name} : Ulaşılamıyor");
                    else
                        WriteToTerminal($"{kvp.Key} - {person?.Name} : {kvp.Value:F2}");
                }
            }
            WriteToTerminal("\n");
        }

        /* Dijkstra algoritması, başlangıç düğümünden tüm düğümlere olan en kısa yolları hesaplarken;
        A* algoritması, hedef düğümü de dikkate alarak yalnızca en olası yolları değerlendirir ve bu sayede daha hızlı sonuç üretir. */
        private void btnAStar_Click(object sender, EventArgs e)
        {
            //txtTerminal.Clear();
            // Başlangıç seçilmiş mi?
            if (!(cmbStartNode.SelectedItem is ComboNodeItem startItem) ||
                startItem.Id == -1)
            {
                WriteToTerminal("Lütfen başlangıç düğümünü seçiniz.");
                return;
            }
            // Hedef seçilmiş mi?
            if (!(cmbEndNode.SelectedItem is ComboNodeItem endItem) ||
                endItem.Id == -1)
            {
                WriteToTerminal("Lütfen hedef düğümü seçiniz.");
                return;
            }

            // A* çalıştır
            var aStar = new AStarAlgorithm(_graph);
            aStar.Execute(startItem.Id, endItem.Id);
            var path = aStar.GetPath(endItem.Id);

            WriteToTerminal("\n- A* Algoritması -");
            WriteToTerminal($"Başlangıç: {startItem.Text}");
            WriteToTerminal($"Hedef: {endItem.Text}");

            // Yol bulunamadıysa
            if (path.Count <= 1)
            {
                WriteToTerminal("Sonuç: Yol bulunamadı.");
                return;
            }
            WriteToTerminal("Sonuç: En Kısa Yol");
            WriteToTerminal("Yol: " + string.Join(" -> ", path));
            WriteToTerminal($"Toplam Maliyet: {aStar.TotalCost:F4}\n");
        }

        private void btnConnectedComponents_Click(object sender, EventArgs e)
        {
            var cc = new ConnectedComponentsAlgorithm(_graph);
            cc.Execute();

            WriteToTerminal("\n- Bağlı Bileşenler (Topluluk Analizi) -");
            WriteToTerminal($"Toplam Topluluk Sayısı: {cc.Components.Count}\n");

            int index = 1;
            int maxSize = cc.Components.Max(c => c.Count);

            foreach (var component in cc.Components)
            {
                string type = "";

                if (component.Count == 1)
                    type = "(İzole)";
                else if (component.Count == maxSize)
                    type = "(Baskın Grup)";

                WriteToTerminal($"Topluluk {index} {type} - {component.Count} Kişi:");
                WriteToTerminal("Üyeler: " + string.Join(", ", component));
                WriteToTerminal("");
                index++;
            }
            WriteToTerminal("\nAnaliz Tamamlandı.\n");
        }

        // WELSH POWELL Kodları
        private void btnWelshPowell_Click(object sender, EventArgs e)
        {
            // Eğer renklendirme açıksa kapat
            if (_isColoringActive)
            {
                _nodeColors = null;
                _isColoringActive = false;

                dgvColoring.Rows.Clear();
                WriteToTerminal("Welsh–Powell renklendirme kapatıldı.");

                pbCanvas.Invalidate(); // Canvas'ı yeniden çiz
                return;
            }

            // Grafik boş mu?
            if (_graph.GetAllNodes().Count() == 0)
            {
                WriteToTerminal("Graf boş. Önce düğüm ekleyiniz.");
                return;
            }

            // Welsh–Powell algoritmasını çalıştır
            var coloring = new Coloring(_graph);
            _nodeColors = coloring.ApplyWelshPowell();
            _isColoringActive = true;


            dgvColoring.Rows.Clear();      // Tabloyu temizle

            // Tabloyu RENK NUMARASINA GÖRE sıralı doldur
            foreach (var pair in _nodeColors.OrderBy(x => x.Value))
            {
                dgvColoring.Rows.Add(pair.Key, pair.Value);
            }

            WriteToTerminal("\n- Welsh–Powell Graf Renklendirme -\n");
            WriteToTerminal($"Toplam kullanılan renk sayısı: {_nodeColors.Values.Distinct().Count()}");
            WriteToTerminal("Aynı renge sahip düğümler birbirine komşu değildir.");
            WriteToTerminal("\nRenklendirme tamamlandı.\n");

            pbCanvas.Invalidate(); // Canvas'ı yenile
        }

        private Color GetColorByIndex(int index)
        {
            Color[] colors =
                    {
                Color.LightBlue,
                Color.LightPink,
                Color.LightSalmon,
                Color.LightGreen,
                Color.LightYellow,
                Color.LightCyan,
                Color.Plum,
                Color.Khaki
            };
            return colors[(index - 1) % colors.Length];
        }
    } // class kapanışı
} // namespace kapanışı