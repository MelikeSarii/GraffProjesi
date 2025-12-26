using GraffProjesi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraffProjesi   // Program.cs ile AYNI namespace
{
    // Graf veri yapısını temsil eder (algoritma içermez)
    public class Graph
    {
        // Düğümler: Id -> Node
        private Dictionary<int, Node> _nodes;
        //<anahtar(key),değer> tablosu aslında 
        //burda bizim keyimiz düğümün idsi
        //değerimiz ise o düğümüm komşularının listesi
        // KEY  VALUES (List<int>)
        //  1     [3,2]
        //  2      [1]...


        // Komşuluk listesi: Node -> Komşu Node'lar
        private Dictionary<Node, List<Node>> _adjacency;
        private readonly Random _rnd = new Random();

        public Graph() // kurucu metot
        {
            //burda ise nesne yarattık
            //Bellekte yeni bir Dictionary<int, List<int>> yaratıp adresini alanıma atadık
            _nodes = new Dictionary<int, Node>();
            _adjacency = new Dictionary<Node, List<Node>>();
            //yeni graf oluştuğunda içinde düğüm bağlantı falan olmayan yeni bi sözlük oluşturuyo
        }

        // -------- Node işlemleri --------
        public Node AddNode(int id)// eklenecek olan düğümün id'si
        {
            if (!_nodes.ContainsKey(id))// eğer bu id daha önce eklenmediyse
            {
                Node node = new Node(id);
                _nodes[id] = node;
                _adjacency[node] = new List<Node>();//sözlüğe id numaralı düğüm için boş komşu listesi açılır
            }
            return _nodes[id];
        }

        public Node AddNode(int id, double aktiflik, double etkilesim, int baglantiSayisi) // Düğüme özelliklerini ekleme
        {
            if (!_nodes.ContainsKey(id))
            {
                Node node = new Node(
                    id,
                    aktiflik,       
                    etkilesim,      
                    baglantiSayisi
                );

                _nodes[id] = node;
                _adjacency[node] = new List<Node>();
            }

            return _nodes[id];
        }

        public Node GetNode(int id) //Hatalı veri engellenmesi için
        {
            return _nodes.ContainsKey(id) ? _nodes[id] : null;
        }

        public IEnumerable<Node> GetAllNodes()
        {
            return _nodes.Values;
        }

        public void RemoveNode(int id)
        {
            if (!_nodes.ContainsKey(id)) return;

            Node node = _nodes[id];

            // Tüm komşuların adjacency listesinden çıkar
            foreach (var neighbors in _adjacency.Values)
                neighbors.Remove(node);

            _adjacency.Remove(node);
            _nodes.Remove(id);
        }

        // -------- Edge işlemleri --------

        public void AddEdge(int fromId, int toId)
        {
            Node from = _nodes[fromId];
            Node to = _nodes[toId];

            if (!_adjacency[from].Contains(to))
            {
                _adjacency[from].Add(to);
                _adjacency[to].Add(from);

                // kenar eklenince bağlantı sayısı artsın
                from.BaglantiSayisi++;
                to.BaglantiSayisi++;
            }
        }

        public bool HasEdge(int fromId, int toId)
        {
            if (!_nodes.ContainsKey(fromId) || !_nodes.ContainsKey(toId))
                return false;

            Node from = _nodes[fromId];
            Node to = _nodes[toId];

            return _adjacency[from].Contains(to);
        }

        public List<Node> GetNeighbors(Node node)
        {
            if (_adjacency.ContainsKey(node))
                return _adjacency[node];

            return new List<Node>();
        }

        // ID parametresi alan versiyon (MainForm'daki çizim için kolaylık sağlar)
        public List<int> GetNeighbors(int id)
        {
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

        // -------- Grafı temizleme --------
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
    }
}
