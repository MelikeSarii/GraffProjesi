using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
{
    // Tüm graf algoritmaları için temel soyut sınıf
    public abstract class Algorithm : IAlgorithm
    {
        protected Graph Graph;

        protected Algorithm(Graph graph)
        {
            Graph = graph;
        }

        // Her algoritma kendi Execute metodunu yazmak zorunda
        public abstract void Execute(int startNodeId);

        // -------- Dinamik ağırlık hesabı (Dijkstra & A* kullanacak) --------
        protected double CalculateWeight(Node a, Node b)
        {
            double farkAktiflik = a.Aktiflik - b.Aktiflik;
            double farkEtkilesim = a.Etkilesim - b.Etkilesim;
            double farkBaglanti = a.BaglantiSayisi - b.BaglantiSayisi;

            double uzaklik = Math.Sqrt(
                farkAktiflik * farkAktiflik +
                farkEtkilesim * farkEtkilesim +
                farkBaglanti * farkBaglanti
            );

            return 1.0 / (1.0 + uzaklik);
        }
    }
}
