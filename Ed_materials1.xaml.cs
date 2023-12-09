using System;
using System.Collections.Generic;
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
    /// Interaction logic for Ed_materials1.xaml
    /// </summary>
    public partial class Ed_materials1 : Window
    {
        public Ed_materials1()
        {
            InitializeComponent();
            
        }


        //method to go to another window by clicking textblock
        private void TextBlock1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            ColorSchemesWindow colorSchemesWindow = new ColorSchemesWindow();
            this.Close();
            colorSchemesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }


        //method to go to another window by clicking textblock
        private void TextBlock2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Moving_images moveImagesWindow = new Moving_images();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


           
        }


        //method to go to another window by clicking textblock
        private void TextBlock3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            MainWindow moveImagesWindow = new MainWindow();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }


        //method to go to another window by clicking textblock
        private void TextBlock4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            Ed_materials2 moveImagesWindow = new Ed_materials2();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }


        //method to go to another window by clicking textblock
        private void TextBlock5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Create and show the new ColorSchemesWindow
            WindowMain moveImagesWindow = new WindowMain();
            this.Close();
            moveImagesWindow.ShowDialog(); // Use Show() for a non-blocking window


            
        }
    }
}
