Solution içerisine AnkaKafe.Data adında Class Library türünde bir proje oluşturun ve içerisine aşağıdaki bilgileri kullanarak 4 adet class ve 1 adet enum oluşturun.

Urun
* UrunAd: string
* BirimFiyat: decimal
- ToString(): string

SiparisDetay
* UrunAd: string
* BirimFiyat: decimal
* Adet: int
* TutarTL: string-readonly
- Tutar(): decimal

SiparisDurum (Aktif = 0, Odendi = 1, Iptal = 2)

Siparis
* MasaNo: int
* Durum: SiparisDurum
* OdenenTutar: decimal
* AcilisZamani: DateTime?
* KapanisZamani: DateTime?
* SiparisDetaylar: List<SiparisDetay>
* ToplamTutarTL: string-readonly
- ToplamTutar(): decimal

KafeVeri
* MasaAdet: int
* Urunler: List<Urun>
* AktifSiparisler: List<Siparis>
* GecmisSiparisler: List<Siparis>


* Property
- Method