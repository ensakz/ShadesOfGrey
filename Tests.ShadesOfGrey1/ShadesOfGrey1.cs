using System.Drawing;
using ShadesOfGrey;
//check
namespace Tests.ShadesOfGrey1
{
    [TestClass]
    public class ImageProcessorTests
    {
        [TestMethod]
        public void ConvertToGrayscale_GivenColoredImage_ReturnsGrayscaleImage()
        {
            // ARRANGE
            ImageProcessor ip = new ImageProcessor();
            Bitmap coloredImage = new Bitmap(2, 2);
            coloredImage.SetPixel(0, 0, Color.Red);
            coloredImage.SetPixel(0, 1, Color.Green);
            coloredImage.SetPixel(1, 0, Color.Blue);
            coloredImage.SetPixel(1, 1, Color.Yellow);

            // ACT
            Bitmap grayscaleImage = ip.ConvertToGrayscale(coloredImage);

            // ASSERT
            for (int i = 0; i < grayscaleImage.Width; i++)
            {
                for (int j = 0; j < grayscaleImage.Height; j++)
                {
                    Color pixelColor = grayscaleImage.GetPixel(i, j);
                    Assert.AreEqual(pixelColor.R, pixelColor.G);
                    Assert.AreEqual(pixelColor.G, pixelColor.B);
                }
            }
        }
    }
}
