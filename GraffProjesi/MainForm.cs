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
        private Graph _graph;
        private List<Person> _people;

        public MainForm()
        {
            InitializeComponent();
        }

        // Form açılır açılmaz KÜÇÜK grafı yükle
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv","Küçük Graf");
        }
        private void LoadData(string graphFile, string peopleFile, string description)
        {
            try
            {
                _graph = new Graph();
                _graph.LoadFromFile(graphFile);

                _people = CsvPeopleLoader.Load(peopleFile);
                FillPeopleList();

                MessageBox.Show(
                    $"{description} başarıyla yüklendi.\n" +
                    $"Kişi sayısı: {_people.Count}",
                    "Yükleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{description} yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // Küçük graf butonu
        private void btnLoadSmall_Click(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv");
        }

        // Büyük graf butonu
        private void btnLoadBig_Click(object sender, EventArgs e)
        {
            LoadData("graph_buyuk.txt", "kisiler_buyuk.csv");
        }

        // Ortak yükleme metodu
        private void LoadData(string graphFile, string peopleFile)
        {
            try
            {
                _graph = new Graph();
                _graph.LoadFromFile(graphFile);

                _people = CsvPeopleLoader.Load(peopleFile);

                FillPeopleList();

                MessageBox.Show(
                    $"{graphFile} dosyasından graf başarıyla yüklendi.\n" +
                    $"Kişi sayısı: {_people.Count}",
                    "Yükleme Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Veriler yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ListBox'ı doldur
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

        // ListBox'tan kişi seçilince detay göster
        private void lstPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex == -1 || _people == null)
                return;

            var selectedText = lstPeople.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedText))
                return;

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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData("graph_kucuk.txt", "kisiler_kucuk.csv", "Küçük graf");

        }

        private void btnBuyukGraf_Click(object sender, EventArgs e)
        {
            LoadData("graph_buyuk.txt", "kisiler_buyuk.csv", "Büyük graf");

        }
    }
}