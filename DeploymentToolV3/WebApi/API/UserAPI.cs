using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class UserAPI
    {
        public static void ConfigureUserController(this WebApplication app)
        {
            app.MapGet(pattern: "/Users", GetUserById);
            app.MapPost(pattern: "/Users", InsertUser);
            app.MapPut(pattern: "/Users", UpdateUser);
            app.MapDelete(pattern: "/Users", DeleteUser);
            
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
        private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
        {
            try
            {
                return Results.Ok(await data.UpdateUser(user));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteUser(int userID, IUserData data)
        {
            try
            {
                await data.DeleteUser(userID);
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }



    }
}
