using web.Models;

namespace web.Repositories;

public static class UserRepository
{
    public static User? AuthenticateUser(string email, string password)
    {
        var users = new List<User>();
        users.Add(new User {Id = 1, Email = "test1@email.com", Password = "batman", FullName = "Test 1"});
        users.Add(new User {Id = 2, Email = "test2@email.com", Password = "batman", FullName = "Test 2"});
        return users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).FirstOrDefault();
    }
}
