using System.Net.Mail;
using System.Net;
using WebApi.HelperServices;

namespace WebApi.API
{
    public static class EmailSender
    {
        public static void ConfigureEmailSenderController(this WebApplication app)
        {            
            app.MapPost(pattern: "/SendEmail", SendEmail);            
        }
        private static async Task<IResult> SendEmail(EmailModel em, IEmailService es)
        {
            try
            {
                await es.SendEmailAsync(em.ToEmail, em.Subject, em.Body);
                return Results.Ok(new {EmailSent = true});

            }
            catch (Exception ex)
            { 
                return Results.Problem(ex.Message); 
            }
            
        }
    }
}
