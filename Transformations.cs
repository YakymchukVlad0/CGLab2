using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CGLab2
{


    public static class Transformations
    {
        // Translates the UIElement by the specified amounts along the X and Y axes
        public static void Translate(UIElement element, double translateX, double translateY)
        {
            // Create a translation matrix using the specified offsets
            Matrix matrix = new Matrix(1, 0, 0, 1, translateX, translateY);

            // Apply the translation matrix to the UIElement
            ApplyMatrixTransform(element, matrix);
        }

        // Rotates the UIElement by the specified angle in degrees
        public static void Rotate(UIElement element, double angleInDegrees)
        {
            // Convert the angle from degrees to radians
            double angleInRadians = angleInDegrees * (Math.PI / 180);

            // Create a rotation matrix using the converted angle
            Matrix matrix = new Matrix(Math.Cos(angleInRadians), -Math.Sin(angleInRadians), Math.Sin(angleInRadians), Math.Cos(angleInRadians), 0, 0);

            // Apply the rotation matrix to the UIElement
            ApplyMatrixTransform(element, matrix);
        }

        // Applies a MatrixTransform to the UIElement
        private static void ApplyMatrixTransform(UIElement element, Matrix matrix)
        {
            // Check if the UIElement already has a MatrixTransform
            if (element.RenderTransform is MatrixTransform matrixTransform)
            {
                // Combine the existing matrix with the new matrix
                matrix *= matrixTransform.Matrix;
            }

            // Set the UIElement's RenderTransform to a new MatrixTransform with the combined matrix
            element.RenderTransform = new MatrixTransform(matrix);
        }
    }
}
