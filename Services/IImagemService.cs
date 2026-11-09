namespace Plot.WebAPI.Services
{
    public interface IImagemService
    {
        bool IsConfigured { get; }

        Task<string> UploadImageAsync(IFormFile arquivo, ImageFolder pasta);

        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
