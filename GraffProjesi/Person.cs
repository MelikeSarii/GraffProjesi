using System;

namespace GraffProjesi
{
    // Her kişi / düğüm için temel bilgiler
    public class Person
    {
        public int Id { get; set; }          // graf düğüm ID'si ile aynı
        public string AdSoyad { get; set; }
        public string Sehir { get; set; }
        public string Rol { get; set; }      // Gönüllü / Depremzede / Koordinatör vb.
        public int Yas { get; set; }

        // İsterseniz raporlarda kullanacağınız ekstra kolonlar:
        public double GuvenSkoru { get; set; }
        public int GonulluSaat { get; set; }
        public int AileSayisi { get; set; }

        public bool IhtiyacCadir { get; set; }
        public bool IhtiyacGida { get; set; }
        public bool IhtiyacGiysi { get; set; }
        public bool IhtiyacPsikososyal { get; set; }
    }
}

