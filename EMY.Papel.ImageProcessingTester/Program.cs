using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


long number = 34*34*34*34;
string result = DecimalToArbitrarySystem(number, 34);
Console.WriteLine(result);



/// <summary>
/// Converts the given decimal number to the numeral system with the
/// specified radix (in the range [2, 36]).
/// </summary>
/// <param name="decimalNumber">The number to convert.</param>
/// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
/// <returns></returns>
static string DecimalToArbitrarySystem(long decimalNumber, int radix)
{
    const int BitsInLong = 64;
    const string Digits = "T5AWIQ3Z912BF0EGYX674VKL8DORSUHJPN";

    if (radix < 2 || radix > Digits.Length)
        throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

    if (decimalNumber == 0)
        return "0";

    int index = BitsInLong - 1;
    long currentNumber = Math.Abs(decimalNumber);
    char[] charArray = new char[BitsInLong];

    while (currentNumber != 0)
    {
        int remainder = (int)(currentNumber % radix);
        charArray[index--] = Digits[remainder];
        currentNumber = currentNumber / radix;
    }

    string result = new String(charArray, index + 1, BitsInLong - index - 1);
    if (decimalNumber < 0)
    {
        result = "-" + result;
    }

    return result;
}


























//var filePath = @"C:\Users\enesm\OneDrive\Masaüstü\veggie-master\assets\img\contact-bg.png";

//if (File.Exists(filePath))
//{
//    FileInfo fileInfo = new FileInfo(filePath);
//    Image image = Image.FromFile(filePath);
//    var result = ResizeImage(image, new Size(250, 250));
//    result.Save(fileInfo.Directory.FullName + "\\" +"250x250"+ fileInfo.Name);
//    //GenerateThumbnailFromImage(filePath, 250, 250);
//}

////void resizeAndCenterImage(string filePath, int width, int height)
////{

////    Image image = Image.FromFile(filePath);

////    //step 1 - making cube
////    if (image.Width < image.Height && width < height)
////    {

////    }

////}

//Image ScaleImage(Image image, int maxWidth, int maxHeight)
//{
//    var ratioX = (double)maxWidth / image.Width;
//    var ratioY = (double)maxHeight / image.Height;
//    var ratio = Math.Min(ratioX, ratioY);

//    var newWidth = (int)(image.Width * ratio);
//    var newHeight = (int)(image.Height * ratio);

//    var newImage = new Bitmap(maxWidth, maxHeight);
//    using (var graphics = Graphics.FromImage(newImage))
//    {
//        // Calculate x and y which center the image
//        int y = (maxHeight / 2) - newHeight / 2;
//        int x = (maxWidth / 2) - newWidth / 2;

//        // Draw image on x and y with newWidth and newHeight
//        graphics.DrawImage(image, x, y, newWidth, newHeight);
//    }

//    return newImage;
//}

//static Image ResizeImage(Image imgToResize, Size destinationSize)
//{
//    var originalWidth = imgToResize.Width;
//    var originalHeight = imgToResize.Height;

//    //how many units are there to make the original length
//    var hRatio = (float)originalHeight / destinationSize.Height;
//    var wRatio = (float)originalWidth / destinationSize.Width;

//    //get the shorter side
//    var ratio = Math.Min(hRatio, wRatio);

//    var hScale = Convert.ToInt32(destinationSize.Height * ratio);
//    var wScale = Convert.ToInt32(destinationSize.Width * ratio);

//    //start cropping from the center
//    var startX = (originalWidth - wScale) / 2;
//    var startY = (originalHeight - hScale) / 2;

//    //crop the image from the specified location and size
//    var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

//    //the future size of the image
//    var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

//    //fill-in the whole bitmap
//    var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

//    //generate the new image
//    using (var g = Graphics.FromImage(bitmap))
//    {
//        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//        g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
//    }

//    return bitmap;

//}


//string GenerateThumbnailFromImage(string imageFilePath, int thumbWidth, int thumbHeight)
//{
//    try
//    {
//        //Check if file exist
//        if (File.Exists(imageFilePath))
//        {
//            //bool preserveAspectRatio = true;
//            string oldFilePath = imageFilePath;
//            string folderPath = Path.GetDirectoryName(imageFilePath);
//            string filename = Path.GetFileNameWithoutExtension(imageFilePath);

//            //Rename file with thumbnail size
//            filename = filename + "_" + thumbWidth.ToString() + Path.GetExtension(imageFilePath);
//            imageFilePath = Path.Combine(folderPath, filename);


//            using (Image image = Image.FromFile(oldFilePath))
//            {
//                decimal originalWidth = image.Width;
//                decimal originalHeight = image.Height;
//                decimal requiredThumbWidth = thumbWidth;
//                decimal requiredThumbHeight = thumbHeight;
//                decimal startXPosition = 0;
//                decimal startYPosition = 0;
//                decimal screenWidth = originalWidth;
//                decimal screenHeight = originalHeight;
//                decimal ar = thumbWidth < thumbHeight
//                                 ? originalWidth / originalHeight
//                                 : originalHeight / originalWidth;

//                //Define Starting Position for thumbnail generation
//                if (originalWidth > originalHeight)
//                    startXPosition = (originalWidth - originalHeight) / 2;
//                else if (originalHeight > originalWidth)
//                    startYPosition = (originalHeight - originalWidth) / 2;

//                if (thumbWidth > thumbHeight)
//                {
//                    requiredThumbWidth = thumbWidth;
//                    requiredThumbHeight = requiredThumbWidth * ar;
//                }
//                else if (thumbHeight > thumbWidth)
//                {
//                    requiredThumbHeight = thumbHeight;
//                    requiredThumbWidth = requiredThumbHeight * ar;
//                }
//                else
//                {
//                    requiredThumbWidth = thumbWidth;
//                    requiredThumbHeight = thumbWidth;
//                }

//                using (var bmp = new Bitmap((int)requiredThumbWidth, (int)requiredThumbHeight))
//                {
//                    Graphics gr = Graphics.FromImage(bmp);
//                    gr.SmoothingMode = SmoothingMode.HighQuality;
//                    gr.CompositingQuality = CompositingQuality.HighQuality;
//                    gr.InterpolationMode = InterpolationMode.High;
//                    var rectDestination = new Rectangle(0, 0, (int)requiredThumbWidth, (int)requiredThumbHeight);

//                    gr.DrawImage(image, rectDestination, (int)startXPosition, (int)startYPosition, (int)screenWidth, (int)screenHeight, GraphicsUnit.Pixel);
//                    bmp.Save(imageFilePath);

//                    return filename;
//                }
//            }
//        }
//        return null;
//    }
//    catch (Exception ex)
//    {

//        throw ex;
//    }
//    finally
//    {

//    }
//}