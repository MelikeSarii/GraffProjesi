using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace GraffProjesi
{
    public static class CsvPeopleLoader
    {
        public static List<Person> Load(string fileName)
        {
            var people = new List<Person>();

            // .exe'nin çalıştığı klasör
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Kişi CSV dosyası bulunamadı", fullPath);

            bool firstLine = true;

            foreach (var rawLine in File.ReadAllLines(fullPath, Encoding.UTF8))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) continue;

                // İlk satır başlık satırı ise atla
                if (firstLine && (line.StartsWith("Id;") || line.StartsWith("Id,")))
                {
                    firstLine = false;
                    continue;
                }
                firstLine = false;

                // ; veya , ile ayrılmış olabilir
                var parts = line.Split(new[] { ';', ',' }, StringSplitOptions.None);

                // Id;AdSoyad;Sehir;Rol;Yas;GuvenSkoru;AileSayisi;IhtiyacCadir;IhtiyacGida;IhtiyacGiysi;IhtiyacPsikososyal
                if (parts.Length < 11)
                    continue;

                // Id
                if (!int.TryParse(parts[0].Trim(), out int id))
                    continue;

                string adSoyad = parts[1].Trim();
                string sehir = parts[2].Trim();
                string rol = parts[3].Trim();

                int.TryParse(parts[4].Trim(), out int yas);

                // 0.8 veya 0,8 yazılmış olabilir -> noktaya çevir
                string guvenStr = parts[5].Trim().Replace(',', '.');
                double.TryParse(guvenStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double guvenSkoru);

                int.TryParse(parts[6].Trim(), out int aileSayisi);

                bool ihtiyacCadir = parts[7].Trim() == "1";
                bool ihtiyacGida = parts[8].Trim() == "1";
                bool ihtiyacGiysi = parts[9].Trim() == "1";
                bool ihtiyacPsikososyal = parts[10].Trim() == "1";

                var person = new Person
                {
                    Id = id,
                    AdSoyad = adSoyad,
                    Sehir = sehir,
                    Rol = rol,
                    Yas = yas,
                    GuvenSkoru = guvenSkoru,
                    AileSayisi = aileSayisi,
                    IhtiyacCadir = ihtiyacCadir,
                    IhtiyacGida = ihtiyacGida,
                    IhtiyacGiysi = ihtiyacGiysi,
                    IhtiyacPsikososyal = ihtiyacPsikososyal
                };

                people.Add(person);
            }

            return people;
        }
    }
}
