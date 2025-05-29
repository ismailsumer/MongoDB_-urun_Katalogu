using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDB_ürün_katalog_Projesi.Models
{
    public class models_kategori
    {
        public ObjectId Id { get; set; }

        [BsonElement("categoryname")]
        [BsonRequired]
        [Required(ErrorMessage = "Kategori Alanı BOş Olamaz")]
        public required string KategoriAdi { get; set; }
    }
    
    public class KategoriIndexHelper
    {
        public static void CreateUniqueCategoryIndex(IMongoCollection<models_kategori> collection)
        {
            var indexKeys = Builders<models_kategori>.IndexKeys.Ascending(k => k.KategoriAdi);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<models_kategori>(indexKeys, indexOptions);

            collection.Indexes.CreateOne(indexModel);
        }
    }
}    

