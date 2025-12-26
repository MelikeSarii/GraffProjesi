using System;
using System.Collections.Generic;
using System.Linq;

namespace GraffProjesi
{
    // A* En Kısa Yol Algoritması
    public class AStarAlgorithm : Algorithm
    {
        public Dictionary<int, int?> Previous { get; private set; }
        public Dictionary<int, double> GScore { get; private set; }

        public double TotalCost { get; private set; }

        public AStarAlgorithm(Graph graph) : base(graph)
        {
            Previous = new Dictionary<int, int?>();
            GScore = new Dictionary<int, double>();
        }

        public override void Execute(int startNodeId)
        {
            throw new NotImplementedException(
                "A* algoritması için hedef düğüm gereklidir."
            );
        }

        //  A*
        public void Execute(int startId, int endId)
        {
            Previous.Clear();
            GScore.Clear();
            TotalCost = double.PositiveInfinity;

            var nodes = Graph.GetAllNodes().ToList();
            var openSet = new HashSet<Node>();

            Node start = nodes.FirstOrDefault(n => n.Id == startId);
            Node goal = nodes.FirstOrDefault(n => n.Id == endId);

            if (start == null || goal == null)
                throw new Exception("Başlangıç veya hedef düğüm bulunamadı.");

            foreach (var node in nodes)
            {
                GScore[node.Id] = double.PositiveInfinity;
                Previous[node.Id] = null;
            }

            GScore[start.Id] = 0;
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                Node current = openSet
                    .OrderBy(n => GScore[n.Id] + Heuristic(n, goal))
                    .First();

                if (current.Id == goal.Id)
                {
                    TotalCost = GScore[goal.Id];
                    return;
                }

                openSet.Remove(current);

                foreach (var neighbor in Graph.GetNeighbors(current))
                {
                    double tentativeG =
                        GScore[current.Id] + CalculateWeight(current, neighbor);

                    if (tentativeG < GScore[neighbor.Id])
                    {
                        GScore[neighbor.Id] = tentativeG;
                        Previous[neighbor.Id] = current.Id;
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // Heuristic fonksiyon
        private double Heuristic(Node a, Node b)
        {
            double da = a.Aktiflik - b.Aktiflik;
            double de = a.Etkilesim - b.Etkilesim;
            double db = a.BaglantiSayisi - b.BaglantiSayisi;

            return Math.Sqrt(da * da + de * de + db * db);
        }

        // Yol çıkarma
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
    }
}
