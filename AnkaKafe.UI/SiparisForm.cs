﻿using AnkaKafe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkaKafe.UI
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        private readonly BindingList<SiparisDetay> _blSiparisDetaylar;

        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            _db = kafeVeri;
            _siparis = siparis;
            _blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);
            InitializeComponent();
            dgvSiparisDetaylar.AutoGenerateColumns = false; // otomatik sütun oluşturmayı kapat
            UrunleriGoster();
            EkleFormSifirla();
            MasaNoGuncelle();
            FiyatGuncelle();
            DetaylariListele();
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged;
        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            FiyatGuncelle();
        }

        private void UrunleriGoster()
        {
            cboUrun.DataSource = _db.Urunler;
        }

        private void FiyatGuncelle()
        {
            lblOdemeTutar.Text = _siparis.ToplamTutarTL;
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo} Sipariş Bilgileri";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cboUrun.SelectedIndex == -1 || nudAdet.Value < 1)
                return;// seçili ürün yok , metottan çık
            Urun urun = (Urun)cboUrun.SelectedItem;

            SiparisDetay siparisDetay = new SiparisDetay()
            {
                UrunAd = urun.UrunAd,
                BirimFiyat = urun.BirimFiyat,
                Adet = (int)nudAdet.Value
            };

            // _blSiparisDetaylar içinde _siparis.SiparisDetaylar'ı da içerdiği için
            // aynı zamanda Form'dan gelen _siparis nesnesinin detaylarına da bu detayı ekleyecektir.
            // ve datagridview'ı kendindeki verilerin değiştiği konusunda bilgilendirecektir
            _blSiparisDetaylar.Add(siparisDetay);
            EkleFormSifirla();
        }

        private void EkleFormSifirla()
        {
            cboUrun.SelectedIndex = -1;
            nudAdet.Value = 1;
        }

        private void DetaylariListele()
        {
            dgvSiparisDetaylar.DataSource = _blSiparisDetaylar;
        }

        private void dgvSiparisDetaylar_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                text: "Seçili sipariş detayları silinecektir. Onaylıyor musun?",
                caption: "Silme Onayı",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Exclamation, 
                defaultButton: MessageBoxDefaultButton.Button2
            );

            // true atamanız sonucunda satır silme işleminin önüne geçmiş olursunuz
            e.Cancel = dr == DialogResult.No;
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            SiparisKapat(SiparisDurum.Iptal, 0);
        }

        private void btnOde_Click(object sender, EventArgs e)
        {
            SiparisKapat(SiparisDurum.Odendi, _siparis.ToplamTutar());
        }


        private void SiparisKapat(SiparisDurum siparisDurum, decimal odenenTutar)
        {
            _siparis.Durum = siparisDurum;
            _siparis.KapanisZamani = DateTime.Now;
            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            Close();
        }
    }
}
