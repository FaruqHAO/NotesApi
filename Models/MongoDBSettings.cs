namespace NotesApi.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotesCollectionName { get; set; } = null!;
    }
}
