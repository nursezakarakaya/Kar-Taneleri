using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kar_Taneleri
{
    public partial class Form1 : Form
    {
        public class KarTanesi
        {
            public Label Etiket { get; set; }
            public int YatayHiz { get; set; }
        }

        List<KarTanesi> karTaneleri = new List<KarTanesi>();
        Random rnd = new Random();
        Timer timerHareket = new Timer();

        public Form1()
        {
            InitializeComponent();

            // PENCERE AYARLARI
            this.BackColor = Color.FromArgb(20, 20, 40);
            this.Size = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //yeniden boyutlandırılamaz
            this.MaximizeBox = false; // Ekranı kapla butonu devre dışı
            this.StartPosition = FormStartPosition.CenterScreen;

            // ORTADAKİ METİN
            Label lblMesaj = new Label();
            lblMesaj.Text = "Mutlu Yıllar! :3";
            lblMesaj.ForeColor = Color.White;
            lblMesaj.BackColor = Color.Transparent;
            lblMesaj.AutoSize = true;
            lblMesaj.Font = new Font("Segoe Script", 25, FontStyle.Bold | FontStyle.Italic);
            this.Controls.Add(lblMesaj);

            // Yazıyı formun tam ortasına konumlandırma
            lblMesaj.Location = new Point(
                (this.ClientSize.Width - lblMesaj.Width) / 2,
                (this.ClientSize.Height - lblMesaj.Height) / 2
            );

            // Başlangıçta ekrana rastgele dağılmış 50 adet kar tanesi ekleme
            for (int i = 0; i < 50; i++)
            {
                Label lbl = YeniKarTanesiOlustur();
                // Kar tanelerini pencere içine dağıtıyoruz
                lbl.Location = new Point(rnd.Next(0, this.ClientSize.Width), rnd.Next(0, this.ClientSize.Height));
                this.Controls.Add(lbl);
                karTaneleri.Add(new KarTanesi { Etiket = lbl, YatayHiz = 0 });
            }

            // Her 100 ms'de aşağı kayma
            timerHareket.Interval = 100;
            timerHareket.Tick += TimerHareket_Tick;
            timerHareket.Start();
        }

        private Label YeniKarTanesiOlustur()
        {
            Label lbl = new Label();
            lbl.Text = "*";
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.AutoSize = true;
            lbl.Font = new Font("Arial", rnd.Next(10, 30), FontStyle.Bold);
            return lbl;
        }

        private void TimerHareket_Tick(object sender, EventArgs e)
        {
            for (int i = karTaneleri.Count - 1; i >= 0; i--)
            {
                var kar = karTaneleri[i];

                kar.YatayHiz = rnd.Next(-5, 6);

                int yeniX = kar.Etiket.Location.X + kar.YatayHiz;
                int yeniY = kar.Etiket.Location.Y + 3;

                kar.Etiket.Location = new Point(yeniX, yeniY);

                // En alta ulaşan kar tanesini yok etme ve yenisini üstte oluşturma
                if (yeniY > this.ClientSize.Height)
                {
                    this.Controls.Remove(kar.Etiket); //karı ekrandan gizler
                    kar.Etiket.Dispose(); // tamamen serbest bırakır. böylece çöp olanlar bellekte yer kaplamaz
                    karTaneleri.RemoveAt(i); //silinen kar tanesinin listedeki indexini siler. böylece artık var olmayan kar tanelerini kod boşuna hareket ettirmeye çalışmaz.

                    Label yeniLbl = YeniKarTanesiOlustur();
                    yeniLbl.Location = new Point(rnd.Next(0, this.ClientSize.Width), -30);
                    this.Controls.Add(yeniLbl);
                    karTaneleri.Add(new KarTanesi { Etiket = yeniLbl, YatayHiz = 0 });
                }
            }
        }
    }
}