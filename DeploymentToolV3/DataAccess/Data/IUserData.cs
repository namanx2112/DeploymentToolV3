using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<UserModel?> GetUserById(int id);
        Task<IEnumerable<UserModel>> GetUsers();
        Task InsertUser(UserModel user);
    }
}