using System;
using GraffProjesi;
using System.Collections.Generic;
using System.Linq;

namespace GraffProjesi
{
    // Welsh–Powell Graf Renklendirme Algoritması
    public class Coloring
    {
        private Graph _graph;

        public Coloring(Graph graph)
        {
            _graph = graph;
        }

        // Sonuç: <NodeId, ColorNo>
        public Dictionary<int, int> ApplyWelshPowell()
        {
            var result = new Dictionary<int, int>();

            // 1) Düğümleri degree (bağlantı sayısı) büyükten küçüğe sırala
            var nodes = _graph.GetAllNodes()
                              .OrderByDescending(n => _graph.GetDegree(n))
                              .ToList();

            int currentColor = 1;

            // 2) Tüm düğümler renklendirilene kadar devam et
            foreach (var node in nodes)
            {
                if (result.ContainsKey(node.Id))
                    continue;

                // Bu düğüme yeni renk ata
                result[node.Id] = currentColor;

                // 3) Aynı rengi alabilecek diğer düğümleri kontrol et
                foreach (var other in nodes)
                {
                    if (result.ContainsKey(other.Id))
                        continue;

                    bool conflict = false;

                    // Aynı renkteki düğümlerle komşu mu?
                    foreach (var colored in result.Where(x => x.Value == currentColor))
                    {
                        Node coloredNode = nodes.First(n => n.Id == colored.Key);

                        if (_graph.GetNeighbors(coloredNode).Contains(other))
                        {
                            conflict = true;
                            break;
                        }
                    }

                    if (!conflict)
                    {
                        result[other.Id] = currentColor;
                    }
                }

                currentColor++;
            }

            return result;
        }
    }
}
