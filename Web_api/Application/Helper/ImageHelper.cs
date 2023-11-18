using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Application.Helper
{
    public static class ImageHelper
    {
        public static async Task<MemoryStream> ResizeImage(Stream stream, int maxWidth, int maxHeight)
        {

            using (var image = await Image.LoadAsync(stream))
            {
                var newWidth = maxWidth;
                var newHeight = (int)(image.Height * maxWidth) / image.Width;

                if (image.Width < maxWidth)
                {
                    newWidth = image.Width;
                    newHeight = image.Height;
                }

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(newWidth, newHeight),
                    Mode = ResizeMode.Max
                }));
                var outputStream = new MemoryStream();
                image.Save(outputStream, new JpegEncoder { Quality = 80 });
                return outputStream;
            }
        }

        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image")) return true;

            string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));

        }
    }
}
