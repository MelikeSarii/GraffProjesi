using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
{
    // A* En Kısa Yol Algoritması
    public class AStarAlgorithm : Algorithm
    {
        public int TargetNodeId { get; set; }

        // Sonuç: en kısa mesafeler
        public Dictionary<int, double> Distances { get; private set; }

        public AStarAlgorithm(Graph graph) : base(graph)
        {
            Distances = new Dictionary<int, double>();
        }

        // -------- A* --------
        public override void Execute(int startNodeId)
        {
            Distances.Clear();

            var openSet = new HashSet<Node>();
            var closedSet = new HashSet<Node>();

            var gScore = new Dictionary<Node, double>();
            var fScore = new Dictionary<Node, double>();

            var nodes = Graph.GetAllNodes().ToList();

            Node startNode = nodes.FirstOrDefault(n => n.Id == startNodeId);
            Node targetNode = nodes.FirstOrDefault(n => n.Id == TargetNodeId);

            if (startNode == null || targetNode == null)
                throw new Exception("Başlangıç veya hedef düğüm bulunamadı!");

            foreach (var node in nodes)
            {
                gScore[node] = double.PositiveInfinity;
                fScore[node] = double.PositiveInfinity;
                Distances[node.Id] = double.PositiveInfinity;
            }

            gScore[startNode] = 0;
            fScore[startNode] = Heuristic(startNode, targetNode);

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // fScore'u en küçük olan düğümü seç
                Node current = openSet.OrderBy(n => fScore[n]).First();

                if (current == targetNode)
                {
                    // Hedefe ulaşıldı
                    break;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in Graph.GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    double tentativeG = gScore[current] + CalculateWeight(current, neighbor);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (tentativeG >= gScore[neighbor])
                        continue;

                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, targetNode);
                    Distances[neighbor.Id] = gScore[neighbor];
                }
            }

            // Son durumda gScore'u Distances'a aktar
            foreach (var pair in gScore)
            {
                Distances[pair.Key.Id] = pair.Value;
            }
        }

        // -------- Heuristic fonksiyonu --------
        private double Heuristic(Node current, Node target)
        {
            // Node özelliklerine dayalı tahmini maliyet
            return CalculateWeight(current, target);
        }
    }
}
