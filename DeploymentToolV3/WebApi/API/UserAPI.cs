using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class UserAPI
    {
        public static void ConfigureUserController(this WebApplication app)
        {
            app.MapGet(pattern: "/Users", GetUserById);
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

    }
}
