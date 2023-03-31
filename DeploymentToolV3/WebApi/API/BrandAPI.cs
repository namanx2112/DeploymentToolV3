using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class BrandAPI
    {
        public static void ConfigureBrandController(this WebApplication app)
        {
            app.MapGet(pattern: "/Brands", GetBrandById);
            app.MapPost(pattern: "/Brands", InsertBrand);
            app.MapPut(pattern: "/Brands", UpdateBrand);
            app.MapDelete(pattern: "/Brands", DeleteBrand);
        }
        private static async Task<IResult> GetBrandById(int BrandId, string BrandName, int UserID, IBrandData data)
        {
            try
            {
                return Results.Ok(await data.GetBrandById(BrandId,BrandName,UserID));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> InsertBrand(BrandModel brand, IBrandData data)
        {
            try
            {
                return Results.Ok(await data.InsertBrand(brand));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> UpdateBrand(BrandModel brand, IBrandData data)
        {
            try
            {
                return Results.Ok(await data.UpdateBrand(brand));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteBrand(int BrandId, IBrandData data)
        {
            try
            {
                await data.DeleteBrand(BrandId);
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
