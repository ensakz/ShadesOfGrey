using System;
using System.Drawing;
using System.IO;

namespace ShadesOfGrey
{
    public class ImageProcessor
    {
        public Bitmap ConvertToGrayscale(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);
            for (int j = 0; j < input.Height; j++)
                for (int i = 0; i < input.Width; i++)
                {
                    UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                    float R = (float)((pixel & 0x00FF0000) >> 16);
                    float G = (float)((pixel & 0x0000FF00) >> 8);
                    float B = (float)(pixel & 0x000000FF);
                    R = G = B = (R + G + B) / 3.0f;
                    UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                    output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                }
            return output;
        }

        public void SaveImage(Bitmap image, string path)
        {
            if (image != null)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        image.Save(path);
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Impossible to save the file", ex);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Path cannot be null or empty.");
                }
            }
            else
            {
                throw new ArgumentNullException("Image cannot be null.");
            }
        }
    }
}