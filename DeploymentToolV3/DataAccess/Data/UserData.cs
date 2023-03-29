using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<IEnumerable<UserModel>> GetUsers(UserModel user)
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

        public async Task<UserModel> InsertUser(UserModel user)
        {
            var inputParams = new
            {

                tName = user.tUserName
                ,
                tUserName = user.tUserName
                ,
                tPassword = GenerateRandomPassword()
                ,
                tEmail = user.tEmail
                ,
                tMobile = user.tMobile
                ,
                nCreatedBy = 1
                ,
                nDepartment = user.nDepartment
                ,
                nRole = user.nRole
                ,
                tEmpID = user.tEmpID
                ,
                udfUserBrandRel = ConvertListToDataTable(user.nBrandID, "nBrandID")
                ,
                udfUserFunctionRel = ConvertListToDataTable(user.nFunctionID, "nFunctionID")
        };
            var outputParameters = new { nUserID = 0 };
            var result = await _db.SaveData("SPROC_AddUser", inputParams, outputParameters);
            if (result.TryGetValue("nUserID", out object value))
            {
                user.aUserID = value != null && int.TryParse(value.ToString(), out int val) ? val : 0;
            }
            
            return user;

        }
        private static string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,./<>?"; // All valid characters for the password
            int passwordLength = 8; 
            int requiredCharsCount = 4; 

            StringBuilder password = new StringBuilder();


            password.Append(RandomChar(validChars, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            password.Append(RandomChar(validChars, "abcdefghijklmnopqrstuvwxyz"));
            password.Append(RandomChar(validChars, "1234567890")); 
            password.Append(RandomChar(validChars, "!@#$%^&*()_+-=[]{}|;:,./<>?"));


            for (int i = 0; i < passwordLength - requiredCharsCount; i++)
            {
                password.Append(RandomChar(validChars));
            }


            string shuffledPassword = new string(password.ToString().OrderBy(x => Guid.NewGuid()).ToArray());

            return shuffledPassword;
        }


        private static char RandomChar(string validChars, string requiredChars = "")
        {
            string chars = requiredChars + validChars;
            int randomIndex = new Random().Next(0, chars.Length - 1);
            return chars[randomIndex];
        }

        private static DataTable ConvertListToDataTable(List<int> list, string columnName)
        {
            // create a new DataTable
            DataTable dataTable = new DataTable();

            // add the specified column to the DataTable
            dataTable.Columns.Add(columnName);

            // add the values from the list to the DataTable
            foreach (int value in list)
            {
                dataTable.Rows.Add(value);
            }

            return dataTable;
        }




    }
}
