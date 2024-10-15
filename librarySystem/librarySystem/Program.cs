using System;
using System.Collections.Generic;
using System.IO;

namespace librarySystem
{
    internal class Program
    {
        static List<string> kitapList = new List<string>();
        static string dosyaYolu = Path.Combine(Environment.CurrentDirectory, "kitapList.txt");
        

        
        public static void KitaplariDosyadanOku()
        {
            if (File.Exists(dosyaYolu))
            {
                kitapList = new List<string>(File.ReadAllLines(dosyaYolu));
                Console.WriteLine("Kitaplar başarıyla yüklendi.");
            }
            else
            {
                Console.WriteLine("Kitap dosyası bulunamadı.");
            }
        }

        
        public static void KitaplariDosyayaKaydet()
        {
            File.WriteAllLines(dosyaYolu, kitapList);
            Console.WriteLine("Kitaplar başarıyla kaydedildi.");
        }


        public static void KitapArat(string search)
        {
            foreach (string kitap in kitapList)
            {
                if (kitap.ToLower().Contains(search.ToLower()))
                {
                    Console.WriteLine("\n" + kitap);
                }
            }
        }

        public static void KategorilereGozAt()
        {
            var kategoriler = new HashSet<string>();

            foreach (string kitap in kitapList)
            {
                string[] parts = kitap.Split('-');
                if (parts.Length >= 3)
                {
                    string kategori = parts[2].Split('[')[0].Trim();
                    kategoriler.Add(kategori);
                }
            }

            Console.WriteLine("\n|---------------------Kategoriler----------------------|");
            foreach (string kategori in kategoriler)
            {
                Console.WriteLine(kategori);
            }
            Console.WriteLine("|------------------------o------------------------|\n");

            Console.Write("Bir kategori seçin (veya ana menüye dönmek için 'q' girin): ");
            string seciliKategori = Console.ReadLine();

            if (seciliKategori.ToLower() != "q")
            {
                Console.WriteLine($"\n|---------------------{seciliKategori} Kategorisindeki Kitaplar----------------------|");
                foreach (string kitap in kitapList)
                {
                    if (kitap.Contains(seciliKategori))
                    {
                        Console.WriteLine(kitap);
                    }
                }
                Console.WriteLine("|--------------------------------o--------------------------------|\n");
            }
        }

        public static bool kitapDurumuGuncelle(int kitapKodu, string yeniDurum)
        {
            for (int i = 0; i < kitapList.Count; i++)
            {
                if (kitapList[i].StartsWith(kitapKodu.ToString()))
                {
                    string[] parts = kitapList[i].Split(new string[] { "[ ", " ]" }, StringSplitOptions.None);
                    if (parts.Length >= 2)
                    {
                        string updatedBook = kitapList[i].Replace(parts[parts.Length - 2], yeniDurum);
                        kitapList[i] = updatedBook;
                        return true;
                    }
                }
            }
            return false;
        }

        public static void kitapEkle(int kitapKodu, string yazar, string kitapAdi, int sayfaSayisi, string kitapKategori, string status)
        {

            kitapList.Add($"{kitapKodu} - {yazar} | {kitapAdi} ({sayfaSayisi}) - {kitapKategori} [ {status} ]");
        }
        public static bool kitapSil(int kitapKodu)
        {
            string kitapToRemove = null;
            foreach (string kitap in kitapList)
            {
                if (kitap.StartsWith(kitapKodu.ToString()))
                {
                    kitapToRemove = kitap;
                    break;
                }
            }

            if (kitapToRemove != null)
            {
                kitapList.Remove(kitapToRemove);
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            KitaplariDosyadanOku();
            
            Console.WriteLine("Kütüphanemize hoşgeldiniz.");
            
            start:

            int islem;
            Console.Write("1. Tüm kitaplara göz at\n" +
                "2. Kategorilere göz at\n" +
                "3. Yazar veya kitap adı ile arat\n" +
                "4. Sistemden Ayrıl\n" +
                "0. Admin Girişi\n" +
                "\nYapmak istediğiniz işlemi belirtiniz: ");

            // Kullanıcının girdisini okuyalım
            if (int.TryParse(Console.ReadLine(), out islem))
            {
                switch (islem)
                {
                    case 0:
                        // Admin İşlemleri
                        string adminNickname = "admin";
                        string adminPassword = "123456";
                    admingiris:
                        Console.Write("Kullanıcı Adı: "); string enteredNickname = Console.ReadLine();
                        Console.Write("Şifre: "); string enteredPassword = Console.ReadLine();

                        if (enteredNickname == adminNickname && enteredPassword == adminPassword)
                        {
                        adminislem:
                            
                            Console.WriteLine("Admin paneline hoşgeldin.\n" +
                                "1. Tüm Kitapları Göster\n" +
                                "2. Kitap Ekle\n" +
                                "3. Kitap Durumu Güncelle\n" +
                                "4. Kitap Sil\n" +
                                "0. Admin panelinden ayrıl\n" +
                                "Yapmak istediğin işlemi gir: "); int adminSecim;
                            if (int.TryParse(Console.ReadLine(), out adminSecim))
                            {
                                switch (adminSecim)
                                {
                                    case 0:
                                        // Ana menüye döndürür.
                                        Console.Clear();
                                        goto start;
                                    case 1:
                                        // Tüm kitapları göster
                                        Console.WriteLine("\n|---------------------Tüm Kitaplar----------------------|");
                                        foreach (string kitap in kitapList)
                                        {
                                            Console.WriteLine(kitap + "\n");
                                        }
                                        Console.WriteLine("|---------------------------o---------------------------|\n");
                                        goto adminislem;


                                    case 2:
                                    kitapEkle:
                                        // Kitap Ekle

                                        try
                                        {
                                            Console.Write("Kitap Kodu: "); int kitapKodu = Convert.ToInt32(Console.ReadLine());
                                            Console.Write("Yazarı: "); string kitapYazari = Convert.ToString(Console.ReadLine());
                                            Console.Write("Kitap Adı: "); string kitapAdi = Convert.ToString(Console.ReadLine());
                                            Console.Write("Sayfa Sayısı: "); int sayfaSayisi = Convert.ToInt32(Console.ReadLine());
                                            Console.Write("Kategori: "); string kitapKategori = Convert.ToString(Console.ReadLine());
                                            Console.Write("Durum: "); string bookStatus = Console.ReadLine();
                                            kitapEkle(kitapKodu, kitapYazari, kitapAdi, sayfaSayisi, kitapKategori, bookStatus);
                                            Console.WriteLine("\nKitabınız başarıyla eklendi.\n");
                                        }
                                        catch (FormatException fe)
                                        {
                                            Console.WriteLine(fe.Message + "\nKitap eklenirken bir hata ile karşılaştık. İstenen bilgilere geçersiz karakter girmediğinizden emin olun!\n");
                                            goto kitapEkle;
                                        }
                                        goto adminislem;
                                    case 3:

                                        Console.Write("Güncellemek istediğiniz kitabın sıra kodunu giriniz: "); int sequence = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Seçtiğiniz kitabın yeni durumunu giriniz: "); string status = Console.ReadLine();
                                        kitapDurumuGuncelle(sequence, status);

                                        goto adminislem;                                       
                                    case 4:
                                    // Kitap Sil
                                    kitapSilme:
                                        try
                                        {
                                            Console.Write("Silmek istediğiniz kitabın kodunu giriniz: ");
                                            int silinecekKitapKodu = Convert.ToInt32(Console.ReadLine());

                                            if (kitapSil(silinecekKitapKodu))
                                            {
                                                Console.WriteLine("\nKitap başarıyla silindi.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nBu koda sahip bir kitap bulunamadı.\n");
                                            }
                                        }
                                        catch (FormatException fe)
                                        {
                                            Console.WriteLine(fe.Message + "\nGeçerli bir kitap kodu giriniz!\n");
                                            goto kitapSilme;
                                        }
                                        goto adminislem;
                                    default:
                                        Console.WriteLine("Böyle bir seçenek yok.");
                                        goto adminislem;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geçerli bir sayı gir.");
                            }

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Hatalı giriş bilgisi. Tekrar deneyin.");
                            goto admingiris;
                        }
                        break;

                    case 1:
                        // Tüm kitapları göster
                        Console.WriteLine("\n|---------------------Tüm Kitaplar----------------------|");
                        foreach (string kitap in kitapList)
                        {
                            Console.WriteLine(kitap + "\n");
                        }
                        Console.WriteLine("|---------------------------o---------------------------|\n");
                        goto start;

                    case 2:
                        // Kategorileri göster
                        KategorilereGozAt();
                        goto start;

                    case 3:
                        // Arat
                        Console.Write("Aratmak istediğiniz yazarı veya kitap adını giriniz: ");
                        string search = Console.ReadLine();

                        KitapArat(search);


                        goto start;

                    case 4:
                        // Sistemden çıkış yapılır.
                        goto cikis;
                    default:
                        // Hata
                        Console.WriteLine("Geçersiz bir seçenek girdiniz.");
                        goto start;
                }
            }
            else
            {
                Console.WriteLine("Lütfen geçerli bir sayı girin.");
            }
            cikis:
            KitaplariDosyayaKaydet();
        }
    }
}
