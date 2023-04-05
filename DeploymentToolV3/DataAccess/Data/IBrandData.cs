using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IBrandData
    {
        Task DeleteBrand(int BrandID);
        Task<BrandModel> GetBrandById(int BrandId, string BrandName, int UserID);
        Task<IEnumerable<BrandModel>> GetBrands(BrandModel brand);
        Task<BrandModel> InsertBrand(BrandModel brand);
        Task<BrandModel> UpdateBrand(BrandModel brand);
    }
}