namespace DataAccess.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<T> ExecuteScalar<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T, U, TOutput>(string storedProcedure, U parameters, TOutput outputParameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T, U>(string stroredProcedure, U parameters, string connectionId = "Default");
        Task<(TModel, List<int>, List<int>)> LoadDataWithLists<TModel>(string storedProcedure, object parameters, string connectionId = "Default");
        Task SaveData<T, TOutput>(string storedProcedure, T parameters, TOutput outputParameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}