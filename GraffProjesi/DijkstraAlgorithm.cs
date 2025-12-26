using System;
using System.Collections.Generic;
using System.Linq;

namespace GraffProjesi
{
    // Dijkstra En Kısa Yol Algoritması (dinamik ağırlık ile)
    /* Başlangıç düğümünden diğer düğümlere en kısa maliyetleri hesaplar
     * Başlangıç seçili -> Onun tüm düğümlere olan en kısa yol
     * Hedef & Başlangıç seçili -> iki düğüm arasındaki en kısa yol
     */
    public class DijkstraAlgorithm : Algorithm
    {   // Her düğüm için başlangıçtan olan en kısa mesafe
        public Dictionary<int, double> Distances { get; private set; }
        public Dictionary<int, int?> Previous { get; private set; }


        public DijkstraAlgorithm(Graph graph) : base(graph)
        {
            Distances = new Dictionary<int, double>();
        }

        public List<int> GetPath(int endId)
        {
            var path = new List<int>();
            int? current = endId;

            while (current != null)
            {
                path.Insert(0, current.Value);
                current = Previous[current.Value];
            }

            return path;
        }

        public override void Execute(int startNodeId)
        {
            Distances.Clear();

            var visited = new HashSet<Node>(); // Ziyaret edilen düğümler
            var nodes = Graph.GetAllNodes().ToList(); // Graft üstündeki tüm düğümler

            Node startNode = nodes.FirstOrDefault(n => n.Id == startNodeId);
            if (startNode == null)
                throw new Exception("Başlangıç düğümü bulunamadı!");

            // Mesafeler ve önceki düğümler tek seferde hazırlanır
            Previous = new Dictionary<int, int?>();

            foreach (var node in nodes)
            {
                Distances[node.Id] = double.PositiveInfinity;
                Previous[node.Id] = null;
            }

            // Başlangıç düğümü 0
            Distances[startNode.Id] = 0;

            while (visited.Count < nodes.Count) // Tüm düğümlere ziyaret
            {
                Node current = null; // Ziyaret edilmemiş, en küçük mesafeli düğümü seç
                double minDistance = double.PositiveInfinity;

                foreach (var node in nodes)
                {
                    if (!visited.Contains(node) && Distances[node.Id] < minDistance)
                    {
                        minDistance = Distances[node.Id];
                        current = node;
                    }
                }

                // Artık ulaşılabilecek düğüm yoksa çık
                if (current == null)
                    break;

                visited.Add(current); // Düğüm ziyaret edildi olarak işaretle

                foreach (var neighbor in Graph.GetNeighbors(current)) // Komşuların mesafelerini güncelle
                {
                    if (visited.Contains(neighbor))
                        continue;

                    double weight = CalculateWeight(current, neighbor); // 2 düğüm arasındaki dinamik kenar ağırlığı hesapla
                    double newDistance = Distances[current.Id] + weight; // Alternatif yolun toplam maliyeti

                    if (newDistance < Distances[neighbor.Id]) // Eğer daha kısa bulduysa güncelle!!
                    {
                        Distances[neighbor.Id] = newDistance;
                        Previous[neighbor.Id] = current.Id;
                    }
                }
            }
        }
    }
}