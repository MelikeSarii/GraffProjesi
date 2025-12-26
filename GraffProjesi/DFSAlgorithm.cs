using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
    {
        // Depth-First Search algoritması
        /* Seçilen düğümden erişilebilen tüm düğümleri derine inerek yazdırır
         * Stack!
         */
        public class DFSAlgorithm : Algorithm
        {
            public List<int> Result { get; private set; }

            public DFSAlgorithm(Graph graph) : base(graph)
            {
                Result = new List<int>();
            }
                    // -------- DFS --------
            public override void Execute(int startNodeId)
            {
                Result.Clear();

                var visited = new HashSet<Node>();
                var stack = new Stack<Node>();

                Node startNode = Graph.GetNode(startNodeId);
                if (startNode == null)
                    throw new Exception("Başlangıç düğümü bulunamadı!");

                stack.Push(startNode);

                while (stack.Count > 0)
                {
                    Node current = stack.Pop();

                    if (visited.Contains(current))
                        continue;

                    visited.Add(current);
                    Result.Add(current.Id);

                    // Komşuları stack'e ekle (ters sırayla)
                    var neighbors = Graph.GetNeighbors(current);
                    for (int i = neighbors.Count - 1; i >= 0; i--)
                    {
                        if (!visited.Contains(neighbors[i]))
                            stack.Push(neighbors[i]);
                    }
                }
            }
        }
    }

