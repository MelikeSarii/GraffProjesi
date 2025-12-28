using System;
using System.Collections.Generic;//list queue hashSet yapıları için lazım

namespace GraffProjesi
{
    // Breadth-First Search algoritması
    /* Seçilen düğümden erişilebilen tüm düğümleri yazdırır.
     * Queue, bağlı tüm düğümler listelenir
     */
    public class BFSAlgorithm : Algorithm
    {//algo sınıfından aklıtım alıyor
        public List<int> Result { get; private set; }
        //bfs niin gezdiği id ler burda tutulacak
        public BFSAlgorithm(Graph graph) : base(graph)
        {
            //dışardan graf geliyo bu grafı da üst sınıfa gönderiyro
            Result = new List<int>();
            //sonuç listesi oluşturuluyo (boş şu an için)
        }

        // bfs miz başlıyo
        public override void Execute(int startNodeId)
        {//aşgo sınfındaki execute metotunu eziyo
            Result.Clear();
            //eski sonucu silmeliyiz çünkü yeni bir başlangıç düğümü ile çalışıyoruz
           
            var visited = new HashSet<Node>();//ziyaret edilen düğümleri tutar
            var queue = new Queue<Node>();//bfs için kuyruğu
            //ilk giren ilk çıkar fifo

            // Başlangıç düğümünü Graph'tan al
            Node startNode = null;
            foreach (var node in Graph.GetAllNodes())
            {
                if (node.Id == startNodeId)
                {//verdiğimiz id ile düğümü bulmaya çalışıyor
                    startNode = node;
                    break;
                }
            }

            if (startNode == null)
                throw new Exception("Başlangıç düğümü bulunamadı!");
            //yoksa o düğüm hata ver
            visited.Add(startNode);
            queue.Enqueue(startNode);//başkangıç düğümü kuyruğa konuluyor

            while (queue.Count > 0)
            {//kuyruk boş olana kadar döngümüz devam eder
                Node current = queue.Dequeue();
                //kuyruğun başındaki elemanı al ve kuyruktan çıkar(current yani şu an işlenen)
                Result.Add(current.Id);
                //result listesine ekle
                foreach (var neighbor in Graph.GetNeighbors(current))
                {//bu sefer işlem yapılan düğümün komşuları dolaşılıyor
                    if (!visited.Contains(neighbor))
                    {//bu kısımda da eğer bu komşu daha önce ziyaret edilmediyse
                        //ziyaret edilenlere ve kuyruğa ekle
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }
}//genel mantığı bi düğüm alıp komşularını kuyruğa ekleriz,böyle katman katman baakrız