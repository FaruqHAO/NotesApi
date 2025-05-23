using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesApi.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // Nullable

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        [BsonElement("userId")]
        public string UserId { get; set; } = string.Empty; // 👈 Add this
    }
    public class CreateNoteDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    // Models/ApplicationUser.cs

    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;
    }
    // Models/RegisterModel.cs
    public class RegisterModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Models/LoginModel.cs
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
