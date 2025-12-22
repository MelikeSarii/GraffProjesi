using System;
using System.Collections.Generic;
using System.Linq;

namespace GraffProjesi
{
    // Dijkstra En Kısa Yol Algoritması (dinamik ağırlık ile)
    // Başlangıç düğümünden diğer düğümlere en kısa maliyetleri hesaplar
    public class DijkstraAlgorithm : Algorithm
    {
        public Dictionary<int, double> Distances { get; private set; }

        public DijkstraAlgorithm(Graph graph) : base(graph)
        {
            Distances = new Dictionary<int, double>();
        }

        // -------- Dijkstra --------
        public override void Execute(int startNodeId)
        {
            Distances.Clear();

            var visited = new HashSet<Node>();
            var nodes = Graph.GetAllNodes().ToList();

            Node startNode = nodes.FirstOrDefault(n => n.Id == startNodeId);
            if (startNode == null)
                throw new Exception("Başlangıç düğümü bulunamadı!");

            // Başlangıçta tüm mesafeleri sonsuz yap
            foreach (var node in nodes)
            {
                Distances[node.Id] = double.PositiveInfinity;
            }

            Distances[startNode.Id] = 0; // Başlangıç düğümünün mesafesi 0

            while (visited.Count < nodes.Count)
            {
                // Ziyaret edilmemiş, en küçük mesafeli düğümü seç
                Node current = null;
                double minDistance = double.PositiveInfinity;

                foreach (var node in nodes)
                {
                    if (!visited.Contains(node) && Distances[node.Id] < minDistance)
                    {
                        minDistance = Distances[node.Id];
                        current = node;
                    }
                }
                // Ulaşılabilecek düğüm kalmadıysa çık
                if (current == null)
                    break;

                visited.Add(current);

                // Komşuların mesafelerini güncelle
                foreach (var neighbor in Graph.GetNeighbors(current))
                {
                    if (visited.Contains(neighbor))
                        continue;
                    // Dinamik ağırlık burada hesaplanıyor
                    double weight = CalculateWeight(current, neighbor);
                    double newDistance = Distances[current.Id] + weight;

                    if (newDistance < Distances[neighbor.Id])
                    {
                        Distances[neighbor.Id] = newDistance;
                    }
                }
            }
        }
    }
}
