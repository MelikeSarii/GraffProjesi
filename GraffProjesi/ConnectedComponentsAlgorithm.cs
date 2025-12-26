using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
{
    // Bağlı Bileşen (Topluluk) Analizi
    /* Graf içinde kendi arasında bağlantılı olan,
     * ancak diğer gruplarla bağlantısı olmayan düğüm kümeleridir. */
    // BFS veya DFS kullanır
    public class ConnectedComponentsAlgorithm
    {
        private Graph _graph;

        public List<List<int>> Components { get; private set; }

        public ConnectedComponentsAlgorithm(Graph graph)
        {
            _graph = graph;
            Components = new List<List<int>>();
        }

        public void Execute()
        {
            Components.Clear();

            var visited = new HashSet<int>();

            foreach (var node in _graph.GetAllNodes())
            {
                if (visited.Contains(node.Id))
                    continue;

                // Yeni topluluk başlat
                var component = new List<int>();
                DFS(node.Id, visited, component);

                Components.Add(component);
            }
        }

        private void DFS(int nodeId, HashSet<int> visited, List<int> component)
        {
            visited.Add(nodeId);
            component.Add(nodeId);

            foreach (var neighborId in _graph.GetNeighbors(nodeId))
            {
                if (!visited.Contains(neighborId))
                    DFS(neighborId, visited, component);
            }
        }
    }
}

