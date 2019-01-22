﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Docházka
{

    public partial class Main : Form
    {
        public String[] mesice = new String[12] { "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
        private String[] dnyEn = new String[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private String[] dnyCz = new String[7] { "PONDĚLÍ", "ÚTERÝ", "STŘEDA", "ČTVRTEK", "PÁTEK", "SOBOTA", "NEDĚLE" };

        public List<Osoba> Osoby;
        public List<Karta> poleKaret;
        public OsobniTabulka UniversalniTabulka;

        public String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public Random random;

        public Main()
        {
            InitializeComponent();
            random = new Random();
            UniversalniTabulka = new OsobniTabulka();
            Osoby = new List<Osoba>();
            poleKaret = new List<Karta>();
            UniversalniTabulka.Reset();
        }

        private void pridat_Click(object sender, EventArgs e)
        {
            Editace_osob pridat = new Editace_osob("NEW", this);
            pridat.ShowDialog();

        }

        private void nastaveni_MouseEnter(object sender, EventArgs e)
        {
            nastaveni.BackColor = System.Drawing.Color.White;

        }

        private void nastaveni_MouseLeave(object sender, EventArgs e)
        {
            nastaveni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(218)))), ((int)(((byte)(219)))));

        }

        private void pridat_MouseEnter(object sender, EventArgs e)
        {
            pridat.BackColor = System.Drawing.Color.White;

        }

        private void pridat_MouseLeave(object sender, EventArgs e)
        {
            pridat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(218)))), ((int)(((byte)(219)))));

        }

        /**
         * 
         * Metoda Nastaví čas jméno svátku dnes a datum
         * vykreslí okno ve středu obrazovky
         **/
        private void Main_Load(object sender, EventArgs e)
        {
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2));

            cas.Text = String.Format("{0:00}", DateTime.Now.Hour) + ":" + String.Format("{0:00}", DateTime.Now.Minute);
            datum.Text = DateTime.Now.Day + ". " + mesice[DateTime.Now.Month - 1];
            den.Text = "" + dnyCz[IndexOf(dnyEn, "" + DateTime.Now.DayOfWeek)];

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            jmeno.Text = (webClient.DownloadString("http://svatky.adresa.info/txt").Split(';'))[1];

            for (int i = 0; i < 20; i++)
                Osoby.Add(new Osoba() {jmeno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((RandomString(random.Next(2, 8)).ToLower())), prijmeni = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((RandomString(random.Next(2, 8)).ToLower())) });

            timer.Start();
        }

        string RandomString(int length)
        {

            string characters = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int r = random.Next(characters.Length);
                result.Append(characters[r]);
            }
            return result.ToString();
        }


        /**
         * Metoda vrací index stringu ve stringovém poli
         **/
        public int IndexOf(String[] arr, String value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        private void Seznam_Click(object sender, EventArgs e)
        {
            Seznam seznam = new Seznam(this);
            this.Hide();
            seznam.ShowDialog();
            this.Show();
        }

        private void Seznam_MouseEnter(object sender, EventArgs e)
        {
            Seznam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(67)))), ((int)(((byte)(73)))));

        }

        private void Seznam_MouseLeave(object sender, EventArgs e)
        {
            Seznam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));

        }

        private void Seznam_MouseEnter_1(object sender, EventArgs e)
        {
            this.Seznam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(67)))), ((int)(((byte)(73)))));
        }

        private void Seznam_MouseLeave_1(object sender, EventArgs e)
        {
            this.Seznam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));

        }

        private void Karty_MouseEnter(object sender, EventArgs e)
        {
            this.Karty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(67)))), ((int)(((byte)(73)))));
        }

        private void Karty_MouseLeave(object sender, EventArgs e)
        {
            this.Karty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));

        }

        private void Karty_Click(object sender, EventArgs e)
        {
            Karty k = new Karty(this);
            Hide();
            k.ShowDialog();
            Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            cas.Text = String.Format("{0:00}", DateTime.Now.Hour) + ":" + String.Format("{0:00}", DateTime.Now.Minute);

        }
    }
}
