namespace PetAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string email, string code);
    }
}
