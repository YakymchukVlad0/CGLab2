using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace CGLab2
{
    /// <summary>
    /// Interaction logic for ColorSchemesWindow.xaml
    /// </summary>
    // Partial class for the ColorSchemesWindow, a window for color scheme exploration
 public partial class ColorSchemesWindow : Window
    {
        // URI string for the image file
        String uri;

        // Array of color types for selection
        public string[] colorTypes { get; set; }

        // BitmapImage for displaying the image
        BitmapImage bitmapImage;

        // Image control to display the loaded image
        System.Windows.Controls.Image image;

        // WriteableBitmap for working with the image
        WriteableBitmap myWriteableBitmap;

        // Selected color type property with OnPropertyChanged notification
        private string selectedColorType;
        public string SelectedType
        {
            get { return selectedColorType; }
            set
            {
                if (selectedColorType != value)
                {
                    selectedColorType = value;
                    OnPropertyChanged("selectedColorType");
                }
            }
        }

        // Constructor for the ColorSchemesWindow
        public ColorSchemesWindow()
        {
            InitializeComponent();

            // Set the data context to this instance
            DataContext = this;

            // Initialize available color types
            colorTypes = new string[]
            {
            "RGB",
            "HSV"
            };

            // Set the default selected color type
            selectedColorType = "RGB";

            // Attach mouse left button down event handlers for canvases
            myCanvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            myResCanvas.MouseLeftButtonDown += ResCanvas_MouseLeftButtonDown;
        }

        // Event handler for the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to invoke PropertyChanged event for property changes
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Event handler for mouse left button down on the main canvas
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the mouse position relative to the canvas
            System.Windows.Point position = e.GetPosition(myCanvas);

            // Convert mouse position to integer coordinates
            int x = (int)position.X;
            int y = (int)position.Y;

            // Load the image using the provided URI
            Bitmap bb = new Bitmap(uri);

            // Trace the RGB values of the pixel at the specified position
            Trace.WriteLine(bb.GetPixel(x, y));

            // Check if the position is within the canvas boundaries
            if (x >= 0 && x < myCanvas.ActualWidth && y >= 0 && y < myCanvas.ActualHeight)
            {
                // Create a WriteableBitmap from the BitmapImage
                WriteableBitmap bitmap = new WriteableBitmap(bitmapImage);

                // Get the color of the pixel at the specified position
                System.Windows.Media.Color color = GetPixelColor(bitmap, x, y);

                // Update the text boxes with position and color information based on the selected color type
                tbxX.Text = $"{x}";
                tbxY.Text = $"{y}";

                if (cbColors.SelectedItem.ToString() == "RGB")
                {
                    tbxR.Text = $"{color.B}";
                    tbxG.Text = $"{color.G}";
                    tbxB.Text = $"{color.R}";
                }
                else if (cbColors.SelectedItem.ToString() == "HSV")
                {
                    // Convert RGB to HSV and update the text boxes
                    double hue, saturation, value;
                    RgbToHsv(color.R, color.G, color.B, out hue, out saturation, out value);
                    Trace.WriteLine($"H: {hue}");

                    tbxR.Text = $"{hue}";
                    tbxG.Text = $"{saturation}";
                    tbxB.Text = $"{value}";
                }
            }
        }
    






        //method to go to another window
        private void TextBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            MainWindow colorSchemesWindow = new MainWindow();
            this.Close();
            colorSchemesWindow.ShowDialog(); // Use Show() for a non-blocking window


          
        }



        //method to go to another window
        private void TextBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Moving_images moveImagesWindow = new Moving_images();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }


        //method to go to another window
        private void TextBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Ed_materials1 moveImagesWindow = new Ed_materials1();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }


        //method to go to another window
        private void TextBlock4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Ed_materials2 moveImagesWindow = new Ed_materials2();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


           
        }


        //method to go to another window
        private void TextBlock5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            WindowMain moveImagesWindow = new WindowMain();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


           
        }






        // Event handler for mouse left button down on the result canvas
        private void ResCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the mouse position relative to the result canvas
            System.Windows.Point position = e.GetPosition(myResCanvas);

            // Convert mouse position to integer coordinates
            int x = (int)position.X;
            int y = (int)position.Y;

            // Check if the position is within the boundaries of the main canvas
            if (x >= 0 && x < myCanvas.ActualWidth && y >= 0 && y < myCanvas.ActualHeight)
            {
                // Get the color of the pixel at the specified position from the WriteableBitmap
                System.Windows.Media.Color color = GetPixelColor(myWriteableBitmap, x, y);

                // Update text boxes with position and color information based on the selected color type
                tbxX.Text = $"{x}";
                tbxY.Text = $"{y}";

                if (cbColors.SelectedItem.ToString() == "RGB")
                {
                    tbxR.Text = $"{color.B}";
                    tbxG.Text = $"{color.G}";
                    tbxB.Text = $"{color.R}";
                }
                else if (cbColors.SelectedItem.ToString() == "HSV")
                {
                    // Convert RGB to HSV and update the text boxes
                    double hue, saturation, value;
                    RgbToHsv(color.R, color.G, color.B, out hue, out saturation, out value);

                    tbxR.Text = $"{value}";
                    tbxG.Text = $"{saturation}";
                    tbxB.Text = $"{hue}";
                }
            }
        }

        /* private void AdjustBrightnessAndSaturation(WriteableBitmap bitmap, double brightnessFactor, double saturationFactor)
         {
             int width = bitmap.PixelWidth;
             int height = bitmap.PixelHeight;

             int[] pixels = new int[width * height];
             bitmap.CopyPixels(pixels, width * 4, 0);

             for (int i = 0; i < pixels.Length; i++)
             {
                 int pixel = pixels[i];

                 byte[] argb = BitConverter.GetBytes(pixel);
                 double a = argb[3] / 255.0;
                 double r = argb[2] / 255.0;
                 double g = argb[1] / 255.0;
                 double b = argb[0] / 255.0;

                 // Adjust brightness and saturation
                 r = Math.Pow(r, brightnessFactor) * saturationFactor;
                 g = Math.Pow(g, brightnessFactor);
                 b = Math.Pow(b, brightnessFactor);

                 // Clamp values
                 r = Math.Max(0, Math.Min(1, r));
                 g = Math.Max(0, Math.Min(1, g));
                 b = Math.Max(0, Math.Min(1, b));

                 argb[2] = (byte)(r * 255);
                 argb[1] = (byte)(g * 255);
                 argb[0] = (byte)(b * 255);

                 pixels[i] = BitConverter.ToInt32(argb, 0);
             }

             bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);
         }



         private void btnApply_Click(object sender, RoutedEventArgs e)
         {
             // Отримайте значення зі слайдерів
             double brightnessValue = brightnessSlider.Value;
             double saturationValue = saturationSlider.Value;
             myWriteableBitmap = new WriteableBitmap(bitmapImage);
             // Create a WriteableBitmap based on the original image


             // Apply brightness and saturation adjustments
             AdjustBrightnessAndSaturation(myWriteableBitmap, brightnessValue, saturationValue); // Ви можете налаштувати ці значення

             System.Windows.Controls.Image image2 = new System.Windows.Controls.Image
             {
                 Source = myWriteableBitmap
                // Stretch = Stretch.Fill,
             };

            image2.Width = myResCanvas.Width;
             image2.Height = myResCanvas.Height;

             myResCanvas.Children.Clear();
             myResCanvas.Children.Add(image2);
         }*/

        // Static method to adjust the red saturation and brightness of an image
        public static void AdjustRedSaturationBrightness(BitmapSource source, WriteableBitmap bitmap, double saturationFactor, double brightnessFactor)
        {
            // Get dimensions and stride of the source image
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * 4; // Assuming 32 bits per pixel (4 channels: Red, Green, Blue, Alpha)

            // Array to hold pixel data
            byte[] pixels = new byte[height * stride];

            // Copy pixel data from source to array
            source.CopyPixels(pixels, stride, 0);

            // Loop through each pixel in the array
            for (int i = 0; i < pixels.Length; i += 4)
            {
                // Obtain pixel color values
                byte blue = pixels[i];
                byte green = pixels[i + 1];
                byte red = pixels[i + 2];

                // Convert RGB color to HSV space
                double hue, saturation, brightness;
                System.Windows.Media.Color convertedColor = System.Windows.Media.Color.FromRgb(red, green, blue);
                HsvColor hsvColor = ConvertRgbToHsv(convertedColor);

                // Extract HSV components
                hue = hsvColor.H;
                saturation = hsvColor.S;
                brightness = hsvColor.V;

                // Adjust saturation and brightness for the red color channel
                double newSaturation = (red > green && red > blue) ? saturationFactor * (1 - saturation) : 0;
                saturation += newSaturation;

                double newBrightness = brightnessFactor * (1 - brightness);
                brightness += newBrightness;

                // Convert back to RGB
                convertedColor = ConvertHsvToRgb(hue, saturation, brightness);

                // Update pixel color values in the array
                pixels[i] = convertedColor.B;
                pixels[i + 1] = convertedColor.G;
                pixels[i + 2] = convertedColor.R;
            }

            // Write modified pixel data back to the WriteableBitmap
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
        }

        // Helper method to convert RGB color to HSV
        private static HsvColor ConvertRgbToHsv(System.Windows.Media.Color color)
        {
            // Extract RGB components
            double min = Math.Min(Math.Min(color.R, color.G), color.B);
            double max = Math.Max(Math.Max(color.R, color.G), color.B);
            double delta = max - min;

            // Calculate HSV components
            double hue = 0;
            double saturation = (max == 0) ? 0 : (1d - (min / max));
            double brightness = max / 255d;

            if (delta != 0)
            {
                if (color.R == max)
                    hue = (color.G - color.B) / delta;
                else if (color.G == max)
                    hue = 2 + (color.B - color.R) / delta;
                else if (color.B == max)
                    hue = 4 + (color.R - color.G) / delta;

                hue *= 60;

                if (hue < 0)
                    hue += 360;
            }

            return new HsvColor { H = hue, S = saturation, V = brightness };
        }

        // Helper method to convert HSV color to RGB
        private static System.Windows.Media.Color ConvertHsvToRgb(double hue, double saturation, double brightness)
        {
            // Calculate RGB components from HSV
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            brightness *= 255;
            byte v = Convert.ToByte(brightness);
            byte p = Convert.ToByte(brightness * (1 - saturation));
            byte q = Convert.ToByte(brightness * (1 - f * saturation));
            byte t = Convert.ToByte(brightness * (1 - (1 - f) * saturation));

            // Return the resulting RGB color
            if (hi == 0)
                return System.Windows.Media.Color.FromRgb(v, t, p);
            else if (hi == 1)
                return System.Windows.Media.Color.FromRgb(q, v, p);
            else if (hi == 2)
                return System.Windows.Media.Color.FromRgb(p, v, t);
            else if (hi == 3)
                return System.Windows.Media.Color.FromRgb(p, q, v);
            else if (hi == 4)
                return System.Windows.Media.Color.FromRgb(t, p, v);
            else
                return System.Windows.Media.Color.FromRgb(v, p, q);
        }

        // Helper class to represent HSV color
        private class HsvColor
        {
            public double H { get; set; }
            public double S { get; set; }
            public double V { get; set; }
        }

        //method to apply changes of brightness and saturation
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            // Get values from sliders
            double brightnessValue = brightnessSlider.Value;
            double saturationValue = saturationSlider.Value;

            // Create a WriteableBitmap based on the original image
            myWriteableBitmap = new WriteableBitmap(bitmapImage);
            // Create a WriteableBitmap based on the original image

            // Apply brightness and saturation adjustments
            AdjustRedSaturationBrightness(myWriteableBitmap, myWriteableBitmap, saturationValue, brightnessValue);

            System.Windows.Controls.Image image2 = new System.Windows.Controls.Image
            {
                Source = myWriteableBitmap
                //Stretch = Stretch.Fill,
            };

            image2.Width = myResCanvas.ActualWidth;
            image2.Height = myResCanvas.ActualHeight;

            myResCanvas.Children.Clear();
            myResCanvas.Children.Add(image2);
        }






        // Event handler for the selection change in the color combo box
        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected color type from the combo box
            string selectedColor = cbColors.SelectedItem.ToString();

            // Update text labels based on the selected color type
            switch (selectedColor)
            {
                case "RGB":
                    tblR.Text = "R: ";
                    tblG.Text = "G: ";
                    tblB.Text = "B: ";
                    break;

                case "HSV":
                    tblR.Text = "H: ";
                    tblG.Text = "S: ";
                    tblB.Text = "V: ";
                    break;

                default:
                    tblR.Text = "R: ";
                    tblG.Text = "G: ";
                    tblB.Text = "B: ";
                    break;
            }
        }

        // Convert RGB values to HSV
        public static void RgbToHsv(int red, int green, int blue, out double hue, out double saturation, out double value)
        {
            // Normalize RGB values to the range [0, 1]
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            // Find the maximum and minimum values among R, G, B
            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            // Calculate value (brightness)
            value = max;

            // If the value is 0, set hue and saturation to 0 and return
            if (value == 0)
            {
                hue = 0;
                saturation = 0;
                return;
            }

            // Calculate saturation
            saturation = (max - min) / max;

            // Calculate hue based on the position of the maximum value
            if (max == r)
                hue = (g - b) / (max - min);
            else if (max == g)
                hue = 2.0 + (b - r) / (max - min);
            else
                hue = 4.0 + (r - g) / (max - min);

            // Convert hue to degrees and adjust if necessary
            hue *= 60.0;
            if (hue < 0)
                hue += 360;
        }

        // Get the color of a pixel in a WriteableBitmap
        private System.Windows.Media.Color GetPixelColor(WriteableBitmap bitmap, int x, int y)
        {
            // Check if the pixel coordinates are within the valid range
            if (x < 0 || x >= bitmap.PixelWidth || y < 0 || y >= bitmap.PixelHeight)
            {
                return Colors.Transparent;
            }

            // Calculate the index of the pixel in the array
            int index = y * bitmap.PixelWidth + x;

            // Copy pixel data for the specified pixel
            int[] pixels = new int[1];
            bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, 4, 0);

            // Extract RGB components from the pixel data
            byte blue = (byte)((pixels[0] >> 16) & 0xFF);
            byte green = (byte)((pixels[0] >> 8) & 0xFF);
            byte red = (byte)(pixels[0] & 0xFF);

            // Return the resulting Color
            return System.Windows.Media.Color.FromRgb(red, green, blue);
        }

        // Event handler for the "Upload" button click
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Image",
                Filter = "Image files|*.jpg;*.jpeg;*.gif;",
            };

            // If a file is selected, load the image and display it on the canvas
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                // Update the URI and load the image
                uri = imagePath;
                bitmapImage = new BitmapImage(new Uri(imagePath));
                image = new System.Windows.Controls.Image
                {
                    Source = bitmapImage,
                };

                // Set the image dimensions to match the canvas
                image.Width = myCanvas.ActualWidth;
                image.Height = myCanvas.ActualHeight;

                // Clear existing children and add the new image to the canvas
                myCanvas.Children.Clear();
                myCanvas.Children.Add(image);
            }
        }

        // Save the current image displayed on the canvas as a JPEG file using a file dialog
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to save the image with a file dialog
            SaveImageWithDialog(myWriteableBitmap);
        }

        // Save the given WriteableBitmap as a JPEG file with the specified file name
        private void SaveImageAsJpeg(WriteableBitmap bitmap, string fileName)
        {
            // Open a file stream and create a JPEG encoder
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                // Add the WriteableBitmap as a frame to the encoder
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.QualityLevel = 100; // Set the JPEG quality level (adjust as needed)

                // Save the encoded image to the file stream
                encoder.Save(stream);
            }
        }

        // Open a file dialog to select a destination and save the current image as a JPEG
        private void SaveImageWithDialog(WriteableBitmap bitmap)
        {
            // Create a save file dialog with JPEG file filter
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JPEG Image|*.jpg",
                Title = "Save Image"
            };

            // If the user chooses a file, call the method to save the image
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                SaveImageAsJpeg(bitmap, fileName);
            }
        }
    }
}
