using UserApiProject.Models;

namespace UserApiProject.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public List<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public User Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User updated)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = updated.Name;
            existing.Email = updated.Email;
            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _users.Remove(user);
            return true;
        }
    }
}
