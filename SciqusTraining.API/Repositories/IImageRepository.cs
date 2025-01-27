using SciqusTraining.API.Models.Domains;

namespace SciqusTraining.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image); 
    }
}
