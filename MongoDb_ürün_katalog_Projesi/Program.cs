using MongoDB_ürün_katalog_Projesi.Utils;
using MongoDB_ürün_katalog_Projesi.Actions;

namespace MongoDB_ürün_katalog_Projesi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MongoDB Ürün Katalog Uygulaması");
            Console.WriteLine("1) Ürün Ekleme");
            Console.WriteLine("2) Ürün Güncelleme");
            Console.WriteLine("3) Ürün Silme");
            Console.WriteLine("4) Ürün Listeleme");
            Console.WriteLine("5) Ürün Arama");
            Console.WriteLine("6) Ürün Filtreleme");
            Console.WriteLine("7) Kategori Ekleme");
            Console.WriteLine("8) Kategori Güncelleme");
            Console.WriteLine("9) Kategori Silme");
            Console.WriteLine("10) Kategori Listeleme");
            Console.WriteLine("11) Hata Loglarını Görüntüleme");
            Console.WriteLine("12) Hata Loglarını Arama");

            Console.Write("Bir seçenek giriniz: ");
            string secim = Console.ReadLine() ?? "";

            actions_kategori actions_Kategori = new actions_kategori();
            actions_ürün actions_Urun = new actions_ürün();
            HataLog hataLog = new HataLog();
            switch (secim)
            {
                case "1":
                    //Ürün Ekleme kodu
                    actions_Urun.actions_add_ürün();
                    break;
                case "2":
                    // Ürün Güncelleme kodu
                    actions_Urun.actions_update_ürün();
                    break;
                case "3":
                    // Ürün Silme kodu
                    actions_Urun.actions_delete_ürün();
                    break;
                case "4":
                    // Ürün Listeleme kodu
                    actions_Urun.actions_list_ürün();
                    break;
                case "5":
                    // Ürün Arama kodu
                    actions_Urun.actions_search_ürün();
                    break;
                case "6":
                    // Ürün Filtreleme kodu
                    actions_Urun.actions_filter_ürün();
                    break;
                case "7":
                    // Kategori Ekleme kodu
                    actions_Kategori.actions_add_kategori();
                    break;
                case "8":
                    // Kategori Güncelleme kodu
                    actions_Kategori.actions_update_kategori();
                    break;
                case "9":
                    // Kategori Silme kodu
                    actions_Kategori.actions_delete_kategori();
                    break;
                case "10":
                    // Kategori Listeleme kodu
                    actions_Kategori.actions_list_kategori();
                    break;
                case "11":
                    // Hata Loglarını Görüntüleme kodu
                    Console.WriteLine("Hata Logları:");
                    var logList = hataLog.ListLog();
                    break;
                case "12":
                    // Hata Loglarını Arama kodu
                    Console.WriteLine("Aranacak log dosyasının adını giriniz:");
                    string? logFileName = Console.ReadLine();
                    var foundLog = hataLog.ReadLog();
                    if (foundLog != null)
                    {
                        Console.WriteLine($"Log Bulundu: {foundLog}");
                    }
                    else
                    {
                        Console.WriteLine("Log Bulunamadı.");
                    }
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }
    }
}

