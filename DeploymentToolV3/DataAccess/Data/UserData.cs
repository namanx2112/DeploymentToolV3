using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{



    public class UserData : IUserData
    {
        private readonly ISQLDataAccess _db;

        public UserData(ISQLDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers()
        {
            return _db.LoadData<UserModel, dynamic>(stroredProcedure: "", new { });
        }

        public async Task<UserModel?> GetUserById(int id)
        {
            var results = await _db.LoadData<UserModel, dynamic>(stroredProcedure: "", new { Id = id });
            return results.FirstOrDefault();
        }

        public async Task InsertUser(UserModel user)
        {
            await _db.SaveData(storedProcedure: "", user);
        }
    }
}
