﻿using System;
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

        public static float[] Normalize(Bitmap image)
        {
            var greyImage = MakeGreyScale(image);
            var smallImage = ResizeImage(greyImage, 28, 28);

            float[] imagepixelsFlattened = new float[28 * 28];
            for (int i = 0; i < smallImage.Width; i++)
            {
                for (int j = 0; j < smallImage.Height; j++)
                {
                    imagepixelsFlattened[i + j] = (float)(smallImage.GetPixel(i, j).R / 255.0);
                }
            }
            return imagepixelsFlattened;
        }
    }
}
