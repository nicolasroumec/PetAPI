namespace PetAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string email, string code);
    }
}
