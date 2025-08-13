namespace condogestcet97.web.Services
{
    // Interface for email sending service
    public interface IEmailServices
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
