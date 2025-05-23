using AspNetCore.Identity.Mongo.Model;

namespace NotesApi.Models
{
    public class ApplicationUser : MongoUser
    {
        public string FullName { get; set; } = string.Empty;
    }
   
}
