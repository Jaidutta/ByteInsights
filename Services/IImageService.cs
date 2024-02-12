using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ByteInsights.Services
{
    public interface IImageService
    {
        // IFormfile is something that a user selects via input type file
        // It could be an image for the registered user, an image in a blog, etc
        // This is going to be used when someone is interacting with input type file
        Task<byte[]> EncodeImageAsync(IFormFile file);

        Task<byte[]> EncodeImageAsync(string fileName); // represent a static image for a user 
                                                        // For example default image for a user, default image for a logo, etc

        // This second overload is used for encoding images that are already stored in my project
        // we will be referring to that a path


        // The purpose of this method is to display the acutal image
        string DecodeImage(byte[] data, string type); // byte[] of the image and string for the type


        string ContentType(IFormFile file); // For the content type of the image


        int Size(IFormFile file);  // records the size of the image
    }
}
