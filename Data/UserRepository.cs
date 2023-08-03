using DotNetApi.Models;

namespace DotNetApi.Data {
    public class UserRepository : IUserRepository
    {
        DataContextEF _dataContextEF;
        public UserRepository(IConfiguration configuration)
        {
            _dataContextEF = new DataContextEF(configuration);
        }

        public bool SaveChanges()
        {
            return (_dataContextEF.SaveChanges() >= 0);
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null) {
                _dataContextEF.Add(entityToAdd);
            }
        }
        public void RemoveEntity<T>(T entityToDelete)
        {
            if (entityToDelete != null) {
                _dataContextEF.Remove(entityToDelete);
            }
        }
         public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _dataContextEF.Users.ToList<User>();

            return users;
        }
    }
}