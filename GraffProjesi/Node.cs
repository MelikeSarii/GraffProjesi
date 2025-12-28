using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi   // Program.cs ile AYNI namespace
{//namesapce ile isim alanlarını hep aynı tuttuk ki diğer sınıflar birbirini direkt olarak görebilsin
    // Graf üzerindeki bir düğümü temsil eder
    public class Node//bütün kullanıcılar bu sınıfı kullanır
    {
        public int Id { get; set; }

        // UI için
        public string Name { get; set; }
        public PointF Position { get; set; }

        // --- Dinamik ağırlık hesabında kullanılan özellikler ---
        public double Aktiflik { get; set; }
        public double Etkilesim { get; set; }//kullanıcının beğeni,yorum sayısı gibi
        public int BaglantiSayisi { get; set; }//kullanıcın kaç komşusu var

        public Node(int id) // sadece id vererek basit düğüm oluşturmak istersek eğer
        {
            Id = id;
        }

        public Node(int id, double aktiflik, double etkilesim, int baglantiSayisi) //düğüüm oluştururken tum özellikleri aynı anda set etmek için bu kısım
        {
            Id = id;
            Aktiflik = aktiflik;
            Etkilesim = etkilesim;
            BaglantiSayisi = baglantiSayisi;
        }
    }
}
//burda iki tane constructor var canımız istediğinde sadece ıd alan bi node yazmak için
//diğperi node yazarken bütün değerleri doldurmak için

 
