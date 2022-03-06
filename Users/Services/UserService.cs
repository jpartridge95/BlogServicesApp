using Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Users.Services
{
    public class UserService : IUserActions
    {
        HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress 
                = new Uri(Environment.GetEnvironmentVariable("DATA_BASE_URL"));
        }

        public virtual async Task<bool> CreateUser(User user)
        {
            string asJson = JsonSerializer.Serialize<User>(user);
            StringContent content = new(asJson, Encoding.UTF8, "application/json");

            string query = "create";

            HttpResponseMessage response = await _client.PostAsync(query, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteUser(int userId)
        {
            string query = String.Format("byid?userid={0}", userId);

            HttpResponseMessage response = await _client.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<List<User>> GetAllUsers()
        {
            string query = "all";

            try
            {
                List<User> response 
                    = await _client.GetFromJsonAsync<List<User>>(query);

                if (response.Count == 0)
                {
                    return null;
                }

                return response;
            }
            catch
            {
                return null;
            }

        }

        public virtual async Task<User> GetByEmail(string email)
        {
            string query = String.Format("byemail?email={0}", email);

            try
            {
                var response = await _client.GetFromJsonAsync<User>(query);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<User> GetByID(int id)
        {
            string query = String.Format("byid?id={0}", id);

            try
            {
                var response = await _client.GetFromJsonAsync<User>(query);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<bool> UpdateUser(int userId, User newData)
        {
            string asJson = JsonSerializer.Serialize<User>(newData);
            StringContent content = new(asJson, Encoding.UTF8, "application/json");

            string query = String.Format("edit?userId={0}", userId);

            var response = await _client.PutAsJsonAsync(query, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
