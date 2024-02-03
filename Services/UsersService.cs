using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using web.Models;

namespace web.Services;

public class UsersService
{
    private readonly IMongoCollection<UserDTO> _usersCollection;

    public UsersService()
    {
        var settings = new UserStoreDatabaseSettings();
        var mongoClient = new MongoClient(settings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<UserDTO>(settings.UsersCollectionName);
    }

    public async Task<List<UserDTO>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<UserDTO?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task<UserDTO?> LoginAsync(string email, string password)
    {
        var account = await _usersCollection.Find(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

        if(account == null || !BC.Verify(password, account.Password))
        {
            return null;
        }

        return account;
    }

    public async Task CreateAsync(UserDTO newUser)
    {
        newUser.Password = BC.HashPassword(newUser.Password);
        await _usersCollection.InsertOneAsync(newUser);
    }

    public async Task UpdateAsync(string id, UserDTO updateUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}
