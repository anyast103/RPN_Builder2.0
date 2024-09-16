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
        public static Label CoordinatesBox;
        public static Popup Popup;
        private Canvas MyCanvas { get; set; }

        public IDrawer(Canvas canvas)
        {
            MyCanvas = canvas;
            CoordinatesBox = new Label() { FontSize = 20, Foreground = Brushes.White, Background = Brushes.Black };
            Popup = new Popup() { Child = CoordinatesBox };
            Popup.PlacementTarget = MyCanvas;
            Popup.Placement = PlacementMode.MousePoint;
        }
        void polyLeave(object sender, MouseEventArgs e)
        {
            Popup.IsOpen = false;
        }
        void polyMove(object sender, MouseEventArgs e)
        {
            Point pos = Mouse.GetPosition(MyCanvas);
            CoordinatesBox.Content = $"x:{(pos.X - (MyCanvas.Width / 2)) / 40:0} y:{-(pos.Y - (MyCanvas.Height / 2)) / 40:0}";
            Popup.IsOpen = true;
        }
        public void DrawAll()
        {
            SetField();
            DrawCoordinatePlate();
            DrawFunction();
        }

        private void DrawCoordinatePlate()
        {
            MyCanvas.Children.Clear();
            DrawOX();
            DrawOY();
            XMarks();
            YMarks();
            DrawNumsX();
            DrawNumsY();
        }
        private void DrawOY()
        {
            Line oyLine = new Line
            {
                X1 = MyCanvas.Width / 2,
                Y1 = 0,
                X2 = MyCanvas.Width / 2,
                Y2 = MyCanvas.Height,
                Stroke = Brushes.Black
            };
            MyCanvas.Children.Add(oyLine);
        }
        private void DrawOX()
        {
            Line oxLine = new Line
            {
                X1 = 0,
                Y1 = MyCanvas.Height / 2,
                X2 = MyCanvas.Width,
                Y2 = MyCanvas.Height / 2,
                Stroke = Brushes.Black
            };
            MyCanvas.Children.Add(oxLine);
        }

        private void XMarks()
        {
            var height = MyCanvas.Height;
            var width = MyCanvas.Width;

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
                MyCanvas.Children.Add(markX);
            }
        }
        private void YMarks()
        {
            var height = MyCanvas.Height;
            var width = MyCanvas.Width;

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
                MyCanvas.Children.Add(markY);
            }
        }
        private void DrawNumsY()
        {
            var height = MyCanvas.Height;
            var width = MyCanvas.Width;
            for (double i = Math.Round(height / 80, 1); i >= height / -80; i--)
            {
                Label numY = new Label
                {
                    Content = $"{Math.Round(i, 0)}"
                };
                Canvas.SetLeft(numY, width / 2 + 6);
                Canvas.SetTop(numY, (height / 80 - i) * 40 - 15);
                MyCanvas.Children.Add(numY);
            }
        }
        private void DrawNumsX()
        {
            var height = MyCanvas.Height;
            var width = MyCanvas.Width;
            for (double i = Math.Round(width / -80, 1); i <= width / 80; i++)
            {
                Label numX = new Label
                {
                    Content = $"{Math.Round(i, 0)}"
                };
                Canvas.SetLeft(numX, (i + width / 80) * 40 - 8);
                Canvas.SetTop(numX, height / 2 + 6);
                MyCanvas.Children.Add(numX);
            }
        }

        private void DrawFunction()
        {
            var height = MyCanvas.Height;
            var width = MyCanvas.Width;
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

            MyCanvas.Children.Add(func);
        }
        private void SetField()
        {
            if (Math.Abs(Function.End) > Math.Abs(Function.Start))
                MyCanvas.Width = Math.Abs(Math.Round(Function.Start) * 80);
            else
                MyCanvas.Width = Math.Abs(Math.Round(Function.End, 0)) * 80;
            double maxY = 0;
            for (int i = 0; i < Calculating.ResultList.Count; i++)
            {
                if (Math.Abs(Calculating.ResultList[i]) > maxY)
                    maxY = Math.Abs(Calculating.ResultList[i]);
            }
            MyCanvas.Height = Math.Round(maxY, 0) * 80;
        }
    }
}
