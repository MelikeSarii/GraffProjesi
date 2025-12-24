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

        public MainForm()
        {
            InitializeComponent();
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
            DeleteEdge
        }

        private EditMode _currentMode = EditMode.None;

        private void LoadData(string graphFile, string peopleFile, string description)
        {
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

        //düğüm çizimi
        // Bu metot sadece pbCanvas için çizim yapar
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_nodePositions == null || _nodePositions.Count == 0) return;

            // 1. Önce Kenarları (Çizgileri) ve Ağırlıkları Çiz
            foreach (var nodePos in _nodePositions)
            {
                int fromId = nodePos.Key;
                // Graph.cs'ye yeni eklediğimiz GetNeighbors(int id) metodunu kullanıyoruz
                var neighbors = _graph.GetNeighbors(fromId);

                foreach (var toId in neighbors)
                {
                    // Eğer komşu düğümün de konumu biliniyorsa çizgi çek
                    if (_nodePositions.ContainsKey(toId))
                    {
                        PointF p1 = nodePos.Value;
                        PointF p2 = _nodePositions[toId];
                        g.DrawLine(Pens.Gray, p1, p2); // Kenarı çiz

                        // Dinamik ağırlığı hesapla ve çizginin ortasına yaz
                        //double weight = _graph.CalculateWeight(fromId, toId);
                        //g.DrawString(weight.ToString("F2"), this.Font, Brushes.Red, (p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                    }
                }
            }

            // 2. Düğümleri (Kişileri) Çiz
            foreach (var node in _nodePositions)
            {       // Mavi yuvarlaklar çiziyoruz
                g.FillEllipse(Brushes.SkyBlue, node.Value.X - 15, node.Value.Y - 15, 30, 30);
                g.DrawEllipse(Pens.Black, node.Value.X - 15, node.Value.Y - 15, 30, 30);

                // ID yazıyoruz
                g.DrawString(
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

            _graph.AddNode(id, aktiflik, 0, 0);  // Graph'a ekle
            
            _nodePositions[id] = location; // Çiz
    
            _people.Add(new Person // Listeye ekle
            {
                Id = id,
                Name = name
            });

            FillPeopleList();
            pbCanvas.Invalidate();

            txtTerminal.AppendText($"Düğüm {id} - {name} eklendi.\r\n");
        }

        private (string name, double aktiflik)? AskNodeInfo() // Yeni düğüme bilgi girme
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 220;
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

                Button btnOk = new Button()
                {
                    Text = "Tamam",
                    Left = 200,
                    Width = 70,
                    Top = 140,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.Add(lblName);
                prompt.Controls.Add(txtName);
                prompt.Controls.Add(lblAktiflik);
                prompt.Controls.Add(txtAktiflik);
                prompt.Controls.Add(btnOk);

                prompt.AcceptButton = btnOk;
                prompt.StartPosition = FormStartPosition.CenterParent;

                if (prompt.ShowDialog() != DialogResult.OK)
                    return null;

                if (string.IsNullOrWhiteSpace(txtName.Text)) // İsim boş olduğunda
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

                return (txtName.Text.Trim(), aktiflik);
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

            txtTerminal.AppendText($"Düğüm {nodeId} - {name} silindi.\r\n");
        }

        // DÜĞÜM GÜNCELLEME
        private void UpdateNode(int nodeId)
        {
            var person = _people.FirstOrDefault(p => p.Id == nodeId);
            if (person == null) return;

            string oldName = person.Name;

            string newName = AskNodeNameWithDefault(oldName);
            if (string.IsNullOrWhiteSpace(newName) || newName == oldName)
                return;

            person.Name = newName;

            FillPeopleList();
            pbCanvas.Invalidate();

            txtTerminal.AppendText($"{nodeId} - {oldName} → {newName} güncellendi.\r\n");
        }
        private string AskNodeNameWithDefault(string defaultName) //mecvut ismi görerek düzenleme
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "Düğüm Güncelle";

                Label lbl = new Label() { Left = 10, Top = 10, Text = "Yeni isim:" };
                TextBox txt = new TextBox()
                {
                    Left = 10,
                    Top = 35,
                    Width = 260,
                    Text = defaultName
                };

                Button btnOk = new Button()
                {
                    Text = "Güncelle",
                    Left = 200,
                    Width = 70,
                    Top = 70,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.Add(lbl);
                prompt.Controls.Add(txt);
                prompt.Controls.Add(btnOk);
                prompt.AcceptButton = btnOk;
                prompt.StartPosition = FormStartPosition.CenterParent;

                return prompt.ShowDialog() == DialogResult.OK
                    ? txt.Text
                    : null;
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
                txtTerminal.AppendText($"1. düğüm seçildi: {nodeId}\r\n");
                return;
            }

            // Aynı düğümse iptal
            if (_selectedEdgeNodeId == nodeId)
            {
                txtTerminal.AppendText("Aynı düğüm seçilemez.\r\n");
                return;
            }

            int fromId = _selectedEdgeNodeId.Value;
            int toId = nodeId;

            // Edge zaten varsa
            if (_graph.HasEdge(fromId, toId))
            {
                txtTerminal.AppendText($"{fromId} ↔ {toId} bağlantısı zaten var.\r\n");
                _selectedEdgeNodeId = null;
                return;
            }

            _graph.AddEdge(fromId, toId); // Edge ekle
            UpdateTopNodesTable(); // top5 tablosunu güncelle
            pbCanvas.Invalidate(); // canvası güncelle
            txtTerminal.AppendText($"{fromId} ↔ {toId} bağlantısı eklendi.\r\n");
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

            txtTerminal.AppendText($"{fromId} ↔ {toId} bağlantısı silindi.\r\n");
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

        }

        // ListBox'ı doldur
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

        private const float NODE_RADIUS = 15f; //düğüm yarıçapı
        private int? GetNodeAtPoint(Point p) //düğümün neresine basarsan bas, siler
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
                    txtTerminal.AppendText("Düğüm ekleme modu aktif.\r\n");
                    break;

                case EditMode.DeleteNode:
                    button2.BackColor = Color.IndianRed;
                    txtTerminal.AppendText("Düğüm silme modu aktif.\r\n");
                    break;

                case EditMode.AddEdge:
                    btnAddEdge.BackColor = Color.LightBlue;
                    txtTerminal.AppendText("Kenar ekleme modu aktif.\r\n");
                    break;

                case EditMode.DeleteEdge:
                    btnDeleteEdge.BackColor = Color.OrangeRed;
                    txtTerminal.AppendText("Kenar silme modu aktif.\r\n");
                    break;

                case EditMode.None:
                default:
                    txtTerminal.AppendText("Düzenleme modu kapalı.\r\n");
                    break;
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burası boş
        }

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

            // Arayüz bileşenlerini temizle
            lstPeople.Items.Clear();
            dgvTopNodes.Rows.Clear();
            txtTerminal.Clear();
            txtTerminal.AppendText("Sistem tamamen sıfırlandı. Yeni graf yükleyebilir veya düğüm ekleyebilirsiniz.\r\n");

            // Canvas'ı yeniden çizdir (boş)
            pbCanvas.Invalidate();

            MessageBox.Show("Tüm veriler ve çizim alanı temizlendi.", "Sıfırla", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnDijkstra_Click(object sender, EventArgs e)
        {

        }

        private void btnAStar_Click(object sender, EventArgs e)
        {

        }

        private void btnDFS_Click(object sender, EventArgs e)
        {

        }

        private void btnConnectedComponents_Click(object sender, EventArgs e)
        {

        }

        private void cmbStartNode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbEndNode_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    txtTerminal.AppendText("Tıklanan yerde düğüm yok.\r\n");
                }
            }
            else if (_currentMode == EditMode.UpdateNode)
            {
                int? nodeId = GetNodeAtPoint(e.Location);
                if (nodeId.HasValue)
                    UpdateNode(nodeId.Value);
                else
                    txtTerminal.AppendText("Tıklanan yerde düğüm yok.\r\n");
            }
            else if (_currentMode == EditMode.AddEdge)
            {
                int? nodeId = GetNodeAtPoint(e.Location);

                if (!nodeId.HasValue)
                {
                    txtTerminal.AppendText("Bir düğüme tıklayın.\r\n");
                    return;
                }
                HandleAddEdgeClick(nodeId.Value);
            }
            else if (_currentMode == EditMode.DeleteEdge)
            {   // düğüme mi tıklandı?
                int? nodeId = GetNodeAtPoint(e.Location);
                if (nodeId.HasValue)
                {
                    txtTerminal.AppendText("Kenar silmek için çizgiye tıklayın.\r\n");
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
                    txtTerminal.AppendText("Tıklanan yerde kenar yok.\r\n");
                }
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
            txtTerminal.Clear();
        }
    } // class kapanışı
} // namespace kapanışı