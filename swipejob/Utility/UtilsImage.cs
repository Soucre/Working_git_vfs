using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace SwipeJob.Utility
{
    public static class UtilsImage
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static void SquareCropAndResize(string inFile, Size newSize, string outFile)
        {
            using (Bitmap src = (Bitmap)Image.FromFile(inFile))
            {
                Rectangle cropRect = new Rectangle();
                if (src.Width > src.Height)
                {
                    cropRect.X = (src.Width - src.Height) / 2;
                    cropRect.Y = 0;
                    cropRect.Width = src.Height;
                    cropRect.Height = src.Height;
                }
                else
                {
                    cropRect.X = 0;
                    cropRect.Y = (src.Height - src.Width) / 2;
                    cropRect.Width = src.Width;
                    cropRect.Height = src.Width;
                }

                Rectangle resizeRect = new Rectangle(0, 0, newSize.Width, newSize.Height);
                using (Bitmap target = new Bitmap(resizeRect.Width, resizeRect.Height))
                {
                    target.SetResolution(src.HorizontalResolution, src.VerticalResolution);
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.CompositingMode = CompositingMode.SourceCopy;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            g.DrawImage(src, resizeRect, cropRect.X, cropRect.Y, cropRect.Width, cropRect.Height, GraphicsUnit.Pixel, wrapMode);
                        }

                        g.Dispose();
                    }

                    SaveToJpg(outFile, target, 90L);
                    target.Dispose();
                    src.Dispose();
                }
            }
        }

        public static void ConvertJpgAndResize720(string input, string output)
        {
            using (Image img = Image.FromFile(input))
            {
                const int resizeWidth = 720;
                if (img.Width > resizeWidth)
                {
                    int height = (int) (img.Height*((float) 720/(float) img.Width));
                    var destRect = new Rectangle(0, 0, resizeWidth, height);
                    using (Bitmap destImage = new Bitmap(resizeWidth, height))
                    {
                        destImage.SetResolution(img.HorizontalResolution, img.VerticalResolution);
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
                                graphics.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel,
                                    wrapMode);
                            }
                        }

                        SaveToJpg(output, destImage, 70L);
                    }
                }
                else
                {
                    SaveToJpg(output, img, 70L);
                }
            }
        }

        public static bool IsPortraitImage(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) req.GetResponse();
                Stream stream = response.GetResponseStream();
                Image img = Image.FromStream(stream);
                stream.Close();
                return img.Width < img.Height;
            }
            catch
            {
                return true;
            }
        }

        public static byte[] CircleImage(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (Image image = Image.FromStream(stream))
                {
                    int radius = image.Width;
                    if (image.Height < image.Width)
                    {
                        radius = image.Height;
                    }

                    using (Image dstImage = new Bitmap(radius, radius, image.PixelFormat))
                    {
                        using (Graphics g = Graphics.FromImage(dstImage))
                        {
                            using (Brush br = new SolidBrush(Color.White))
                            {
                                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                            }

                            g.CompositingMode = CompositingMode.SourceCopy;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            using (var wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                                GraphicsPath path = new GraphicsPath();
                                path.AddEllipse(0, 0, radius, radius);
                                g.SetClip(path);
                                g.DrawImage(image, new Rectangle(0, 0, radius, radius), 0, 0, radius, radius, GraphicsUnit.Pixel, wrapMode);
                            }

                            return SaveToStreamJpg(dstImage, 90L);
                        }
                    }
                }
            }
        }

        public static void SaveToJpg(string output, Image img, long quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Jpeg);
            EncoderParameters encoderParams = new EncoderParameters(1) {Param = {[0] = qualityParam}};
            img.Save(output, jpegCodec, encoderParams);
            img.Dispose();
        }

        private static byte[] SaveToStreamJpg(Image img, long quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Png);
            EncoderParameters encoderParams = new EncoderParameters(1) {Param = {[0] = qualityParam}};
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, jpegCodec, encoderParams);
                img.Dispose();
                return stream.ToArray();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        
    }
}
