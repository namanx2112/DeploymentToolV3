using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Models.UserModel;

namespace DataAccess.Data
{



    public class UserData : IUserData
    {
        private readonly ISQLDataAccess _db;

        public UserData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.LoadData<UserModel, dynamic>(stroredProcedure: "", new { });
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var parameters = new { nUserID = id };
            var result = await _db.LoadDataWithLists<UserModel>("sproc_GetUserDetails", new { nUserID = id });
            UserModel model = result.Item1;
            model.nBrandID = result.Item2;
            model.nFunctionID = result.Item3;
            return model;
        }

        public async Task InsertUser(UserModel user)
        {
            await _db.SaveData(storedProcedure: "", user);
        }
    }
}
