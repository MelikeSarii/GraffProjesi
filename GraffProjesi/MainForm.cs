using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GraffProjesi;          // Graph sınıfı için
using GraffProjesi;
using System.IO;                 // gerekirse


namespace GraffProjesi
{
    public partial class MainForm : Form
    {
        // Form içinde kullanacağımız graf nesnesi
        private Graph _graph;
        private List<Person> _people;

        public MainForm()
        {
            InitializeComponent();
        }
        
        // Form açılırken çalışacak
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _graph = new Graph();

                // Küçük grafı yükle (15 kişilik olan)
                // İstersen bunu "graph.txt" veya "graph_buyuk.txt" yapabilirsin
                _graph.LoadFromFile("graph_kucuk.txt");

                _people = CsvPeopleLoader.Load("kisiler_kucuk.csv");
                // ListBox'u doldur
                lstPeople.Items.Clear();
                foreach (var p in _people)
                {
                    lstPeople.Items.Add($"{p.Id} - {p.AdSoyad}");
                }

                MessageBox.Show(
                     $"Küçük graf başarıyla yüklendi.\n" +
                    $"Kişi sayısı: {_people.Count}",
                    "Yükleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Graf yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void FillPeopleList()
        {
            lstPeople.Items.Clear();

            if (_people == null || _people.Count == 0)
            {
                lstPeople.Items.Add("Kişi bulunamadı.");
                return;
            }

            foreach (var p in _people)
            {
                lstPeople.Items.Add($"{p.Id} - {p.AdSoyad}");
            }
        }
       

        private void lstPeople_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex == -1 || _people == null)
                return;

            var selectedText = lstPeople.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedText))
                return;

            // Soldaki "Id" kısmını alıyoruz: "1 - Ayşe (Depremzede) [Hatay]" → "1"
            var idPart = selectedText.Split('-')[0].Trim();

            if (!int.TryParse(idPart, out int id))
                return;

            var person = _people.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return;


            MessageBox.Show(
             $"Id: {person.Id}\nAd Soyad: {person.AdSoyad}",
                "Kişi Bilgisi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
