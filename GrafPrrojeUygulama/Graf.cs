using System.Collections.Generic;
using System.Linq;


namespace GraffProjesi   // Program.cs'deki namespace ile AYNI olsun
{
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

        public Graph()//kurucu metot
        {
            //burda ise nesne yarattık
            //Bellekte yeni bir Dictionary<int, List<int>> yaratıp adresini alanıma atadık
            _adjacency = new Dictionary<int, List<int>>();
            //yeni graf oluştuğunda içinde düğüm bağlantı falan olmayan yeni bi sözlük oluşturuyo
        }

        public void AddNode(int id)// eklenecek olan düğümün id'si
        {
            if (!_adjacency.ContainsKey(id))// eğer bu id daha önce eklenmediyse
            {
                _adjacency[id] = new List<int>();
                //sözlüğe id numaralı düğüm için boş komşu listesi açılır
            }
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
     



    }
}
