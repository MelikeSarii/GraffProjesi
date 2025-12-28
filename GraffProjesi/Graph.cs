using GraffProjesi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GraffProjesi   // Program.cs ile AYNI namespace
{
    // Graf veri yapısını temsil eder (algoritma içermez)
    public class Graph
    {
        // Düğümler: Id -> Node
        private Dictionary<int, Node> _nodes;
        //key düğümün idsi Node düğümün kendisi
        //burda her id ye karşılık bi node tutuluyo


        //<anahtar(key),değer> tablosu aslında 
        //burda bizim keyimiz düğümün idsi
        //değerimiz ise o düğümüm komşularının listesi
        // KEY  VALUES (List<int>)
        //  1     [3,2]
        //  2      [1]...


        // Komşuluk listesi: Node -> Komşu Node'lar
        private Dictionary<Node, List<Node>> _adjacency;
        //burda her idye nodea karşılık komşu listedi tutuluyo 

        private readonly Random _rnd = new Random();

        public Graph() // kurucu metot
        {
            //üstte alan yarattık burda ise o alana 
            // nesne yarattık
            //Bellekte yeni bir Dictionary<int, List<int>> yaratıp adresini alanıma atadık
            _nodes = new Dictionary<int, Node>();
            _adjacency = new Dictionary<Node, List<Node>>();
            //yeni graf oluştuğunda içinde düğüm bağlantı falan olmayan yeni bi sözlük oluşturuyo
        }

        // Node işlemleri 
        public Node AddNode(int id)// eklenecek olan düğümün id'si
        {
            if (!_nodes.ContainsKey(id))// eğer bu id daha önce eklenmediyse
            {
                Node node = new Node(id);//bellekte yeni düğüm oluşturuyroyz
                _nodes[id] = node;//bu yeni nesneyi bu id ile _nodes sözlüğüne kaydediiyorum
                _adjacency[node] = new List<Node>();//sözlüğe id numaralı düğüm için boş komşu listesi açıyoruz
            }
            return _nodes[id];
        }

        public Node AddNode(int id, double aktiflik, double etkilesim, int baglantiSayisi) // Düğüme özelliklerini ekleme
        {
            //if (!_nodes.ContainsKey(id))
            //{
            //    Node node = new Node(
            //        id,
            //        aktiflik,       
            //        etkilesim,      
            //        baglantiSayisi
            //    );

            //    _nodes[id] = node;
            //    _adjacency[node] = new List<Node>();
            //}

            //return _nodes[id];

            if (!_nodes.ContainsKey(id))
            {
                Node node = new Node(id, aktiflik, etkilesim, baglantiSayisi);
                _nodes[id] = node;
                _adjacency[node] = new List<Node>();
            }
            return _nodes[id];
        }

        public Node GetNode(int id) //Hatalı veri engellenmesi için
        {//burda istenen node varsa getirir yoksa null döndğrğr
            return _nodes.ContainsKey(id) ? _nodes[id] : null;
        }

        public IEnumerable<Node> GetAllNodes()
        {//burda bütün nodeları döndürme kısmımız
            return _nodes.Values;
        }

        public void RemoveNode(int id)
        {
            if (!_nodes.ContainsKey(id)) return;
            //ilk önce node var mı diye baktık 
            Node node = _nodes[id];
            //burda silinmesi istenen nodeun idsi ile o nodeu bulduk
            // Tüm komşuların adjacency listesinden çıkar
            foreach (var neighbors in _adjacency.Values)
                neighbors.Remove(node);

            _adjacency.Remove(node);//bu nodeun komşuluklistesini sil
            _nodes.Remove(id);// bu nodeu sil
        }

        // -------- Edge işlemleri --------

        public void AddEdge(int fromId, int toId)
        {//burda Node tipinde iki nesen oluşturduk
            //ilkinde girlen id yi sözlüğümüz sayesinde eşleşen nodeu alır ve from a atar
            //diğerininde aynı mantıkla id girilnce çıkan nodeu to ya atar
            Node from = _nodes[fromId];
            Node to = _nodes[toId];

            if (!_adjacency[from].Contains(to))
            {//burda fromdakki nodeun komşuluk listesinde to var mı diye bakar
                //forumun k. listesine to yu,to nunkine from mu ekleriz
                _adjacency[from].Add(to);
                _adjacency[to].Add(from);

                // kenar eklenince bağlantı sayısı artsın
                from.BaglantiSayisi++;
                to.BaglantiSayisi++;
            }
        }

        public bool HasEdge(int fromId, int toId)
        {//seçilenler arasında bağlantı var mı diye kontrol
            if (!_nodes.ContainsKey(fromId) || !_nodes.ContainsKey(toId))//Bu id’lerden biri graph’ta var mı yok mu baktık
                return false;

            Node from = _nodes[fromId];
            Node to = _nodes[toId];
            //Node nesneleri bulunuyo
            return _adjacency[from].Contains(to);
            //from’un komşu listesinde to var mı?varsa true
        }

        public List<Node> GetNeighbors(Node node)//Bu node’un komşularını getir
        {
            if (_adjacency.ContainsKey(node))
                return _adjacency[node];
            //“Bu node adjacency tablosunda var mı?”
            //varsa onun komşu listesini direkt ver
            return new List<Node>();//yoksa null
        }

        // ID parametresi alan versiyon (MainForm'daki çizim için kolaylık sağlar)
        public List<int> GetNeighbors(int id)
        {//verilen idli nodeun komşu listesin getirir
            if (!_nodes.ContainsKey(id)) return new List<int>();

            Node node = _nodes[id];
            if (_adjacency.ContainsKey(node))
            {
                // Komşu Node'ların sadece ID'lerini listeye çevirip döndürür
                return _adjacency[node].Select(n => n.Id).ToList();
            }

            return new List<int>();
        }

        public void RemoveEdge(int fromId, int toId) // Edge silme
        {
            if (!_nodes.ContainsKey(fromId) || !_nodes.ContainsKey(toId))
                return;

            Node from = _nodes[fromId];
            Node to = _nodes[toId];

            if (_adjacency[from].Remove(to))
            {
                _adjacency[to].Remove(from);

                // kenar silinince bağlantı ve etkileşim düşer
                from.BaglantiSayisi--;
                to.BaglantiSayisi--;

                from.Etkilesim = Math.Max(0, from.Etkilesim - 1);
                to.Etkilesim = Math.Max(0, to.Etkilesim - 1);
            }
        }

        // -------- Degree --------
        public int GetDegree(Node node)
        {
            if (_adjacency.ContainsKey(node))
                return _adjacency[node].Count;

            return 0;
        }

        // Grafı temizleme 
        public void Clear()
        {
            _nodes.Clear();
            _adjacency.Clear();
        }

        // -------- Dosyadan graf yükleme --------
        // Her satır: from to
        // # ile başlayan satırlar yorum satırı

        public void LoadFromFile(string path)
        {   // Grafı sıfırla (Clear metodu yoksa direkt _adjacency.Clear() kullandık)
            // Eğer ayrıca Clear() diye bir metod yazmak istersek:
            // public void Clear() { _adjacency.Clear(); }
          
            _nodes.Clear();  // 1️- Önce grafı tamamen temizle
            _adjacency.Clear();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, path);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Graf dosyası bulunamadı", fullPath);

            // 2️- Dosyayı satır satır oku
            foreach (var rawLine in File.ReadAllLines(fullPath))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;

                var parts = line.Split(
                    new[] { ' ', '\t', ',', ';' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2) continue;

                if (int.TryParse(parts[0], out int from) &&
                    int.TryParse(parts[1], out int to))
                {
                    // 3️- DÜĞÜM YOKSA RANDOM ÖZELLİKLERLE EKLE
                    if (!_nodes.ContainsKey(from))
                    {
                        AddNode(
                            from,
                            Math.Round(_rnd.NextDouble(), 2), // Aktiflik: 0–1
                            _rnd.Next(1, 11),                 // Etkileşim: 1–10
                            0
                        );
                    }

                    if (!_nodes.ContainsKey(to))
                    {
                        AddNode(
                            to,
                            Math.Round(_rnd.NextDouble(), 2),
                            _rnd.Next(0, 11),
                            0
                        );
                    }

                    // 4️- Kenar ekle (bağlantı ve etkileşim artışı burada olur)
                    AddEdge(from, to);
                }
            }
        }

        public List<Node> GetTop5DegreeNodes()
        {
            if (_nodes == null || _nodes.Count == 0)
                return new List<Node>();

            // Düğümleri GetDegree metoduna göre büyükten küçüğe sıralayıp ilk 5'ini alır
            return _nodes.Values
                .OrderByDescending(node => GetDegree(node))
                .Take(5)
                .ToList();
        }

        public double CalculateWeight(int iId, int jId)
        {
            if (!_nodes.ContainsKey(iId) || !_nodes.ContainsKey(jId))
                return 0;

            Node ni = _nodes[iId];
            Node nj = _nodes[jId];

            // Görseldeki formül: 1 / (1 + sqrt( (A1-A2)^2 + (E1-E2)^2 + (B1-B2)^2 ))
            double farkAktiflik = ni.Aktiflik - nj.Aktiflik;
            double farkEtkilesim = ni.Etkilesim - nj.Etkilesim;
            double farkBaglanti = ni.BaglantiSayisi - nj.BaglantiSayisi;

            double uzaklik = Math.Sqrt(
                farkAktiflik * farkAktiflik +
                farkEtkilesim * farkEtkilesim +
                farkBaglanti * farkBaglanti
            );

            return 1.0 / (1.0 + uzaklik);
        }
        // ---------------- CSV EXPORT / IMPORT ----------------

        public void ExportToCsv(string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            string nodesPath = Path.Combine(folderPath, "nodes.csv");
            string edgesPath = Path.Combine(folderPath, "edges.csv");

            // 1) Nodes
            var sbNodes = new StringBuilder();
            sbNodes.AppendLine("Id;Aktiflik;Etkilesim;BaglantiSayisi;Name;Komsular;PosX;PosY"); //Daha güzel gözükmesi için değiştirdim

            foreach (var n in _nodes.Values.OrderBy(x => x.Id))
            {
                // Name içinde virgül olabilir diye basit escape (tırnak)
                string name = n.Name ?? "";
                if (name.Contains(",")) name = $"\"{name.Replace("\"", "\"\"")}\"";

                // Komşu düğümleri de göstersin
                var neighbors = _adjacency[n]
                    .Select(node => node.Id)
                    .OrderBy(id => id)
                    .ToList();

                string komsular = neighbors.Any()
                    ? string.Join(",", neighbors)
                    : "-";

                float x = n.Position.X;
                float y = n.Position.Y;

                sbNodes.AppendLine( // Daha güzel görsel için değiştirdim
                    $"{n.Id};" +
                    $"{n.Aktiflik.ToString(CultureInfo.InvariantCulture)};" +
                    $"{n.Etkilesim.ToString(CultureInfo.InvariantCulture)};" +
                    $"{n.BaglantiSayisi};" +
                    $"{name};" +
                    $"{komsular};" +
                    $"{x};" +
                    $"{y}"
                );
            }

            File.WriteAllText(nodesPath, sbNodes.ToString(), Encoding.UTF8);

            // 2) Edges (yönsüz: aynı kenarı iki kere yazmamak için)
            var sbEdges = new StringBuilder();
            sbEdges.AppendLine("FromId,ToId");

            var written = new HashSet<string>();
            foreach (var from in _adjacency.Keys)
            {
                foreach (var to in _adjacency[from])
                {
                    int a = Math.Min(from.Id, to.Id);
                    int b = Math.Max(from.Id, to.Id);
                    string key = $"{a}-{b}";
                    if (!written.Add(key)) continue;

                    sbEdges.AppendLine($"{a},{b}");
                }
            }

            File.WriteAllText(edgesPath, sbEdges.ToString(), Encoding.UTF8);
        }

        public void ImportFromCsv(string folderPath)
        {
            string nodesPath = Path.Combine(folderPath, "nodes.csv");

            if (!File.Exists(nodesPath))
                throw new FileNotFoundException("nodes.csv bulunamadı", nodesPath);

            // Grafı sıfırla
            Clear();

            // 1) Nodes oku
            var nodeLines = File.ReadAllLines(nodesPath, Encoding.UTF8)
                                .Select(l => l.Trim())
                                .Where(l => !string.IsNullOrWhiteSpace(l))
                                .ToList();

            // başlık satırını geç
            for (int i = 1; i < nodeLines.Count; i++)
            {
                // Basit CSV parse:
                // Çok karmaşık CSV beklemiyoruz; ayırıcı olarak ; kullanıyoruz
                var parts = nodeLines[i].Split(';');
                if (parts.Length < 8) continue;

                int id = int.Parse(parts[0]);

                double aktiflik = double.Parse(
                    parts[1].Replace(',', '.'),
                    CultureInfo.InvariantCulture
                );

                double etkilesim = double.Parse(
                    parts[2].Replace(',', '.'),
                    CultureInfo.InvariantCulture
                );

                // BaglantiSayisi CSV'de olsa bile,
                // graf doğru olsun diye kenarlardan tekrar hesaplanacak
                int baglantiSayisi = int.Parse(parts[3]);

                string name = parts[4];

                int posX = int.Parse(parts[6]);
                int posY = int.Parse(parts[7]);

                // Düğümü ekle
                var n = AddNode(id, aktiflik, etkilesim, 0);
                n.Name = name;
                n.Position = new Point(posX, posY);

                // dosyada gelen baglantiSayisi varsa bile,
                // graf doğru olsun diye kenarlardan tekrar hesaplamak daha sağlıklı.
                // O yüzden şimdilik 0 veriyoruz.
            }

            // 2) Komşulukları oku (edges.csv yerine nodes.csv içinden)
            for (int i = 1; i < nodeLines.Count; i++)
            {
                var parts = nodeLines[i].Split(';');
                if (parts.Length < 6) continue;

                int fromId = int.Parse(parts[0]);
                string komsularRaw = parts[5];

                if (string.IsNullOrWhiteSpace(komsularRaw))
                    continue;

                // Komşular virgülle ayrılmış
                var neighbors = komsularRaw
                    .Split(',')
                    .Select(x => int.Parse(x.Trim()));

                foreach (var toId in neighbors)
                {
                    // yönsüz graf olduğu için çift kenar eklememek adına
                    if (fromId < toId)
                        AddEdge(fromId, toId);
                }
            }

            // BaglantiSayisi zaten AddEdge içinde artıyor.
        }
    }
}
