using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Models.ProjectNoteModel;

namespace DataAccess.Data
{
    public class ProjectNoteData : IProjectNoteData
    {
        private readonly ISQLDataAccess _db;

        public ProjectNoteData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<ProjectNoteModel> InsertProjectNote(ProjectNoteModel Projectnote)
        {
            var inputParams = new
            {

                nLoggedInUserID = Projectnote.nUserID,
                nProjectID = Projectnote.nProjectID,
                nStoreID = Projectnote.nStoreID,
                nNoteType = Projectnote.nNoteType,
                tSource = Projectnote.tSource,
                tNoteDesc = Projectnote.tNoteDescription,

            };
            var outputParameters = new { nNoteID = 0, bIsSuccess = 0, tErrorDescription = "" };
            var result = await _db.SaveData("sproc_CreateNote", inputParams, outputParameters);
            if (result.TryGetValue("nNoteID", out object value))
            {
                Projectnote.aNoteID = value != null && int.TryParse(value.ToString(), out int val) ? val : 0;
            }

            return Projectnote;

        }
        public async Task<ProjectNoteModel> UpdateProjectNote(ProjectNoteModel Projectnote)
        {
            var inputParams = new
            {
                nNoteID = Projectnote.aNoteID,
                nLoggedInUserID = Projectnote.nUserID,
                nProjectID = Projectnote.nProjectID,
                nStoreID = Projectnote.nStoreID,
                nNoteType = Projectnote.nNoteType,
                tSource = Projectnote.tSource,
                tNoteDesc = Projectnote.tNoteDescription,

            };
            var outputParameters = new { bIsSuccess = 0, tErrorDescription = "" };
            var result = await _db.SaveData("sproc_UpdateNote", inputParams, outputParameters);

            return Projectnote;

        }
        public async Task<ProjectNoteModel> DeleteProjectNote(ProjectNoteModel Projectnote)
        {
            var inputParams = new
            {
                nNoteID = Projectnote.aNoteID,
                nLoggedInUserID = Projectnote.nUserID
            };
            var outputParameters = new { bIsSuccess = 0, tErrorDescription = "" };
            var result = await _db.SaveData("sproc_DeleteNote", inputParams, outputParameters);
            return Projectnote;
        }
        public async Task<ProjectNoteModel> GetProjectNote(ProjectNoteModel Projectnote)
        {
            var inputParams = new
            {
                nStoreID = Projectnote.nStoreID,
                nProjectID = Projectnote.nProjectID,
                nLoggedInUserID = Projectnote.nUserID
            };
            var outputParameters = new { bIsSuccess = 0, tErrorDescription = "" };
            var result = await _db.LoadDataWithLists<ProjectNoteModel>("sproc_GetNotes", inputParams);
            ProjectNoteModel model = result.Item1;
            return model;
        }
    }
}
