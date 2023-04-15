using System.Runtime.CompilerServices;

namespace WebApi.API
{
    public static class ProjectNoteAPI
    {
        public static void ConfigureProjectNoteController(this WebApplication app)
        {
            app.MapPost(pattern: "/ProjectNotes", GetProjectNote);
           // app.MapPost(pattern: "/ProjectNotes", InsertProjectNote);
            app.MapPut(pattern: "/ProjectNotes", UpdateProjectNote);
           // app.MapDelete(pattern: "/ProjectNotes", DeleteProjectNote);
        }
        private static async Task<IResult> GetProjectNote(ProjectNoteModel Projectnote, IProjectNoteData data)
        {
            try
            {
                return Results.Ok(await data.GetProjectNote(Projectnote));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> InsertProjectNote(ProjectNoteModel Projectnote, IProjectNoteData data)
        {
            try
            {
                return Results.Ok(await data.InsertProjectNote(Projectnote));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> UpdateProjectNote(ProjectNoteModel Projectnote, IProjectNoteData data)
        {
            try
            {
                return Results.Ok(await data.UpdateProjectNote(Projectnote));

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteProjectNote(ProjectNoteModel Projectnote, IProjectNoteData data)
        {
            try
            {
                await data.DeleteProjectNote(Projectnote);
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
