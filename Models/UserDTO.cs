using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web.Models;

public class UserDTO
{

    public UserDTO(User user)
    {
        this.FullName = user.FullName;
        this.Email = user.Email;
        this.Password = user.Password;
        this.Role = user.Role != null ? user.Role : "Guest";
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Guest";
}
