using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

var filePath = @"C:\Users\enesm\OneDrive\Masaüstü\veggie-master\assets\img\contact-bg.png";

if (File.Exists(filePath))
{
    FileInfo fileInfo = new FileInfo(filePath);
    Image image = Image.FromFile(filePath);
    var result = ResizeImage(image, new Size(250, 250));
    result.Save(fileInfo.Directory.FullName + "\\" +"250x250"+ fileInfo.Name);
    //GenerateThumbnailFromImage(filePath, 250, 250);
}

//void resizeAndCenterImage(string filePath, int width, int height)
//{

//    Image image = Image.FromFile(filePath);

//    //step 1 - making cube
//    if (image.Width < image.Height && width < height)
//    {

//    }

//}

Image ScaleImage(Image image, int maxWidth, int maxHeight)
{
    var ratioX = (double)maxWidth / image.Width;
    var ratioY = (double)maxHeight / image.Height;
    var ratio = Math.Min(ratioX, ratioY);

    var newWidth = (int)(image.Width * ratio);
    var newHeight = (int)(image.Height * ratio);

    var newImage = new Bitmap(maxWidth, maxHeight);
    using (var graphics = Graphics.FromImage(newImage))
    {
        // Calculate x and y which center the image
        int y = (maxHeight / 2) - newHeight / 2;
        int x = (maxWidth / 2) - newWidth / 2;

        // Draw image on x and y with newWidth and newHeight
        graphics.DrawImage(image, x, y, newWidth, newHeight);
    }

    return newImage;
}

static Image ResizeImage(Image imgToResize, Size destinationSize)
{
    var originalWidth = imgToResize.Width;
    var originalHeight = imgToResize.Height;

    //how many units are there to make the original length
    var hRatio = (float)originalHeight / destinationSize.Height;
    var wRatio = (float)originalWidth / destinationSize.Width;

    //get the shorter side
    var ratio = Math.Min(hRatio, wRatio);

    var hScale = Convert.ToInt32(destinationSize.Height * ratio);
    var wScale = Convert.ToInt32(destinationSize.Width * ratio);

    //start cropping from the center
    var startX = (originalWidth - wScale) / 2;
    var startY = (originalHeight - hScale) / 2;

    //crop the image from the specified location and size
    var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

    //the future size of the image
    var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

    //fill-in the whole bitmap
    var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

    //generate the new image
    using (var g = Graphics.FromImage(bitmap))
    {
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
    }

    return bitmap;

}


string GenerateThumbnailFromImage(string imageFilePath, int thumbWidth, int thumbHeight)
{
    try
    {
        //Check if file exist
        if (File.Exists(imageFilePath))
        {
            //bool preserveAspectRatio = true;
            string oldFilePath = imageFilePath;
            string folderPath = Path.GetDirectoryName(imageFilePath);
            string filename = Path.GetFileNameWithoutExtension(imageFilePath);

            //Rename file with thumbnail size
            filename = filename + "_" + thumbWidth.ToString() + Path.GetExtension(imageFilePath);
            imageFilePath = Path.Combine(folderPath, filename);


            using (Image image = Image.FromFile(oldFilePath))
            {
                decimal originalWidth = image.Width;
                decimal originalHeight = image.Height;
                decimal requiredThumbWidth = thumbWidth;
                decimal requiredThumbHeight = thumbHeight;
                decimal startXPosition = 0;
                decimal startYPosition = 0;
                decimal screenWidth = originalWidth;
                decimal screenHeight = originalHeight;
                decimal ar = thumbWidth < thumbHeight
                                 ? originalWidth / originalHeight
                                 : originalHeight / originalWidth;

                //Define Starting Position for thumbnail generation
                if (originalWidth > originalHeight)
                    startXPosition = (originalWidth - originalHeight) / 2;
                else if (originalHeight > originalWidth)
                    startYPosition = (originalHeight - originalWidth) / 2;

                if (thumbWidth > thumbHeight)
                {
                    requiredThumbWidth = thumbWidth;
                    requiredThumbHeight = requiredThumbWidth * ar;
                }
                else if (thumbHeight > thumbWidth)
                {
                    requiredThumbHeight = thumbHeight;
                    requiredThumbWidth = requiredThumbHeight * ar;
                }
                else
                {
                    requiredThumbWidth = thumbWidth;
                    requiredThumbHeight = thumbWidth;
                }

                using (var bmp = new Bitmap((int)requiredThumbWidth, (int)requiredThumbHeight))
                {
                    Graphics gr = Graphics.FromImage(bmp);
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.CompositingQuality = CompositingQuality.HighQuality;
                    gr.InterpolationMode = InterpolationMode.High;
                    var rectDestination = new Rectangle(0, 0, (int)requiredThumbWidth, (int)requiredThumbHeight);

                    gr.DrawImage(image, rectDestination, (int)startXPosition, (int)startYPosition, (int)screenWidth, (int)screenHeight, GraphicsUnit.Pixel);
                    bmp.Save(imageFilePath);

                    return filename;
                }
            }
        }
        return null;
    }
    catch (Exception ex)
    {
       
        throw ex;
    }
    finally
    {

    }
}