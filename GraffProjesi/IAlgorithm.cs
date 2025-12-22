using GraffProjesi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraffProjesi
{
    // Tüm algoritmalar için ortak arayüz
    public interface IAlgorithm
    {
        // Algoritmayı çalıştır
        void Execute(int startNodeId);
    }
}
