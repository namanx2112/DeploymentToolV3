using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class ConfigureUserAPI
    {
        public static void ConfigureUserController(this WebApplication app)
        {
            app.MapGet(pattern: "/Users", GetUsers);
        }
        private static async Task<IResult> GetUsers(IUserData data)
        {
            try
            {
                return Results.Ok(await data.GetUsers());

            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
