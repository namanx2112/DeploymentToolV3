using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Models.BrandModel;

namespace DataAccess.Data
{
    public class BrandData : IBrandData
    {
        private readonly ISQLDataAccess _db;
        public BrandData(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<BrandModel>> GetBrands(BrandModel brand)
        {
            return await _db.LoadData<BrandModel, dynamic>(stroredProcedure: "", new { });
        }

        public async Task<BrandModel> GetBrandById(int BrandId, string BrandName, int UserID)
        {
            var inputParams = new
            {
                nBrandID = BrandId,
                tBrandName = BrandName,
                nUserID = UserID,
                nPageSize = 10,
                nPageNumber = 1
            };
            var result = await _db.LoadDataWithLists<BrandModel>("sprocBrandGet", inputParams);
            BrandModel model = result.Item1;
            return model;
        }
        public async Task<BrandModel> InsertBrand(BrandModel brand)
        {
            var inputParams = new
            {
                tBrandName = brand.tBrandName,
                tBrandDescription = brand.tBrandDescription,
                tBrandWebsite = brand.tBrandWebsite,
                tBrandCountry = brand.tBrandCountry,
                tBrandEstablished = brand.tBrandEstablished,
                tBrandCategory = brand.tBrandCategory,
                tBrandContact = brand.tBrandContact,
                nBrandLogoAttachmentID = brand.nBrandLogoAttachmentID,
                nUserID = brand.nUserID
            };
            var outputParameters = new { nBrandID = 0 };
            var result = await _db.SaveData("sprocBrandCreate", inputParams, outputParameters);
            if (result.TryGetValue("nBrandID", out object value))
            {
                brand.tBrandId = value != null && int.TryParse(value.ToString(), out int val) ? val : 0;
            }

            return brand;

        }
        public async Task<BrandModel> UpdateBrand(BrandModel brand)
        {
            var inputParams = new
            {
                nBrandId = brand.tBrandId,
                tBrandName = brand.tBrandName,
                tBrandDescription = brand.tBrandDescription,
                tBrandWebsite = brand.tBrandWebsite,
                tBrandCountry = brand.tBrandCountry,
                tBrandEstablished = brand.tBrandEstablished,
                tBrandCategory = brand.tBrandCategory,
                tBrandContact = brand.tBrandContact,
                nBrandLogoAttachmentID = brand.nBrandLogoAttachmentID,
                nUserId = 1

            };
            var outputParameters = new { isSuccess = 0 };
            var result = await _db.SaveData("sprocBrandUpdate", inputParams, outputParameters);
            return brand;

        }
        public async Task DeleteBrand(int BrandID)
        {

            await _db.SaveData("SprocBrandDelete", new { nBrandId = BrandID, nUserID = 1 }, new { isSuccess = 0 });
        }
    }
}
