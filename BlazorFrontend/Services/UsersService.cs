using Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BlazorFrontend.Services
{
    public class UsersService : IUserActions
    {

        HttpClient _client;

        public UsersService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress =
                new Uri("https://test.com");
            // TODO Change this to the appropriate value
        }

        public virtual async Task<bool> CreateUser(User user)
        {
            string asJson = JsonSerializer.Serialize<User>(user);
            StringContent content = new StringContent(asJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://example.com", content);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            return true;
        }

        public Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(int userId, User newData)
        {
            throw new NotImplementedException();
        }
    }
}
