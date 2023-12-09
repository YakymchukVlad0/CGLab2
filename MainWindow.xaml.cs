using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CGLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class SelectionHistory
    {
        // Represents the selected color in the history
        public string SelectedColor { get; set; }

        // Represents the zoom value in the history
        public double ZoomValue { get; set; }
    }











    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        // List to store the selection history
        private List<SelectionHistory> history = new List<SelectionHistory>();

        // Index to keep track of the current position in the history
        private int historyIndex = -1;

        // Maximum number of iterations for fractal rendering
        private int maxIterations;

        // Array of available fractal types
        public string[] fractalTypes { get; set; }

        // Selected color property with OnPropertyChanged notification
        private string selectedColor;
        public string SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    OnPropertyChanged("selectedColor");
                }
            }
        }

        // Selected fractal type property with OnPropertyChanged notification
        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged("selectedType");
                }
            }
        }

        // WriteableBitmap for rendering the fractal
        WriteableBitmap bitmap = new WriteableBitmap(387, 250, 96, 96, PixelFormats.Bgr32, null);



        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize available fractal types
            fractalTypes = new string[]
            {
            "Algebraic(z*sin(z))",
            "Algebraic(cos(z)*sin(z))",
            "Algebraic(z*cos(z))",
            "Geometric"
            };

            // Set default values for SelectedColor and zoomSlider
            SelectedColor = "Black";
            zoomSlider.ValueChanged += zoomSlider_ValueChanged;
            cbFractals.SelectionChanged += cbFractal_SelectionChanged;

            // Add the initial selection to the history
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
            maxIterations = 256;
        }




        // Event handler for fractal type selection change
        private void cbFractal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFractals.SelectedItem != null)
            {
                SelectedType = cbFractals.SelectedItem.ToString();
            }
        }

        // Event for property change notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Event handlers for color selection buttons
        private void btnBlack_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Black";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;

        }

        // Event handlers for color selection buttons
        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Blue";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handlers for color selection buttons
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Red";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }


        // Event handlers for color selection buttons
        private void btnPurple_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Purple";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }
        // Event handlers for color selection buttons
        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Green";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handlers for color selection buttons
        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Yellow";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handlers for color selection buttons
        private void btnPink_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Pink";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handlers for color selection buttons
        private void btnBrown_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Brown";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handlers for color selection buttons
        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = "Orange";
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            //adding to history
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }




        // Method to set pixel on concrete position and color on bitmap
        public static void SetPixel(WriteableBitmap bitmap, int x, int y, Color color)
        {
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            int[] pixels = new int[1] { (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B };
            bitmap.WritePixels(rect, pixels, 4, 0);
        }




        //method for async fractal rendering
        private async Task RenderFractal()
        {
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;
           

            double zoomFactor = zoomSlider.Value;

           
            double newRange = 4.0 / zoomFactor;

            double xMin = -2.0 - newRange / 2.0;
            double xMax = 2.0 + newRange / 2.0;
            double yMin = -2.0 - newRange / 2.0;
            double yMax = 2.0 + newRange / 2.0;
            int maxIterations = 256;

            
            myCanvas.Children.Clear();

            for (int x = 0; x < canvasWidth; x++)
            {
                for (int y = 0; y < canvasHeight; y++)
                {
                    
                    double a = xMin + (x / canvasWidth) * (xMax - xMin);
                    double b = yMin + (y / canvasHeight) * (yMax - yMin);
                    Complex z = new Complex(a, b);

                    int iterations = 0;
                    Complex c = z;

                    while (Complex.Abs(z) < 2.0 && iterations < maxIterations)
                    {
                        z = z * Complex.Sin(z);
                        iterations++;
                    }

                    
                    Color color = ComputeColor(iterations,maxIterations);

                    
                    Rectangle rect = new Rectangle
                    {
                        Width = 1,
                        Height = 1,
                        Fill = new SolidColorBrush(color)
                    };
                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                    myCanvas.Children.Add(rect);

                    await Task.Delay(1);
                }
            }
        }



        //method to save fractal as image
        private void SaveFractalImage(WriteableBitmap bitmap, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }
        }



        //method to go to another window by clicking a textblock
        private void TextBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            ColorSchemesWindow colorSchemesWindow = new ColorSchemesWindow();
            this.Close();
            colorSchemesWindow.ShowDialog(); // Use Show() for a non-blocking window


          
        }
        //method to go to another window by clicking a textblock
        private void TextBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Moving_images moveImagesWindow = new Moving_images();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }
        //method to go to another window by clicking a textblock
        private void TextBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Ed_materials1 moveImagesWindow = new Ed_materials1();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


        }

        //method to go to another window by clicking a textblock
        private void TextBlock4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Ed_materials2 moveImagesWindow = new Ed_materials2();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


           
        }

        //method to go to another window by clicking a textblock
        private void TextBlock5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            WindowMain moveImagesWindow = new WindowMain();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }




        // Event handler for the "Apply" button click
        private async void btnApply_Click(object sender, RoutedEventArgs e)
        {
            // Set the canvas background color to white
            myCanvas.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");

            // Get the actual width and height of the canvas
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;

            // Get the current zoom factor from the slider
            double zoomFactor = zoomSlider.Value;

            // Calculate the new range based on the zoom factor
            double newRange = 40.0 / zoomFactor;

            // Set the boundaries for the fractal rendering
            double xMin = -2.0 - newRange;
            double xMax = 2.0 + newRange;
            double yMin = -2.0 - newRange;
            double yMax = 2.0 + newRange;

            // Set the maximum number of iterations for the fractal algorithm
            int maxIterations = 256;

            // Check the selected fractal type and apply the corresponding rendering logic
            if (cbFractals.SelectedItem.ToString() == "Algebraic(z*sin(z))")
            {

                 for (int x = 0; x < canvasWidth; x++)
                 {
                     for (int y = 0; y < canvasHeight; y++)
                     {
                         
                         double a = xMin + (x / canvasWidth) * (xMax - xMin);
                         double b = yMin + (y / canvasHeight) * (yMax - yMin);
                         Complex z = new Complex(a, b);
                         Complex c = z;

                         int iterations = 0;

                         while (Complex.Abs(z) < 2.0 && iterations < maxIterations)
                         {
                             
                             z = Complex.Sin(z) * c;
                             iterations++;
                         }

                         
                         Color color = ComputeColor(iterations, maxIterations);

                         
                         SetPixel(bitmap, x, y, color);
                     }
                 }

                 
                 var image = new Image
                 {
                     Source = bitmap
                 };
                 myCanvas.Children.Add(image);


              //  RenderFractal();
            }


            if (cbFractals.SelectedItem.ToString() == "Algebraic(cos(z)*sin(z))")
            {

                for (int x = 0; x < canvasWidth; x++)
                {
                    for (int y = 0; y < canvasHeight; y++)
                    {
                        
                        double a = xMin + (x / canvasWidth) * (xMax - xMin);
                        double b = yMin + (y / canvasHeight) * (yMax - yMin);
                        Complex z = new Complex(a, b);
                        Complex c = z;

                        int iterations = 0;

                        while (Complex.Abs(z) < 10.0 && iterations < maxIterations)
                        {
                           
                            z = Complex.Sin(z) * Complex.Cos(c);
                            iterations++;
                        }

                        
                        Color color = ComputeColor(iterations, maxIterations);

                        
                        SetPixel(bitmap, x, y, color);
                    }
                }

               
                var image = new Image
                {
                    Source = bitmap
                };
                myCanvas.Children.Add(image);
            }

            if(cbFractals.SelectedItem.ToString()== "Algebraic(z*cos(z))")
            {
                for (int x = 0; x < canvasWidth; x++)
                {
                    for (int y = 0; y < canvasHeight; y++)
                    {
                        
                        double a = xMin + (x / canvasWidth) * (xMax - xMin);
                        double b = yMin + (y / canvasHeight) * (yMax - yMin);
                        Complex z = new Complex(a, b);
                        Complex c = z;
                        
                        int iterations = 0;

                        while (Complex.Abs(z) < 10.0 && iterations < maxIterations)
                        {
                            
                            z = c * Complex.Cos(z);
                            iterations++;
                        }

                        
                        Color color = ComputeColor(iterations, maxIterations);

                        
                        SetPixel(bitmap, x, y, color);
                    }
                }

                
                var image = new Image
                {
                    Source = bitmap
                };
                myCanvas.Children.Add(image);

            }


            if (cbFractals.SelectedItem.ToString() == "Geometric")
            {
                // Clear the canvas and set its background color
                myCanvas.Children.Clear();
                myCanvas.Width = 400;
                myCanvas.Height = 260;
                myCanvas.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#AFA207");

                // Draw a geometric fractal with lines and depth
                Point point1 = new Point(90, 50);
                Point point2 = new Point(290, 50);
                Point point3 = new Point(190, 225);

                DrawFractalLinesWithDepth(myCanvas, point1, point2, 4);
                DrawFractalLinesWithDepth(myCanvas, point2, point3, 4);
                DrawFractalLinesWithDepth(myCanvas, point3, point1, 4);

            }



        }



        // Method to get the opposite color of a given color
        public Color GetOppositeColor(Color color)
        {
            // Calculate opposite RGB values
            byte oppositeR = (byte)(255 - color.R);
            byte oppositeG = (byte)(255 - color.G);
            byte oppositeB = (byte)(255 - color.B);

            // Return the opposite color
            return Color.FromRgb(oppositeR, oppositeG, oppositeB);
        }

        // Method to compute the color based on the number of iterations and maximum iterations
        private Color ComputeColor(int iterations, int maxIterations)
        {
            // Check if the maximum iterations are reached, return black
            if (iterations == maxIterations)
            {
                return Colors.Black;
            }
            //if no calculate a color based on pressed button
            else
            {



                Color baseColor;
                switch (selectedColor)
                {
                    case "Black":
                        baseColor = Colors.Black;
                        break;
                    case "Blue":
                        baseColor = Colors.Blue;
                        break;
                    case "Red":
                        baseColor = Colors.Red;
                        break;
                    case "Brown":
                        baseColor = Colors.Brown;
                        break;
                    case "Green":
                        baseColor = Colors.Green;
                        break;
                    case "Yellow":
                        baseColor = Colors.Yellow;
                        break;
                    case "Orange":
                        baseColor = Colors.Orange;
                        break;
                    case "Pink":
                        baseColor = Colors.Pink;
                        break;
                    case "Purple":
                        baseColor = Colors.Purple;
                        break;

                    default:
                        baseColor = Colors.Black;
                        break;
                }
                
                //calculating color
                Color startColor = baseColor;
                Color endColor = GetOppositeColor(baseColor);

                double ratio = (double)iterations / maxIterations;
                byte red = (byte)(startColor.R + ratio * (endColor.R - startColor.R));
                byte green = (byte)(startColor.G + ratio * (endColor.G - startColor.G));
                byte blue = (byte)(startColor.B + ratio * (endColor.B - startColor.B));

                return Color.FromRgb(red, green, blue);
            }
        }














        // Method to capture the content of a Canvas as a RenderTargetBitmap
        private RenderTargetBitmap CaptureCanvasAsImage(Canvas canvas)
        {
            // Create a new RenderTargetBitmap with the specified dimensions and resolution
            var renderTargetBitmap = new RenderTargetBitmap(
                (int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            // Render the content of the Canvas onto the RenderTargetBitmap
            renderTargetBitmap.Render(canvas);

            // Return the captured RenderTargetBitmap
            return renderTargetBitmap;
        }

        // Method to save a RenderTargetBitmap as a PNG image file
        private void SaveImageAsPng(RenderTargetBitmap renderTargetBitmap, string fileName)
        {
            // Create a PngBitmapEncoder and add the RenderTargetBitmap as a frame
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            // Save the encoded image to the specified file using a FileStream
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }

        // Event handler for the ZoomSlider's ValueChanged event
        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Create a new SelectionHistory object with the current color and zoom values
            var currentSelection = new SelectionHistory
            {
                SelectedColor = SelectedColor,
                ZoomValue = zoomSlider.Value
            };

            // Add the current selection to the history list and update the history index
            history.Add(currentSelection);
            historyIndex = historyIndex + 1;
        }

        // Event handler for the "Download" button click
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            // Check if the selected fractal type is "Geometric"
            if (cbFractals.SelectedItem.ToString() == "Geometric")
            {
                // Adjust canvas size, capture canvas content as an image, and save as a PNG file
                myCanvas.Width = 440;
                myCanvas.Height = 260;
                var canvasImage = CaptureCanvasAsImage(myCanvas);

                string filePath = "C:\\Users\\symbi\\source\\repos\\CGLab2\\FractalImages\\Geometric.png";

                SaveImageAsPng(canvasImage, filePath);
            }
            else
            {
                // Open a SaveFileDialog to allow the user to choose the save location and filename
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png";

                // If the user selects a valid file path, save the fractal image using SaveFractalImage method
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    SaveFractalImage(bitmap, fileName);
                }
            }
        }

        // Event handler for the "Back" button click
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are previous selections in the history
            if (historyIndex > 0)
            {
                // Move back in the history and update the UI with the previous selection
                historyIndex--;
                var previousSelection = history[historyIndex];

                SelectedColor = previousSelection.SelectedColor;
                zoomSlider.Value = previousSelection.ZoomValue;
            }
        }


        public static void DrawFractalLinesWithDepth(Canvas canvas, Point startPoint, Point endPoint, int depth)
        {
            if (depth == 0)
            {
                // Базовий випадок: не малюємо додаткові лінії, просто початкову лінію
                Line line = new Line
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = Brushes.Black
                };
                canvas.Children.Add(line);
            }
            else
            {
                // Розраховуємо середину між початковою та кінцевою точками
                Point midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

                // Розраховуємо довжину відрізка між початковою та кінцевою точками
                double segmentLength = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));

                // Розраховуємо кут нахилу основної лінії
                double angle = Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);

                // Розраховуємо координати для двох ліній під кутами -50 та 50 градусів відносно основної лінії
                double angle45 = angle - Math.PI * 50.0 / 180.0;  // -50 градусів в радіанах
                double angleNeg45 = angle + Math.PI * 50.0 / 180.0;  // 50 градусів в радіанах
                double lineLength = segmentLength / 4.0;  // Довжина ліній

                double line1X1 = midPoint.X + lineLength * Math.Cos(angle45);
                double line1Y1 = midPoint.Y + lineLength * Math.Sin(angle45);
                double line1X2 = midPoint.X;
                double line1Y2 = midPoint.Y;

                double line2X1 = midPoint.X;
                double line2Y1 = midPoint.Y;
                double line2X2 = midPoint.X - lineLength * Math.Cos(angleNeg45);
                double line2Y2 = midPoint.Y - lineLength * Math.Sin(angleNeg45);


                Point L1First = new Point();
                Point L2First = new Point();
                Point L1FSecond = new Point();
                Point L2FSecond = new Point();
                // Малюємо дві лінії під кутами -50 та 50 градусів
                Line line1 = new Line
                {
                    X1 = L1First.X = line1X1,
                    Y1 = L1First.Y = line1Y1,
                    X2 = L1FSecond.X = line1X2,
                    Y2 = L1FSecond.Y = line1Y2,
                    Stroke = Brushes.Black
                };

                Line line2 = new Line
                {
                    X1 = L2First.X = line2X1,
                    Y1 = L2First.Y = line2Y1,
                    X2 = L2FSecond.X = line2X2,
                    Y2 = L2FSecond.Y = line2Y2,
                    Stroke = Brushes.Black
                };

                canvas.Children.Add(line1);
                canvas.Children.Add(line2);


                DrawFractalNewLinesWithDepth(canvas, L1First, L1FSecond, depth - 1);
                DrawFractalNewLinesWithDepth(canvas, L2First, L2FSecond, depth - 1);

                // Рекурсивно викликаємо функцію для кожної з частин
                DrawFractalLinesWithDepth(canvas, startPoint, midPoint, depth - 1);
                DrawFractalLinesWithDepth(canvas, midPoint, endPoint, depth - 1);
            }
        }




        public static void DrawFractalNewLinesWithDepth(Canvas canvas, Point startPoint, Point endPoint, int depth) //ДЛЯ НОВИХ
        {
            if (depth == 0)
            {
                // Базовий випадок: не малюємо додаткові лінії, просто початкову лінію
                Line line = new Line
                {
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = endPoint.X,
                    Y2 = endPoint.Y,
                    Stroke = Brushes.Black
                };
                canvas.Children.Add(line);
            }
            else
            {
                // Розраховуємо середину між початковою та кінцевою точками
                Point midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

                // Розраховуємо довжину відрізка між початковою та кінцевою точками
                double segmentLength = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));

                // Розраховуємо кут нахилу основної лінії
                double angle = Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);

                // Розраховуємо координати для двох ліній під кутами -50 та 50 градусів відносно основної лінії
                double angle45 = angle - Math.PI * 50.0 / 180.0;  // -50 градусів в радіанах
                double angleNeg45 = angle + Math.PI * 50.0 / 180.0;  // 50 градусів в радіанах
                double lineLength = segmentLength / 4.0;  // Довжина ліній

                double line1X1 = midPoint.X + lineLength * Math.Cos(angle45);
                double line1Y1 = midPoint.Y + lineLength * Math.Sin(angle45);
                double line1X2 = midPoint.X - lineLength * Math.Cos(angle45);
                double line1Y2 = midPoint.Y - lineLength * Math.Sin(angle45);

                double line2X1 = midPoint.X + lineLength * Math.Cos(angleNeg45);
                double line2Y1 = midPoint.Y + lineLength * Math.Sin(angleNeg45);
                double line2X2 = midPoint.X - lineLength * Math.Cos(angleNeg45);
                double line2Y2 = midPoint.Y - lineLength * Math.Sin(angleNeg45);


                Point L1First = new Point();
                Point L2First = new Point();
                Point L1FSecond = new Point();
                Point L2FSecond = new Point();
                // Малюємо дві лінії під кутами -50 та 50 градусів
                Line line1 = new Line
                {
                    X1 = L1First.X = line1X1,
                    Y1 = L1First.Y = line1Y1,
                    X2 = L1FSecond.X = line1X2,
                    Y2 = L1FSecond.Y = line1Y2,
                    Stroke = Brushes.Black
                };

                Line line2 = new Line
                {
                    X1 = L2First.X = line2X1,
                    Y1 = L2First.Y = line2Y1,
                    X2 = L2FSecond.X = line2X2,
                    Y2 = L2FSecond.Y = line2Y2,
                    Stroke = Brushes.Black
                };

                canvas.Children.Add(line1);
                canvas.Children.Add(line2);

                DrawFractalNewLinesWithDepth(canvas, L1First, L1FSecond, depth - 1);
                DrawFractalNewLinesWithDepth(canvas, L2First, L2FSecond, depth - 1);

                DrawFractalNewLinesWithDepth(canvas, startPoint, midPoint, depth - 1);
                DrawFractalNewLinesWithDepth(canvas, midPoint, endPoint, depth - 1);
            }
        }

    }
}
