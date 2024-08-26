using LaptopCenter.Constraints;

namespace LaptopCenter.Services
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile, EFileType fileType);

        bool DeleteImage(string imageFileName, EFileType fileType);
    }
}
