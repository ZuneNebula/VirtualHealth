using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Models
{
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public String Email { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;

        public String Password { get; set; } = String.Empty;

        public String PhoneNumber { get; set; } = String.Empty;

        public DateTime? DateOfBirth { get; set; } = null;  
    }
}
