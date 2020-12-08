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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day24_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private SolidColorBrush _fillColorBrush;
        private void Fill(SolidColorBrush brush)
        {
            _fillColorBrush = brush;
        }

        private void Fill(byte r, byte g, byte b, byte alpha = 255)
        {
            _fillColorBrush = new SolidColorBrush(Color.FromArgb(alpha, r, g, b));
        }


        private void Line(double x1, double y1, double x2, double y2, SolidColorBrush fill, SolidColorBrush stroke)
        {
            Line line = new Line();
            line.Stroke = stroke;
            line.Fill = fill;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.StrokeThickness = 1;
            Canvas.Children.Add(line);
        }

        private void GetEllipse(double x, double y, Color color)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(color);
            ellipse.StrokeThickness = 0;
            ellipse.Stroke = Brushes.Transparent;

            // Set the width and height of the Ellipse.
            ellipse.Width = 5;
            ellipse.Height = 5;
            ellipse.Margin = new Thickness(x - 2, y - 2, 0, 0);
            Canvas.Children.Add(ellipse);
        }
    }
}
