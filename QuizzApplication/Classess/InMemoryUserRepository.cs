using QuizzApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class InMemoryUserRepository : IRepository<User>
    {
        private List<User> _storage = new List<User>();

        public void Add(User user)
        {
            if (GetByEmail(user.Email) == null)
            {
                user.UserId = _storage.Count > 0 ? _storage.Max(u => u.UserId) + 1 : 1;
                _storage.Add(user);
            }
            else
            {
                throw new InvalidOperationException("User with the same email already exists.");
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);

            if (user != null)
            {
                _storage.Remove(user);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _storage;
        }

        public User GetById(int id)
        {
            return _storage.FirstOrDefault(u => u.UserId == id);
        }

        public User GetByEmail(string email)
        {
            return _storage.FirstOrDefault(u => u.Email == email);
        }

        public void Update(User user)
        {
            var existingUser = GetById(user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
            }
        }
    }
}
