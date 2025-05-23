using MongoDB.Driver;
using NotesApi.Models;
using Microsoft.Extensions.Options;

namespace NotesApi.Services
{
    public class NotesService
    {
        private readonly IMongoCollection<Note> _notesCollection;

        public NotesService(IOptions<MongoDBSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _notesCollection = mongoDatabase.GetCollection<Note>(settings.Value.NotesCollectionName);
        }

        public async Task<List<Note>> GetAsync() =>
            await _notesCollection.Find(_ => true).ToListAsync();
        public async Task<List<Note>> GetByUserIdAsync(string userId)
        {
            return await _notesCollection.Find(note => note.UserId == userId).ToListAsync();
        }

        public async Task<Note?> GetAsync(string id) =>
            await _notesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Note note) =>
            await _notesCollection.InsertOneAsync(note);

        public async Task UpdateAsync(string id, Note note) =>
            await _notesCollection.ReplaceOneAsync(x => x.Id == id, note);

        public async Task DeleteAsync(string id) =>
            await _notesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
