using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using static Dapper.SqlMapper;
using static DataAccess.Models.UserModel;

namespace DataAccess.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;

        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<T>> LoadData<T, U>(
        string stroredProcedure,
        U parameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(stroredProcedure, parameters, commandType: CommandType.StoredProcedure);

        }

        public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        //var parameters = new { FirstName = "John", LastName = "Doe", Suffix="Mr" };
        //var outputParameters = new { UserId = 0 };
        //await _db.SaveData("InsertUser", parameters, outputParameters);
        //Console.WriteLine($"Inserted user with id {outputParameters.UserId}");

        public async Task<Dictionary<string, object>> SaveData<T, TOutput>(
        string storedProcedure,
        T parameters,
        TOutput outputParameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var dynamicParameters = new DynamicParameters(parameters);

            foreach (var property in typeof(TOutput).GetProperties())
            {
                dynamicParameters.Add(property.Name, direction: ParameterDirection.Output, size: property.ToString().Length);
            }

            await connection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
            Dictionary<string, object> outparam = new Dictionary<string, object>();
            foreach (var property in typeof(TOutput).GetProperties())
            {
                var value = dynamicParameters.Get<dynamic>(property.Name);
                outparam.Add(property.Name, value);
                // property.SetValue(outputParameters, value);
            }

            return outparam;
        }


        //var inputParams = new { Param1 = "value1", Param2 = 2 };
        //var outputParams = new { OutputParam1 = string.Empty, OutputParam2 = 0 };
        //var results = await LoadData<MyModel, dynamic, dynamic>(
        //    "MyStoredProcedure", inputParams, outputParams, "MyConnection");

        public async Task<IEnumerable<T>> LoadData<T, U, TOutput>(
        string storedProcedure,
        U parameters,
        TOutput outputParameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var dynamicParameters = new DynamicParameters(parameters);

            if (outputParameters != null)
            {
                foreach (var property in typeof(TOutput).GetProperties())
                {

                    dynamicParameters.Add(property.Name, direction: ParameterDirection.Output);
                }
            }

            var result = await connection.QueryAsync<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
            if (outputParameters != null)
            {
                foreach (var property in typeof(TOutput).GetProperties())
                {
                    var value = dynamicParameters.Get<dynamic>(property.Name);
                    property.SetValue(outputParameters, value);
                }
            }

            return result;
        }
        //var result = await ExecuteScalar<int, DynamicParameters>("GetCount", new DynamicParameters(new { param1 = "value1", param2 = "value2" }));

        public async Task<T> ExecuteScalar<T, U>(
        string storedProcedure,
        U parameters,

        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var result = await connection.ExecuteScalarAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }


        public async Task<(TModel, List<int>, List<int>)> LoadDataWithLists<TModel>(
        string storedProcedure,
        object parameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var dynamicParameters = new DynamicParameters(parameters);

            using var multi = await connection.QueryMultipleAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            var model = await multi.ReadSingleOrDefaultAsync<TModel>();
            var list1 = (await multi.ReadAsync<int>()).ToList();
            var list2 = (await multi.ReadAsync<int>()).ToList();

            return (model, list1, list2);
        }










    }


}
