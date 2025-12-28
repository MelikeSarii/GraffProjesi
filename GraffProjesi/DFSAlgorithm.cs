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
        //dfs nin gezidği idleri tutar
            public DFSAlgorithm(Graph graph) : base(graph)
            {
                Result = new List<int>();
            }
                    // -------- DFS --------
            public override void Execute(int startNodeId)
            {
                Result.Clear();

                var visited = new HashSet<Node>();//ziyaret edilen düğümleri tutar
            var stack = new Stack<Node>();//dfs nin stack yapısı
            //ilk giren son çıkar lifo

                Node startNode = Graph.GetNode(startNodeId);
                if (startNode == null)
                    throw new Exception("Başlangıç düğümü bulunamadı!");

                stack.Push(startNode);
            //başlangıç düğümü stacke koyuyoruz
                while (stack.Count > 0)
                {
                    Node current = stack.Pop();
                //stack den bir düğüm çıkarıyoruz(en üsttekini)
                if (visited.Contains(current))
                        continue;//eğer bu düğüm ziyaret edildiyse whleın başına döner

                    visited.Add(current);
                    Result.Add(current.Id);

                // cuurent ın komşularını al ve ters sırayla stack e ekle
                var neighbors = Graph.GetNeighbors(current);
                    for (int i = neighbors.Count - 1; i >= 0; i--)
                    {
                        if (!visited.Contains(neighbors[i]))
                            stack.Push(neighbors[i]);
                    }
                }
            }
        }
    }//stack yapısını kullanır,1 düğümden derine iner daha da inemezse geri döner

