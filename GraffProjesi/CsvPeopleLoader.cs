using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GraffProjesi
{
    public static class CsvPeopleLoader
    {
        // kisiler_kucuk.csv veya kisiler_buyuk.csv dosyasını okur
        // CSV formatı (noktalı virgül ile):
        // Id;AdSoyad;Sehir;Rol;Yas;GuvenSkoru;GonulluSaat;AileSayisi;Cadir;Gida;Giysi;Psiko
        public static List<Person> Load(string fileName)
        {
            var people = new List<Person>();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Kişi CSV dosyası bulunamadı.", fullPath);

            var lines = File.ReadAllLines(fullPath);

            // 0. satır başlık olduğu için 1’den başlıyoruz
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 12)
                    continue; // eksik satır

                var p = new Person();

                int tempInt;
                double tempDouble;

                if (int.TryParse(parts[0], out tempInt))
                    p.Id = tempInt;

                p.AdSoyad = parts[1].Trim();
                p.Sehir = parts[2].Trim();
                p.Rol = parts[3].Trim();

                if (int.TryParse(parts[4], out tempInt))
                    p.Yas = tempInt;

                if (double.TryParse(parts[5],
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out tempDouble))
                {
                    p.GuvenSkoru = tempDouble;
                }

                if (int.TryParse(parts[6], out tempInt))
                    p.GonulluSaat = tempInt;

                if (int.TryParse(parts[7], out tempInt))
                    p.AileSayisi = tempInt;

                p.IhtiyacCadir = ToBool(parts[8]);
                p.IhtiyacGida = ToBool(parts[9]);
                p.IhtiyacGiysi = ToBool(parts[10]);
                p.IhtiyacPsikososyal = ToBool(parts[11]);

                people.Add(p);
            }

            return people;
        }

        // "1", "true", "evet", "x" gibi değerleri true kabul ediyoruz
        private static bool ToBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim().ToLowerInvariant();
            return value == "1" || value == "true" || value == "evet" || value == "x";
        }
    }
}
