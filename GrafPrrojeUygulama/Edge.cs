using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi   // Program.cs ile AYNI namespace
    {
        // Graf üzerindeki iki düğüm arasındaki bağlantıyı temsil eder
        public class Edge
        {
            public Node From { get; }
            public Node To { get; }

            // Kenar yönsüz olduğu için sadece iki ucu tutuyoruz
            public Edge(Node from, Node to)
            {
                From = from;
                To = to;
            }
        }
    }