using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Moving_images.xaml
    /// </summary>
    public partial class Moving_images : Window
    {


        // Triangle and mirrorTriangle represent the main and mirrored polygons
        private Polygon triangle;
        private Polygon mirrorTriangle;

        // Boolean to determine if mirroring is applied
        bool isMirror = false;

        // Center point of the canvas
        private Point center;

        // Collection to store original points of the main triangle
        PointCollection originalPoints;

        public Moving_images()
        {
            InitializeComponent();
        }

        // Event handler when the Canvas is loaded
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            // Draw the coordinate system on the canvas
            DrawCoordinateSystem(myCanvas);
            moveSlider.ValueChanged += moveSlider_ValueChanged;
            scalingSlider.ValueChanged += UpdateScaling;
        }

        // Method to draw the X and Y axis on the canvas
        private void DrawCoordinateSystem(Canvas canvas)
        {
            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;

            
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = canvasHeight / 2,
                X2 = canvasWidth,
                Y2 = canvasHeight / 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvas.Children.Add(xAxis);

            
            Line yAxis = new Line
            {
                X1 = canvasWidth / 2,
                Y1 = 0,
                X2 = canvasWidth / 2,
                Y2 = canvasHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            canvas.Children.Add(yAxis);

            
            TextBlock xLabel = new TextBlock
            {
                Text = "X",
                Foreground = Brushes.Black,
                
                Margin = new Thickness(canvasWidth - 14, ((canvasHeight / 2) -4) - 10, 0, 0)
            };
            canvas.Children.Add(xLabel);

            TextBlock yLabel = new TextBlock
            {
                Text = "Y",
                Foreground = Brushes.Black,
                Margin = new Thickness(canvasWidth / 2 + 10, 0, 0, 0)
            };
            canvas.Children.Add(yLabel);
        }




        // Event handler when the "Color Schemes" text block is clicked
        private void TextBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open the ColorSchemesWindow and close the current window
            ColorSchemesWindow colorSchemesWindow = new ColorSchemesWindow();
            this.Close();
            colorSchemesWindow.ShowDialog();
        }

        // Event handler when the moveSlider value changes
        private void moveSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Move the triangle based on the slider value
            double newX = moveSlider.Value;

            // Apply translation to the triangle
            if (isMirror)
            {
                Matrix matrix = new Matrix(1, 0, 0, 1, newX, -newX);
                triangle.RenderTransform = new MatrixTransform(matrix);
                mirrorTriangle.RenderTransform = new MatrixTransform(matrix);
            }
            else
            {
                Matrix matrix = new Matrix(1, 0, 0, 1, newX, -newX);
                triangle.RenderTransform = new MatrixTransform(matrix);
            }
        }

        // Event handler when the scalingSlider value changes
        private void UpdateScaling(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Update the scale of the triangle based on the slider value
            if (triangle != null)
            {
                double scaleValue = scalingSlider.Value;

                // Apply scaling to the triangle
                ScaleTransform scaleTransform = new ScaleTransform(scaleValue, scaleValue, center.X, center.Y);
                triangle.RenderTransform = scaleTransform;

                // Apply scaling to the mirrored triangle if mirroring is enabled
                if (isMirror)
                {
                    ScaleTransform mirrorScaleTransform = new ScaleTransform(scaleValue, scaleValue, center.X, center.Y);
                    mirrorTriangle.RenderTransform = mirrorScaleTransform;
                }
            }
        }

        // Method to remove the main triangle from the canvas
        private void RemoveTriangle(Canvas canvas)
        {
            canvas.Children.Remove(triangle);
        }

        // Method to remove the mirrored triangle from the canvas
        private void RemoveMirrorTriangle(Canvas canvas)
        {
            canvas.Children.Remove(mirrorTriangle);
        }

        // Method to draw the main triangle on the canvas
        private void DrawTriangle(Canvas canvas, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            triangle = new Polygon
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.LightBlue 
            };

            double centerX = canvas.Width / 2;
            double centerY = canvas.Height / 2;
            center = new Point(centerX, centerY);

            PointCollection points = new PointCollection
            {
                new Point(x1 + centerX, -y1 + centerY),
                new Point(x2 + centerX, -y2 + centerY),
                new Point(x3 + centerX, -y3 + centerY)
            };
            originalPoints = points;


            triangle.Points = points;
            canvas.Children.Add(triangle);
        }

        // Method to draw the mirror triangle from main triangle on the canvas
        private void DrawMirrorTriangle(Canvas canvas, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            mirrorTriangle = new Polygon
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.LightBlue
            };


            double centerX = canvas.Width / 2;
            double centerY = canvas.Height / 2;

            PointCollection mirroredPoints = new PointCollection
            {
                new Point(originalPoints[0].Y, originalPoints[0].X),
                new Point(originalPoints[1].Y, originalPoints[1].X),
                new Point(originalPoints[2].Y, originalPoints[2].X)
            };

            mirrorTriangle.Points = mirroredPoints;
            canvas.Children.Add(mirrorTriangle);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            // Remove existing triangles
            RemoveTriangle(myCanvas);

            // Parse the user input to obtain triangle coordinates
            double x1 = Convert.ToDouble(tbxX1.Text);
            double x2 = Convert.ToDouble(tbxX2.Text);
            double x3 = Convert.ToDouble(tbxX3.Text);
            double y1 = Convert.ToDouble(tbxY1.Text);
            double y2 = Convert.ToDouble(tbxY2.Text);
            double y3 = Convert.ToDouble(tbxY3.Text);

            // Draw a new main triangle on the canvas
            DrawTriangle(myCanvas, x1, y1, x2, y2, x3, y3);
        }

        private void TextBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open the MainWindow and close the current window
            MainWindow moveImagesWindow = new MainWindow();
            this.Close();
            moveImagesWindow.ShowDialog();
        }

        private void TextBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open the Ed_materials1 window and close the current window
            Ed_materials1 moveImagesWindow = new Ed_materials1();
            this.Close();
            moveImagesWindow.ShowDialog();
        }

        private void TextBlock4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open the Ed_materials2 window and close the current window
            Ed_materials2 moveImagesWindow = new Ed_materials2();
            this.Close();
            moveImagesWindow.ShowDialog();
        }

        private void TextBlock5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open the WindowMain window and close the current window
            WindowMain moveImagesWindow = new WindowMain();
            this.Close();
            moveImagesWindow.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Action for the "Back" button, currently empty
        }

        private void SaveImageWithDialog(WriteableBitmap bitmap)
        {
            // Show a save file dialog for saving the image as JPEG
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JPEG Image|*.jpg",
                Title = "Save Image"
            };

            // If the user selects a file, save the image
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                SaveImageAsJpeg(bitmap, fileName);
            }
        }

        private void SaveImageAsJpeg(WriteableBitmap bitmap, string fileName)
        {
            // Save the WriteableBitmap as a JPEG file
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.QualityLevel = 100; // Adjust the quality level as needed
                encoder.Save(stream);
            }
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            // Render the canvas as a WriteableBitmap and save it as JPEG
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(myCanvas);

            // Convert RenderTargetBitmap to WriteableBitmap
            WriteableBitmap bitmap = new WriteableBitmap(renderBitmap);

            // Save the WriteableBitmap as a JPEG using a dialog
            SaveImageWithDialog(bitmap);
        }

        private void btnReflection_Click(object sender, RoutedEventArgs e)
        {
            // Remove existing mirrored triangle
            RemoveMirrorTriangle(myCanvas);

            // Parse the user input to obtain triangle coordinates
            double x1 = Convert.ToDouble(tbxX1.Text);
            double x2 = Convert.ToDouble(tbxX2.Text);
            double x3 = Convert.ToDouble(tbxX3.Text);
            double y1 = Convert.ToDouble(tbxY1.Text);
            double y2 = Convert.ToDouble(tbxY2.Text);
            double y3 = Convert.ToDouble(tbxY3.Text);

            // Draw a mirrored triangle on the canvas
            DrawMirrorTriangle(myCanvas, x1, y1, x2, y2, x3, y3);

            // Set the flag to indicate that mirroring is applied
            isMirror = true;
        }
    }
}
