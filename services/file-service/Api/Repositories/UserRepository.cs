using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data; // <-- Это покажет репозиторию, где лежит AppDbContext
using Api.Models; // <-- Скорее всего понадобится для модели User


namespace Api.Repositories
{
    public class UserRepositoryDb : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepositoryDb(AppDbContext db)
        {
            _db = db;
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}