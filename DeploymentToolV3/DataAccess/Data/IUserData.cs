using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<UserModel> GetUserById(int id);
        Task<IEnumerable<UserModel>> GetUsers(UserModel user);
        Task<UserModel> InsertUser(UserModel user);
    }
}