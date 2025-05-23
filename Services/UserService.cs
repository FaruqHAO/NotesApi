using NotesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


public class UserService
{
    private readonly IMongoCollection<AppUser> _users;

    public UserService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<AppUser>("Users");
    }

    public async Task<AppUser?> GetByUsernameAsync(string username) =>
        await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

    public async Task CreateAsync(AppUser user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        await _users.InsertOneAsync(user);
    }

    public async Task<AppUser?> ValidateUserAsync(string username, string password)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;
        return user;
    }
}
