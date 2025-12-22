using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi   // Program.cs ile AYNI namespace
{
    // Graf üzerindeki bir düğümü temsil eder
    public class Node
    {
        public int Id { get; set; }

        // --- Dinamik ağırlık hesabında kullanılan özellikler ---
        public double Aktiflik { get; set; }
        public double Etkilesim { get; set; }
        public int BaglantiSayisi { get; set; }

        // Boş kurucu
        public Node(int id)
        {
            Id = id;
        }

        // Özelliklerle birlikte kurucu
        public Node(int id, double aktiflik, double etkilesim, int baglantiSayisi)
        {
            Id = id;
            Aktiflik = aktiflik;
            Etkilesim = etkilesim;
            BaglantiSayisi = baglantiSayisi;
        }
    }
}


