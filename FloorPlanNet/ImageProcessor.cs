using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanNet
{
    public static class ImageProcessor
    {
        public static Bitmap MakeGreyScale(Image image)
        {
            Bitmap newImage = new Bitmap(image);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    var pixel = newImage.GetPixel(x, y);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    newImage.SetPixel(x, y, Color.FromArgb(average, average, average));
                }
            }
            return newImage;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap ChangeWhite(Image image)
        {
            Bitmap newImage = new Bitmap(image);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    var pixel = newImage.GetPixel(x, y);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    if (average > 250)
                    {
                        average = 125;
                        newImage.SetPixel(x, y, Color.FromArgb(average, average, average));
                    }
                }
            }
            return newImage;
        }

        public static Bitmap Flip(Image image)
        {
            Bitmap newImage = new Bitmap(image);
            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    var pixelTop = newImage.GetPixel(x, y);
                    var pixelInvert = newImage.GetPixel(newImage.Width - 1 - x, newImage.Height - 1 - y);
                    newImage.SetPixel(x, y, Color.FromArgb(pixelInvert.A, pixelInvert.R, pixelInvert.G, pixelInvert.B));
                    newImage.SetPixel(newImage.Width - 1 - x, newImage.Height - 1 - y, Color.FromArgb(pixelTop.A, pixelTop.R, pixelTop.G, pixelTop.B));
                    //newImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    //newImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
            return newImage;
        }

        public static double[] Normalize(Bitmap image)
        {
            var smallImage = ResizeImage(image, 28, 28);
            var greyImage = MakeGreyScale(smallImage);

            double[] imagepixelsFlattened = new double[28 * 28];
            int count = 0;
            for (int i = 0; i < greyImage.Width; i++)
            {
                for (int j = 0; j < greyImage.Height; j++)
                {
                    imagepixelsFlattened[count++] = (greyImage.GetPixel(i, j).R / 255.0);
                }
            }
            return imagepixelsFlattened;
        }
    }
}
