using GraffProjesi;
using GrafPrrojeUygulama;
using System;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm()); // Yeni oluşturduğunuz formun adı
    }
    //static void Main()
    //{
    //    Console.WriteLine("===== GRAF OLUŞTURULUYOR =====\n");

    //    Graph graph = new Graph();

    //    // Basit graf ekleyebiliriz: Ağırlıkları 1 olduğu için tam sayı mesafe verir.
    //    // graph.LoadFromFile("graph.txt");

    //    // Console.WriteLine("graph.txt başarıyla yüklendi!\n");

    //    // ---- Node ekleme (özellikli) ----
    //    graph.AddNode(1, 0.8, 12, 3);
    //    graph.AddNode(2, 0.4, 5, 2);
    //    graph.AddNode(3, 0.6, 9, 4);
    //    graph.AddNode(4, 0.9, 15, 5);
    //    graph.AddNode(5, 0.3, 4, 1);

    //    // ---- Edge ekleme ----
    //    graph.AddEdge(1, 2);
    //    graph.AddEdge(1, 3);
    //    graph.AddEdge(2, 4);
    //    graph.AddEdge(3, 4);
    //    graph.AddEdge(4, 5);

    //    Console.WriteLine("Graf başarıyla oluşturuldu.\n");

    //    // ======================================================
    //    // BFS TEST
    //    // ======================================================
    //    Console.WriteLine("===== BFS TEST =====");

    //    BFSAlgorithm bfs = new BFSAlgorithm(graph);
    //    bfs.Execute(1);

    //    Console.Write("BFS Sonucu: ");
    //    foreach (var id in bfs.Result)
    //    {
    //        Console.Write(id + " ");
    //    }
    //    Console.WriteLine("\n");

    //    // ======================================================
    //    // DFS TEST
    //    // ======================================================
    //    Console.WriteLine("===== DFS TEST =====");

    //    DFSAlgorithm dfs = new DFSAlgorithm(graph);
    //    dfs.Execute(1);

    //    Console.Write("DFS Sonucu: ");
    //    foreach (var id in dfs.Result)
    //    {
    //        Console.Write(id + " ");
    //    }
    //    Console.WriteLine("\n");

    //    // ======================================================
    //    // DIJKSTRA TEST
    //    // ======================================================
    //    Console.WriteLine("===== DIJKSTRA TEST =====");

    //    DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(graph);
    //    dijkstra.Execute(1);

    //    Console.WriteLine("Dijkstra (Başlangıç: 1):");
    //    foreach (var pair in dijkstra.Distances)
    //    {
    //        Console.WriteLine($"Node {pair.Key} -> Mesafe: {pair.Value:F4}");
    //    }
    //    Console.WriteLine();

    //    // ======================================================
    //    // A* TEST
    //    // ======================================================
    //    Console.WriteLine("===== A* TEST =====");

    //    AStarAlgorithm aStar = new AStarAlgorithm(graph);
    //    aStar.TargetNodeId = 5;
    //    aStar.Execute(1);

    //    Console.WriteLine("A* (1 -> 5):");
    //    foreach (var pair in aStar.Distances)
    //    {
    //        Console.WriteLine($"Node {pair.Key} -> Mesafe: {pair.Value:F4}");
    //    }
    //    Console.WriteLine();

    //    // ======================================================
    //    // WELSH–POWELL COLORING TEST
    //    // ======================================================
    //    Console.WriteLine("===== WELSH–POWELL RENKLENDİRME =====");

    //    Coloring coloring = new Coloring(graph);
    //    var colors = coloring.ApplyWelshPowell();

    //    foreach (var pair in colors)
    //    {
    //        Console.WriteLine($"Node {pair.Key} -> Renk {pair.Value}");
    //    }

    //    Console.WriteLine("\n===== TÜM TESTLER TAMAMLANDI =====");
    //}
}
