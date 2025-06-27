using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAccountBook.Models
{
    public class TransactionDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [BsonElement("date")]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [BsonElement("category")]
        [JsonPropertyName("category")]
        public string Category { get; set; } = null!;

        [BsonElement("item")]
        [JsonPropertyName("item")]
        public string Item { get; set; } = null!;

        [BsonElement("amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [BsonElement("note")]
        [JsonPropertyName("note")]
        public string Note { get; set; } = null!;
    };
}