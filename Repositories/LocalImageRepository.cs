using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccesor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
                                    IHttpContextAccessor httpContextAccesor,
                                    NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccesor = httpContextAccesor;
            this.dbContext = dbContext;
        }

        
        //Uploading image chosen from local device.
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                                              $"{ image.FileName}{ image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //Path for image.
            // https://localhost:7002/images/image.jpg
            var urlFilePath = $"{httpContextAccesor.HttpContext.Request.Scheme}://{httpContextAccesor.HttpContext.Request.Host}{httpContextAccesor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        
            image.FilePath= urlFilePath;

            //Add the image top the Image Table in Databasxe
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        
        }
    }
}
