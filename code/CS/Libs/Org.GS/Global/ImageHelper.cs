using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace Org.GS
{
    public class ImageHelper
    {
        private static Encryptor encryptor = new Encryptor();

        public static XElement GetPifFromImage(Image image)
        {
            /*      PatientLink Image Form (PIF) Processing  
             *      
             *      This method translates a bitonal (1-bit depth) image (such as used in the PatientLink Scanner) from a binary image to 
             *      an XElement which contains an encrypted string representing an efficiently packaged data format which can be used to 
             *      reconstitute the image.
             *      
             *      Only black pixels are recorded.  All other pixels are assumed to be white.  An "entry" is made in the "PIF containing
             *      the beginning X coordinate and beginning Y coordinate of a region of black pixels.  The region also has a length, which 
             *      indicates the number of contiguous black pixels found.  
             * 
             *      As the image is processed, when a black pixel is located, its x,y position is recorded (in startX and startY respective) 
             *      and the length of the black region is set to 1.  If another black pixel is encountered next, the length is simply incremented 
             *      to 2 and so forth.
             *      
             *      When a white pixel is encountered, thus bringing the black region to an end, the three integer values (startX, startY and length)
             *      are composed together to form a 32-bit value (12 bits from startX, plus 12 bits from startY, plus 8 bits from length) which is 
             *      converted to a 32-bit integer.  The 32-bit integer is then converted to a byte array with a length of 4 bytes which is then
             *      written to a memory stream.
             *      
             *      When the image processing is finished, the memory stream is read back out as a string which is then encrypted and formatted as
             *      a base-64 which is suitable for inclusion in the XElement (and thus for communication in web service calls).
             * 
             */


            Bitmap img = (Bitmap)image;
            MemoryStream ms = new MemoryStream();

            bool inBlack = false;
            int startX = 0;
            int startY = 0;
            int length = 0;

            int w = img.Width;
            int h = img.Height;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color c = img.GetPixel(x, y);
                    int totalRGB = c.R + c.G + c.B;

                    switch (totalRGB)
                    {
                        case 0:
                            if (!inBlack)
                            {
                                startX = x;
                                startY = y;
                                length = 1;
                                inBlack = true;
                            }
                            else
                            {
                                if (length == 255)
                                {
                                    ms.Write(GetPifBytes(startX, startY, length), 0, 4);

                                    startX = x;
                                    startY = y;
                                    length = 1;
                                }
                                else
                                {
                                    length++;
                                }
                            }

                            break;

                        case 765:   // white pixel
                            if (inBlack)
                                ms.Write(GetPifBytes(startX, startY, length), 0, 4);

                            inBlack = false;
                            break;

                        default:
                            img.Dispose();
                            ms.Dispose();
                            throw new Exception("Invalid pixel color found in bitmap: R=" + c.R.ToString() + " G=" + c.G.ToString() + " B=" + c.B.ToString() +
                                                " at image offset x=" + x.ToString() + " y=" + y.ToString() + ".");
                    }

                }
            }

            if (inBlack)
                ms.Write(GetPifBytes(startX, startY, length), 0, 4);

            ms.Write(GetPifBytes(4095, 4095, 255), 0, 4);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);

            string encryptedString = encryptor.EncryptByteArray(ms.GetBuffer());

            XElement pif = new XElement("PIF");
            pif.Add(new XAttribute("Height", h.ToString()));
            pif.Add(new XAttribute("Width", w.ToString()));
            pif.Value = encryptedString;

            img.Dispose();
            ms.Dispose();

            return pif;
        }

        private static byte[] GetPifBytes(int startX, int startY, int length)
        {
            short xShort = (short)startX;
            short yShort = (short)startY;
            string xString = g.PadWithLeadingZeros(Convert.ToString(xShort, 2), 12);
            string yString = g.PadWithLeadingZeros(Convert.ToString(yShort, 2), 12);
            string lthString = g.PadWithLeadingZeros(Convert.ToString(length, 2), 8);
            string valueString = xString + yString + lthString;
            int value = Convert.ToInt32(valueString, 2);
            byte[] bytes = BitConverter.GetBytes(value);

            return bytes;
        }

        public static Image GetImageFromPif(XElement pif)
        {
            int h = Int32.Parse(pif.Attribute("Height").Value);
            int w = Int32.Parse(pif.Attribute("Width").Value);

            Bitmap indexedImage = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
            indexedImage.SetResolution(203.2F, 203.2F);
            Bitmap img2 = CreateNonIndexedImage(indexedImage);
            Bitmap img = ClearImage(img2);

            string encryptedString = pif.Value;
            byte[] rawBytes = encryptor.DecryptByteArray(encryptedString);

            MemoryStream ms = new MemoryStream(rawBytes);

            ms.Position = 0;
            byte[] bytes = new byte[4];

            while (ms.Read(bytes, 0, 4) == 4)
            {
                int value = BitConverter.ToInt32(bytes, 0);
                string valueString = g.PadWithLeadingZeros(Convert.ToString(value, 2), 32);
                string xString = g.PadWithLeadingZeros(valueString.Substring(0, 12), 32);
                string yString = g.PadWithLeadingZeros(valueString.Substring(12, 12), 32);
                string lthString = g.PadWithLeadingZeros(valueString.Substring(24, 8), 32);

                int x = Convert.ToInt32(xString, 2);
                int y = Convert.ToInt32(yString, 2);
                int length = Convert.ToInt32(lthString, 2);

                if (x == 4095 && y == 4095 && length == 255)
                    break;

                int actualX = x;
                int actualY = y;

                for (int i = 0; i < length; i++)
                {
                    img.SetPixel(actualX, actualY, Color.Black);
                    actualX++;
                    if (actualX > img.Width - 1)
                    {
                        actualX = 0;
                        actualY++;
                    }
                }
            }

            ms.Close();
            ms.Dispose();

            return img;
        }

        private static Bitmap CreateNonIndexedImage(Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.DrawImage(src, 0, 0);
            }

            return newBmp;
        }

        public static Bitmap ClearImage(Image original)
        {
            original = new Bitmap(original);

            Color c = Color.White;

            //get a graphics object from the new image 
            Graphics g = Graphics.FromImage(original);

            //create the ColorMatrix 
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]{ 
                    new float[] {0, 0, 0, 0, 0}, 
                    new float[] {0, 0, 0, 0, 0}, 
                    new float[] {0, 0, 0, 0, 0}, 
                    new float[] {0, 0, 0, 1, 0}, 
                    new float[] {c.R / 255.0f, 
                                 c.G / 255.0f, 
                                 c.B / 255.0f, 
                                 0, 1} 
                });

            //create some image attributes 
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute 
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image 
            //using the color matrix 
            g.DrawImage(original,
                new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height,
                GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object 
            g.Dispose();

            //return a bitmap 
            return (Bitmap)original;
        } 
    }
}