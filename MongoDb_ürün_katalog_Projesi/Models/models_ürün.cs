using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_ürün_katalog_Projesi.Models
{
    public class models_ürün
    {
        public ObjectId Id { get; set; }

        [BsonElement("ad")]
        [BsonRequired]
        [Required(ErrorMessage ="Ad Alanı Boş Olamaz!")]
        public required string Ad { get; set; }

        public string? Aciklama { get; set; }

        [BsonElement("fiyat")]
        [BsonRequired]
        [Required(ErrorMessage = "Fiyat Alanı Boş Olamaz!")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Fiyat Sıfırdan Büyük Bir Değer Olmalıdır!")]
        public decimal Fiyat { get; set; }

        [BsonElement("stokadedi")]
        [BsonRequired]
        [Required(ErrorMessage = "Stok Adedi Boş olamaz!")]
        [Range(0, int.MaxValue,ErrorMessage ="Stok Adedi Negatif Olamaz!")]
        public int StokAdedi { get; set; }

        public required models_kategori Kategori { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
    }
}