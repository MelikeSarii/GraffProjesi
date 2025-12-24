using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraffProjesi
{
    public static class CsvPeopleLoader
    {
        // Beklenen format:
        // Id;Name
        // 1;Ayşe Yılmaz
        // 2;Mehmet Demir
        public static List<Person> Load(string fileName)
        {
            var people = new List<Person>();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDir, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Kişi CSV dosyası bulunamadı.", fullPath);

            bool firstLine = true;

            foreach (var rawLine in File.ReadAllLines(fullPath, Encoding.UTF8))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line.StartsWith("#"))
                    continue;

                // İlk satır başlık: "Id;Name"
                if (firstLine)
                {
                    firstLine = false;
                    // Başlık satırıysa atla
                    if (line.StartsWith("Id"))
                        continue;
                }

                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                if (!int.TryParse(parts[0].Trim(), out int id))
                    continue;

                string name = parts[1].Trim();

                people.Add(new Person
                {
                    Id = id,
                    Name = name
                });
            }

            return people;
        }
    }
}
