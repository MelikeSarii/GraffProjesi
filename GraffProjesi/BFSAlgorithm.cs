using System;
using System.Collections.Generic;

namespace GraffProjesi
{
    // Breadth-First Search algoritması
    /* Seçilen düğümden erişilebilen tüm düğümleri yazdırır.
     * Queue, bağlı tüm düğümler listelenir
     */
    public class BFSAlgorithm : Algorithm
    {
        public List<int> Result { get; private set; }

        public BFSAlgorithm(Graph graph) : base(graph)
        {
            Result = new List<int>();
        }

        // -------- BFS --------
        public override void Execute(int startNodeId)
        {
            Result.Clear();

            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();

            // Başlangıç düğümünü Graph'tan al
            Node startNode = null;
            foreach (var node in Graph.GetAllNodes())
            {
                if (node.Id == startNodeId)
                {
                    startNode = node;
                    break;
                }
            }

            if (startNode == null)
                throw new Exception("Başlangıç düğümü bulunamadı!");

            visited.Add(startNode);
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Result.Add(current.Id);

                foreach (var neighbor in Graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }
}
