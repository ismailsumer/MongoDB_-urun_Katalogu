using MongoDB_ürün_katalog_Projesi.Models;
using MongoDB_ürün_katalog_Projesi.Utils;
using MongoDB.Driver;



namespace MongoDB_ürün_katalog_Projesi.Services
{
    public class Services_ürün

    {
        private readonly IMongoCollection<models_ürün> _urunCollection;

        public Services_ürün(IMongoDatabase database)
        {
            _urunCollection = database.GetCollection<models_ürün>("urun");
        }

        private readonly HataLog hataLog = new();

        //Ürün Ekleme
        public int AddUrun(models_ürün urun)
        {
            try
            {
                _urunCollection.InsertOne(urun);
                return 1;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return 0;
        }

        //Ürün Güncelleme
        public bool UpdateUrun(models_ürün urun)
        {
            try
            {
                var Filter = Builders<models_ürün>.Filter.Eq(item => item.Id, urun.Id);
                ReplaceOneResult replaceOneResult = _urunCollection.ReplaceOne(Filter, urun);
                return replaceOneResult.IsAcknowledged && replaceOneResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return false;
        }

        //Ürün Silme
        public void DeleteUrun(string Id)
        {
            try
            {
                var objectId = MongoDB.Bson.ObjectId.Parse(Id);
                _urunCollection.DeleteOne(x => x.Id == objectId);
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
        }

        //Ürün Getirme
        public models_ürün? GetUrun(models_ürün urun)
        {
            try
            {
                var filter = Builders<models_ürün>.Filter.Eq(x => x.Id, urun.Id);
                return _urunCollection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return null;
        }

        //Ürün Listeleme
        public List<models_ürün> GetUruns()
        {
            try
            {
                var result = _urunCollection.Find(Builders<models_ürün>.Filter.Empty)
                .SortByDescending(x => x.EklenmeTarihi)
                .Skip(1)
                .Limit(10)
                .ToList();
                return result;
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return new List<models_ürün>();
        }

        //Ürün Adına Göre Arama
        public List<models_ürün> GetUrun(string ad, string Acıklama)
        {
            try
            {
                var filter = Builders<models_ürün>.Filter.And
                (
                    Builders<models_ürün>.Filter.Eq(x => x.Ad, ad),
                    Builders<models_ürün>.Filter.Regex(y => y.Aciklama, new MongoDB.Bson.BsonRegularExpression(Acıklama, "i"))
                );

                return _urunCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return new List<models_ürün>();
        }

        //Ürün Filtreleme
        public List<models_ürün> GetFilterUrun(decimal? minfiyat, decimal? maxfiyat, string kategori)
        {
            try
            {
                var filters = new List<FilterDefinition<models_ürün>>();

                if (!string.IsNullOrEmpty(kategori))
                {
                    filters.Add(Builders<models_ürün>.Filter.Eq("Kategori", kategori));
                }
                if (minfiyat.HasValue)
                {
                    filters.Add(Builders<models_ürün>.Filter.Gte(x => x.Fiyat, minfiyat.Value));
                }
                if (maxfiyat.HasValue)
                {
                    filters.Add(Builders<models_ürün>.Filter.Lte(x => x.Fiyat, maxfiyat.Value));
                }

                var filter = filters.Count > 0
                    ? Builders<models_ürün>.Filter.And(filters)
                    : Builders<models_ürün>.Filter.Empty;

                return _urunCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                hataLog.WriteToLog(ex.ToString());
            }
            return new List<models_ürün>();
        }
    }
}