using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                        double weight = _graph.CalculateWeight(fromId, toId);
                        g.DrawString(weight.ToString("F2"), this.Font, Brushes.Red, (p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                    }
                }
            }

            // 2. Düğümleri (Kişileri) Çiz
            foreach (var node in _nodePositions)
            {       // Mavi yuvarlaklar çiziyoruz
                g.FillEllipse(Brushes.SkyBlue, node.Value.X - 15, node.Value.Y - 15, 30, 30);
                g.DrawEllipse(Pens.Black, node.Value.X - 15, node.Value.Y - 15, 30, 30);
                    // İçine id numarası yazıyoruz
                g.DrawString(node.Key.ToString(), this.Font, Brushes.Black, node.Value.X - 8, node.Value.Y - 8);
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
                lstPeople.Items.Add($"{p.Id} - {p.AdSoyad}");
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
                $"Id: {person.Id}\nAd Soyad: {person.AdSoyad}",
                "Kişi Bilgisi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

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

    } // class kapanışı
} // namespace kapanışı