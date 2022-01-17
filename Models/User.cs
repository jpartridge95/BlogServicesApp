namespace Models
{
    public interface IUserActions
    {
        // Read Actions
        Task<User> GetByID(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetByEmail(string email);

        // Create actions
        Task<bool> CreateUser(User user);

        // Delete actions
        Task<bool> DeleteUser(int userId);

        // Update actions
        Task<bool> UpdateUser(int userId, User newData);
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}