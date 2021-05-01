using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using RPN_Recreate.WPF;

namespace RPN_Recreate.WPF
{
    class IDrawer
    {
        public static Label label;
        public static Popup popup;

        public IDrawer()
        {
            label = new Label() { FontSize = 20, Foreground = Brushes.White, Background = Brushes.Black };
            popup = new Popup() { Child = label };
            popup.PlacementTarget = ((MainWindow)System.Windows.Application.Current.MainWindow).Field;
            popup.Placement = PlacementMode.MousePoint;
        }
        void polyLeave(object sender, MouseEventArgs e)
        {
            popup.IsOpen = false;
        }
        void polyMove(object sender, MouseEventArgs e)
        {
            Point pos = Mouse.GetPosition(((MainWindow)System.Windows.Application.Current.MainWindow).Field);
            label.Content = $"x:{(pos.X - (((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width / 2)) / 40:0} y:{-(pos.Y - (((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height / 2)) / 40:0}";
            popup.IsOpen = true;
        }
        public void DrawAll()
        {
            SetField();
            DrawCoordinatePlate();
            DrawFunction();
        }

        private void DrawCoordinatePlate()
        {
            DrawOX();
            DrawOY();
            XMarks();
            YMarks();
            DrawNumsX();
            DrawNumsY();
        }
        private void DrawOY()
        {
            Line oyLine = new Line();
            oyLine.X1 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width / 2;
            oyLine.Y1 = 0;
            oyLine.X2 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width / 2;
            oyLine.Y2 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            oyLine.Stroke = Brushes.Black;
            ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(oyLine);
        }
        private void DrawOX()
        {
            Line oxLine = new Line();
            oxLine.X1 = 0;
            oxLine.Y1 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height / 2;
            oxLine.X2 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;
            oxLine.Y2 = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height / 2;
            oxLine.Stroke = Brushes.Black;
            ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(oxLine);
        }

        private void XMarks()
        {
            var height = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            var width = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;

            for (int i = 0; i <= width / 40; i++)
            {
                var markX = new Ellipse()
                {
                    Width = 5,
                    Height = 5,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(markX, i * 40 - 3);
                Canvas.SetTop(markX, height / 2 - 3);

                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(markX);
            }
        }
        private void YMarks()
        {
            var height = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            var width = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;

            for (int i = 0; i <= height / 50; i++)
            {
                var markY = new Ellipse()
                {
                    Width = 5,
                    Height = 5,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(markY, width / 2 - 3);
                Canvas.SetTop(markY, i * 40 - 3);
                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(markY);
            }
        }
        private void DrawNumsY()
        {
            var height = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            var width = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;
            for (double i = Math.Round(height / 80, 1); i >= height / -80; i--)
            {
                Label numY = new Label();
                numY.Content = $"{Math.Round(i, 0)}";
                Canvas.SetLeft(numY, width / 2 + 6);
                Canvas.SetTop(numY, (height / 80 - i) * 40 - 15);
                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(numY);
            }
        }
        private void DrawNumsX()
        {
            var height = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            var width = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;
            for (double i = Math.Round(width / -80, 1); i <= width / 80; i++)
            {
                Label numX = new Label();
                numX.Content = $"{Math.Round(i, 0)}";
                Canvas.SetLeft(numX, (i + width / 80) * 40 - 8);
                Canvas.SetTop(numX, height / 2 + 6);
                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(numX);
            }
        }

        private void DrawFunction()
        {
            var height = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height;
            var width = ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width;
            var func = new Polyline();

            func.MouseLeave += polyLeave;
            func.MouseMove += polyMove;
            func.StrokeThickness = 2;

            func.Points = new PointCollection();


            for (var i = 0; i < Calculating.ResultList.Count; i++)
            {

                if (double.IsNaN(Calculating.ResultList[i]))
                    continue;
                func.Points.Add(new Point(width / 2 + 40 * Calculating.XList[i], Math.Round(height / 2 - 40 * Calculating.ResultList[i], 12)));

            }
            func.Stroke = Brushes.Black;

            ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Children.Add(func);
        }
        private void SetField()
        {
            if (Math.Abs(Function.End) > Math.Abs(Function.Start))
                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width = Math.Abs(Math.Round(Function.Start) * 80);
            else
                ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Width = Math.Abs(Math.Round(Function.End, 0)) * 80;
            double maxY = 0;
            for (int i = 0; i < Calculating.ResultList.Count; i++)
            {
                if (Math.Abs(Calculating.ResultList[i]) > maxY)
                    maxY = Math.Abs(Calculating.ResultList[i]);
            }
            ((MainWindow)System.Windows.Application.Current.MainWindow).Field.Height = Math.Round(maxY, 0) * 80;
        }
    }
}
