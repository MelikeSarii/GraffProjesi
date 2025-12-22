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

        public Graph() // kurucu metot
        {
            //burda ise nesne yarattık
            //Bellekte yeni bir Dictionary<int, List<int>> yaratıp adresini alanıma atadık
            _nodes = new Dictionary<int, Node>();
            _adjacency = new Dictionary<Node, List<Node>>();
            //yeni graf oluştuğunda içinde düğüm bağlantı falan olmayan yeni bi sözlük oluşturuyo
        }

        // -------- Node işlemleri --------

        public Node AddNode(int id) // eklenecek olan düğümün id'si
        {
            if (!_nodes.ContainsKey(id)) // eğer bu id daha önce eklenmediyse
            {
                Node node = new Node(id);
                _nodes[id] = node;
                _adjacency[node] = new List<Node>();
                //sözlüğe id numaralı düğüm için boş komşu listesi açılır
            }

            return _nodes[id];
        }

        public Node AddNode(int id, double aktiflik, double etkilesim, int baglantiSayisi) // düğüme özelliklerini ekleme
        {
            if (!_nodes.ContainsKey(id))
            {
                Node node = new Node(id, aktiflik, etkilesim, baglantiSayisi);
                _nodes[id] = node;
                _adjacency[node] = new List<Node>();
            }

            return _nodes[id];
        }

        public IEnumerable<Node> GetAllNodes()
        {
            return _nodes.Values;
        }

        // -------- Edge işlemleri --------

        public void AddEdge(int fromId, int toId)
        {
            Node from = AddNode(fromId);
            Node to = AddNode(toId);

            // Yönsüz graf
            if (!_adjacency[from].Contains(to))
                _adjacency[from].Add(to);

            if (!_adjacency[to].Contains(from))
                _adjacency[to].Add(from);
        }

        public List<Node> GetNeighbors(Node node)
        {
            if (_adjacency.ContainsKey(node))
                return _adjacency[node];

            return new List<Node>();
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
        // # ile başlayan satırlar yorum satırıdır

        public void LoadFromFile(string path)
        {
            // Grafı sıfırla (Clear metodu yoksa direkt _adjacency.Clear() kullan)
            _adjacency.Clear();
            // Eğer ayrıca Clear() diye bir metod yazmak istersen:
            // public void Clear() { _adjacency.Clear(); }

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, path);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Graf dosyası bulunamadı", fullPath);

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
                    int.TryParse(parts[1], out int to))   // <-- buradaki f gitti
                {
                    AddEdge(from, to);
                }
            }
        }
        }
}
