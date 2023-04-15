using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IProjectNoteData
    {
        Task<ProjectNoteModel> DeleteProjectNote(ProjectNoteModel Projectnote);
        Task<ProjectNoteModel> GetProjectNote(ProjectNoteModel Projectnote);
        Task<ProjectNoteModel> InsertProjectNote(ProjectNoteModel Projectnote);
        Task<ProjectNoteModel> UpdateProjectNote(ProjectNoteModel Projectnote);
    }
}