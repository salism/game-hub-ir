using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Features.Identity.Models;

public class RefreshToken
{
    [BsonId]
    public string? Token { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }
}