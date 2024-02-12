using Org.BouncyCastle.Tls;

namespace ByteInsights.Services
{
    public class BasicImageService : IImageService
    {
        public string ContentType(IFormFile file)
        {
            return file?.ContentType; 
            /* if there is a contentType it will just return it
             * Otherwise it will return null 
             */

        }

        // This is for data coming out of the database
        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null) return null;

            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        // data going into the database
        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null) return null;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray(); // to statisfy the byte[] return type

        }

        // This overload is for a situation where we are dealing with a path
        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{fileName}";

            return await File.ReadAllBytesAsync(fileName);
        }

        int IImageService.Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);

        }
    }
}
