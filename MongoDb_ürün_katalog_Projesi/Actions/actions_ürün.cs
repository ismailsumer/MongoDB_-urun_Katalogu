using MongoDB_ürün_katalog_Projesi.Services;
using MongoDB_ürün_katalog_Projesi.Utils;
using MongoDB.Driver;

namespace MongoDB_ürün_katalog_Projesi.Actions
{
    public class actions_ürün
    {
        private readonly MongoDb dbMongo;
        private readonly IMongoDatabase database;
        private readonly Services_ürün services_ürün;

        public actions_ürün()
        {
            dbMongo = new MongoDb();
            database = dbMongo._database;
            services_ürün = new Services_ürün(database);
        }
        public void actions_add_ürün()
        {
            Console.WriteLine("Eklenecek Ürünün Girişini Yapınız:");
            Console.WriteLine("Ürün Adı:");
            string? urunAdi = Console.ReadLine();
            Console.WriteLine("Açıklama:");
            string? Aciklama = Console.ReadLine();
            Console.WriteLine("Fiyat:");
            string? fiyatInput = Console.ReadLine();
            decimal fiyat = 0;
            if (!string.IsNullOrEmpty(fiyatInput) && decimal.TryParse(fiyatInput, out decimal parsedFiyat))
            {
                fiyat = parsedFiyat;
            }
            else
            {
                Console.WriteLine("Geçersiz fiyat girdiniz. Varsayılan olarak 0 atanacak.");
            }
            Console.WriteLine("Stok Adedi:");
            string? stokAdediInput = Console.ReadLine();
            int stokAdedi = 0;
            if (!string.IsNullOrEmpty(stokAdediInput) && int.TryParse(stokAdediInput, out int parsedStokAdedi))
            {
                stokAdedi = parsedStokAdedi;
            }
            else
            {
                Console.WriteLine("Geçersiz stok adedi girdiniz. Varsayılan olarak 0 atanacak.");
            }
            Console.WriteLine("Kategori Adı:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(urunAdi) && !string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                var urun = new Models.models_ürün { Ad = urunAdi, Aciklama = Aciklama, Fiyat = fiyat, StokAdedi = stokAdedi, Kategori = kategori };
                services_ürün.AddUrun(urun);
            }
        }
        public void actions_update_ürün()
        {
            Console.WriteLine("Güncellenecek Ürünün Girişini Yapınız:");
            Console.WriteLine("Ürün Adı:");
            string? urunAdi = Console.ReadLine();
            Console.WriteLine("Açıklama:");
            string? Aciklama = Console.ReadLine();
            Console.WriteLine("Fiyat:");
            string? fiyatInput = Console.ReadLine();
            decimal fiyat = 0;
            if (!string.IsNullOrEmpty(fiyatInput) && decimal.TryParse(fiyatInput, out decimal parsedFiyat))
            {
                fiyat = parsedFiyat;
            }
            else
            {
                Console.WriteLine("Geçersiz fiyat girdiniz. Varsayılan olarak 0 atanacak.");
            }
            Console.WriteLine("Stok Adedi:");
            string? stokAdediInput = Console.ReadLine();
            int stokAdedi = 0;
            if (!string.IsNullOrEmpty(stokAdediInput) && int.TryParse(stokAdediInput, out int parsedStokAdedi))
            {
                stokAdedi = parsedStokAdedi;
            }
            else
            {
                Console.WriteLine("Geçersiz stok adedi girdiniz. Varsayılan olarak 0 atanacak.");
            }
            Console.WriteLine("Kategori Adı:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(urunAdi) && !string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                var urun = new Models.models_ürün { Ad = urunAdi, Aciklama = Aciklama, Fiyat = fiyat, StokAdedi = stokAdedi, Kategori = kategori };
                services_ürün.UpdateUrun(urun);
            }
        }
        public void actions_delete_ürün()
        {
            Console.WriteLine("Silinecek Ürünün Girişini Yapınız:");
            Console.WriteLine("Ürün Adı:");
            string? urunAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(urunAdi))
            {
                // Kategori bilgisi zorunlu olduğu için kullanıcıdan alınmalı
                Console.WriteLine("Kategori Adı:");
                string? kategoriAdi = Console.ReadLine();
                if (!string.IsNullOrEmpty(kategoriAdi))
                {
                    var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                    // Eğer DeleteUrun ürün adı ile silme yapıyorsa:
                    services_ürün.DeleteUrun(urunAdi);
                }
                else
                {
                    Console.WriteLine("Kategori adı boş olamaz.");
                }
            }
        }
        public void actions_get_ürün()
        {
            Console.WriteLine("Getirilecek Ürünün Girişini Yapınız:");
            Console.WriteLine("Ürün Adı:");
            string? urunAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(urunAdi))
            {
                Console.WriteLine("Kategori Adı:");
                string? kategoriAdi = Console.ReadLine();
                if (!string.IsNullOrEmpty(kategoriAdi))
                {
                    var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                    var urun = services_ürün.GetUrun(new Models.models_ürün { Ad = urunAdi, Kategori = kategori });
                    if (urun != null)
                    {
                        Console.WriteLine($"Ürün Bulundu: {urun.Ad}");
                    }
                    else
                    {
                        Console.WriteLine("Ürün Bulunamadı.");
                    }
                }
                else
                {
                    Console.WriteLine("Kategori adı boş olamaz.");
                }
            }
        }
        public void actions_list_ürün()
        {
            Console.WriteLine("Ürünleri Listeleyin:");
            var urunler = services_ürün.GetUruns();
            if (urunler != null && urunler.Count > 0)
            {
                foreach (var urun in urunler)
                {
                    Console.WriteLine($"Ürün Adı: {urun.Ad}, Fiyat: {urun.Fiyat}, Stok Adedi: {urun.StokAdedi}");
                }
            }
            else
            {
                Console.WriteLine("Ürünler Bulunamadı.");
            }
        }
        public void actions_search_ürün()
        {
            Console.WriteLine("Aranacak Ürünün Girişini Yapınız:");
            Console.WriteLine("Ürün Adı:");
            string? urunAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(urunAdi))
            {
                var urun = services_ürün.GetUrun(new Models.models_ürün { Ad = urunAdi, Kategori = new Models.models_kategori { KategoriAdi = string.Empty } });
                if (urun != null)
                {
                    Console.WriteLine($"Ürün Bulundu: {urun.Ad}");
                }
                else
                {
                    Console.WriteLine("Ürün Bulunamadı.");
                }
            }
        }
        public void actions_filter_ürün()
        {
            Console.WriteLine("Filtreleme için kriterleri giriniz:");
            Console.WriteLine("Minimum Fiyat:");
            string? minFiyatInput = Console.ReadLine();
            decimal? minFiyat = null;
            if (!string.IsNullOrEmpty(minFiyatInput) && decimal.TryParse(minFiyatInput, out decimal parsedMinFiyat))
            {
                minFiyat = parsedMinFiyat;
            }
            Console.WriteLine("Maksimum Fiyat:");
            string? maxFiyatInput = Console.ReadLine();
            decimal? maxFiyat = null;
            if (!string.IsNullOrEmpty(maxFiyatInput) && decimal.TryParse(maxFiyatInput, out decimal parsedMaxFiyat))
            {
                maxFiyat = parsedMaxFiyat;
            }
            Console.WriteLine("Kategori Adı:");
            string? kategoriAdi = Console.ReadLine();
            // kategoriAdi null ise boş string ata
            var urunler = services_ürün.GetFilterUrun(minFiyat, maxFiyat, kategoriAdi ?? string.Empty);
            if (urunler != null && urunler.Count > 0)
            {
                foreach (var urun in urunler)
                {
                    Console.WriteLine($"Ürün Adı: {urun.Ad}, Fiyat: {urun.Fiyat}, Stok Adedi: {urun.StokAdedi}");
                }
            }
            else
            {
                Console.WriteLine("Filtreleme sonucu ürün bulunamadı.");
            }
        }
    }
}