using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Models
{
    public class Doctor
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public String Email { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty; 

        public String Password { get; set; } = String.Empty;

        public String PhoneNumber { get; set; } = String.Empty;

        public String Specialization { get; set; } = String.Empty;

        public String licenseNo { get; set; } = String.Empty;
    }
}
