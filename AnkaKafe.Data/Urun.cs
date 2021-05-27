using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkaKafe.Data
{
    public class Urun
    {
        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return $"{UrunAd} (₺{BirimFiyat:0.00})";
        }
    }
}
