using System;

namespace GraffProjesi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph();
            g.LoadFromFile("graf.txt");

            // ---- Düğüm özellikleri (test için manuel giriyoruz) ----
            // CSV daha sonra gelecek, şimdilik test verisi
            g.AddNodeFeature(1, 0.8, 12, 3);
            g.AddNodeFeature(2, 0.4, 5, 2);
            g.AddNodeFeature(3, 0.6, 9, 2);
            g.AddNodeFeature(4, 0.2, 3, 1);
            g.AddNodeFeature(5, 0.9, 15, 1);


            Console.WriteLine("DFS (1'den başlayarak):");
            var dfsSonuc = g.DFS(1);
            foreach (var nodeId in dfsSonuc)
            {
                Console.WriteLine(nodeId);
            }

            Console.WriteLine("-----");

            Console.WriteLine("BFS (1'den başlayarak):");
            var bfsSonuc = g.BFS(1);
            foreach (var nodeId in bfsSonuc)
            {
                Console.WriteLine(nodeId);
            }
            Console.WriteLine("-----");
            Console.WriteLine("Bağlı bileşenler:");

            var components = g.GetConnectedComponents();
            int index = 1;
            foreach (var comp in components)
            {
                Console.Write("Bileşen " + index + ": ");
                Console.WriteLine(string.Join(", ", comp));
                index++;
            }

            Console.WriteLine("-----");
            Console.WriteLine("Degree değerleri:");

            var degrees = g.GetDegrees();
            foreach (var pair in degrees)
            {
                Console.WriteLine("Düğüm " + pair.Key + " -> degree: " + pair.Value);
            }

            Console.WriteLine("-----");
            Console.WriteLine("En etkili 3 düğüm:");
            var top3 = g.GetTopDegreeNodes(3);
            Console.WriteLine(string.Join(", ", top3));

            Console.WriteLine("-----");
            Console.WriteLine("Dijkstra (1'den başlayarak):");

            var dijkstraSonuc = g.Dijkstra(1);
            foreach (var pair in dijkstraSonuc)
            {
                Console.WriteLine($"1 -> {pair.Key} maliyet: {pair.Value:F4}");
            }

            Console.WriteLine("-----");
            Console.WriteLine("A* (1 -> 5):");

            var aStarSonuc = g.AStar(1, 5);
            Console.WriteLine($"1 -> 5 maliyet: {aStarSonuc[5]:F4}");

            Console.WriteLine("-----");
            Console.WriteLine("Welsh-Powell Graf Renklendirme:");

            var coloring = g.WelshPowellColoring();
            foreach (var pair in coloring)
            {
                Console.WriteLine("Düğüm " + pair.Key + " -> Renk " + pair.Value);
            }

            Console.ReadLine();    
        }
    }
}
