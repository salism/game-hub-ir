using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Features.Identity.Models
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] string? Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool EmailConfirmed { get; set; } = false;

        public bool IsActive { get; set; } = true;

    }
}