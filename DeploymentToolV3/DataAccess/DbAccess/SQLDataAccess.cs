using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using static Dapper.SqlMapper;

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
       //var parameters = new { FirstName = "John", LastName = "Doe" };
       //var outputParameters = new { UserId = 0 };
       //await _db.SaveData("InsertUser", parameters, outputParameters);
       //Console.WriteLine($"Inserted user with id {outputParameters.UserId}");

        public async Task SaveData<T, TOutput>(
        string storedProcedure,
        T parameters,
        TOutput outputParameters,
        string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var dynamicParameters = new DynamicParameters(parameters);

            foreach (var property in typeof(TOutput).GetProperties())
            {
                dynamicParameters.Add(property.Name, direction: ParameterDirection.Output);
            }

            await connection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            foreach (var property in typeof(TOutput).GetProperties())
            {
                var value = dynamicParameters.Get<dynamic>(property.Name);
                property.SetValue(outputParameters, value);
            }
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

            foreach (var property in typeof(TOutput).GetProperties())
            {

                dynamicParameters.Add(property.Name, direction: ParameterDirection.Output);
            }

            var result = await connection.QueryAsync<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            foreach (var property in typeof(TOutput).GetProperties())
            {
                var value = dynamicParameters.Get<dynamic>(property.Name);
                property.SetValue(outputParameters, value);
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







    }


}
