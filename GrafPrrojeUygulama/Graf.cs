using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace GraffProjesi   // Program.cs'deki namespace ile AYNI olsun
{
    public class NodeFeature // Düğüm özelliklerini tutan sınıf
    {
        public int Id { get; set; }
        public double Aktiflik { get; set; }
        public double Etkilesim { get; set; }
        public int BaglantiSayisi { get; set; }
    }

    public class Graph
    {
        private Dictionary<int, List<int>> _adjacency;//komşuluk listesi kısmı
        //<anahtar(key),değer> tablosu aslında 
        //burda bizim keyimiz düğümün idsi
        //değerimiz ise o düğümüm komşularının listesi
        // KEY  VALUES (List<int>)
        //  1     [3,2]
        //  2      [1]...

        //_adjacency --> Dictionary<int, List<int>> tipinde bir alan tanımladık
        private Dictionary<int, NodeFeature> _nodeFeatures; //Düğümün sayısal özellikleri için


        public Graph()//kurucu metot
        {
            //burda ise nesne yarattık
            //Bellekte yeni bir Dictionary<int, List<int>> yaratıp adresini alanıma atadık
            _adjacency = new Dictionary<int, List<int>>();
            //yeni graf oluştuğunda içinde düğüm bağlantı falan olmayan yeni bi sözlük oluşturuyo
            _nodeFeatures = new Dictionary<int, NodeFeature>(); // Düğümün özellik paketleri için
        }

        public void AddNode(int id)// eklenecek olan düğümün id'si
        {
            if (!_adjacency.ContainsKey(id))// eğer bu id daha önce eklenmediyse
            {
                _adjacency[id] = new List<int>();
                //sözlüğe id numaralı düğüm için boş komşu listesi açılır
            }
        }

        public void AddNodeFeature(int id, double aktiflik, double etkilesim, int baglantiSayisi) // düğüm özelliklerini ekleme
        {
            _nodeFeatures[id] = new NodeFeature
            {
                Id = id,
                Aktiflik = aktiflik,
                Etkilesim = etkilesim,
                BaglantiSayisi = baglantiSayisi
            };
        }

        // -------- Dinamik ağırlık hesaplama -------- İsterlerde verildi
        // İki düğüm arasındaki maliyeti (kenar ağırlığını) hesaplar
        private double CalculateWeight(int i, int j)
        {
            // Güvenlik kontrolü: özellikler yoksa hata fırlat
            if (!_nodeFeatures.ContainsKey(i) || !_nodeFeatures.ContainsKey(j))
                throw new Exception("Düğüm özellikleri eksik!");

            var ni = _nodeFeatures[i];
            var nj = _nodeFeatures[j];

            double farkAktiflik = ni.Aktiflik - nj.Aktiflik;
            double farkEtkilesim = ni.Etkilesim - nj.Etkilesim;
            double farkBaglanti = ni.BaglantiSayisi - nj.BaglantiSayisi;

            double uzaklik = Math.Sqrt(
                farkAktiflik * farkAktiflik +
                farkEtkilesim * farkEtkilesim +
                farkBaglanti * farkBaglanti
            );

            return 1.0 / (1.0 + uzaklik); // İsterde verilen formül
        }

        // -------- Heuristic fonksiyonu (A* için) --------
        // Mevcut düğümden hedef düğüme tahmini maliyet
        private double Heuristic(int current, int target)
        {
            return CalculateWeight(current, target);
        }

        // -------- A* Algoritması --------
        // startId -> targetId arasındaki en kısa yolu hesaplar
        public Dictionary<int, double> AStar(int startId, int targetId)
        {
            var openSet = new HashSet<int>();
            var closedSet = new HashSet<int>();

            var gScore = new Dictionary<int, double>(); // Gerçek maliyet
            var fScore = new Dictionary<int, double>(); // g + h

            foreach (var node in _adjacency.Keys)
            {
                gScore[node] = double.PositiveInfinity;
                fScore[node] = double.PositiveInfinity;
            }

            gScore[startId] = 0;
            fScore[startId] = Heuristic(startId, targetId);

            openSet.Add(startId);

            while (openSet.Count > 0)
            {
                // fScore'u en küçük olan düğümü seç
                int current = openSet
                    .OrderBy(n => fScore[n])
                    .First();

                if (current == targetId)
                {
                    // Hedefe ulaşıldı
                    return gScore;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in _adjacency[current])
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    double tentativeG = gScore[current] + CalculateWeight(current, neighbor);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (tentativeG >= gScore[neighbor])
                        continue;

                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, targetId);
                }
            }

            return gScore;
        }


        // -------- Dijkstra (dinamik ağırlık ile) --------
        // Başlangıç düğümünden diğer düğümlere en kısa maliyetleri hesaplar
        public Dictionary<int, double> Dijkstra(int startId)
        {
            var distances = new Dictionary<int, double>();
            var visited = new HashSet<int>();

            // Başlangıçta tüm mesafeleri sonsuz yap
            foreach (var node in _adjacency.Keys)
            {
                distances[node] = double.PositiveInfinity;
            }

            // Başlangıç düğümünün mesafesi 0
            distances[startId] = 0;

            while (visited.Count < _adjacency.Count)
            {
                int current = -1;
                double minDistance = double.PositiveInfinity;

                // Ziyaret edilmemiş en küçük mesafeli düğümü bul
                foreach (var pair in distances)
                {
                    if (!visited.Contains(pair.Key) && pair.Value < minDistance)
                    {
                        minDistance = pair.Value;
                        current = pair.Key;
                    }
                }

                // Ulaşılabilecek düğüm kalmadıysa çık
                if (current == -1)
                    break;

                visited.Add(current);

                // Komşuların mesafelerini güncelle
                foreach (var neighbor in _adjacency[current])
                {
                    if (visited.Contains(neighbor))
                        continue;

                    // Dinamik ağırlık burada hesaplanıyor
                    double weight = CalculateWeight(current, neighbor);
                    double newDistance = distances[current] + weight;

                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                    }
                }
            }

            return distances;
        }

        // -------- Welsh-Powell Graf Renklendirme --------
        // Sonuç: <DüğümId, RenkNumarası>
        public Dictionary<int, int> WelshPowellColoring()
        {
            // 1) Düğümleri degree'e göre büyükten küçüğe sırala
            var nodesByDegree = _adjacency
                .OrderByDescending(p => p.Value.Count)
                .Select(p => p.Key)
                .ToList();

            var colorResult = new Dictionary<int, int>();
            int currentColor = 1;

            // 2) Tüm düğümler renklendirilene kadar devam et
            foreach (var node in nodesByDegree)
            {
                if (colorResult.ContainsKey(node))
                    continue;

                // Bu düğüme yeni bir renk ata
                colorResult[node] = currentColor;

                // 3) Aynı rengi alabilecek diğer düğümleri kontrol et
                foreach (var otherNode in nodesByDegree)
                {
                    if (colorResult.ContainsKey(otherNode))
                        continue;

                    // Komşu mu kontrolü
                    bool komsuMu = _adjacency[node].Contains(otherNode) ||
                                   _adjacency[otherNode].Contains(node);

                    if (!komsuMu)
                    {
                        // Diğer düğüm, bu renkteki başka düğümlerle de komşu olmamalı
                        bool conflict = false;

                        foreach (var colored in colorResult.Where(x => x.Value == currentColor))
                        {
                            if (_adjacency[colored.Key].Contains(otherNode))
                            {
                                conflict = true;
                                break;
                            }
                        }

                        if (!conflict)
                        {
                            colorResult[otherNode] = currentColor;
                        }
                    }
                }

                currentColor++; // Yeni renk
            }

            return colorResult;
        }


        public void AddEdge(int from, int to)
        {
            AddNode(from);//yukardaki node ekleme metotunu çağrıyo
            //düğüm  ekledik
            AddNode(to);//yine düğüm ekledik aslında 
            /*_adjacency:(ama aralarında bağlantı yok
              1 -> []
              2 -> []*/

            _adjacency[from].Add(to);//forma komşu listesine to yu ekle
            _adjacency[to].Add(from); // to nun komşu listesine formu ekle
        }

        // -------- DFS --------
        public List<int> DFS(int startId)//kaç numaralı düğümden başlayacağız?  
        {
            var visited = new HashSet<int>();//ziyaret ettiklerimizi tutan alan. 1den fazlaysa 1 kere yazar o elemanı
            var result = new List<int>();//ziyaret edilenleri sırayla tutuğumuz liste

            DFS_Recursive(startId, visited, result);
            return result;
        }

        private void DFS_Recursive(int current, HashSet<int> visited, List<int> result)
        {
            if (visited.Contains(current))//eğer ziyaret edildiyse geri dön bulaşma
                return;

            visited.Add(current);//biz bu düğüme geldik
            result.Add(current);//ziyaret sıramızı kaydet

            if (!_adjacency.ContainsKey(current))
                return;

            foreach (var neighbor in _adjacency[current])
            {
                DFS_Recursive(neighbor, visited, result);
            }
        }

        // -------- BFS --------
        public List<int> BFS(int startId)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            var result = new List<int>();

            visited.Add(startId);
            queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                result.Add(current);

                if (!_adjacency.ContainsKey(current))
                    continue;

                foreach (var neighbor in _adjacency[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }


            return result;
        }
        // -------- Bağlı bileşenler --------
        // Sonuç: her elemanı, kendi içinde bağlı olan düğümler listesi
        public List<List<int>> GetConnectedComponents()
        {
            var components = new List<List<int>>();
            var visited = new HashSet<int>();

            foreach (var node in _adjacency.Keys)
            {
                if (visited.Contains(node))
                    continue;

                var component = new List<int>();
                var stack = new Stack<int>();

                stack.Push(node);
                visited.Add(node);

                while (stack.Count > 0)
                {
                    int current = stack.Pop();
                    component.Add(current);

                    foreach (var neighbor in _adjacency[current])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            stack.Push(neighbor);
                        }
                    }
                }

                components.Add(component);
            }

            return components;
        }
        // -------- Degree (bağlantı sayısı) --------
        // Her düğümün bağlantı sayısını döndürür
        public Dictionary<int, int> GetDegrees()
        {
            var result = new Dictionary<int, int>();

            foreach (var pair in _adjacency)
            {
                int node = pair.Key;
                int degree = pair.Value.Count;
                result[node] = degree;
            }

            return result;
        }

        // En çok bağlantısı olan ilk N düğümü döndürür
        public List<int> GetTopDegreeNodes(int count)
        {
            return _adjacency
                .OrderByDescending(p => p.Value.Count)
                .Take(count)
                .Select(p => p.Key)
                .ToList();
        }

        // Grafı sıfırlamak için (isteğe bağlı)
        public void Clear()
        {
            _adjacency.Clear();
        }

        // -------- Dosyadan graf yükleme --------
        // Her satır: from to    (örnek: 1 2)
        // # ile başlayan satırlar yorum satırıdır.
        public void LoadFromFile(string path)
        {
            _adjacency.Clear();

            // Çalışan .exe'nin klasörünü al
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, path);

            // Eğer bu isimle dosya yoksa, bir de ".txt" eklenmiş halini dene
            if (!File.Exists(fullPath))
            {
                // Örn: graf.txt -> graf.txt.txt durumu için
                var altPath = fullPath + ".txt";

                if (File.Exists(altPath))
                {
                    fullPath = altPath; // onu kullan
                }
                else
                {
                    throw new FileNotFoundException(
                        "Graf dosyası bulunamadı: " + fullPath, fullPath);
                }
            }

            foreach (var rawLine in File.ReadAllLines(fullPath))
            {
                var line = rawLine.Trim();

                // Boş satır / yorum satırı
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;

                var parts = line.Split(
                    new[] { ' ', '\t', ';', ',' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2) continue;

                if (int.TryParse(parts[0], out int from) &&
                    int.TryParse(parts[1], out int to))
                {
                    AddEdge(from, to);
                }
            }
        }









    }
}
