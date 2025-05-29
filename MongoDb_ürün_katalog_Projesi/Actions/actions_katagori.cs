using MongoDB_ürün_katalog_Projesi.Services;
using MongoDB_ürün_katalog_Projesi.Utils;
using MongoDB.Driver;


namespace MongoDB_ürün_katalog_Projesi.Actions
{
    public class actions_kategori
    {
        private readonly MongoDb dbMongo;
        private readonly IMongoDatabase database;
        private readonly Services_Kategori services_Kategori;

        public actions_kategori()
        {
            dbMongo = new MongoDb();
            database = dbMongo._database;
            services_Kategori = new Services_Kategori(database);
        }

        public void actions_add_kategori()
        {
            Console.WriteLine("Eklenecek Kategori Adını Girin:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                services_Kategori.AddKategori(kategori);
            }
        }
        public void actions_update_kategori()
        {
            Console.WriteLine("Güncellenecek Kategori Adını Girin:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                services_Kategori.UpdateKategori(kategori);
            }
        }
        public void actions_delete_kategori()
        {
            Console.WriteLine("Silinecek Kategori Adını Girin:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = new Models.models_kategori { KategoriAdi = kategoriAdi };
                services_Kategori.DeleteKategori(kategori);
            }
        }
        public void actions_get_kategori()
        {
            Console.WriteLine("Getirilecek Kategori Adını Girin:");
            string? kategoriAdi = Console.ReadLine();
            if (!string.IsNullOrEmpty(kategoriAdi))
            {
                var kategori = services_Kategori.GetKategori(new Models.models_kategori { KategoriAdi = kategoriAdi });
                if (kategori != null)
                {
                    Console.WriteLine($"Kategori Bulundu: {kategori.KategoriAdi}");
                }
                else
                {
                    Console.WriteLine("Kategori Bulunamadı.");
                }
            }
        }
        public void actions_list_kategori()
        {
            Console.WriteLine("Kategorileri Listeleyin:");
            var kategoriler = services_Kategori.GetAllKategoris();
            if (kategoriler != null && kategoriler.Count > 0)
            {
                foreach (var kategori in kategoriler)
                {
                    Console.WriteLine($"Kategori Adı: {kategori.KategoriAdi}");
                }
            }
            else
            {
                Console.WriteLine("Kategoriler Bulunamadı.");
            }
        }
    }
}