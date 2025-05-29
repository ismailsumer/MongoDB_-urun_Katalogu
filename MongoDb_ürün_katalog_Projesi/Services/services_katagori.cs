using MongoDB_ürün_katalog_Projesi.Models;
using MongoDB_ürün_katalog_Projesi.Utils;
using MongoDB.Driver;

namespace MongoDB_ürün_katalog_Projesi.Services
{
    public class Services_Kategori
    {
        
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<models_kategori> _kategoriCollection;
        private readonly HataLog hataLog = new();
        private string? kategori;

        public Services_Kategori(IMongoDatabase database, string? kategori = null)
        {
            _database = database;
            _kategoriCollection = _database.GetCollection<models_kategori>("Kategoriler");
            this.kategori = kategori;
        }

        //Kategori Ekleme
        public int AddKategori(models_kategori kategori)
        {
            try
            {
                _kategoriCollection.InsertOne(kategori);
                return 1;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return 0;
        }

        //Kategori Güncelleme
        public bool UpdateKategori(models_kategori kategori)
        {
            try
            {
                var filter = Builders<models_kategori>.Filter.Eq(item => item.Id, kategori.Id);
                var replaceOneResult = _kategoriCollection.ReplaceOne(filter, kategori);
                return replaceOneResult.IsAcknowledged && replaceOneResult.ModifiedCount > 0;

            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return false;
        }
        //Kategori Silme
        public int DeleteKategori(models_kategori kategori)
        {
            try
            {
                var urunCollection = _database.GetCollection<models_ürün>("Urunler");
                var filterUrun = Builders<models_ürün>.Filter.Eq("Kategori.Id", kategori.Id);
                long urunSayisi = urunCollection.CountDocuments(filterUrun);

                if (urunSayisi > 0) { return 0; }

                var filterKategori = Builders<models_kategori>.Filter.Eq(x => x.Id, kategori.Id);
                var result = _kategoriCollection.DeleteOne(filterKategori);
                return result.DeletedCount > 0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return 0;
        }

        //Kategori Getirme
        public models_kategori? GetKategori(models_kategori kategori)
        {
            try
            {
                var filter = Builders<models_kategori>.Filter.Eq(x => x.Id, kategori.Id);
                return _kategoriCollection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return null;
        }

        //Kategori Listeleme
        public List<models_kategori> GetAllKategoris()
        {
            try
            {
                List<models_kategori> list = _kategoriCollection.Find(_ => true).ToList();
                return list;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return new List<models_kategori>();
        }
    }
}