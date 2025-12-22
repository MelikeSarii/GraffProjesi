using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
{
    // Depth-First Search algoritması
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

            DFSRecursive(startNode, visited);
        }

        private void DFSRecursive(Node current, HashSet<Node> visited)
        {
            if (visited.Contains(current))
                return;

            visited.Add(current);
            Result.Add(current.Id);

            foreach (var neighbor in Graph.GetNeighbors(current))
            {
                DFSRecursive(neighbor, visited);
            }
        }
    }
}

