using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class UserAPI
    {
        public static void ConfigureUserController(this WebApplication app)
        {
            app.MapGet(pattern: "/Users", GetUserById);
            app.MapPost(pattern: "/Users", InsertUser);
        }
        private static async Task<IResult> GetUserById(int id, IUserData data)
        {
            try
            {
                return Results.Ok(await data.GetUserById(id));

            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> InsertUser(UserModel user, IUserData data)
        {
            try
            {
                return Results.Ok(await data.InsertUser(user));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
