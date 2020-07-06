using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_design_patterns_Question_11_Memento
{
    static class Statics
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        public static int generateRandomNumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }




        public static void drawBorder<T>(this T drawableObject, int borderWidth, Color bordercolor) where T : class
        {            
            int width = 0; int height = 0;
            if (drawableObject is Image) { width = (drawableObject as Image).Width; height = (drawableObject as Image).Height; }
            if (drawableObject is Control) { width = (drawableObject as Control).Width; height = (drawableObject as Control).Height; }

            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphicsObj = Graphics.FromImage(bitmap);

            Pen myPen = new Pen(bordercolor, borderWidth);
            graphicsObj.DrawRectangle(myPen, 0, 0, width - 1, height - 1);

            if (drawableObject is Image) drawableObject = bitmap as T;
            else
            {
                drawableObject.GetType().GetProperty("BackgroundImage")?.SetValue(drawableObject, bitmap);
                drawableObject.GetType().GetProperty("Image")?.SetValue(drawableObject, bitmap);
            }
            graphicsObj.Dispose();
        }

        public static Color colorFromString(string colorString)
        {
            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            KnownColor[] allColors = new KnownColor[colorsArray.Length];
            Array.Copy(colorsArray, allColors, colorsArray.Length);

            Color colorFromString = new Color();
            for (int i = 0; i < allColors.Length; i++)
            {
                bool cont = false;
                int j;
                for (j = 0; j < allColors.Length; j++)
                {
                    if (colorString.Contains(allColors[j].ToString())) { cont = true; break; }
                }

                if (cont) { colorFromString = Color.FromKnownColor(allColors[j]); }
                else
                {
                    colorString = colorString.Replace(" ", string.Empty);
                    string A = colorString.Substring(colorString.IndexOf("=") + 1, colorString.IndexOf(",") - colorString.IndexOf("=") - 1);
                    string R = colorString.Substring(colorString.IndexOf("=", colorString.IndexOf("=") + 1) + 1, colorString.IndexOf(",", colorString.IndexOf(",") + 1) - colorString.IndexOf("=", colorString.IndexOf("=") + 1) - 1);
                    string G = colorString.Substring(colorString.LastIndexOf("=", colorString.LastIndexOf("=") - 1) + 1, colorString.LastIndexOf(",") - colorString.LastIndexOf("=", colorString.LastIndexOf("=") - 1) - 1);
                    string B = colorString.Substring(colorString.LastIndexOf("=") + 1, colorString.LastIndexOf("]") - colorString.LastIndexOf("=") - 1);

                    colorFromString = Color.FromArgb(Convert.ToInt32(A), Convert.ToInt32(R), Convert.ToInt32(G), Convert.ToInt32(B));
                }

                //Color [A=255, R=26, G=179, B=95]
            }
            return colorFromString;

        }



        public static List<DataType> ShuffleList<DataType>(List<DataType> inputList)
        {
            List<DataType> randomList = new List<DataType>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count - 1); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        static public string GetUniqueKeyOriginal_BIASED(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }


        public static Color RandomColor()
        {
            int r = Statics.generateRandomNumberBetween(0, 255);
            int g = Statics.generateRandomNumberBetween(0, 255);
            int b = Statics.generateRandomNumberBetween(0, 255);
            return Color.FromArgb(r, g, b);
        }



        #region Drawing Circle graphics extention

        public static void DrawCircle(this Graphics g, Pen pen, float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush, float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,  radius + radius, radius + radius);
        }

        #endregion

    }
}
