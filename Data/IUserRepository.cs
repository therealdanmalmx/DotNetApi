using DotNetApi.Models;

namespace DotNetApi.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToDelete);
        public IEnumerable<User> GetUsers();

    }
}