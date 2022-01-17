using Data.Context;
using Microsoft.Extensions.Logging;
using Models;
using System.Collections.Generic;

namespace Data.DataAccess
{
    public class UserAccess : IUserActions
    {

        public SocialContext _context;

        public UserAccess(SocialContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> DeleteUser(int userId)
        {
            try
            {
                User user = await _context.Users.FindAsync(userId);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<List<User>> GetAllUsers()
        {
            List<User> users = _context.Users.ToList();

            if (users.Count == 0)
            {
                return null;
            }

            return users;
        }

        public virtual async Task<User> GetByID(int id)
        {
            
            User user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public virtual async Task<bool> UpdateUser(int userId, User newData)
        {
            if (newData == null)
            {
                return false;
            }

            User oldData = await _context.Users.FindAsync(userId);

            if (oldData == null)
            {
                return false;
            }

            oldData.Firstname = newData.Firstname;
            oldData.Lastname = newData.Lastname;
            oldData.Email = newData.Email;
            oldData.Password = newData.Password;
            oldData.Username = newData.Username;

            _context.SaveChanges();

            return true;
        }

        public virtual async Task<User> GetByEmail (string email)
        {
            User user = _context.Users.AsQueryable()
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
