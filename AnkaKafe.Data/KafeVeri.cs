using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkaKafe.Data
{
    public class KafeVeri
    {
        public int MasaAdet { get; set; } = 20; // varsayılan değer

        public List<Urun> Urunler { get; set; } = new List<Urun>();

        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>();

        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>();
    }
}
